
using System;

using Atlanta.Application.Domain.Common;
using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    public enum MediaType
    {
        None,
        Book,
        Cd,
        Dvd,
    }


    [Serializable]
    public partial class Media : DomainObjectBase
    {

        protected Media() { }

        public virtual Library      Library         { get; protected set; }
        public virtual MediaType    Type            { get; protected set; }
        public virtual string       Name            { get; protected set; }
        public virtual string       Description     { get; protected set; }

        
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
        
        internal static Media InstantiateMedia( Library     owningLibrary,
                                                MediaType   type,
                                                string      name,
                                                string      description)
        {
            Media media = InstantiateOrphanedMedia(type, name, description);
            media.Library = owningLibrary;
            return media;
        }        
        
        
        /// <summary> Update the details of this (transient) media. </summary>
        public virtual void ModifyDetails(  MediaType   newType,
                                            string      newName,
                                            string      newDescription)
        {
            Type = newType;
            Name = newName;
            Description = newDescription;
        }
        
    }

}

