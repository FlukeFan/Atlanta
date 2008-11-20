
using System;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Services.ServiceBase.Test
{

    [TestFixture]
    public class ServiceTestBase : DomainPersistenceTestBase
    {

        static private Repository _repository;

        static public Repository GetRepository()
        {
            return _repository;
        }

        override public void SetUp()
        {
            base.SetUp();

            // create a session for the service calls
            _repository = Repository;
        }

    }

}

