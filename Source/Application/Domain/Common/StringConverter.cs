
using System;
using System.Reflection;
using System.Text;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Common
{
    /// <summary>
    /// Class to convert objects to a string representation
    /// </summary>
    public class StringConverter
    {
        /// <summary>
        /// Convert the specified object.
        /// </summary>    
        /// <remarks>    
        /// Reflection based implementation.
        /// </remarks>    
        public static string Convert(object objectToConvert)
        {
            return Convert(objectToConvert, typeof(DomainObjectBase));
        }
    
        /// <summary>
        /// Convert the specified object.
        /// </summary>    
        /// <remarks>    
        /// Reflection based implementation.
        /// </remarks>    
        public static string Convert(object objectToConvert, Type superclass)
        {
            StringBuilder convertedObject = new StringBuilder();
        
            PropertyInfo[] propertyInfo = objectToConvert.GetType().GetProperties();            
            for(int i=0; i<propertyInfo.Length; i++)
            {
                if (IsPropertyStringVisible(propertyInfo[i]))
                {            
                    string currentPropertyName = propertyInfo[i].Name;
                    string currentPropertyStringValue = "null";

                    if (propertyInfo[i].GetValue(objectToConvert, null) != null)
                    {
                        if (propertyInfo[i].PropertyType.IsSubclassOf(superclass))
                        {
                            currentPropertyStringValue = Convert(propertyInfo[i].GetValue(objectToConvert, null));
                        }                    
                        else
                        {
                            currentPropertyStringValue = propertyInfo[i].GetValue(objectToConvert, null).ToString();                
                        }
                    }

                    convertedObject.Append(String.Format("{0}=<{1}>", currentPropertyName, currentPropertyStringValue));  
                    if (i < propertyInfo.Length - 1)
                    {
                        convertedObject.Append(",");
                    }
                }                    
            }                  
            
            return convertedObject.ToString();
        } 
        
        private static bool IsPropertyStringVisible(PropertyInfo property)
        {
            bool visible = true;
        
            object[] attributes = property.GetCustomAttributes(typeof(StringVisibleAttribute), false);

            if (attributes.Length > 0)
            {
                visible = ((StringVisibleAttribute)attributes[0]).Value; 
            }

            return visible;       
        }
    }
}