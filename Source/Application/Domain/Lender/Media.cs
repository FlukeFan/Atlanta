
using System;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    /// <summary>
    /// MediaType enumeration
    /// </summary>
    public enum MediaType
    {
        /// <summary> None </summary>
        None,
    
        /// <summary> Book </summary>
        Book,

        /// <summary> Cd </summary>
        Cd,

        /// <summary> Dvd </summary>
        Dvd,
    }


    /// <summary>
    /// Class to represent Media
    /// </summary>
    public class Media : DomainObjectBase
    {
        #region Fields

        private Library     _owningLibrary;
        private MediaType   _type;
        private string      _name;
        private string      _description;

        #endregion
        
        #region Constructors
        
        /// <summary> constructor </summary>
        protected Media() 
            : this(null, MediaType.None, String.Empty, String.Empty)
        {
        }
        
        /// <summary> constructor </summary>
        protected Media(Library     owningLibrary,
                        MediaType   type,
                        string      name,
                        string      description)
            : base()                        
        {
            OwningLibrary = owningLibrary;
            Type = type;
            Name = name;
            Description = description;
        }        
        
        #endregion 
        
        #region Factory Methods
        
        /// <summary> factory method </summary>
        public static Media InstantiateOrphanedMedia(   MediaType   type,
                                                        string      name,
                                                        string      description)
        {
            return new Media(null, type, name, description);
        }
        
        /// <summary> factory method </summary>
        public static Media InstantiateMedia(   Library     owningLibrary,
                                                MediaType   type,
                                                string      name,
                                                string      description)
        {
            return new Media(owningLibrary, type, name, description);
        }        
        
        #endregion
        
        #region Properties            

        /// <summary> Library </summary>
        public virtual Library OwningLibrary
        {
            get { return _owningLibrary; }
            protected set { _owningLibrary = value; }
        }

        /// <summary> Type </summary>
        public virtual MediaType Type
        {
            get { return _type; }
            protected set { _type = value; }
        }

        /// <summary> Name </summary>
        public virtual string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        /// <summary> Description </summary>
        public virtual string Description
        {
            get { return _description; }
            protected set { _description = value; }
        }

        #endregion
        
        #region Business Methods
        
        /// <summary> Update the details of this media. </summary>
        public virtual void ModifyDetails(  MediaType   newType,
                                            string      newName,
                                            string      newDescription)
        {
            Type = newType;
            Name = newName;
            Description = newDescription;
        }
        
        #endregion
        
    }

}

