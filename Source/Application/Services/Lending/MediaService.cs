
using System;
using System.Collections.Generic;

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
            return null;
        }

        /// <summary>
        ///  Create a Media in the system Library
        /// </summary>
        public Media Create(User    user,
                            Media   orphanedMedia)
        {
            return null;
        }

        /// <summary>
        ///  Modify an existing Media.
        /// </summary>
        public Media Modify(User    user,
                            Media   modifiedMediaCopy)
        {
            return null;
        }

        /// <summary>
        ///  Delete the media from the system Library.
        /// </summary>
        public void Delete( User    user,
                            Media   mediaCopy)
        {
        }

    }


}

