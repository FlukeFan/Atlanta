
using System;
using System.Collections.Generic;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestUser : DomainTestBase
    {

        [Test]
        public void InstantiateUser_Ok()
        {
            Media media = Media.InstantiateOrphanedMedia(MediaType.Dvd, "test", "test description");
            User user = User.InstantiateUser("username");

            Assert.AreEqual("username", user.Login);
        }

    }
}

