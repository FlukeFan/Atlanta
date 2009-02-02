
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Atlanta.Application.Domain.DomainBase
{

    /// <summary>
    /// Extension class to allow graphing of objects using fluent syntax
    /// </summary>
    public static class GraphExtensions
    {

        /// <summary>
        /// Create a Graph&lt;T&gt; for an object"/>
        /// </summary>
        public static Graph<T> Graph<T>(this T source)
        {
            return new Graph<T>(source);
        }

    }

    /// <summary>
    /// interface to allow non-generic use of Graph
    /// </summary>
    public interface IGraph
    {
        /// <summary> Set the source object for this graph </summary>
        void SetSource(object source);

        /// <summary> Make a copy of the source object </summary>
        object Copy();
    }

    /// <summary>
    /// Class to allow creation of copies of a graph of objects without circular references
    /// </summary>
    public class Graph<T> : IGraph
    {

        private T _source;
        private IDictionary<MemberInfo, IGraph> _subGraphs = new Dictionary<MemberInfo, IGraph>();

        private Graph() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public Graph(T source)
        {
            _source = source;
        }

        /// <summary>
        /// Return a copy of the selected graph of objects
        /// </summary>
        public T Copy()
        {
            T target = CreateBlankCopy();
            CopyValues(target);
            CopySubGraphs(target);

            return target;
        }

        object IGraph.Copy()
        {
            return Copy();
        }

        void IGraph.SetSource(object source)
        {
            _source = (T)source;
        }

        /// <summary>
        /// Add a copy of the selected property to the graph
        /// </summary>
        public Graph<T> Add<U>(Expression<Func<T, U>> property)
        {
            _subGraphs.Add(FindMember(property.Body), new Graph<U>());
            return this;
        }

        private MemberInfo FindMember(Expression expression)
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

            return me.Member;
        }

        private void CopySubGraphs(T target)
        {
            foreach (MemberInfo member in _subGraphs.Keys)
            {
                IGraph graph = _subGraphs[member];
                if (member is PropertyInfo)
                {
                    PropertyInfo property = (PropertyInfo)member;
                    graph.SetSource(property.GetValue(_source, null));
                    property.SetValue(target, graph.Copy(), null);
                }
            }
        }

        private void CopyValues(T target)
        {
            Type targetType = typeof(T);
            while (targetType != null)
            {
                foreach (FieldInfo field in targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (field.FieldType.IsValueType
                        || typeof(string).IsAssignableFrom(field.FieldType))
                    {
                        field.SetValue(target, field.GetValue(_source));
                    }
                }
                targetType = targetType.BaseType;
            }
        }

        private T CreateBlankCopy()
        {
            ConstructorInfo constructor =
                typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                                        null, new Type[] {}, null);

            object copy = (T)constructor.Invoke(null);
            return (T)copy;
        }

    }

}
