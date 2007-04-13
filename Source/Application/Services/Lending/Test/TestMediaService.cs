
using System;

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
            MediaService mediaService = new MediaService();
            mediaService.GetMediaList(_user, new MediaCriteria());
        }

        [Test]
        public void TestCreate_Ok()
        {
            MediaService mediaService = new MediaService();
            mediaService.Create(_user, Media.InstantiateOrphanedMedia(MediaType.Cd, "test name", "test description"));
        }

        [Test]
        public void TestModify_Ok()
        {
            MediaService mediaService = new MediaService();
            mediaService.Modify(_user, Media.InstantiateOrphanedMedia(MediaType.Cd, "test name", "test description"));
        }

        [Test]
        public void TestDelete_Ok()
        {
            MediaService mediaService = new MediaService();
            mediaService.Delete(_user, Media.InstantiateOrphanedMedia(MediaType.Cd, "test name", "test description"));
        }

    }
}

