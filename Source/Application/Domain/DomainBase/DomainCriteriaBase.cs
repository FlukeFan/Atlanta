
using System;
using System.Collections.Generic;

namespace Atlanta.Application.Domain.DomainBase
{

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

