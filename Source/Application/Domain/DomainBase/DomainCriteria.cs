
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using NHibernate;
using NHibernate.Criterion;

namespace Atlanta.Application.Domain.DomainBase
{

    /// <summary>
    /// Class for domain criteria (simple detached criteria useable by a thin client)
    /// </summary>
    [Serializable]
    public class DomainCriteria
    {

        private string _type;
        private List<ICriterion> _criterionList = new List<ICriterion>();

        /// <summary>
        /// Create a criteria for the given persistent type
        /// </summary>
        public DomainCriteria(Type persistentType)
        {
            _type = persistentType.FullName;
        }

        internal ICriteria ToExecutableCriteria()
        {
            Type type = Type.GetType(_type);
            ICriteria criteria = DomainRegistry.Session.CreateCriteria(type);

            foreach (ICriterion criterion in _criterionList)
            {
                criteria.Add(criterion);
            }

            return criteria;
        }

        /// <summary>
        /// Add the criterion to the DomainCriteria
        /// </summary>
        public DomainCriteria Add(ICriterion criterion)
        {
            _criterionList.Add(criterion);
            return this;
        }

    }

}

