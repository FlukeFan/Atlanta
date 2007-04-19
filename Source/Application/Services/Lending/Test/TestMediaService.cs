
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

            library.OwnedMedia.Add(Media.InstantiateMedia(library, MediaType.Book,  "Book", "A test book"));
            library.OwnedMedia.Add(Media.InstantiateMedia(library, MediaType.Cd,    "CD", "A test cd"));
            library.OwnedMedia.Add(Media.InstantiateMedia(library, MediaType.Dvd,   "DVD", "A test dvd"));

            Session.Flush();
            Session.Clear();

            _user = User.InstantiateUser("testServiceUser");
        }

        [Test]
        public void TestGetMediaList_Ok()
        {
            IList<Media> mediaList =
                AtlantaServices.MediaService
                    .GetMediaList(_user, new MediaCriteria());

            //Assert.AreEqual(3, mediaList.Count);
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

