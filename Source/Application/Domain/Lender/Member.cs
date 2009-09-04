using System;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{
    /// <summary></summary>
    public enum MemberStatus
    {
        /// <summary></summary>
        None,
    
        /// <summary></summary>
        Active,

        /// <summary></summary>
        Suspended,
    }


    /// <summary>
    /// Class to represent Media
    /// </summary>
    [Serializable]
    public class Member : DomainObjectBase
    {
        #region Fields

        private Library         _owningLibrary;
        private string          _name;
        private DateTime        _dateOfBirth;
        private MemberStatus    _status;

        #endregion
        
        #region Constructors        
        
        /// <summary> constructor </summary>
        protected Member()
        {
        }

        /// <summary> constructor </summary>
        protected Member(   Library         owningLibrary,
                            string          name,
                            DateTime        dateOfBirth,
                            MemberStatus    status)
        {
            OwningLibrary = owningLibrary;
            Name = name;
            DateOfBirth = dateOfBirth;
            Status = status;
        }           
        
        #endregion
        
        #region Factory Methods
        
        /// <summary> factory method </summary>
        public static Member InstantiateOrphanedMember(string  name, DateTime dateOfBirth, MemberStatus status)
        {
            return new Member(null, name, dateOfBirth, status);
        }         
        
        /// <summary> factory method </summary>
        public static Member InstantiateMember(Library owningLibrary, string  name, DateTime dateOfBirth, MemberStatus status)
        {
            return new Member(owningLibrary, name, dateOfBirth, status);
        }        
        
        #endregion
              
        #region Properties            
              
        /// <summary></summary>
        public virtual Library OwningLibrary
        {
            get { return _owningLibrary; }
            protected set { _owningLibrary = value; }
        }
        
        /// <summary></summary>        
        public virtual string Name
        {
            get { return _name; }
            protected set { _name = value; }        
        }
        
        /// <summary></summary>           
        public virtual DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            protected set { _dateOfBirth = value; }          
        }
        
        /// <summary></summary>           
        public virtual MemberStatus Status
        {
            get { return _status; }
            protected set { _status = value; }
        }
        
        #endregion
        
        #region Business Methods
        
        /// <summary></summary>           
        public virtual void Suspend()
        {
            Status = MemberStatus.Suspended;
        }

        /// <summary></summary>           
        public virtual void Activate()
        {
            Status = MemberStatus.Active;
        }

        /// <summary>Modify the details of this member.</summary>
        public virtual void ModifyDetails(string newName, DateTime newDateOfBirth)
        {
            Name = newName;
            DateOfBirth = newDateOfBirth;
        }
        
        #endregion
    }
}    