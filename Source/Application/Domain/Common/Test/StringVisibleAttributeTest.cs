
using System;

using NUnit.Framework;

using Atlanta.Application.Domain.Lender;

namespace Atlanta.Application.Domain.Common.Test
{

    [TestFixture]
    public class StringVisibleAttributeTest
    {
        [Test]
        public void Constructor_True()
        {
            StringVisibleAttribute attribute = new StringVisibleAttribute(true);
            
            Assert.AreEqual(true, attribute.Value);
        }    
        
        [Test]
        public void Constructor_False()
        {
            StringVisibleAttribute attribute = new StringVisibleAttribute(false);
            
            Assert.AreEqual(false, attribute.Value);
        }            
    }
}    