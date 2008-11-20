
using System;
using System.Collections.Generic;

using NHibernate;

using Atlanta.Application.Domain.Lender;

namespace Atlanta.Application.Domain.DomainBase
{


    /// <summary>
    /// Registry class to allow the domain to find system settings and objects
    /// </summary>
    public class DomainRegistry
    {

        [ThreadStatic]
        static private ISession _session;

        [ThreadStatic]
        private static IRepository _repository;

        [ThreadStatic]
        static private Library _library;

        /// <summary> thread-local session </summary>
        static public ISession Session
        {
            get { return _session; }
            set { _session = value; }
        }

        /// <summary> thread-local Repository </summary>
        static public IRepository Repository
        {
            get { return _repository; }
            set { _repository = value; }
        }

        /// <summary> get/find the  </summary>
        static public Library Library
        {
            get
            {
                if (_library == null)
                {
                    LoadSingleSystemLibrary();
                }

                return _library;
            }
            set { _library = value; }
        }

        static private void LoadSingleSystemLibrary()
        {
            IList<Library> libraryList =
                Session
                    .CreateCriteria(typeof(Library))
                    .List<Library>();

            // there should be one, and only one library in the system ...
            if (libraryList.Count < 1)
            {
                throw new Exception("no library found in database");
            }

            if (libraryList.Count > 1)
            {
                throw new Exception("more than 1 library found in database");
            }

            _library = libraryList[0];
        }

    }

}

