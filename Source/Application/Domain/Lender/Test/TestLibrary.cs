
using System;

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

            Assert.AreEqual(0, library.OwnedMedia.Count);
        }

        [Test]
        public void AddMedia_Ok()
        {
            Library library = Library.InstantiateLibrary();

            Media media = Media.InstantiateOrphanedMedia(MediaType.Dvd, "test dvd", "test description");

            Media newMedia = library.Add(media);

            Assert.AreEqual(1, library.OwnedMedia.Count);
            Assert.AreEqual(newMedia, library.OwnedMedia[0]);
            Assert.AreEqual(library, newMedia.OwningLibrary);
            Assert.AreEqual("test dvd", library.OwnedMedia[0].Name);
        }

    }
}

