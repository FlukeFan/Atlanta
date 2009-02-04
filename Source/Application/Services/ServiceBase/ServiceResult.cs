using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Atlanta.Application.Services.ServiceBase
{

    /// <summary>
    /// Generic class for returning results from services (or wrapping an exception otherwise)
    /// </summary>
    [DataContract]
    public class ServiceResult<T> : ServiceResult
    {

        private T _result;

        /// <summary> protected constructor </summary>
        protected ServiceResult() { }

        /// <summary> return value </summary>
        [DataMember]
        public T Result
        {
            get
            {
                return _result;
            }
            protected set
            {
                _result = value;
            }
        }

        /// <summary> Create a return value </summary>
        public static ServiceResult<T> Return(T result)
        {
            ServiceResult<T> serviceResult = new ServiceResult<T>();
            serviceResult.Result = result;
            return serviceResult;
        }

    }

    /// <summary>
    /// Wraps a return value from a service call, or an exception thrown from one.
    /// </summary>
    [DataContract]
    public class ServiceResult
    {

        /// <summary> Protected constructor </summary>
        protected ServiceResult()
        {
        }

        /// <summary> is return value void </summary>
        [DataMember] public bool IsVoid { get; protected set; }

        /// <summary> is return value an error </summary>
        [DataMember] public bool IsError { get; protected set; }

        /// <summary> full detail of exception </summary>
        [DataMember] public string ExceptionDetail { get; protected set; }

        /// <summary> message to pass to the constructor of the exception </summary>
        [DataMember] public string ExceptionMessage { get; protected set; }

        /// <summary> class name of exception </summary>
        [DataMember] public string ExceptionClass { get; protected set; }

        /// <summary> dictionary of properties of the exception </summary>
        [DataMember] public IDictionary<string, object> Properties { get; protected set; }

        /// <summary>
        /// Void service return value
        /// </summary>
        public static ServiceResult Void
        {
            get
            {
                ServiceResult serviceResult = new ServiceResult();
                serviceResult.IsVoid = true;
                return serviceResult;
            }
        }

        /// <summary>
        /// Wrap an exception to be returned from a service call
        /// </summary>
        public static ServiceResult Error(Type resultType, Exception exception)
        {
            ServiceResult serviceResult = (ServiceResult)Activator.CreateInstance(resultType, true);
            serviceResult.IsError = true;
            serviceResult.ExceptionMessage = exception.Message;
            serviceResult.ExceptionDetail = exception.ToString();
            serviceResult.ExceptionClass = exception.GetType().FullName;
            serviceResult.Properties = new Dictionary<string, object>();

            foreach (PropertyInfo property in exception.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (property.CanRead && property.CanWrite)
                {
                    serviceResult.Properties.Add(property.Name, property.GetValue(exception, null));
                }
            }

            return serviceResult;
        }

        /// <summary>
        /// Process an exception is there is one
        /// </summary>
        public void ProcessException()
        {
            if (!IsError)
                return;

            Type exceptionType = null;
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (exceptionType == null)
                {
                    exceptionType = assembly.GetType(ExceptionClass);
                }
            }

            if (exceptionType == null)
                throw new Exception("Unrecognised exception type (" + ExceptionClass + ")\r\n" + ExceptionMessage);

            ConstructorInfo messageConstructor = exceptionType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null,
                                                                    new Type[] { typeof(string) }, null);
            if (messageConstructor == null)
                throw new Exception("No valid constructor taking a string for (" + ExceptionClass + ")\r\n" + ExceptionMessage);

            Exception exception = (Exception) messageConstructor.Invoke(new object[] { ExceptionMessage });
            foreach (string propertyName in Properties.Keys)
            {
                PropertyInfo property = exceptionType.GetProperty(propertyName);
                if (property == null)
                    throw new Exception("No property (" + propertyName + ") on (" + ExceptionClass + ")\r\n" + ExceptionMessage);

                try
                {
                    property.SetValue(exception, Properties[propertyName], null);
                }
                catch(Exception e)
                {
                    throw new Exception("Error setting property (" + propertyName + ") on (" + ExceptionClass + ")\r\n" + ExceptionMessage, e);
                }
            }

            throw exception;
        }

    }

}
