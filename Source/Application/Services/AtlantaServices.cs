
using Spring.Context;
using Spring.Context.Support;

using Atlanta.Application.Services.Interfaces;

namespace Atlanta.Application.Services
{

    /// <summary>
    ///  Utility class for accessing atlanta services
    /// </summary>
    public class AtlantaServices
    {

        static private IApplicationContext _context = ContextRegistry.GetContext();

        /// <summary> MediaService </summary>
        static public IMediaService MediaService { get { return (IMediaService) _context["MediaService"]; } }

    }

}
