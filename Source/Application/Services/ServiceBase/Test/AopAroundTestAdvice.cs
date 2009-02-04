
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using NHibernate;

using AopAlliance.Intercept;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Services.Interfaces;

namespace Atlanta.Application.Services.ServiceBase.Test
{

    /// <summary>
    ///  Class for applying 'around' AOP advice for testing services
    /// </summary>
    public class AopAroundTestAdvice : IMethodInterceptor
    {

        private Repository _repository;
        private IDictionary<object, object> _visitedObjects = new Dictionary<object, object>();

        private void VerifyNoRealDomainObjects(object source)
        {
            if (source == null)
                return;

            if (_visitedObjects.ContainsKey(source))
                return;

            _visitedObjects.Add(source, null);

            if (source is string)
                return;

            if (_repository.Session.Contains(source))
                throw new ArgumentException("Do not serialise 'real' DomainObjects across services - use .Graph().Copy() instead", source.ToString());

            Type sourceType = source.GetType();

            if (sourceType.Assembly.FullName.StartsWith("NHibernate"))
                throw new ArgumentException("Do not serialise 'real' DomainObjects across services - use .Graph().Copy() instead", source.ToString());

            if (source is IEnumerable)
            {
                foreach (object listItem in (source as IEnumerable))
                    VerifyNoRealDomainObjects(listItem);
            }
            else
            {
                foreach (PropertyInfo property in sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
                    VerifyNoRealDomainObjects(property.GetValue(source, null));
            }
        }

        /// <summary>
        ///  Handle 'around' advice for testing services
        /// </summary>
        public object Invoke(IMethodInvocation invocation)
        {
            object returnValue = null;
            _repository = ServiceTestBase.GetRepository();

            DomainRegistry.Repository = _repository;
            DomainRegistry.Library = null;

            try
            {
                returnValue = invocation.Proceed();

                _repository.Flush();
                _repository.Clear();
            }
            catch (Exception e)
            {
                returnValue = ServiceResult.Error(invocation.Method.ReturnType, e);
            }

            _visitedObjects.Clear();
            VerifyNoRealDomainObjects(returnValue);
            return returnValue;
        }

    }

}

