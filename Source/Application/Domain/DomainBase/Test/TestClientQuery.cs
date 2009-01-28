
using NUnit.Framework;

namespace Atlanta.Application.Domain.DomainBase.Test
{

    public class QueryClass
    {
        public string Name { get; set; }
        public QueryClassRelation RelatedTo { get; set; }
    }

    public class QueryClassRelation : DomainObjectBase
    {
    }

    [TestFixture]
    public class TestClientQuery : DomainTestBase
    {

        [Test] [Ignore("working on client expressions using Lambdas")]
        public void TestCreateQuery()
        {
            ClientQuery query =
                ClientQuery.For<QueryClass>()
                    .Add((QueryClass q) => q.Name == "test name");

            Assert.AreEqual("Atlanta.Application.Domain.DomainBase.Test.QueryClass", query.ForClass);
            Assert.AreEqual(1, query.Expressions.Count);
            Assert.AreEqual("Name", query.Expressions[0].Property);
            Assert.AreEqual("Eq", query.Expressions[0].Operator);
            Assert.AreEqual("test name", query.Expressions[0].Operand);
        }

    }

}

