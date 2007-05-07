
using System;
using System.Collections;

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

        private static IApplicationContext _context = ContextRegistry.GetContext();

        [ThreadStatic]
        private static Hashtable _services;

        private static void CreateServicesTable()
        {
            if (_services == null)
            {
                _services = new Hashtable();
            }
        }

        private static object GetService(string serviceName)
        {
            CreateServicesTable();

            object service = _services[serviceName];

            if (service == null)
            {
                service = _context[serviceName];
                _services[serviceName] = service;
            }

            return service;
        }

        private static void SetService( string  serviceName,
                                        object  service)
        {
            CreateServicesTable();
            _services[serviceName] = service;
        }

        /// <summary> MediaService </summary>
        public static IMediaService MediaService
        {
            get { return (IMediaService) GetService("MediaService"); }
            set { SetService("MediaService", value); }
        }

    }

}
