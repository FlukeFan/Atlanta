
using NUnit.Framework;

namespace Atlanta.Application.Domain.DomainBase.Test
{

    public class QueryClass
    {
        public string Name { get; set; }
    }

    [TestFixture]
    public class TestClientQuery : DomainTestBase
    {

        [Test]
        public void TestCreateQuery()
        {
            ClientQuery query =
                ClientQuery.For<QueryClass>();

            Assert.AreEqual("Atlanta.Application.Domain.DomainBase.Test.QueryClass", query.ForClass);
        }

    }

}

