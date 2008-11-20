
using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Services.ServiceBase
{

    /// <summary>
    ///  Base class for services
    /// </summary>
    public class ServiceObjectBase
    {

        private IRepository _repository;

        /// <summary>
        ///  The NHibernate session for the service call
        /// </summary>
        public IRepository Repository
        {
            get { return _repository; }
            set { _repository = value; }
        }

    }

}

