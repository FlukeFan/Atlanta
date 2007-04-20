
using NHibernate;

namespace Atlanta.Application.Services.ServiceBase
{

    /// <summary>
    ///  Base class for services
    /// </summary>
    public class ServiceObjectBase
    {

        private ISession _session;

        /// <summary>
        ///  The NHibernate session for the service call
        /// </summary>
        public ISession Session
        {
            get { return _session; }
            set { _session = value; }
        }

    }

}

