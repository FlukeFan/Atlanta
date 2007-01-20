
using System;
using System.Collections.Generic;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    /// <summary>
    /// Class to represent a Library
    /// </summary>
    [Serializable]
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


        private void ValidateNoMediaWithNameAndType(Media media)
        {
            IList<Media> mediaWithNameAndType = new MediaCriteria()
                                                    .SetTypeFilter(media.Type)
                                                    .SetNameFilter(media.Name)
                                                    .List(OwnedMedia);

            if (mediaWithNameAndType.Count != 0)
            {
                throw new DuplicationException(mediaWithNameAndType[0]);
            }
        }


        /// <summary>
        ///  Add new Media to the library.  The combination of Media.Name and Media.Type
        ///   need to be unique within the Library.  Throws DuplicationException if they are not.
        ///   Returns the newly create media object.
        /// </summary>
        virtual public Media Create(Media media)
        {
            ValidateNoMediaWithNameAndType(media);

            Media newMedia = Media.InstantiateMedia(this, media.Type, media.Name, media.Description);
            OwnedMedia.Add(newMedia);

            return newMedia;
        }

        /// <summary>
        ///  Modify existing media in the Library.  The combination of Media.Name and Media.Type
        ///   need to be unique within the Library.  Throws DuplicationException if they are not.
        ///   Returns the modified media object.
        /// </summary>
        virtual public Media Modify(Media existingMedia,
                                    Media modifiedMedia)
        {
            existingMedia.ModifyDetails(modifiedMedia.Type, modifiedMedia.Name, modifiedMedia.Description);
            return existingMedia;
        }

    }

}

