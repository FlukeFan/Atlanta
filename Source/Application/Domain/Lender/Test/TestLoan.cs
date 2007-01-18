using System;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Domain.Lender.Test
{

    [TestFixture]
    public class TestLoan : DomainTestBase
    {
        [Test]
        public void InstantiateLoan_Ok()
        {
            Loan loan = Loan.InstantiateLoan(null, null, new DateTime(2006,1,1), new DateTime(2006,1,2));
            
            Assert.AreEqual(null, loan.LoaningMember);            
            Assert.AreEqual(null, loan.LoanedMedia);            
            Assert.AreEqual(new DateTime(2006,1,1), loan.StartDate);            
            Assert.AreEqual(new DateTime(2006,1,2), loan.EndDate);                    
        }
    }
}    