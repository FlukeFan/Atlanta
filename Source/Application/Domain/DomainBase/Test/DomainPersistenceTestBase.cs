
using System;

using NHibernate;
using NHibernate.Cfg;

using NUnit.Framework;

namespace Atlanta.Application.Domain.DomainBase.Test
{

    [TestFixture]
    public class DomainPersistenceTestBase : DomainTestBase
    {

        private Repository _repository;

        protected Repository Repository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new Repository(Repository.SessionFactory).BeginTransaction();
                    DomainRegistry.Repository = _repository;
                }

                return _repository;
            }
        }

        public override void SetUp()
        {
            base.SetUp();
            DomainRegistry.Repository = Repository;
        }

        override public void TearDown()
        {
            base.TearDown();

            if (_repository != null)
            {
                _repository.Dispose();
                _repository = null;
            }

            DomainRegistry.Repository = null;
        }

    }

}

