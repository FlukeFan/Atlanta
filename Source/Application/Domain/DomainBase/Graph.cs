
using System;
using System.Collections.Generic;
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
    /// Class to allow creation of copies of a graph of objects without circular references
    /// </summary>
    public class Graph<T>
    {

        private T _source;

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

            return target;
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
