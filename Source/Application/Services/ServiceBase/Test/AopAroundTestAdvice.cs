
using System;

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

            DomainRegistry.Repository = repository;
            DomainRegistry.Library = null;

            try
            {
                object returnValue = invocation.Proceed();

                repository.Flush();
                repository.Clear();

                return returnValue;
            }
            catch (Exception e)
            {
                return ServiceResult.Error(invocation.Method.ReturnType, e);
            }
        }

    }

}

