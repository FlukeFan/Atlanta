
using System;
using System.Reflection;
using System.Text;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Common
{
    /// <summary>
    /// Class to representing a custom attribute to indicate whether a property is included in Xml conversion functionality
    /// </summary>
    public class XmlConvertableAttribute : BooleanAttributeBase
    {
        /// <summary>
        /// Constructor
        /// </summary>    
        public XmlConvertableAttribute()
            : this(true)
        {
        }    
    
        /// <summary>
        /// Constructor
        /// </summary>    
        public XmlConvertableAttribute(bool value)
            : base(value)
        {
        }
    }
}    