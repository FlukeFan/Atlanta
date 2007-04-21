
using System;
using System.Reflection;
using System.Xml;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Common
{
    /// <summary>
    /// Class to convert objects to a string representation
    /// </summary>
    public class XmlConverter
    {
        /// <summary>
        /// Convert the specified object.
        /// </summary>    
        /// <remarks>    
        /// Reflection based implementation.
        /// </remarks>    
        public static XmlDocument Convert(object objectToConvert)
        {
            XmlDocument xmlDoc = new XmlDocument();
        
            if (IsTypeXmlConvertable(objectToConvert.GetType()))
            {        
                xmlDoc.AppendChild(Convert(objectToConvert, xmlDoc));
            }
            
            return xmlDoc;    
        }
        
        private static XmlNode Convert(object objectToConvert, XmlDocument xmlDoc)
        {
            XmlElement rootNode = xmlDoc.CreateElement(objectToConvert.GetType().ToString());                        

            PropertyInfo[] propertyInfo = objectToConvert.GetType().GetProperties();            
            for(int i=0; i<propertyInfo.Length; i++)
            {
                if (IsPropertyXmlVisible(propertyInfo[i]))
                {                                    
                    XmlNode propertyNode = xmlDoc.CreateElement(propertyInfo[i].Name);
                    rootNode.AppendChild(propertyNode);

                    if (propertyInfo[i].GetValue(objectToConvert, null) != null)
                    {
                        if (IsTypeXmlConvertable(propertyInfo[i].PropertyType))
                        {                                                        
                            propertyNode.AppendChild(Convert(propertyInfo[i].GetValue(objectToConvert, null), xmlDoc));
                        }
                        else
                        {
                            XmlText propertyValueText = xmlDoc.CreateTextNode(propertyInfo[i].GetValue(objectToConvert, null).ToString());
                            propertyNode.AppendChild(propertyValueText);
                        }
                    }
                }                        
            }
            
            return rootNode;
        }
        
        
        
        private static bool IsTypeXmlConvertable(Type toCheck)
        {
            bool convertable = false;
        
            object[] attributes = toCheck.GetCustomAttributes(true);
            foreach(object attribute in attributes)
            {
                if (attribute is XmlConvertableAttribute)
                {
                    convertable = ((XmlConvertableAttribute)attribute).Value; 
                }
            }
            
            return convertable;
        }    
        
        private static bool IsPropertyXmlVisible(PropertyInfo toCheck)
        {
            bool visible = true;
        
            //object[] attributes = toCheck.GetCustomAttributes(typeof(XmlVisibleAttribute), false);
            //foreach(object attribute in attributes)
            //{
            //    if (attribute is XmlVisibleAttribute)
            //    {
            //        visible = ((XmlVisibleAttribute)attribute).Value; 
            //    }
            //}            

            return visible;       
        }        
    }
}    