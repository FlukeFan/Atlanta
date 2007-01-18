
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using NHibernate;
using NHibernate.Type;

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

        private List<object>    _queryParameterValues;
        private string          _whereOrAnd;
        private string          _queryString;

        private IList<D> ListFromInMemoryList(IList<D> sourceList)
        {
            DomainList<D> resultList = new DomainList<D>();

            foreach (D domainObject in sourceList)
            {
                if (PassesFilter(domainObject))
                    resultList.Add(domainObject);
            }

            return resultList;
        }

        private IList<D> ListFromPersistentList(IList<D> sourceList)
        {
            DomainList<D> resultList = new DomainList<D>();

            InitialiseQueryParameters();
            CreateQueryParameters();

            IQuery query = Registry.Session.CreateFilter(sourceList, _queryString);
            SetQueryParameters(query);
            IList queryList = query.List();

            foreach (D domainObject in queryList)
            {
                resultList.Add(domainObject);
            }

            return resultList;
        }

        private void InitialiseQueryParameters()
        {
            _queryParameterValues = new List<object>();
            _whereOrAnd = "where";
            _queryString = "";
        }

        private void SetQueryParameters(IQuery query)
        {
            for (int i=0; i<_queryParameterValues.Count; i++)
            {
                query.SetParameter(i, _queryParameterValues[i]);
            }
        }

        private string GetSqlOperator(  string          parameterName,
                                        FilterCondition condition)
        {
            switch (condition)
            {
                case FilterCondition.Equal:
                    return parameterName + " = ?";

                case FilterCondition.NotEqual:
                    return "not " + parameterName + " = ?";

                case FilterCondition.GreaterThan:
                    return parameterName + " > ?";

                case FilterCondition.GreaterThanOrEqual:
                    return parameterName + " >= ?";

                case FilterCondition.LessThan:
                    return parameterName + " < ?";

                case FilterCondition.LessThanOrEqual:
                    return parameterName + " <= ?";

                case FilterCondition.Like:
                    return parameterName + " like ?";

                default:
                    throw new Exception("filter not recognised");
            }
        }

        private void AddToQuery(string          parameterName,
                                FilterCondition parameterCondition)
        {
            string sql = _whereOrAnd + " ( " + GetSqlOperator(parameterName, parameterCondition) + " ) ";
            _whereOrAnd = "and";

            _queryString += sql;
        }

        /// <summary>
        ///  Return true if the supplied domain object passes the filters
        /// </summary>
        abstract protected bool PassesFilter(D domainObject);


        /// <summary>
        ///  Override to create the custom HQL for the criteria
        /// </summary>
        abstract protected void CreateQueryParameters();


        /// <summary> Adds a single filter to a query </summary>
        protected void AddToQuery(  string          parameterName,
                                    FilterCondition parameterCondition,
                                    object          parameterValue)
        {
            AddToQuery(parameterName, parameterCondition);
            _queryParameterValues.Add(parameterValue);
        }

        /// <summary> Returns true if the value passes the filter </summary>
        protected bool CompareStringFilter( string          check,
                                            string          filter,
                                            FilterCondition condition)
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
                throw new Exception("Unsupported filter condition");
            }
        }

        /// <summary> Returns true if the value passes the filter </summary>
        protected bool CompareIntFilter(int             check,
                                        int             filter,
                                        FilterCondition condition)
        {
            if (condition == FilterCondition.Equal)
            {
                return (check == filter);
            }
            else if (condition == FilterCondition.NotEqual)
            {
                return (check != filter);
            }
            else if (condition == FilterCondition.GreaterThan)
            {
                return (check > filter);
            }
            else if (condition == FilterCondition.GreaterThanOrEqual)
            {
                return (check >= filter);
            }
            else if (condition == FilterCondition.LessThan)
            {
                return (check < filter);
            }
            else if (condition == FilterCondition.LessThanOrEqual)
            {
                return (check <= filter);
            }
            else
            {
                throw new Exception("Unsupported filter condition");
            }
        }


        /// <summary>
        ///  Return a list of the objects from the source, with the criteria applied
        /// </summary>
        public IList<D> List(IList<D> sourceList)
        {
            if (sourceList is DomainList<D>)
            {
                return ListFromInMemoryList(sourceList);
            }
            else
            {
                return ListFromPersistentList(sourceList);
            }
        }
    }

}

