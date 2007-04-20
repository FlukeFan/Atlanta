
using System;

using NHibernate;
using NHibernate.Cfg;

using NUnit.Framework;

namespace Atlanta.Application.Domain.DomainBase.Test
{

    [TestFixture]
    public class DomainPersistenceTestBase : DomainTestBase
    {

        static private Configuration    _configuration;
        static private ISessionFactory  _sessionFactory;

        private ISession        _session;
        private ITransaction    _transaction;

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

        protected ISession Session
        {
            get
            {
                if (_session == null)
                {
                    _session = SessionFactory.OpenSession();
                    DomainRegistry.Session = _session;

                    _transaction = _session.BeginTransaction();
                }

                return _session;
            }
        }

        override public void TearDown()
        {
            base.TearDown();
            
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            if (_session != null)
            {
                _session.Close();
                _session = null;
            }

            DomainRegistry.Session = null;
        }

    }

}

