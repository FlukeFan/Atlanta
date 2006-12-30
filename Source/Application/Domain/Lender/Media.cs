
using System;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{


    /// <summary>
    /// Class to represent Media
    /// </summary>
    public class Media : DomainObject
    {

        private string m_name;
        private string m_description;

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

