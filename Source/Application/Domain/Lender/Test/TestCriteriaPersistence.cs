
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

        [Test]
        public void FilterStringAndEnumProperty_Ok()
        {
            Library library = (Library) Session.Load(typeof(Library), 1L);

            IList<Media> filteredList;
            {
                filteredList = new MediaCriteria()
                                    .SetNameFilter("CD")
                                    .List(library.Media);

                Assert.AreEqual(1, filteredList.Count);
                Assert.AreEqual("CD", filteredList[0].Name);
            }

            {
                filteredList = new MediaCriteria()
                                    .SetTypeFilter(MediaType.Dvd)
                                    .List(library.Media);

                Assert.AreEqual(1, filteredList.Count);
                Assert.AreEqual("DVD", filteredList[0].Name);
            }

            {
                filteredList = new MediaCriteria()
                                    .SetTypeFilter(MediaType.Dvd)
                                    .SetNameFilter("CD")
                                    .List(library.Media);

                Assert.AreEqual(0, filteredList.Count);
            }

            {
                filteredList = new MediaCriteria()
                                    .SetTypeFilter(MediaType.Dvd, FilterCondition.NotEqual)
                                    .List(library.Media);

                Assert.AreEqual(2, filteredList.Count);
                Assert.AreEqual("Book", filteredList[0].Name);
                Assert.AreEqual("CD", filteredList[1].Name);
            }
        }

    }

}

