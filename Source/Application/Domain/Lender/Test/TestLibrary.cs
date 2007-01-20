
using System;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase;
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
        public void CreateMedia_Ok()
        {
            Library library = Library.InstantiateLibrary();

            Media media = Media.InstantiateOrphanedMedia(MediaType.Dvd, "test dvd", "test description");

            Media newMedia = library.Create(media);

            Assert.AreEqual(1, library.OwnedMedia.Count);
            Assert.AreEqual(newMedia, library.OwnedMedia[0]);
            Assert.AreEqual(library, newMedia.OwningLibrary);
            Assert.AreEqual("test dvd", library.OwnedMedia[0].Name);
        }

        [Test]
        public void CreateMedia_FailDuplicate()
        {
            Library library = Library.InstantiateLibrary();
            library.OwnedMedia.Add(Media.InstantiateMedia(library, MediaType.Dvd, "test dvd", "test description"));

            Media media = Media.InstantiateOrphanedMedia(MediaType.Dvd, "test dvd", "test description 2");

            try
            {
                Media newMedia = library.Create(media);
                Assert.Fail("execption not thrown");
            }
            catch(DuplicationException e)
            {
                Assert.AreEqual("test description", ((Media) e.Duplicate).Description);
            }
        }

        [Test]
        public void ModifyMedia_Ok()
        {
            Library library = Library.InstantiateLibrary();
            Media existingMedia = Media.InstantiateMedia(library, MediaType.Dvd, "test dvd", "test description");
            library.OwnedMedia.Add(existingMedia);

            Media mediaClientCopy = (Media) MakeCopy(existingMedia);
            mediaClientCopy.ModifyDetails(MediaType.Cd, "test name changed", "test description changed");

            Media modifiedMedia = library.Modify(existingMedia, mediaClientCopy);

            Assert.AreEqual(1, library.OwnedMedia.Count);
            Assert.AreEqual(existingMedia, modifiedMedia);
            Assert.AreEqual(MediaType.Cd, modifiedMedia.Type);
            Assert.AreEqual("test name changed", modifiedMedia.Name);
            Assert.AreEqual("test description changed", modifiedMedia.Description);
        }

    }
}

