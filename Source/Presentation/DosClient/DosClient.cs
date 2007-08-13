
using System;
using System.ServiceModel;

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
            string result = service.TempMethodToTestWebServices();
            Console.WriteLine(result);
        }

    }


}
