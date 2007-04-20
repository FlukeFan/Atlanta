
using NHibernate;

namespace Atlanta.Application.Services.Interfaces
{

    /// <summary>
    /// Interface common to all services
    /// </summary>
    public interface IServiceBase
    {

        /// <summary>
        ///  Accessor for the current NHibernate Session
        /// </summary>
        ISession Session { get; set; }

    }
}
