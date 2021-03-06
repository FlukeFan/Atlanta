
using System;

using NHibernate;
using NHibernate.Cfg;

using AopAlliance.Intercept;

using Atlanta.Application.Domain.DomainBase;

using Atlanta.Application.Services.Interfaces;

namespace Atlanta.Application.Services.ServiceBase
{

    /// <summary>
    ///  Class for applying 'around' AOP advice for services
    /// </summary>
    public class AopAroundAdvice : IMethodInterceptor
    {

        static private Configuration _configuration;
        static private ISessionFactory _sessionFactory;

        private ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _configuration = new Configuration();
                    _configuration.AddAssembly("Atlanta.Application.Domain");

                    _sessionFactory = _configuration.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        /// <summary>
        ///  Handle 'around' advice for services
        /// </summary>
        public object Invoke(IMethodInvocation invocation)
        {
            object returnValue = null;

            using (Repository repository = new Repository(Repository.SessionFactory))
            {
                repository.BeginTransaction();

                DomainRegistry.Repository = repository;
                DomainRegistry.Library = null;

                try
                {
                    returnValue = invocation.Proceed();

                    repository.CommitTransaction();
                }
                catch (Exception e)
                {
                    returnValue = ServiceResult.Error(invocation.Method.ReturnType, e);
                }
            }

            return returnValue;
        }

    }

}

