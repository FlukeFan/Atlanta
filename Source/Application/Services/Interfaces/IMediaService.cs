
using System.Collections.Generic;
using System.ServiceModel;

using NHibernate.Criterion;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.Lender;
using Atlanta.Application.Services.ServiceBase;

namespace Atlanta.Application.Services.Interfaces
{

    /// <summary>
    /// Interface for Media service
    /// </summary>
    [ServiceContract()]
    public interface IMediaService : IServiceBase
    {

        /// <summary>
        ///  Get a list of Media for the system Library
        /// </summary>
        [OperationContract]
        ServiceResult<IList<Media>> GetMediaList(   User            user,
                                                    ClientQuery     mediaCriteria);

        /// <summary>
        ///  Create a Media in the system Library
        /// </summary>
        [OperationContract]
        ServiceResult<Media> Create(User    user,
                                    Media   orphanedMedia);

        /// <summary>
        ///  Modify an existing Media.
        /// </summary>
        [OperationContract]
        ServiceResult<Media> Modify(User    user,
                                    Media   modifiedMediaCopy);

        /// <summary>
        ///  Delete the media from the system Library.
        /// </summary>
        [OperationContract]
        ServiceResult Delete(   User    user,
                                Media   mediaCopy);

    }
}
