
using System;
using System.Collections.Generic;

using Atlanta.Application.Domain.Common;
using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    [Serializable]
    public class User : DomainObjectBase
    {

        protected User() { }

        protected User(string login)
        {
            Login = login;
        }

        [StringVisible(true)]
        public virtual string   Login   { get; protected set; }


        public static User InstantiateUser(string login)
        {
            return new User(login);
        }

    }

}

