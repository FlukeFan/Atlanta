
using System;
using System.Collections.Generic;

using NUnit.Framework;

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

        [Test] [Ignore("RGB - WIP - need to add criteria to persisted collectons")]
        public void FilterStringProperty_Ok()
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
        }

    }

}

