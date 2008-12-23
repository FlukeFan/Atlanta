
using System;
using System.Collections.Generic;
using System.ServiceModel;

using NHibernate.Criterion;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.Lender;
using Atlanta.Application.Services.Interfaces;

namespace Atlanta.Presentation
{

    /// <summary>
    ///  Class to invoke web-service
    /// </summary>
    public class DosClient
    {

        /// <summary> Main entry point </summary>
        static void Main(string[] args)
        {
            IMediaService service = new ChannelFactory<IMediaService>("MediaService").CreateChannel();
            DetachedCriteria mediaCriteria = DetachedCriteria.For<Media>();
            IList<Media> mediaList = service.GetMediaList(null, mediaCriteria);
            foreach (Media media in mediaList)
            {
                Console.WriteLine(media.Name + ", " + media.Type.ToString() + ", " + media.Description);
            }
        }

    }


}
