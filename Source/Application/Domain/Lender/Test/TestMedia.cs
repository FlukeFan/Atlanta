
using System;

using NHibernate;
using NHibernate.Cfg;

using NUnit.Framework;

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
        public void Insert_Load_Ok()
        {
            Library library = new Library();
            library.Id = 1;

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

    }

}

