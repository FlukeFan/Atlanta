
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using NHibernate.Proxy;

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

        private object _source;
        private IDictionary<MemberInfo, IGraph> _subGraphs = new Dictionary<MemberInfo, IGraph>();

        /// <summary> Constructor </summary>
        public Graph() { }

        /// <summary> Constructor </summary>
        public Graph(T source)
        {
            (this as IGraph).SetSource(source);
        }

        /// <summary>
        /// Return a copy of the selected graph of objects
        /// </summary>
        public T Copy()
        {
            return (T)((IGraph)this).Copy();
        }

        /// <summary>
        /// Add a copy of the selected property to the graph
        /// </summary>
        public Graph<T> Add<U>(Expression<Func<T, U>> property)
        {
            return Add(property, new Graph<U>());
        }

        /// <summary>
        /// Add a copy of the selected property to the graph using the supplied Graph
        /// </summary>
        public Graph<T> Add<U>(Expression<Func<T, U>> property, IGraph subGraph)
        {
            _subGraphs.Add(FindMember(property.Body), subGraph);
            return this;
        }

        object IGraph.Copy()
        {
            if (_source == null)
                return default(T);

            if (_source is IList)
                return CopyList();

            return CopySingleObject(typeof(T), _source);
        }

        void IGraph.SetSource(object source)
        {
            if (source is INHibernateProxy)
            {
                INHibernateProxy proxy = (INHibernateProxy)source;
                _source = proxy.HibernateLazyInitializer.GetImplementation();
            }
            else
            {
                _source = source;
            }
        }

        private object CopySingleObject(Type type, object source)
        {
            object target = Activator.CreateInstance(type, true);
            CopyValues(source, target);
            CopySubGraphs(source, target);
            return target;
        }

        private Type FindListType()
        {
            if (typeof(T).IsGenericType)
                return typeof(T).GetGenericArguments()[0];

            return typeof(T);
        }

        private IList CreateGenericList(Type type)
        {
            return null;
        }

        private object CopyList()
        {
            Type targetType = FindListType();
            IList listCopy = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { targetType }));

            foreach (object source in (IList)_source)
            {
                listCopy.Add(CopySingleObject(targetType, source));
            }

            return listCopy;
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

        private void CopySubGraphs(object source, object target)
        {
            foreach (MemberInfo member in _subGraphs.Keys)
            {
                IGraph graph = _subGraphs[member];
                if (member is PropertyInfo)
                {
                    PropertyInfo property = (PropertyInfo)member;

                    if (property.Name.EndsWith("Enumeration"))
                    {
                        property = property.DeclaringType.GetProperty(property.Name.Substring(0, property.Name.Length - 11),
                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    }

                    graph.SetSource(property.GetValue(source, null));
                    property.SetValue(target, graph.Copy(), null);
                }
            }
        }

        private void CopyValues(object source, object target)
        {
            Type targetType = target.GetType();
            while (targetType != null)
            {
                foreach (FieldInfo field in targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (field.FieldType.IsValueType
                        || typeof(string).IsAssignableFrom(field.FieldType))
                    {
                        field.SetValue(target, field.GetValue(source));
                    }
                }
                targetType = targetType.BaseType;
            }
        }

    }

}
