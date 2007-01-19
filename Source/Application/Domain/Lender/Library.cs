
using System;
using System.Collections.Generic;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    /// <summary>
    /// Class to represent a Library
    /// </summary>
    public class Library : DomainObjectBase
    {

        private IList<Media> _ownedMedia = new DomainList<Media>();


        /// <summary> constructor </summary>
        protected Library()
        {
        }


        /// <summary> factory method </summary>
        public static Library InstantiateLibrary()
        {
            return new Library();
        }


        /// <summary> Media collection </summary>
        virtual public IList<Media> OwnedMedia
        {
            get { return _ownedMedia; }
            protected set { _ownedMedia = value; }
        }


        /// <summary>
        ///  Add new Media to the library.  The combination of Media.Name and Media.Type
        ///   need to be unique within the Library.  Throws DuplicationException if they are not.
        ///   Returns the newly create media object.
        /// </summary>
        virtual public Media Add(Media media)
        {
            Media newMedia = Media.InstantiateMedia(this, media.Type, media.Name, media.Description);
            OwnedMedia.Add(newMedia);

            return newMedia;
        }

    }

}

