
using System;

using NHibernate.Cfg;

using NUnit.Framework;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestMedia
    {

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

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
            Configuration configuration = new Configuration();
            configuration.AddAssembly("Atlanta.Application.Domain");
        }

    }

}

