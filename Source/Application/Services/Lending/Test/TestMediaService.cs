
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

            _user = User.InstantiateUser("testServiceUser");
        }

        [Test]
        public void TestGetMediaList_Ok()
        {
            IList<Media> mediaList =
                AtlantaServices.MediaService
                    .GetMediaList(_user, new MediaCriteria());
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

