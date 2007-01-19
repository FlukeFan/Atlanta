
using System;

namespace Atlanta.Application.Domain.DomainBase
{

    /// <summary>
    /// Exception caused by duplication of data (e.g., name not unique)
    /// </summary>
    [Serializable]
    public class DuplicationException : Exception
    {

        private DomainObjectBase _duplicate;

        /// <summary>
        /// Constructor
        /// </summary>
        public DuplicationException(DomainObjectBase duplicate)
        {
            _duplicate = duplicate;
        }

        /// <summary>
        /// The existing duplicate domain object (specific to the field and context of the exception)
        /// </summary>
        public DomainObjectBase Duplicate
        {
            get { return _duplicate; }
            set { _duplicate = value; }
        }

    }
}


