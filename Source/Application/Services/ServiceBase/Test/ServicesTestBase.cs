
using System;

using NHibernate;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Services.ServiceBase.Test
{

    [TestFixture]
    public class ServiceTestBase : DomainPersistenceTestBase
    {

        static private ISession _session;

        static public ISession GetSession()
        {
            return _session;
        }

        override public void SetUp()
        {
            base.SetUp();

            // create a session for the service calls
            _session = Repository.Session;
        }

    }

}

