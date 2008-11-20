
using NHibernate;

using AopAlliance.Intercept;

using Atlanta.Application.Domain.DomainBase;

using Atlanta.Application.Services.Interfaces;

namespace Atlanta.Application.Services.ServiceBase.Test
{

    /// <summary>
    ///  Class for applying 'around' AOP advice for testing services
    /// </summary>
    public class AopAroundTestAdvice : IMethodInterceptor
    {

        /// <summary>
        ///  Handle 'around' advice for testing services
        /// </summary>
        public object Invoke(IMethodInvocation invocation)
        {
            Repository repository = ServiceTestBase.GetRepository();

            ((IServiceBase) invocation.This).Repository = repository;

            DomainRegistry.Repository = repository;
            DomainRegistry.Library = null;

            object returnValue = invocation.Proceed();

            repository.Flush();
            repository.Clear();

            return returnValue;
        }

    }

}

