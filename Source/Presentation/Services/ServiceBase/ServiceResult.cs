
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Threading;

namespace Atlanta.Application.Services.ServiceBase
{

    public delegate void ServiceCallback(ServiceCallStatus status);

    public class ServiceCallStatus
    {
        public object State { get; set; }
        public ServiceCallback Callback { get; set; }
        public IAsyncResult AsyncResult { get; set; }

        public static void InvokeResultOnUiThread(  Dispatcher          dispatcher,
                                                    ServiceCallStatus   serviceCallStatus)
        {
            if (dispatcher == null)
            {
                serviceCallStatus.Callback(serviceCallStatus);
            }
            else
            {
                dispatcher.BeginInvoke(serviceCallStatus.Callback, new object[] { serviceCallStatus });
            }
        }
    }

    [DataContract]
    public class ServiceResult<T> : ServiceResult
    {
        [DataMember]
        public T Result { get; set; }
    }

    [DataContract]
    public class ServiceResult
    {
        [DataMember] public bool IsVoid { get; set; }
        [DataMember] public bool IsError { get; set; }
        [DataMember] public string ExceptionMessage { get; set; }
        [DataMember] public string ExceptionClass { get; set; }
        [DataMember] public IDictionary<string, object> Properties { get; set; }

        public void ProcessException()
        {
            if (!IsError)
                return;

            Type exceptionType = Type.GetType(ExceptionClass);

            if (exceptionType == null)
                throw new Exception("Unrecognised exception type (" + ExceptionClass + ")\r\n" + ExceptionMessage);

            ConstructorInfo messageConstructor = exceptionType.GetConstructor(new Type[] { typeof(string) });
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

