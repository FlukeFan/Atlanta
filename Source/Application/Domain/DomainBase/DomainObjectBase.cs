
using System;

using Atlanta.Application.Domain.Common;

namespace Atlanta.Application.Domain.DomainBase
{


    /// <summary>
    /// Base class for domain objects
    /// </summary>
    [Serializable, StringConvertable, XmlConvertable]    
    abstract public class DomainObjectBase
    {

        private long _id;

        /// <summary> Id </summary>
        [StringVisible(true)]        
        virtual public long Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

    }

}

