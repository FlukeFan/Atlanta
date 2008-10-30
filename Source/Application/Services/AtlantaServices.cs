
using System;
using System.Collections;

using AopAlliance.Intercept;
using Spring.Aop.Framework;

using Atlanta.Application.Services.Interfaces;

namespace Atlanta.Application.Services
{

    /// <summary>
    ///  Utility class for accessing atlanta services
    /// </summary>
    public class AtlantaServices
    {

        [ThreadStatic]
        private static Hashtable _services;

        private static void CreateServicesTable()
        {
            if (_services == null)
            {
                _services = new Hashtable();
            }
        }

        /// <summary>
        /// Retrieve previously registered service.
        /// </summary>
        public static object GetService(Type interfaceType)
        {
            CreateServicesTable();

            object service = _services[interfaceType];

            if (service == null)
                throw new Exception("Service of type '" + interfaceType.ToString() + "' not registered."
                    + "  Register service using AddAdvisedService first.");

            return service;
        }

        /// <summary>
        /// Clear any existing advised services
        /// </summary>
        public static void ClearServices()
        {
            _services = null;
        }

        /// <summary>
        /// Add a service proxied and advised using the appropriate around AOP advice
        /// </summary>
        public static void AddAdvisedService<IServiceInterface>(IServiceInterface   implementation,
                                                                IMethodInterceptor  interceptor)
        {
            CreateServicesTable();
            ProxyFactory proxyFactory = new ProxyFactory(implementation);
            proxyFactory.AddAdvice(interceptor);
            IServiceInterface service = (IServiceInterface)proxyFactory.GetProxy();
            _services.Add(typeof(IServiceInterface), service);
        }

        /// <summary> MediaService </summary>
        public static IMediaService MediaService
        {
            get { return (IMediaService)GetService(typeof(IMediaService)); }
        }

    }

}
