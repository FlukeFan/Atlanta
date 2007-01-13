
using System;

namespace Atlanta.Application.Domain.DomainBase
{


    /// <summary>
    /// Base class for domain objects
    /// </summary>
    public class DomainObject
    {

        private long _id;

        /// <summary> Id </summary>
        virtual public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }

}

