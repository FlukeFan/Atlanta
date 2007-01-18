
using System;
using System.Collections.Generic;

namespace Atlanta.Application.Domain.DomainBase
{
    
    /// <summary>
    /// Condition types for filters.
    /// </summary>
    public enum FilterCondition
    {
        /// <summary>Equality condition</summary>
        Equal,
        /// <summary>Inequality condition</summary>
        NotEqual,
        /// <summary>GreaterThan</summary>
        GreaterThan,
        /// <summary>GreaterThan or equal to</summary>
        GreaterThanOrEqual,
        /// <summary>LessThan</summary>
        LessThan,
        /// <summary>LessThan or equal to</summary>
        LessThanOrEqual,
        /// <summary>Like condition</summary>
        Like
    };

    /// <summary>
    /// Base class for domain criteria objects
    /// </summary>
    abstract public class DomainCriteriaBase<T, D>
    {

        /// <summary>
        ///  Return a list of the objects from the source, with the criteria applied
        /// </summary>
        public IList<D> List(IList<D> sourceList)
        {
            return new DomainList<D>();
        }
    }

}

