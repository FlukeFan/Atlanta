
using NHibernate.Criterion;
using NHibernate.LambdaExtensions;

using NUnit.Framework;

namespace Atlanta.Application.Domain.DomainBase.Test
{

    [TestFixture]
    public class TestClientQueryConverter : DomainTestBase
    {

        [Test]
        public void TestMapping()
        {
            QueryClassRelation relation = new QueryClassRelation().SetId(3);

            ClientQuery clientQuery =
                ClientQuery.For<QueryClass>()
                    .Add((QueryClass q) => q.Name == "test name")
                    .Add((QueryClass q) => q.Type == QueryClassType.First)
                    .Add((QueryClass q) => q.RelatedTo == relation);

            DetachedCriteria criteria =
                DetachedCriteria.For<QueryClass>()
                    .Add((QueryClass q) => q.Name == "test name")
                    .Add((QueryClass q) => q.Type == QueryClassType.First)
                    .Add((QueryClass q) => q.RelatedTo == relation);

            DetachedCriteria convertedCriteria = ClientQueryConverter.ToDetachedCriteria(clientQuery);

            Assert.AreEqual(criteria.CriteriaClass, convertedCriteria.CriteriaClass);
            Assert.AreEqual(criteria.ToString(), convertedCriteria.ToString());
        }

    }

}
