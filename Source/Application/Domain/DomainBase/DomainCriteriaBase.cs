
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        ///  Return true if the supplied domain object passes the filters
        /// </summary>
        abstract protected bool PassesFilter(D domainObject);


        /// <summary> Returns true if the value passes the filter </summary>
        protected bool CompareStringFilter(string check, string filter, FilterCondition condition)
        {
            if (condition == FilterCondition.Equal)
            {
                if ((check == null) || (filter == null))
                {
                    return (check == filter);
                }

                return (check == filter);
            }
            else if (condition == FilterCondition.NotEqual)
            {
                if ((check == null) || (filter == null))
                {
                    return (check != filter);
                }

                return (check != filter);
            }
            else if (condition == FilterCondition.Like)
            {
                if (check == null)
                {
                    return false;
                }

                string regExString = "^" + filter.Replace("%", "[\\s\\S]*") + "$";
                Regex regEx = new Regex(regExString);
                return regEx.IsMatch(check);
            }
            else
            {
                throw new Exception("Unsupported filter type");
            }
        }


        /// <summary> Returns true if the value passes the filter </summary>
        protected bool CompareIntFilter(int check, int filter, FilterCondition condition)
        {
            return false;
        }


        /// <summary>
        ///  Return a list of the objects from the source, with the criteria applied
        /// </summary>
        public IList<D> List(IList<D> sourceList)
        {
            if (sourceList is DomainList<D>)
            {
                // in-memory list
                DomainList<D> resultList = new DomainList<D>();

                foreach (D domainObject in sourceList)
                {
                    if (PassesFilter(domainObject))
                        resultList.Add(domainObject);
                }

                return resultList;
            }
            else
            {
                throw new Exception("TODO - handle persistent lists");
            }
        }
    }

}

