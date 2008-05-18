
using System;
using System.IO;
using System.Xml;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase.Test;
using Atlanta.Application.Domain.Lender;

namespace Atlanta.Application.Domain.Common.Test
{

    [TestFixture]
    public class XmlConverterTest : DomainPersistenceTestBase
    {
        [Test]
        public void Convert_User()
        {        
            Console.WriteLine(ConvertXmlDocumentToString(XmlConverter.Convert(User.InstantiateUser("Pete Bondourant"))));
        }
        
        [Test]
        public void Convert_OrphanedMedia()
        {
            Console.WriteLine(ConvertXmlDocumentToString(XmlConverter.Convert(Media.InstantiateOrphanedMedia(MediaType.Book, "LA Confidential","Crime Fiction"))));
        }
        

        [Test]
        public void Convert_Media()
        {
            Library library = Library.InstantiateLibrary();
            Media media = library.Create(Media.InstantiateOrphanedMedia(MediaType.Book, "Refactoring", "Programming"));
            Console.WriteLine(ConvertXmlDocumentToString(XmlConverter.Convert(media)));
        }
       
        
        
        private static string ConvertXmlDocumentToString(XmlDocument xmlDocument)
        {
            StringWriter stringWriter = new StringWriter();        
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);;

            xmlDocument.WriteTo(xmlTextWriter);

            return stringWriter.ToString();    
        }           
    }
}    
        