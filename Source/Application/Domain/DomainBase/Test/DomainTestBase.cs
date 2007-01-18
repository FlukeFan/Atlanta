
using System;

using NHibernate;
using NHibernate.Cfg;

using NUnit.Framework;

namespace Atlanta.Application.Domain.DomainBase.Test
{

    [TestFixture]
    public class DomainTestBase
    {

        [SetUp]
        virtual public void SetUp()
        {
        }

        [TearDown]
        virtual public void TearDown()
        {
        }

    }

}

