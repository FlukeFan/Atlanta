
using System;
using System.Reflection;
using System.Text;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Common
{
    /// <summary>
    /// Class to representing a custom attribute to indicate whether a property is included in string conversion functionality
    /// </summary>
    public class StringVisibleAttribute : BooleanAttributeBase
    {
        /// <summary>
        /// Constructor
        /// </summary>    
        public StringVisibleAttribute()
            : this(true)
        {
        }    
    
        /// <summary>
        /// Constructor
        /// </summary>    
        public StringVisibleAttribute(bool value)
            : base(value)
        {
        }
    }
}    