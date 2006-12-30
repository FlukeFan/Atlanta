
using System;

namespace Atlanta.Application.Domain.DomainBase
{


    /// <summary>
    /// Base class for domain objects
    /// </summary>
    public class DomainObject
    {

        private long m_id;

        /// <summary> Id </summary>
        public long Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

    }

}

