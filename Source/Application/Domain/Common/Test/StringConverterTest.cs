
using System;

using NUnit.Framework;

using Atlanta.Application.Domain.Lender;

namespace Atlanta.Application.Domain.Common.Test
{

    [TestFixture]
    public class StringConverterTest
    {
        
        [Test]
        public void Convert_OrphanedMedia()
        {
            string received = StringConverter.Convert(Media.InstantiateOrphanedMedia(MediaType.Book, "LA Confidential","Crime Fiction"));
            string expected = "OwningLibrary=<null>,Type=<Book>,Name=<LA Confidential>,Description=<Crime Fiction>,Id=<0>";
            
            Assert.AreEqual(expected, received);            
        }
        
        [Test]
        public void Convert_Media()
        {
            string received = StringConverter.Convert(Media.InstantiateMedia(Library.InstantiateLibrary(), MediaType.Book, "Refactoring", "Programming"));
            string expected = "OwningLibrary=<Id=<0>>,Type=<Book>,Name=<Refactoring>,Description=<Programming>,Id=<0>";
            
            Assert.AreEqual(expected, received);            
        }        
        
        [Test]
        public void Convert_User()
        {        
            string received = StringConverter.Convert(User.InstantiateUser("Pete Bondourant"));
            string expected = "Login=<Pete Bondourant>,Id=<0>";
            
            Assert.AreEqual(expected, received);
        }
        
        [Test]
        public void Convert_Library()
        {
            string received = StringConverter.Convert(Library.InstantiateLibrary());
            string expected = "Id=<0>";
            
            Assert.AreEqual(expected, received);
        }
    }
}    
    