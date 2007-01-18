
using System;
using System.Collections.Generic;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestMedia : DomainTestBase
    {
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
        public void FilterStringProperty_Ok()
        {
            IList<Media> mediaList = new DomainList<Media>();

            mediaList.Add(Media.InstantiateOrphanedMedia(MediaType.Cd, "test name 1", "test description"));
            mediaList.Add(Media.InstantiateOrphanedMedia(MediaType.Cd, "test name 2", "test description"));
            mediaList.Add(Media.InstantiateOrphanedMedia(MediaType.Cd, "test name 3", "test description"));

            IList<Media> filteredList = new MediaCriteria()
                                            .SetNameFilter("test name 2")
                                            .List(mediaList);

            Assert.AreEqual(1, filteredList.Count);
            Assert.AreEqual("test name 2", filteredList[0].Name);
        }

    }
}

