using System;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestMember : DomainPersistenceTestBase
    {
        [Test]
        public void InstantiateOrphanedMember_Ok()
        {
            Member member = Member.InstantiateOrphanedMember("test", new DateTime(1977,1,1) , MemberStatus.None);

            Assert.AreEqual(null, member.OwningLibrary); 
            Assert.AreEqual("test", member.Name);
            Assert.AreEqual(new DateTime(1977,1,1), member.DateOfBirth);
            Assert.AreEqual(MemberStatus.None, member.Status);
        }
        
        [Test]
        public void InstantiateMember_Ok()
        {
            Library owningLibrary = Library.InstantiateLibrary();
        
            Member member = Member.InstantiateMember(owningLibrary, "test", new DateTime(1977,1,1), MemberStatus.None);

            Assert.AreEqual(owningLibrary, member.OwningLibrary); 
            Assert.AreEqual("test", member.Name);
            Assert.AreEqual(new DateTime(1977,1,1), member.DateOfBirth);
            Assert.AreEqual(MemberStatus.None, member.Status);
        }        
        
        [Test]       
        public void Suspend_Ok()
        {
            Member member = Member.InstantiateOrphanedMember("test", new DateTime(1977,1,1) , MemberStatus.None);            
            member.Suspend();
            
            Assert.AreEqual(MemberStatus.Suspended, member.Status);
        }

        [Test]          
        public void Activate_Ok()
        {
            Member member = Member.InstantiateOrphanedMember("test", new DateTime(1977,1,1) , MemberStatus.None);            
            member.Activate();
            
            Assert.AreEqual(MemberStatus.Active, member.Status);
        }
        
        [Test]          
        public void ModifyDetails_Ok()
        {
            Member member = Member.InstantiateOrphanedMember("test", new DateTime(1977,1,1) , MemberStatus.None);           
            member.ModifyDetails("modified", new DateTime(2006,1,1));
            
            Assert.AreEqual(null, member.OwningLibrary); 
            Assert.AreEqual("modified", member.Name);
            Assert.AreEqual(new DateTime(2006,1,1), member.DateOfBirth);
            Assert.AreEqual(MemberStatus.None, member.Status);
        }
    } 
}

