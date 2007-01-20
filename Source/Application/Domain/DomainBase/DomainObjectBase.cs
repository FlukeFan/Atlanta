
using System;

namespace Atlanta.Application.Domain.DomainBase
{


    /// <summary>
    /// Base class for domain objects
    /// </summary>
    [Serializable]
    abstract public class DomainObjectBase
    {

        private long _id;

        /// <summary> Id </summary>
        virtual public long Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

    }

}

