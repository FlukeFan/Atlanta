
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;

using NHibernate.Criterion;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.Lender;

using Atlanta.Application.Services.Interfaces;
using Atlanta.Application.Services.ServiceBase;

namespace Atlanta.Application.Services.Lending
{

    /// <summary>
    ///  Media services
    /// </summary>
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class MediaService : ServiceObjectBase,
                                IMediaService
    {

        /// <summary>
        ///  Get a list of Media for the system Library
        /// </summary>
        public ServiceResult<IList<Media>> GetMediaList(User            user,
                                                        ClientQuery     mediaCriteria)
        {
            IList<Media> mediaList =
                DomainRegistry.Library.GetMediaList(mediaCriteria);

            // temporary solution that
            // creates a copy to serialise
            // across a web-service
            IList<Media> mediaListCopy = new List<Media>();
            foreach (Media sourceMedia in mediaList)
            {
                Media copy = Media.InstantiateOrphanedMedia(sourceMedia.Type, sourceMedia.Name, sourceMedia.Description);
                PropertyInfo propertyInfo = typeof(Media).GetProperty("Id");
                propertyInfo.SetValue(copy, sourceMedia.Id, null);
                mediaListCopy.Add(copy);
            }

            return ServiceResult<IList<Media>>.Return(mediaListCopy);
        }

        /// <summary>
        ///  Create a Media in the system Library
        /// </summary>
        public ServiceResult<Media> Create( User    user,
                                            Media   orphanedMedia)
        {
            return ServiceResult<Media>
                .Return(DomainRegistry.Library.Create(orphanedMedia));
        }

        /// <summary>
        ///  Modify an existing Media.
        /// </summary>
        public ServiceResult<Media> Modify( User    user,
                                            Media   modifiedMediaCopy)
        {
            Media loadedMedia = Repository.Load<Media>(modifiedMediaCopy.Id);

            return ServiceResult<Media>
                .Return(loadedMedia.OwningLibrary.Modify(loadedMedia, modifiedMediaCopy));
        }

        /// <summary>
        ///  Delete the media from the system Library.
        /// </summary>
        public ServiceResult Delete(User    user,
                                    Media   mediaCopy)
        {
            Media loadedMedia = Repository.Load<Media>(mediaCopy.Id);
            loadedMedia.OwningLibrary.Delete(loadedMedia);
            return ServiceResult.Void;
        }

    }


}

