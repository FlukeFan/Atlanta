
using System;
using System.Collections.Generic;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.Lender;

using Atlanta.Application.Services.Interfaces;
using Atlanta.Application.Services.ServiceBase;

namespace Atlanta.Application.Services.Lending
{

    /// <summary>
    ///  Media services
    /// </summary>
    public class MediaService : ServiceObjectBase,
                                IMediaService
    {

        /// <summary>
        ///  Get a list of Media for the system Library
        /// </summary>
        public IList<Media> GetMediaList(   User            user,
                                            MediaCriteria   criteria)
        {
            return DomainRegistry.Library.GetMediaList(criteria);
        }

        /// <summary>
        ///  Create a Media in the system Library
        /// </summary>
        public Media Create(User    user,
                            Media   orphanedMedia)
        {
            return DomainRegistry.Library.Create(orphanedMedia);
        }

        /// <summary>
        ///  Modify an existing Media.
        /// </summary>
        public Media Modify(User    user,
                            Media   modifiedMediaCopy)
        {
            Media loadedMedia = (Media) Session.Load(typeof(Media), modifiedMediaCopy.Id);
            return loadedMedia.OwningLibrary.Modify(loadedMedia, modifiedMediaCopy);
        }

        /// <summary>
        ///  Delete the media from the system Library.
        /// </summary>
        public void Delete( User    user,
                            Media   mediaCopy)
        {
            Media loadedMedia = (Media) Session.Load(typeof(Media), mediaCopy.Id);
            loadedMedia.OwningLibrary.Delete(loadedMedia);
        }

    }


}

