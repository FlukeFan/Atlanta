
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;

using Atlanta.Application.Domain.Lender;

namespace Atlanta.Application.Domain.DomainBase
{

    /// <summary>
    /// Serialisable class for client query criteria
    /// </summary>
    [DataContract]
    [KnownType(typeof(MediaType))]
    public class ClientQuery
    {

        /// <summary> Constructor </summary>
        public ClientQuery()
        {
            Expressions = new List<ClientQueryExpression>();
        }

        /// <summary> Target class of query </summary>
        [DataMember]
        public string ForClass { get; protected set; }

        /// <summary> List of expressions </summary>
        [DataMember]
        public IList<ClientQueryExpression> Expressions { get; protected set; }

        /// <summary>
        /// Create a query for the given type
        /// </summary>
        public static ClientQuery For<T>()
        {
            return new ClientQuery() { ForClass = typeof(T).FullName };
        }

        /// <summary>
        /// Add a lambda expression to the query
        /// </summary>
        public ClientQuery Add<T>(Expression<Func<T, bool>> expression)
        {
            Expressions.Add(ClientQueryExpression.For(expression));
            return this;
        }

    }
}
