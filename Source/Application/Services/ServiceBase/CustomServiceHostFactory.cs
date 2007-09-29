
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

using Spring.Util;

namespace Atlanta.Application.Services.ServiceBase
{

    /// <summary>
    ///  This code was derived from the Spring.Net trunk.  This can be replace with a direct reference to the
    ///   Spring.Net assemblies once the WCF stuff makes it into a release.  (Currently you can only get it
    ///   by checking out the trunk, hacking a couple of build files, then building it yourself)
    /// </summary>
    public class CustomServiceHostFactory : ServiceHostFactoryBase
    {

        /// <summary>
        ///  Override
        /// </summary>
        public override ServiceHostBase CreateServiceHost(string reference, Uri[] baseAddresses)
        {
            if (StringUtils.IsNullOrEmpty(reference))
            {
                throw new ArgumentException("The service name was not provided in the ServiceHost directive.", "Service");
            }

            string[] refSplitted = reference.Split(':');
            if (refSplitted.Length == 2)
            {
                return new CustomServiceHost(refSplitted[0], refSplitted[1], baseAddresses);
            }
            else
            {
                return new CustomServiceHost(reference, baseAddresses);
            }
        }

    }

}

