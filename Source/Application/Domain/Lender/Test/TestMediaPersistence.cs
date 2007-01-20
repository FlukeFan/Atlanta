
using System;
using System.Collections;

using NHibernate.Expression;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestMediaPersistence : DomainPersistenceTestBase
    {

        [Test]
        public void Insert_Load_Ok()
        {
            Library library = (Library) Session.Load(typeof(Library), 1L);

            Media media = Media.InstantiateMedia(library, MediaType.Dvd, "test", "test description");

            Session.Save(media);
            Assert.IsTrue(media.Id != 0);

            Session.Flush();
            Session.Clear();

            media = (Media) Session.Load(typeof(Media), media.Id);

            Assert.AreEqual(MediaType.Dvd, media.Type);
            Assert.AreEqual("test", media.Name);
            Assert.AreEqual("test description", media.Description);
        }

        [Test]
        public void ExampleByFilter_LoadAllBooks()
        {
            Library library = (Library) Session.Load(typeof(Library), 1L);

            IList bookMediaInLibrary = Session.CreateFilter(library.OwnedMedia, "where type = ?")
                                        .SetParameter(0, MediaType.Book)
                                        .List();

            Assert.AreEqual(1, bookMediaInLibrary.Count);
            Assert.AreEqual("Book", ((Media) bookMediaInLibrary[0]).Name);
        }

        [Test]
        public void ExampleByCriteria_LoadAllBooks()
        {
            Library library = (Library) Session.Load(typeof(Library), 1L);

            IList bookMediaInLibrary = Session.CreateCriteria(typeof(Media))
                                            .Add(Expression.Eq("OwningLibrary", library))
                                            .Add(Expression.Eq("Type", MediaType.Book))
                                            .List();

            Assert.AreEqual(1, bookMediaInLibrary.Count);
            Assert.AreEqual("Book", ((Media) bookMediaInLibrary[0]).Name);
        }

    }

}

