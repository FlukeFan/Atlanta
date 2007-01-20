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
        #region Fields

        private Member      _loaningMember;
        private Media       _loanedMedia;
        private DateTime    _startDate;
        private DateTime    _endDate;
        
        #endregion    
        
        #region Constructors        
        
        /// <summary> constructor </summary>
        protected Loan(Member loaningMember, Media loanedMedia, DateTime startDate, DateTime endDate)
        {
            LoaningMember = loaningMember;
            LoanedMedia = loanedMedia;
            StartDate = startDate;
            EndDate = endDate;
        }
        
        #endregion         
        
        #region Factory Methods
        
        /// <summary> factory method </summary>
        public static Loan InstantiateLoan(Member member, Media media, DateTime startDate, DateTime endDate)
        {
            return new Loan(member, media, startDate, endDate);
        } 
        
        #endregion
        
        #region Properties            
              
        /// <summary></summary>
        public virtual Member LoaningMember
        {   
            get { return _loaningMember; }
            protected set { _loaningMember = value; }        
        }
        
        /// <summary></summary>        
        public virtual Media LoanedMedia
        {
            get { return _loanedMedia; }
            protected set { _loanedMedia = value; }                
        }

        /// <summary></summary> 
        public virtual DateTime StartDate
        {
            get { return _startDate; }
            protected set { _startDate = value; }          
        }
        
        /// <summary></summary>         
        public virtual DateTime EndDate
        {
            get { return _endDate; }
            protected set { _endDate = value; }           
        }
    
        #endregion
    }
}   