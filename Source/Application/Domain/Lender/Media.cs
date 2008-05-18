
using System;

using Atlanta.Application.Domain.Common;
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
    [Serializable]
    public class Media : DomainObjectBase
    {
        #region Fields

        private Library     _owningLibrary;
        private MediaType   _type;
        private string      _name;
        private string      _description;

        #endregion
        
        #region Constructors
        
        /// <summary> default constructor (for NH only)</summary>
        protected Media() { }
        
        #endregion 
        
        #region Factory Methods
        
        /// <summary> factory method </summary>
        public static Media InstantiateOrphanedMedia(   MediaType   type,
                                                        string      name,
                                                        string      description)
        {
            Media media = new Media();
            media.Type = type;
            media.Name = name;
            media.Description = description;
            return media;
        }
        
        /// <summary> factory method </summary>
        public static Media InstantiateMedia(   Library     owningLibrary,
                                                MediaType   type,
                                                string      name,
                                                string      description)
        {
            Media media = InstantiateOrphanedMedia(type, name, description);
            media.OwningLibrary = owningLibrary;
            return media;
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
        
        /// <summary> Update the details of this (transient) media. </summary>
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

