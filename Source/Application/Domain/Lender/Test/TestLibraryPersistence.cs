
using System;
using System.Collections.Generic;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestLibraryPersistence : DomainPersistenceTestBase
    {

        [Test]
        public void Insert_Load_Ok()
        {
            Library library = Library.InstantiateLibrary();

            Session.Save(library);
            Assert.IsTrue(library.Id != 0);

            Session.Flush();
            Session.Clear();

            library = (Library) Session.Load(typeof(Library), library.Id);

            Assert.AreEqual(0, library.Media.Count);
        }

        [Test]
        public void LoadCheckAggregates_Ok()
        {
            Library library = (Library) Session.Load(typeof(Library), 1L);

            Assert.AreEqual(3, library.Media.Count);
        }

        [Test]
        public void InsertChildMedia_Ok()
        {
            Library library = (Library) Session.Load(typeof(Library), 1L);

            Media media = Media.InstantiateMedia(library, MediaType.Dvd, "test", "test description");

            library.Media.Add(media);

            Session.Flush();
            Session.Clear();

            library = (Library) Session.Load(typeof(Library), 1L);
            Assert.AreEqual(4, library.Media.Count);
        }

        [Test]
        public void DeleteChildMedia_Ok()
        {
            Library library = (Library) Session.Load(typeof(Library), 1L);
            Media media = (Media) Session.Load(typeof(Media), 1L);

            library.Media.Remove(media);

            Session.Flush();
            Session.Clear();

            library = (Library) Session.Load(typeof(Library), 1L);
            Assert.AreEqual(2, library.Media.Count);
        }

    }

}

