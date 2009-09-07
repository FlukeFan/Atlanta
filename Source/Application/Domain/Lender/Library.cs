
using System;
using System.Collections.Generic;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.Common;

using NHibernate.Criterion;
using NHibernate.LambdaExtensions;

namespace Atlanta.Application.Domain.Lender
{

    [Serializable]
    public partial class Library : DomainObjectBase
    {

        private IList<Media> _ownedMedia = new List<Media>();

        protected Library() { }

        [StringVisible(false)]
        public virtual IEnumerable<Media> OwnedMedia
        {
            get { return _ownedMedia; }
            protected set { _ownedMedia = (value as IList<Media>); }
        }


        private void ValidateNoMediaWithNameAndType(Media media)
        {
            IList<Media> mediaWithNameAndType =
                DomainRegistry.Repository
                    .CreateQuery<Media>()
                    .Add<Media>(m => m.Library == this)
                    .Add<Media>(m => m.Type == media.Type)
                    .Add<Media>(m => m.Name == media.Name)
                    .List<Media>();

            if (mediaWithNameAndType.Count != 0)
            {
                Media duplicateMedia = mediaWithNameAndType[0];
                string duplicateValue = duplicateMedia.Type + ", " + duplicateMedia.Name;
                throw new DuplicationException(mediaWithNameAndType[0], duplicateValue);
            }
        }


        public static Library InstantiateLibrary()
        {
            Library library = new Library();
            DomainRegistry.Repository.Insert(library);
            return library;
        }

        /// <summary>
        ///  Get a list of Media in the Library using the specified criteria.
        /// </summary>
        public virtual IList<Media> GetMediaList(DetachedCriteria mediaCriteria)
        {
            return
                DomainRegistry.Repository.CreateQuery(mediaCriteria)
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
            _ownedMedia.Add(newMedia);

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
            _ownedMedia.Remove(media);
        }

    }

}

