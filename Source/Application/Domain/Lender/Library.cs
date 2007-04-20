
using System;
using System.Collections.Generic;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.Common;

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
            Library library = new Library();
            DomainRegistry.Library = library;
            return library;
        }


        /// <summary> Media collection </summary>
        [StringVisible(false)]
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
        ///  Get a list of Media in the Library using the specified criteria.
        /// </summary>
        virtual public IList<Media> GetMediaList(MediaCriteria mediaCriteria)
        {
            return mediaCriteria.List(OwnedMedia);
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
            if ((modifiedMedia.Name != existingMedia.Name) || (modifiedMedia.Type != existingMedia.Type))
            {
                ValidateNoMediaWithNameAndType(modifiedMedia);
            }

            existingMedia.ModifyDetails(modifiedMedia.Type, modifiedMedia.Name, modifiedMedia.Description);
            return existingMedia;
        }

        /// <summary>
        ///  Delete the media from the library.
        /// </summary>
        virtual public void Delete(Media media)
        {
            OwnedMedia.Remove(media);
        }

    }

}

