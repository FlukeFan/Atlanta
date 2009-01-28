﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlanta.Application.Domain.DomainBase
{

    /// <summary>
    /// Serialisable class for client query criteria
    /// </summary>
    public class ClientQuery
    {

        /// <summary> Target class of query </summary>
        public string ForClass { get; protected set; }

        /// <summary>
        /// Create a query for the given type
        /// </summary>
        public static ClientQuery For<T>()
        {
            return new ClientQuery() { ForClass = typeof(T).FullName };
        }

    }
}
