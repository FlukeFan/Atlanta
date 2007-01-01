
using System;

using NHibernate;
using NHibernate.Cfg;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestMedia : DomainTestBase
    {

        [Test]
        public void TestConstruction()
        {
            Media media = new Media();

            media.Name = "test";
            media.Description = "test description";

            Assert.AreEqual("test", media.Name);
            Assert.AreEqual("test description", media.Description);
        }

        [Test]
        public void TestInsertLoad()
        {
            Media media = new Media();

            media.Name = "test";
            media.Description = "test description";

            Session.Save(media);
            Assert.IsTrue(media.Id != 0);

            Session.Flush();
            Session.Clear();

            media = (Media) Session.Load(typeof(Media), media.Id);

            Assert.AreEqual("test", media.Name);
            Assert.AreEqual("test description", media.Description);
        }

    }

}

