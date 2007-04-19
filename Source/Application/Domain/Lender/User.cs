
using System;
using System.Collections.Generic;

using Atlanta.Application.Domain.Common;
using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    /// <summary>
    /// Class to represent a User of the application
    /// </summary>
    [Serializable]
    public class User : DomainObjectBase
    {

        private string      _login;

        /// <summary> constructor </summary>
        protected User(string login)
        {
            Login = login;
        }

        /// <summary> factory method </summary>
        public static User InstantiateUser(string login)
        {
            return new User(login);
        }

        /// <summary> Login </summary>
        [StringVisible(true)]
        public virtual string Login
        {
            get { return _login; }
            protected set { _login = value; }
        }

    }

}

