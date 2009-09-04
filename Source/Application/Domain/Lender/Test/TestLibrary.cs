
using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using NHibernate.Criterion;
using NHibernate.LambdaExtensions;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestLibrary : DomainPersistenceTestBase
    {

        private long _libraryId;
        private long _mediaId;

        override public void SetUp()
        {
            base.SetUp();

            Library library = Library.InstantiateLibrary();

            Media media1 = library.Create(Media.InstantiateOrphanedMedia(MediaType.Book,  "Book", "A test book"));
            Media media2 = library.Create(Media.InstantiateOrphanedMedia(MediaType.Cd, "CD", "A test cd"));
            Media media3 = library.Create(Media.InstantiateOrphanedMedia(MediaType.Dvd, "DVD", "A test dvd"));

            Repository.Flush();
            Repository.Clear();
            DomainRegistry.Library = null;

            _libraryId = library.Id;
            _mediaId = library.OwnedMedia.First().Id;
        }

        [Test]
        public void InstantiateLibrary_Ok()
        {
            Library library = Library.InstantiateLibrary();

            Assert.AreEqual(0, library.OwnedMedia.Count());
        }

        [Test]
        public void GetMedaList_Ok()
        {
            Library library = Repository.Load<Library>(_libraryId);
            IList<Media> mediaList = library.GetMediaList(DetachedCriteria.For<Media>().Add<Media>(m => m.Type == MediaType.Dvd));

            Assert.AreEqual(1, mediaList.Count);
        }

        [Test]
        public void GetGraphCopy()
        {
            Library library = Repository.Load<Library>(_libraryId);
            Assert.AreNotEqual(typeof(Library), library.GetType(), "not a proxy");

            Library libraryCopy =
                library
                    .Graph()
                    .Add(l => l.OwnedMedia)
                    .Copy();

            Assert.AreNotSame(library, libraryCopy);
            Assert.AreEqual(typeof(Library), libraryCopy.GetType());
            Assert.AreEqual(_libraryId, libraryCopy.Id);
            Assert.AreEqual(3, libraryCopy.OwnedMedia.Count());

            Media mediaCopy = libraryCopy.OwnedMedia.First();
            Assert.AreEqual(typeof(Media), mediaCopy.GetType());
            Assert.AreEqual(_mediaId, mediaCopy.Id);
        }

        [Test]
        public void GraphAndEnsureIdIsPopulated()
        {
            Library library = Repository.Load<Library>(_libraryId);
            Media newMedia = library.Create(Media.InstantiateOrphanedMedia(MediaType.Book, "a book", "a book"));

            Media mediaCopy = newMedia.Graph().Copy();

            Assert.AreNotEqual(0m, mediaCopy.Id, "Id not populated");
        }

        [Test]
        public void CreateMedia_Ok()
        {
            Library library = Library.InstantiateLibrary();

            Media media = Media.InstantiateOrphanedMedia(MediaType.Dvd, "test dvd", "test description");
            Media newMedia = library.Create(media);

            Assert.AreEqual(1, library.OwnedMedia.Count());
            Assert.AreEqual(newMedia, library.OwnedMedia.First());
            Assert.AreEqual(library, newMedia.Library);
            Assert.AreEqual("test dvd", library.OwnedMedia.First().Name);
        }

        [Test]
        public void CreateMedia_FailDuplicate()
        {
            Library library = Repository.Load<Library>(_libraryId);
            Media first = library.Create(Media.InstantiateOrphanedMedia(MediaType.Dvd, "test dvd", "test description"));
            Repository.Flush();

            Media media = Media.InstantiateOrphanedMedia(MediaType.Dvd, "test dvd", "test description 2");

            try
            {
                Media newMedia = library.Create(media);
                Assert.Fail("exception not thrown");
            }
            catch (DuplicationException e)
            {
                Assert.AreEqual(first.Id, e.DuplicateId);
            }
        }

        [Test]
        public void ModifyMedia_Ok()
        {
            Library library = Library.InstantiateLibrary();
            Media existingMedia = library.Create(Media.InstantiateOrphanedMedia(MediaType.Dvd, "test dvd", "test description"));

            Media mediaClientCopy = (Media)MakeCopy(existingMedia);
            mediaClientCopy.ModifyDetails(MediaType.Cd, "test name changed", "test description changed");

            Media modifiedMedia = library.Modify(existingMedia, mediaClientCopy);

            Assert.AreEqual(1, library.OwnedMedia.Count());
            Assert.AreEqual(existingMedia, modifiedMedia);
            Assert.AreEqual(MediaType.Cd, modifiedMedia.Type);
            Assert.AreEqual("test name changed", modifiedMedia.Name);
            Assert.AreEqual("test description changed", modifiedMedia.Description);
        }

        [Test]
        public void ModifyMedia_FailDuplicate()
        {
            Library library = Library.InstantiateLibrary();
            library.Create(Media.InstantiateOrphanedMedia(MediaType.Cd, "test", "test object 1"));
            library.Create(Media.InstantiateOrphanedMedia(MediaType.Cd, "test2", "test object 2"));
            library.Create(Media.InstantiateOrphanedMedia(MediaType.Dvd, "test", "test object 3"));

            Media existingMedia = library.OwnedMedia.First();
            Media mediaClientCopy = (Media)MakeCopy(existingMedia);

            Media modifiedMedia = library.Modify(existingMedia, mediaClientCopy);
            Assert.AreEqual(modifiedMedia, existingMedia);

            try
            {
                mediaClientCopy = (Media)MakeCopy(existingMedia);
                mediaClientCopy.ModifyDetails(MediaType.Dvd, mediaClientCopy.Name, mediaClientCopy.Description);
                modifiedMedia = library.Modify(existingMedia, mediaClientCopy);
                Assert.Fail("exception not thrown");
            }
            catch (DuplicationException e)
            {
                Assert.AreEqual(library.OwnedMedia.Skip(2).First().Id, e.DuplicateId);
            }

            try
            {
                mediaClientCopy = (Media)MakeCopy(existingMedia);
                mediaClientCopy.ModifyDetails(mediaClientCopy.Type, "test2", mediaClientCopy.Description);
                modifiedMedia = library.Modify(existingMedia, mediaClientCopy);
                Assert.Fail("exception not thrown");
            }
            catch (DuplicationException e)
            {
                Assert.AreEqual(library.OwnedMedia.Skip(1).First().Id, e.DuplicateId);
            }
        }

        [Test]
        public void DeleteMedia_Ok()
        {
            Library library = Library.InstantiateLibrary();
            Media media = library.Create(Media.InstantiateOrphanedMedia(MediaType.Dvd, "test dvd", "test description"));

            library.Delete(media);

            Assert.AreEqual(0, library.OwnedMedia.Count());
        }

        [Test]
        public void Insert_Load_Ok()
        {
            Library library = Library.InstantiateLibrary();

            Repository.Insert(library);
            Assert.IsTrue(library.Id != 0);

            Repository.Flush();
            Repository.Clear();

            library = Repository.Load<Library>(library.Id);

            Assert.AreEqual(0, library.OwnedMedia.Count());
        }

        [Test]
        public void LibraryRegistry_Ok()
        {
            Library library = Repository.Load<Library>(_libraryId);

            Assert.AreEqual(library.Id, DomainRegistry.Library.Id);
            Assert.AreEqual(library, DomainRegistry.Library);
        }

        [Test]
        public void LibraryRegistry_FailMissingLibrary()
        {
            Library library = Repository.Load<Library>(_libraryId);
            Repository.Delete(library);

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
            Repository.Insert(library);

            Repository.Flush();
            Repository.Clear();
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
            Library library = Repository.Load<Library>(_libraryId);

            Assert.AreEqual(3, library.OwnedMedia.Count());
        }

        [Test]
        public void InsertChildMedia_Ok()
        {
            Library library = Repository.Load<Library>(_libraryId);
            Media media = library.Create(Media.InstantiateOrphanedMedia(MediaType.Dvd, "test", "test description"));

            Repository.Flush();
            Repository.Clear();

            library = Repository.Load<Library>(_libraryId);
            Assert.AreEqual(4, library.OwnedMedia.Count());
        }

        [Test]
        public void DeleteChildMedia_Ok()
        {
            Library library = Repository.Load<Library>(_libraryId);
            Media media = Repository.Load<Media>(_mediaId);

            library.Delete(media);

            Repository.Flush();
            Repository.Clear();

            library = Repository.Load<Library>(_libraryId);
            Assert.AreEqual(2, library.OwnedMedia.Count());
        }

    }

}

