
using System;
using System.Reflection;
using System.Reflection.Emit;

using Spring.Context.Support;
using Spring.Objects.Factory;
using Spring.Proxy;

namespace Atlanta.Application.Services.ServiceBase
{

    /// <summary>
    ///  This code was derived from the Spring.Net trunk.  This can be replace with a direct reference to the
    ///   Spring.Net assemblies once the WCF stuff makes it into a release.  (Currently you can only get it
    ///   by checking out the trunk, hacking a couple of build files, then building it yourself)
    /// </summary>
    public class ServiceProxyTypeBuilder : CompositionProxyTypeBuilder
    {
 
        private static readonly MethodInfo GetObject =
            typeof(IObjectFactory).GetMethod("GetObject", new Type[] { typeof(string) });

        private static readonly MethodInfo GetContext =
            typeof(ContextRegistry).GetMethod("GetContext", Type.EmptyTypes);

        private static readonly MethodInfo GetContextByName =
            typeof(ContextRegistry).GetMethod("GetContext", new Type[] { typeof(string) });

        private string contextName;
        private string serviceName;

        /// <summary>
        ///  Constructor
        /// </summary>
        public ServiceProxyTypeBuilder(string contextName, string serviceName, Type serviceType)
        {
            this.contextName = contextName;
            this.serviceName = serviceName;

            this.Name = (contextName == null) ? serviceName : contextName + "." + serviceName;
            this.TargetType = serviceType;
        }

        /// <summary>
        /// Implements constructors for the proxy class.
        /// </summary>
        protected override void ImplementConstructors(TypeBuilder builder)
        {
            MethodAttributes attributes = MethodAttributes.Public |
                MethodAttributes.HideBySig | MethodAttributes.SpecialName |
                MethodAttributes.RTSpecialName;

            ConstructorBuilder cb = builder.DefineConstructor(
                attributes, CallingConventions.Standard, Type.EmptyTypes);

            ILGenerator il = cb.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            if (contextName == null)
            {
                // call ContextRegistry.GetContext()
                il.EmitCall(OpCodes.Call, GetContext, null);
            }
            else
            {
                // call ContextRegistry.GetContext(contextName)
                il.Emit(OpCodes.Ldstr, contextName);
                il.EmitCall(OpCodes.Call, GetContextByName, null);
            }
            il.Emit(OpCodes.Ldstr, serviceName);
            il.EmitCall(OpCodes.Callvirt, GetObject, null);
            il.Emit(OpCodes.Stfld, targetInstance);

            il.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates an appropriate type builder.
        /// </summary>
        protected override TypeBuilder CreateTypeBuilder(string name, Type baseType)
        {
            return DynamicProxyManager.CreateTypeBuilder(name, baseType);
        }

    }

}


/*
 * 


#region Imports

using System;
using System.Reflection;
using System.Reflection.Emit;

using Spring.Proxy;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory;

#endregion

namespace Spring.ServiceModel.Support
{
    /// <summary>
    /// Builds a WCF service type.
    /// </summary>
    /// <author>Bruno Baia</author>
    /// <version>$Id: ServiceProxyTypeBuilder.cs,v 1.1 2007/05/18 21:04:37 bbaia Exp $</version>
    public class ServiceProxyTypeBuilder : CompositionProxyTypeBuilder
    {
        #region Fields

        private static readonly MethodInfo GetObject =
            typeof(IObjectFactory).GetMethod("GetObject", new Type[] { typeof(string) });

        private static readonly MethodInfo GetContext =
            typeof(ContextRegistry).GetMethod("GetContext", Type.EmptyTypes);

        private static readonly MethodInfo GetContextByName =
            typeof(ContextRegistry).GetMethod("GetContext", new Type[] { typeof(string) });

        private string contextName;
        private string serviceName;

        #endregion

        #region Constructor(s) / Destructor

        /// <summary>
        /// Creates a new instance of the 
        /// <see cref="Spring.ServiceModel.Support.ServiceProxyTypeBuilder"/> class.
        /// </summary>
        /// <param name="contextName">The name of the context to use.</param>
        /// <param name="serviceName">The name of the service within Spring's IoC container.</param>
        /// <param name="serviceType">The type of the service.</param>
        public ServiceProxyTypeBuilder(string contextName, string serviceName, Type serviceType)
        {
            this.contextName = contextName;
            this.serviceName = serviceName;

            this.Name = (contextName == null) ? serviceName : contextName + "." + serviceName;
            this.TargetType = serviceType;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Implements constructors for the proxy class.
        /// </summary>
        /// <remarks>
        /// This implementation generates a constructor 
        /// that gets instance of the target object using 
        /// <see cref="Spring.Objects.Factory.IObjectFactory.GetObject(string)"/>.
        /// </remarks>
        /// <param name="builder">
        /// The <see cref="System.Type"/> builder to use.
        /// </param>
        protected override void ImplementConstructors(TypeBuilder builder)
        {
            MethodAttributes attributes = MethodAttributes.Public |
                MethodAttributes.HideBySig | MethodAttributes.SpecialName |
                MethodAttributes.RTSpecialName;

            ConstructorBuilder cb = builder.DefineConstructor(
                attributes, CallingConventions.Standard, Type.EmptyTypes);

            ILGenerator il = cb.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            if (contextName == null)
            {
                // call ContextRegistry.GetContext()
                il.EmitCall(OpCodes.Call, GetContext, null);
            }
            else
            {
                // call ContextRegistry.GetContext(contextName)
                il.Emit(OpCodes.Ldstr, contextName);
                il.EmitCall(OpCodes.Call, GetContextByName, null);
            }
            il.Emit(OpCodes.Ldstr, serviceName);
            il.EmitCall(OpCodes.Callvirt, GetObject, null);
            il.Emit(OpCodes.Stfld, targetInstance);

            il.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates an appropriate type builder.
        /// </summary>
        /// <param name="name">The name to use for the proxy type name.</param>
        /// <param name="baseType">The type to extends if provided.</param>
        /// <returns>The type builder to use.</returns>
        protected override TypeBuilder CreateTypeBuilder(string name, Type baseType)
        {
            return DynamicProxyManager.CreateTypeBuilder(name, baseType);
        }

        #endregion
    }
}
//#endif

*/

