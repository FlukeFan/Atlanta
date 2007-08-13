
using System.Collections.Generic;
using System.ServiceModel;

using Atlanta.Application.Domain.Lender;

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
        IList<Media> GetMediaList(  User            user,
                                    MediaCriteria   criteria);

        /// <summary>
        ///  Create a Media in the system Library
        /// </summary>
        [OperationContract]
        Media Create(   User    user,
                        Media   orphanedMedia);

        /// <summary>
        ///  Modify an existing Media.
        /// </summary>
        [OperationContract]
        Media Modify(   User    user,
                        Media   modifiedMediaCopy);

        /// <summary>
        ///  Delete the media from the system Library.
        /// </summary>
        [OperationContract]
        void Delete(User    user,
                    Media   mediaCopy);

        /// <summary>
        ///  TempMethodToTestWebServices
        /// </summary>
        [OperationContract]
        string TempMethodToTestWebServices();
    }
}
