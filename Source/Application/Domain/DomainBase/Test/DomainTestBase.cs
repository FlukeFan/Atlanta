
using System;

using NHibernate;
using NHibernate.Cfg;

using NUnit.Framework;

namespace Atlanta.Application.Domain.DomainBase.Test
{

    [TestFixture]
    public class DomainTestBase
    {

        static private Configuration    s_configuration;
        static private ISessionFactory  s_sessionFactory;

        private ISession m_session;
        private ITransaction m_transaction;

        private ISessionFactory SessionFactory
        {
            get
            {
                if (s_sessionFactory == null)
                {
                    s_configuration = new Configuration();
                    s_configuration.AddAssembly("Atlanta.Application.Domain");

                    s_sessionFactory = s_configuration.BuildSessionFactory();
                }

                return s_sessionFactory;
            }
        }

        protected ISession Session
        {
            get
            {
                if (m_session == null)
                {
                    m_session = SessionFactory.OpenSession();
                    m_transaction = m_session.BeginTransaction();
                }

                return m_session;
            }
        }

        [SetUp]
        virtual public void SetUp()
        {
        }

        [TearDown]
        virtual public void TearDown()
        {
            if (m_transaction != null)
            {
                m_transaction.Rollback();
                m_transaction = null;
            }

            if (m_session != null)
            {
                m_session.Close();
                m_session = null;
            }
        }

    }

}

