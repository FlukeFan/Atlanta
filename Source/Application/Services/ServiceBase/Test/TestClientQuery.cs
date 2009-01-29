
using System.Linq.Expressions;

using NUnit.Framework;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.DomainBase.Test;

namespace Atlanta.Application.Services.ServiceBase.Test
{

    public enum QueryClassType
    {
        First = 1,
    }

    public class QueryClass
    {
        public string Name { get; set; }
        public QueryClassType Type { get; set; }
        public QueryClassRelation RelatedTo { get; set; }
    }

    public class QueryClassRelation : DomainObjectBase
    {
        public QueryClassRelation SetId(int id) { Id = id; return this; }
    }

    [TestFixture]
    public class TestClientQuery : DomainTestBase
    {

        [Test]
        public void TestCreateQuery()
        {
            QueryClassRelation relation = new QueryClassRelation().SetId(3);

            ClientQuery query =
                ClientQuery.For<QueryClass>()
                    .Add((QueryClass q) => q.Name == "test name")
                    .Add((QueryClass q) => q.Type == QueryClassType.First)
                    .Add((QueryClass q) => q.RelatedTo == relation);

            Assert.AreEqual("Atlanta.Application.Services.ServiceBase.Test.QueryClass", query.ForClass);
            Assert.AreEqual(3, query.Expressions.Count);

            Assert.AreEqual("Name", query.Expressions[0].Property);
            Assert.AreEqual(ExpressionType.Equal, query.Expressions[0].Operator);
            Assert.AreEqual("test name", query.Expressions[0].Operand);

            Assert.AreEqual("Type", query.Expressions[1].Property);
            Assert.AreEqual(ExpressionType.Equal, query.Expressions[1].Operator);
            Assert.AreEqual(QueryClassType.First, query.Expressions[1].Operand);

            Assert.AreEqual("RelatedTo", query.Expressions[2].Property);
            Assert.AreEqual(ExpressionType.Equal, query.Expressions[2].Operator);
            Assert.AreEqual(3, ((QueryClassRelation)query.Expressions[2].Operand).Id);
        }

    }

}

