using System;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    public enum MemberStatus
    {
        None,
        Active,
        Suspended,
    }


    [Serializable]
    public class Member : DomainObjectBase
    {

        protected Member() { }

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

        public virtual Library      OwningLibrary   { get; protected set; }
        public virtual string       Name            { get; protected set; }
        public virtual DateTime     DateOfBirth     { get; protected set; }
        public virtual MemberStatus Status          { get; protected set; }


        public static Member InstantiateOrphanedMember(string  name, DateTime dateOfBirth, MemberStatus status)
        {
            return new Member(null, name, dateOfBirth, status);
        }         
        
        public static Member InstantiateMember(Library owningLibrary, string  name, DateTime dateOfBirth, MemberStatus status)
        {
            return new Member(owningLibrary, name, dateOfBirth, status);
        }        
        
        public virtual void Suspend()
        {
            Status = MemberStatus.Suspended;
        }

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

    }

}    