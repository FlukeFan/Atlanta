
using System;
using System.Reflection;
using System.Text;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Common
{
    /// <summary>
    /// Class to representing a custom attribute to indicate whether a property is included in string conversion functionality
    /// </summary>
    public class StringConvertableAttribute : Attribute
    {
        bool _value = false;
    
        /// <summary>
        /// Constructor
        /// </summary>    
        public StringConvertableAttribute()
            : this(true)
        {
        }    
    
        /// <summary>
        /// Constructor
        /// </summary>    
        public StringConvertableAttribute(bool value)
        {
            _value = value;    
        }
        
        /// <summary>
        /// The value.
        /// </summary>            
        public bool Value
        {
            get
            {
                return _value;
            }
        }
    
    }
}    