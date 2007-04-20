
using System.Collections.Generic;

using Atlanta.Application.Domain.Lender;

namespace Atlanta.Application.Services.Interfaces
{

    /// <summary>
    /// Interface for Media service
    /// </summary>
    public interface IMediaService : IServiceBase
    {

        /// <summary>
        ///  Get a list of Media for the system Library
        /// </summary>
        IList<Media> GetMediaList(  User            user,
                                    MediaCriteria   criteria);

        /// <summary>
        ///  Create a Media in the system Library
        /// </summary>
        Media Create(   User    user,
                        Media   orphanedMedia);

        /// <summary>
        ///  Modify an existing Media.
        /// </summary>
        Media Modify(   User    user,
                        Media   modifiedMediaCopy);

        /// <summary>
        ///  Delete the media from the system Library.
        /// </summary>
        void Delete(User    user,
                    Media   mediaCopy);

    }
}
