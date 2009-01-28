using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;

namespace Atlanta.Application.Domain.DomainBase
{

    /// <summary>
    /// An expression from a client query
    /// </summary>
    [DataContract]
    public class ClientQueryExpression
    {
        /// <summary> Target class of query </summary>
        [DataMember]
        public string Property { get; set; }

        /// <summary> Target class of query </summary>
        [DataMember]
        public string Operator { get; set; }

        /// <summary> Target class of query </summary>
        [DataMember]
        public object Operand { get; set; }
    }

    /// <summary>
    /// Serialisable class for client query criteria
    /// </summary>
    [DataContract]
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
            return this;
        }

    }
}
