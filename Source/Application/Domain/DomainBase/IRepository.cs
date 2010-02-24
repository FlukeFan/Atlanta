using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using NHibernate;
using NHibernate.Criterion;

namespace Atlanta.Application.Domain.DomainBase
{

    /// <summary>
    /// Repository interface wraps DB access through the NHibernate ISession
    /// </summary>
    public interface IRepository
    {

        /// <summary>
        /// Create a repository query (NHibernate ICriteria)
        /// </summary>
        /// <typeparam name="T">The type of object to query the repository for</typeparam>
        /// <returns>NHibernate ICriteria</returns>
        ICriteria CreateQuery<T>();

        /// <summary>
        /// Create a query (NHibernate ICriteria)
        /// </summary>
        /// <param name="detachedCriteria">Detached criteria query</param>
        /// <returns>NHibernate ICriteria</returns>
        ICriteria CreateQuery(DetachedCriteria detachedCriteria);

        /// <summary>
        /// Create a query to count the number of rows (clears orders and paging and adds a RowCount projection).
        /// </summary>
        /// <param name="detachedCriteria">Detached criteria query</param>
        /// <returns>NHibernate ICriteria</returns>
        ICriteria CreateCountQuery(DetachedCriteria detachedCriteria);

        /// <summary>
        /// Create a query to count the number of rows (clears orders and paging and adds a RowCount projection).
        /// </summary>
        /// <param name="criteria">ICriteria query</param>
        /// <returns>NHibernate ICriteria</returns>
        ICriteria CreateCountQuery(ICriteria criteria);

        /// <summary>
        /// Load an existing object from the Repository
        /// </summary>
        /// <typeparam name="T">The type of object to load</typeparam>
        /// <param name="identifier">The unique identifier for the existing object</param>
        /// <returns></returns>
        T Load<T>(long identifier);

        /// <summary>
        /// Add (insert) a new instance into the repository
        /// </summary>
        void Add<T>(T newObject);

        /// <summary>
        /// Remove (delete) an instance from the repository
        /// </summary>
        void Remove<T>(T existingObject);

        /// <summary>
        /// Flush any pending changes to the repository
        /// </summary>
        void Flush();

    }

}
