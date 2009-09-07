using System;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    /// <summary>
    /// Class to represent a Loan
    /// </summary>
    [Serializable]
    public class Loan : DomainObjectBase
    {

        protected Loan() { }

        protected Loan(Member loaningMember, Media loanedMedia, DateTime startDate, DateTime endDate)
        {
            LoaningMember = loaningMember;
            LoanedMedia = loanedMedia;
            StartDate = startDate;
            EndDate = endDate;
        }
        
        public virtual Member       LoaningMember   { get; protected set; }
        public virtual Media        LoanedMedia     { get; protected set; }
        public virtual DateTime     StartDate       { get; protected set; }
        public virtual DateTime     EndDate         { get; protected set; }

        public static Loan InstantiateLoan(Member member, Media media, DateTime startDate, DateTime endDate)
        {
            return new Loan(member, media, startDate, endDate);
        } 
        
    }
}   