
using System;

namespace Atlanta.Application.Domain.DomainBase
{

    /// <summary>
    /// Exception caused by duplication of data (e.g., name not unique)
    /// </summary>
    [Serializable]
    public class DuplicationException : Exception
    {

        private DuplicationException(string message) : base(message) { }

        /// <summary> Constructor </summary>
        public DuplicationException(DomainObjectBase duplicate, string duplicateValue)
        {
            DuplicateId = duplicate.Id;
            DuplicateValue = duplicateValue;
        }

        /// <summary> The duplicate object's Id  </summary>
        public long DuplicateId { get; protected set; }

        /// <summary> The duplicate object's value  </summary>
        public string DuplicateValue { get; protected set; }

    }
}


