
using System;
using System.Collections.Generic;

using NHibernate.Criterion;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestMedia : DomainPersistenceTestBase
    {

        private long _libraryId;

        override public void SetUp()
        {
            base.SetUp();

            Library library = Library.InstantiateLibrary();
            Repository.Insert(library);

            library.Create(Media.InstantiateOrphanedMedia(MediaType.Book,  "Book", "A test book"));
            library.Create(Media.InstantiateOrphanedMedia(MediaType.Cd, "CD", "A test cd"));
            library.Create(Media.InstantiateOrphanedMedia(MediaType.Dvd, "DVD", "A test dvd"));

            Repository.Flush();
            Repository.Clear();

            _libraryId = library.Id;
        }

        [Test]
        public void InstantiateOrphanedMedia_Ok()
        {
            Media media = Media.InstantiateOrphanedMedia(MediaType.Dvd, "test", "test description");

            Assert.AreEqual(null, media.OwningLibrary);
            Assert.AreEqual(MediaType.Dvd, media.Type);
            Assert.AreEqual("test", media.Name);
            Assert.AreEqual("test description", media.Description);
        }

        [Test]
        public void ModifyDetails_Ok()
        {
            Media media = Media.InstantiateOrphanedMedia(MediaType.Dvd, "test", "test description");

            media.ModifyDetails(MediaType.Cd, "new name", "new description");

            Assert.AreEqual(null, media.OwningLibrary);
            Assert.AreEqual(MediaType.Cd, media.Type);
            Assert.AreEqual("new name", media.Name);
            Assert.AreEqual("new description", media.Description);
        }

        [Test]
        public void XmlSerialization_Ok()
        {
            Media toSerialize = Media.InstantiateOrphanedMedia(MediaType.Dvd, "test", "test description");
        }

        [Test]
        public void Insert_Load_Ok()
        {
            Library library = Repository.Load<Library>(_libraryId);

            Media media = library.Create(Media.InstantiateOrphanedMedia(MediaType.Dvd, "test", "test description"));

            Repository.Insert(media);
            Assert.IsTrue(media.Id != 0);

            Repository.Flush();
            Repository.Clear();

            media = Repository.Load<Media>(media.Id);

            Assert.AreEqual(MediaType.Dvd, media.Type);
            Assert.AreEqual("test", media.Name);
            Assert.AreEqual("test description", media.Description);
        }

        [Test]
        public void ExampleByCriteria_LoadAllBooks()
        {
            Library library = Repository.Load<Library>(_libraryId);

            IList<Media> bookMediaInLibrary =
                Repository.CreateQuery<Media>()
                    .Add(Expression.Eq("OwningLibrary", library))
                    .Add(Expression.Eq("Type", MediaType.Book))
                    .List<Media>();

            Assert.AreEqual(1, bookMediaInLibrary.Count);
            Assert.AreEqual("Book", bookMediaInLibrary[0].Name);
        }

    }

}

