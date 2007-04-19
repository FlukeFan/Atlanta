
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

        private long _libraryId;
        private long _mediaId;

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
            DomainRegistry.Library = null;

            _libraryId = library.Id;
            _mediaId = library.OwnedMedia[0].Id;
        }

        [Test]
        public void Insert_Load_Ok()
        {
            Library library = Library.InstantiateLibrary();

            Session.Save(library);
            Assert.IsTrue(library.Id != 0);

            Session.Flush();
            Session.Clear();

            library = (Library) Session.Load(typeof(Library), library.Id);

            Assert.AreEqual(0, library.OwnedMedia.Count);
        }

        [Test]
        public void LibraryRegistry_Ok()
        {
            Library library = (Library) Session.Load(typeof(Library), _libraryId);

            Assert.AreEqual(library.Id, DomainRegistry.Library.Id);
            Assert.AreEqual(library, DomainRegistry.Library);
        }

        [Test]
        public void LibraryRegistry_FailMissingLibrary()
        {
            Library library = (Library) Session.Load(typeof(Library), _libraryId);
            Session.Delete(library);

            try
            {
                library = DomainRegistry.Library;
                Assert.Fail("no exception thrown");
            }
            catch (Exception exception)
            {
                Assert.AreEqual("no library found in database", exception.Message);
            }
        }

        [Test]
        public void LibraryRegistry_FailTooManyLibrary()
        {
            Library library = Library.InstantiateLibrary();
            Session.Save(library);

            Session.Flush();
            Session.Clear();
            DomainRegistry.Library = null;

            try
            {
                library = DomainRegistry.Library;
                Assert.Fail("no exception thrown");
            }
            catch (Exception exception)
            {
                Assert.AreEqual("more than 1 library found in database", exception.Message);
            }
        }

        [Test]
        public void LoadCheckAggregates_Ok()
        {
            Library library = (Library) Session.Load(typeof(Library), _libraryId);

            Assert.AreEqual(3, library.OwnedMedia.Count);
        }

        [Test]
        public void InsertChildMedia_Ok()
        {
            Library library = (Library) Session.Load(typeof(Library), _libraryId);

            Media media = Media.InstantiateMedia(library, MediaType.Dvd, "test", "test description");

            library.OwnedMedia.Add(media);

            Session.Flush();
            Session.Clear();

            library = (Library) Session.Load(typeof(Library), _libraryId);
            Assert.AreEqual(4, library.OwnedMedia.Count);
        }

        [Test]
        public void DeleteChildMedia_Ok()
        {
            Library library = (Library) Session.Load(typeof(Library), _libraryId);
            Media media = (Media) Session.Load(typeof(Media), _mediaId);

            library.OwnedMedia.Remove(media);

            Session.Flush();
            Session.Clear();

            library = (Library) Session.Load(typeof(Library), _libraryId);
            Assert.AreEqual(2, library.OwnedMedia.Count);
        }

    }

}

