
using System;
using System.Collections.Generic;

using NUnit.Framework;

using NHibernate.Criterion;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.Lender;

using Atlanta.Application.Services.Interfaces;
using Atlanta.Application.Services.ServiceBase.Test;

namespace Atlanta.Application.Services.Lending.Test
{

    [TestFixture]
    public class TestMediaService : ServiceTestBase
    {

        private User _user;

        override public void SetUp()
        {
            base.SetUp();

            AtlantaServices.ClearServices();
            AtlantaServices.AddAdvisedService<IMediaService>(new MediaService(), new AopAroundTestAdvice());

            Library library = Library.InstantiateLibrary();
            Session.Save(library);

            library.Create(Media.InstantiateOrphanedMedia(MediaType.Book,  "Book 1",   "A test book"));
            library.Create(Media.InstantiateOrphanedMedia(MediaType.Cd, "CD", "A test cd"));
            library.Create(Media.InstantiateOrphanedMedia(MediaType.Book, "Book 2", "A test book"));

            Session.Flush();
            Session.Clear();

            _user = User.InstantiateUser("testServiceUser");
        }
        
        [Test]
        public void TestGetMediaList_Ok()
        {
            IList<Media> mediaList =
                AtlantaServices.MediaService
                    .GetMediaList(_user, new DomainCriteria(typeof(Media))
                                            .Add(Expression.Eq("Type", MediaType.Book)));

            Assert.AreEqual(2, mediaList.Count);

            Media media1 = mediaList[0];

            // check that we get a new session for each service call
            //  (i.e., the returned objects are disconnected)
            IList<Media> mediaList2 =
                AtlantaServices.MediaService
                    .GetMediaList(_user, new DomainCriteria(typeof(Media))
                                            .Add(Expression.Eq("Name", media1.Name)));

            Assert.AreEqual(1, mediaList2.Count);
            Assert.AreNotEqual(media1, mediaList2[0], "objects from different session matched");
        }

        [Test]
        public void TestCreate_Ok()
        {
            try
            {
                Media media =
                    AtlantaServices.MediaService
                        .Create(_user, Media.InstantiateOrphanedMedia(MediaType.Cd, "CD", "test description"));

                Assert.Fail("exception not thrown");
            }
            catch (DuplicationException duplicationException)
            {
                Assert.AreEqual("A test cd", (duplicationException.Duplicate as Media).Description);
            }
        }

        [Test]
        public void TestCreate_FailDuplicateName()
        {
            Media media =
                AtlantaServices.MediaService
                    .Create(_user, Media.InstantiateOrphanedMedia(MediaType.Cd, "test name", "test description"));

            Assert.AreEqual(4, Session.CreateCriteria(typeof(Media)).List<Media>().Count);
        }

        [Test]
        public void TestModify_Ok()
        {
            Media mediaCopy =
                AtlantaServices.MediaService
                    .GetMediaList(_user, new DomainCriteria(typeof(Media))
                                            .Add(Expression.Eq("Name", "CD")))[0];

            mediaCopy.ModifyDetails(mediaCopy.Type, "modified CD name", "new description");

            mediaCopy = AtlantaServices.MediaService.Modify(_user, mediaCopy);

            Media modifiedMedia = Session.Load<Media>(mediaCopy.Id);
            Assert.AreEqual("modified CD name", modifiedMedia.Name);
            Assert.AreEqual("new description", modifiedMedia.Description);
        }

        [Test]
        public void TestDelete_Ok()
        {
            Media mediaCopy =
                AtlantaServices.MediaService
                    .GetMediaList(_user, new DomainCriteria(typeof(Media))
                                            .Add(Expression.Eq("Name", "CD")))[0];

            AtlantaServices.MediaService
                .Delete(_user, mediaCopy);

            Assert.AreEqual(2, Session.CreateCriteria(typeof(Media)).List<Media>().Count);
        }

    }
}

