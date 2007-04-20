
using System;
using System.Collections.Generic;

using NUnit.Framework;

using Atlanta.Application.Domain.Lender;

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

            Library library = Library.InstantiateLibrary();
            Session.Save(library);

            library.OwnedMedia.Add(Media.InstantiateMedia(library, MediaType.Book,  "Book 1",   "A test book"));
            library.OwnedMedia.Add(Media.InstantiateMedia(library, MediaType.Cd,    "CD",       "A test cd"));
            library.OwnedMedia.Add(Media.InstantiateMedia(library, MediaType.Book,  "Book 2",   "A test book"));

            Session.Flush();
            Session.Clear();

            _user = User.InstantiateUser("testServiceUser");
        }

        [Test]
        public void TestGetMediaList_Ok()
        {
            IList<Media> mediaList =
                AtlantaServices.MediaService
                    .GetMediaList(_user, new MediaCriteria()
                                            .SetTypeFilter(MediaType.Book));

            Assert.AreEqual(2, mediaList.Count);

            Media media1 = mediaList[0];

            // check that we get a new session for each service call
            //  (i.e., the returned objects are disconnected)
            IList<Media> mediaList2 =
                AtlantaServices.MediaService
                    .GetMediaList(_user, new MediaCriteria()
                                            .SetNameFilter(media1.Name));

            Assert.AreEqual(1, mediaList2.Count);
            Assert.AreNotEqual(media1, mediaList2[0], "objects from different session matched");
        }

        [Test]
        public void TestCreate_Ok()
        {
            Media media =
                AtlantaServices.MediaService
                    .Create(_user, Media.InstantiateOrphanedMedia(MediaType.Cd, "test name", "test description"));
        }

        [Test]
        public void TestModify_Ok()
        {
            Media media =
                AtlantaServices.MediaService
                    .Modify(_user, Media.InstantiateOrphanedMedia(MediaType.Cd, "test name", "test description"));
        }

        [Test]
        public void TestDelete_Ok()
        {
            AtlantaServices.MediaService
                .Delete(_user, Media.InstantiateOrphanedMedia(MediaType.Cd, "test name", "test description"));
        }

    }
}

