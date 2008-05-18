
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
            string convertedObject = objectToConvert.ToString();
        
            if (IsTypeStringConvertable(objectToConvert.GetType()))
            {        
                StringBuilder convertedObjectBuilder = new StringBuilder();

                PropertyInfo[] propertyInfo = objectToConvert.GetType().GetProperties();
                Array.Sort<PropertyInfo>(propertyInfo, delegate(PropertyInfo pi1, PropertyInfo pi2) { return pi1.Name.CompareTo(pi2.Name); });
                bool firstProperty = true;
                for(int i=0; i<propertyInfo.Length; i++)
                {
                    if (IsPropertyStringVisible(propertyInfo[i]))
                    {            
                        string currentPropertyName = propertyInfo[i].Name;
                        string currentPropertyStringValue = "null";

                        if (propertyInfo[i].GetValue(objectToConvert, null) != null)
                        {
                            if (IsTypeStringConvertable(propertyInfo[i].PropertyType))
                            {
                                currentPropertyStringValue = Convert(propertyInfo[i].GetValue(objectToConvert, null));
                            }                    
                            else
                            {
                                currentPropertyStringValue = propertyInfo[i].GetValue(objectToConvert, null).ToString();                
                            }
                        }

                        if (!firstProperty)
                        {
                            convertedObjectBuilder.Append(",");
                        }
                        convertedObjectBuilder.Append(String.Format("{0}=<{1}>", currentPropertyName, currentPropertyStringValue));
                        firstProperty = false;
                    }                    
                }
                   
                convertedObject = convertedObjectBuilder.ToString();                   
                  
            }      
           
            return convertedObject;
        } 
        
        private static bool IsTypeStringConvertable(Type toCheck)
        {
            bool convertable = false;
        
            object[] attributes = toCheck.GetCustomAttributes(true);
            foreach(object attribute in attributes)
            {
                if (attribute is StringConvertableAttribute)
                {
                    convertable = ((StringConvertableAttribute)attribute).Value; 
                }
            }
            
            return convertable;
        }
        
        private static bool IsPropertyStringVisible(PropertyInfo toCheck)
        {
            bool visible = true;
        
            object[] attributes = toCheck.GetCustomAttributes(typeof(StringVisibleAttribute), false);
            foreach(object attribute in attributes)
            {
                if (attribute is StringVisibleAttribute)
                {
                    visible = ((StringVisibleAttribute)attribute).Value; 
                }
            }            

            return visible;       
        }
    }
}