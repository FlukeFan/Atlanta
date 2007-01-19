
using System;
using System.Collections.Generic;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    /// <summary>
    /// Class to represent a Library
    /// </summary>
    public class Library : DomainObjectBase
    {

        private IList<Media> _media = new DomainList<Media>();


        /// <summary> constructor </summary>
        protected Library()
        {
        }


        /// <summary> factory method </summary>
        public static Library InstantiateLibrary()
        {
            return new Library();
        }


        /// <summary> Media collection </summary>
        virtual public IList<Media> Media
        {
            get { return _media; }
            protected set { _media = value; }
        }
    }

}

