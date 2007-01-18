
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

            mediaList.Add(Media.InstantiateOrphanedMedia(MediaType.Cd, "test name 1",   "test description"));
            mediaList.Add(Media.InstantiateOrphanedMedia(MediaType.Cd, "test name 2.1", "test description"));
            mediaList.Add(Media.InstantiateOrphanedMedia(MediaType.Cd, "test name 2.2", "test description"));

            IList<Media> filteredList;
            {
                filteredList = new MediaCriteria()
                                    .SetNameFilter("test name 2.1")
                                    .List(mediaList);

                Assert.AreEqual(1, filteredList.Count);
                Assert.AreEqual("test name 2.1", filteredList[0].Name);
            }

            {
                filteredList = new MediaCriteria()
                                    .SetNameFilter("test name 2.1", FilterCondition.NotEqual)
                                    .List(mediaList);

                Assert.AreEqual(2, filteredList.Count);
                Assert.AreEqual("test name 1", filteredList[0].Name);
                Assert.AreEqual("test name 2.2", filteredList[1].Name);
            }

            {
                filteredList = new MediaCriteria()
                                    .SetNameFilter("test name 2%", FilterCondition.Like)
                                    .List(mediaList);

                Assert.AreEqual(2, filteredList.Count);
                Assert.AreEqual("test name 2.1", filteredList[0].Name);
                Assert.AreEqual("test name 2.2", filteredList[1].Name);
            }
        }

        [Test]
        public void FilterEnumProperty_Ok()
        {
            IList<Media> mediaList = new DomainList<Media>();

            mediaList.Add(Media.InstantiateOrphanedMedia(MediaType.Cd,      "test name", "test description"));
            mediaList.Add(Media.InstantiateOrphanedMedia(MediaType.Book,    "test name", "test description"));
            mediaList.Add(Media.InstantiateOrphanedMedia(MediaType.Dvd,     "test name", "test description"));

            IList<Media> filteredList;
            {
                filteredList = new MediaCriteria()
                                    .SetTypeFilter(MediaType.Book)
                                    .List(mediaList);

                Assert.AreEqual(1, filteredList.Count);
                Assert.AreEqual(MediaType.Book, filteredList[0].Type);
            }

            {
                filteredList = new MediaCriteria()
                                    .SetTypeFilter(MediaType.Book, FilterCondition.NotEqual)
                                    .List(mediaList);

                Assert.AreEqual(2, filteredList.Count);
                Assert.AreEqual(MediaType.Cd, filteredList[0].Type);
                Assert.AreEqual(MediaType.Dvd, filteredList[1].Type);
            }
        }

    }
}

