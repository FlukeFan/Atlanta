
using System;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase.Test;
using Atlanta.Application.Domain.Lender;

namespace Atlanta.Application.Domain.Common.Test
{

    [TestFixture]
    public class StringConverterTest : DomainPersistenceTestBase
    {
        
        [Test]
        public void Convert_OrphanedMedia()
        {
            string received = StringConverter.Convert(Media.InstantiateOrphanedMedia(MediaType.Book, "LA Confidential","Crime Fiction"));
            string expected = "Description=<Crime Fiction>,Id=<0>,Library=<null>,Name=<LA Confidential>,Type=<Book>";
            
            Assert.AreEqual(expected, received);            
        }
        
        [Test]
        public void Convert_Media()
        {
            Library library = Library.InstantiateLibrary();
            Media media = library.Create(Media.InstantiateOrphanedMedia(MediaType.Book, "Refactoring", "Programming"));
            string received = StringConverter.Convert(media);
            string expected = "Description=<Programming>,Id=<0>,Library=<Id=<" + library.Id.ToString() + ">>,Name=<Refactoring>,Type=<Book>";
            
            Assert.AreEqual(expected, received);            
        }        
        
        [Test]
        public void Convert_User()
        {        
            string received = StringConverter.Convert(User.InstantiateUser("Pete Bondourant"));
            string expected = "Id=<0>,Login=<Pete Bondourant>";
            
            Assert.AreEqual(expected, received);
        }
        
        [Test]
        public void Convert_Library()
        {
            Library library = Library.InstantiateLibrary();
            string received = StringConverter.Convert(library);
            string expected = "Id=<" + library.Id.ToString() + ">";
            
            Assert.AreEqual(expected, received);
        }
    }
}    
    