
using System;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    /// <summary>
    /// MediaType enumeration
    /// </summary>
    public enum MediaType
    {
        /// <summary> Book </summary>
        Book,

        /// <summary> Cd </summary>
        Cd,

        /// <summary> Dvd </summary>
        Dvd,
    }

    /// <summary>
    /// Class to represent Media
    /// </summary>
    public class Media : DomainObject
    {

        private Library     m_library;

        private MediaType   m_type;
        private string      m_name;
        private string      m_description;

        /// <summary> Library </summary>
        virtual public Library Library
        {
            get { return m_library; }
            set { m_library = value; }
        }

        /// <summary> Type </summary>
        virtual public MediaType Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        /// <summary> Name </summary>
        virtual public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary> Description </summary>
        virtual public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

    }

}

