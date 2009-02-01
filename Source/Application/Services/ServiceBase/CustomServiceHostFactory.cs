
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;

using Spring.Util;

using Atlanta.Application.Services.Interfaces;
using Atlanta.Application.Services.Lending;

namespace Atlanta.Application.Services.ServiceBase
{

    /// <summary>
    /// Custom service factory to return correctly advised services to Asp.Net hosted WCF service.
    /// </summary>
    public class CustomServiceHostFactory : ServiceHostFactory
    {

        private static bool _servicesCreated = false;

        private static void CreateServices()
        {
            if (!_servicesCreated)
                lock(typeof(CustomServiceHostFactory))
                    if (!_servicesCreated)
                    {
                        AtlantaServices.ClearServices();
                        AtlantaServices.AddAdvisedService<IMediaService>(new MediaService(), new AopAroundAdvice());
                        _servicesCreated = true;
                    }
        }

        /// <summary>
        ///  Overriden to create services
        /// </summary>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            CreateServices();
            ServiceHost serviceHost = new ServiceHost(AtlantaServices.GetService(serviceType), baseAddresses);
            serviceHost.Description.Name = serviceType.Name;
            serviceHost.AddServiceEndpoint(serviceType.ToString(), new BasicHttpBinding(), "");

            ServiceBehaviorAttribute serviceBehavior = (ServiceBehaviorAttribute)serviceHost.Description.Behaviors[typeof(ServiceBehaviorAttribute)];
            serviceBehavior.InstanceContextMode = InstanceContextMode.Single;

            ServiceMetadataBehavior metadataBehavior = new ServiceMetadataBehavior();
            metadataBehavior.HttpGetEnabled = true;
            serviceHost.Description.Behaviors.Add(metadataBehavior);

            ServiceDebugBehavior debugBehavior = (ServiceDebugBehavior)serviceHost.Description.Behaviors[typeof(ServiceDebugBehavior)];
            debugBehavior.IncludeExceptionDetailInFaults = true;

            return serviceHost;
        }

    }

}

