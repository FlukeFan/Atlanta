
using System;
using System.Collections.Generic;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestCriteriaPersistence : DomainPersistenceTestBase
    {

        private long _libraryId;

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

            _libraryId = library.Id;
        }

        [Test]
        public void FilterStringAndEnumProperty_Ok()
        {
            Library library = Session.Load<Library>(_libraryId);

            IList<Media> filteredList;
            {
                filteredList = new MediaCriteria()
                                    .SetNameFilter("CD")
                                    .List(library.OwnedMedia);

                Assert.AreEqual(1, filteredList.Count);
                Assert.AreEqual("CD", filteredList[0].Name);
            }

            {
                filteredList = new MediaCriteria()
                                    .SetTypeFilter(MediaType.Dvd)
                                    .List(library.OwnedMedia);

                Assert.AreEqual(1, filteredList.Count);
                Assert.AreEqual("DVD", filteredList[0].Name);
            }

            {
                filteredList = new MediaCriteria()
                                    .SetTypeFilter(MediaType.Dvd)
                                    .SetNameFilter("CD")
                                    .List(library.OwnedMedia);

                Assert.AreEqual(0, filteredList.Count);
            }

            {
                filteredList = new MediaCriteria()
                                    .SetTypeFilter(MediaType.Dvd, FilterCondition.NotEqual)
                                    .List(library.OwnedMedia);

                Assert.AreEqual(2, filteredList.Count);
                Assert.AreEqual("Book", filteredList[0].Name);
                Assert.AreEqual("CD", filteredList[1].Name);
            }
        }

    }

}

