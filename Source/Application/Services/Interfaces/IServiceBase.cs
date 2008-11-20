
using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Services.Interfaces
{

    /// <summary>
    /// Interface common to all services
    /// </summary>
    public interface IServiceBase
    {

        /// <summary>
        ///  Accessor for the current Repository (NHibernate Session wrapper)
        /// </summary>
        IRepository Repository { get; set; }

    }
}
