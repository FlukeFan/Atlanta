
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
            ISession session = SessionFactory.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            ((IServiceBase)invocation.This).Session = session;

            DomainRegistry.Session = session;
            DomainRegistry.Library = null;

            object returnValue = null;
            try
            {
                returnValue = invocation.Proceed();

                transaction.Commit();
                session.Disconnect();
                session.Close();
            }
            catch
            {
                transaction.Rollback();
                session.Disconnect();
                session.Close();
                throw;
            }

            return returnValue;
        }

    }

}

