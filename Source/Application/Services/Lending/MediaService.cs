
using System.Collections.Generic;
using System.Reflection;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.Lender;

using Atlanta.Application.Services.Interfaces;
using Atlanta.Application.Services.ServiceBase;

namespace Atlanta.Application.Services.Lending
{

    /// <summary>
    ///  Media services
    /// </summary>
    public class MediaService : IMediaService
    {

        /// <summary>
        ///  Get a list of Media for the system Library
        /// </summary>
        public ServiceResult<IList<Media>> GetMediaList(User            user,
                                                        ClientQuery     mediaCriteria)
        {
            return ServiceResult<IList<Media>>
                .Return(DomainRegistry.Library.GetMediaList(mediaCriteria.ToDetachedCriteria())
                    .Graph().Copy());
        }

        /// <summary>
        ///  Create a Media in the system Library
        /// </summary>
        public ServiceResult<Media> Create( User    user,
                                            Media   orphanedMedia)
        {
            return ServiceResult<Media>
                .Return(DomainRegistry.Library.Create(orphanedMedia)
                    .Graph().Copy());
        }

        /// <summary>
        ///  Modify an existing Media.
        /// </summary>
        public ServiceResult<Media> Modify( User    user,
                                            Media   modifiedMediaCopy)
        {
            Media loadedMedia = DomainRegistry.Repository.Load<Media>(modifiedMediaCopy.Id);

            return ServiceResult<Media>
                .Return(loadedMedia.OwningLibrary.Modify(loadedMedia, modifiedMediaCopy)
                    .Graph().Copy());
        }

        /// <summary>
        ///  Delete the media from the system Library.
        /// </summary>
        public ServiceResult Delete(User    user,
                                    Media   mediaCopy)
        {
            Media loadedMedia = DomainRegistry.Repository.Load<Media>(mediaCopy.Id);
            loadedMedia.OwningLibrary.Delete(loadedMedia);
            return ServiceResult.Void;
        }

    }


}

