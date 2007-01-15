
using System;

using NHibernate;
using NHibernate.Cfg;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestLibrary : DomainTestBase
    {

        [Test]
        public void InstantiateLibrary_Ok()
        {
            Library library = Library.InstantiateLibrary();

            Assert.AreEqual(0, library.Media.Count);
        }

    }
}

