
using System;
using System.Reflection;
using System.Text;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Common
{
    /// <summary>
    /// Class to representing a custom attribute with a boolean value
    /// </summary>
    public abstract class BooleanAttributeBase : Attribute
    {
        bool _value = false;
    
        /// <summary>
        /// Constructor
        /// </summary>    
        protected BooleanAttributeBase(bool value)
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