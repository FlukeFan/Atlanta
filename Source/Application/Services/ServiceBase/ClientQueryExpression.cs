
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;

namespace Atlanta.Application.Services.ServiceBase
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
        public ExpressionType Operator { get; set; }

        /// <summary> Target class of query </summary>
        [DataMember]
        public object Operand { get; set; }

        /// <summary> Create a ClientQueryExpression for the supplied lambda expression </summary>
        public static ClientQueryExpression For<T>(Expression<Func<T, bool>> expression)
        {
            BinaryExpression be = (BinaryExpression)expression.Body;
            ClientQueryExpression queryExpression = new ClientQueryExpression();
            queryExpression.Property = FindMember(be.Left);
            Type propertyType = FindType(be.Left);

            queryExpression.Operator = be.NodeType;

            var valueExpression = System.Linq.Expressions.Expression.Lambda(be.Right).Compile();
            object value = valueExpression.DynamicInvoke();
            queryExpression.Operand = ConvertType(value, propertyType);

            return queryExpression;
        }

        private static string FindMember(Expression expression)
        {
            MemberExpression me = null;
            if (expression is MemberExpression)
                me = (MemberExpression)expression;

            if (expression is UnaryExpression)
            {
                UnaryExpression unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception("Cannot interpret member from " + expression.ToString());

                me = (MemberExpression)unaryExpression.Operand;
            }

            if (me == null)
                throw new Exception("Could not determine member from " + expression.ToString());

            string member = me.Member.Name;

            while (me.Expression.NodeType == ExpressionType.MemberAccess)
            {
                me = (MemberExpression)me.Expression;
                member = me.Member.Name + "." + member;
            }

            return member;
        }

        private static Type FindType(Expression expression)
        {
            MemberExpression me = null;
            if (expression is MemberExpression)
                me = (MemberExpression)expression;

            if (expression is UnaryExpression)
            {
                UnaryExpression unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception("Cannot interpret member from " + expression.ToString());

                me = (MemberExpression)unaryExpression.Operand;
            }

            if (me == null)
                throw new Exception("Could not determine member type from " + expression.ToString());

            return me.Type;
        }

        private static object ConvertType(object value, Type type)
        {
            if (type.IsAssignableFrom(value.GetType()))
                return value;

            if (type.IsEnum)
                return Enum.ToObject(type, value);

            throw new Exception("Cannot convert '" + value.ToString() + "' to " + type.ToString());
        }

    }

}
