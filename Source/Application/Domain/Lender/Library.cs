
using System;
using System.Collections.Generic;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.Common;

using NHibernate.Criterion;

namespace Atlanta.Application.Domain.Lender
{

    /// <summary>
    /// Class to represent a Library
    /// </summary>
    [Serializable]
    public partial class Library : DomainObjectBase
    {

        private IList<Media> _ownedMedia;


        /// <summary> constructor </summary>
        protected Library()
        {
        }


        /// <summary> factory method </summary>
        public static Library InstantiateLibrary()
        {
            Library library = new Library();
            library._ownedMedia = new List<Media>();
            DomainRegistry.Repository.Insert(library);
            return library;
        }


        /// <summary> Media collection </summary>
        protected virtual IList<Media> OwnedMedia
        {
            get { return _ownedMedia; }
            set { _ownedMedia = value; }
        }

        /// <summary> Read-only wrapper of list </summary>
        [StringVisible(false)]
        public virtual IList<Media> ReadonlyOwnedMedia { get { return new List<Media>(_ownedMedia).AsReadOnly(); } }


        private void ValidateNoMediaWithNameAndType(Media media)
        {
            IList<Media> mediaWithNameAndType =
                DomainRegistry.Repository
                    .CreateQuery<Media>()
                    .Add(Expression.Eq(Media.Properties.OwningLibrary, this))
                    .Add(Expression.Eq(Media.Properties.Type, media.Type))
                    .Add(Expression.Eq(Media.Properties.Name, media.Name))
                    .List<Media>();

            if (mediaWithNameAndType.Count != 0)
            {
                throw new DuplicationException(mediaWithNameAndType[0]);
            }
        }


        /// <summary>
        ///  Get a list of Media in the Library using the specified criteria.
        /// </summary>
        virtual public IList<Media> GetMediaList(DomainCriteria mediaCriteria)
        {
            return
                mediaCriteria
                    .ToExecutableCriteria()
                    .List<Media>();
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

