
namespace Atlanta.Presentation.PresentationBase
{

    /// <summary>
    ///  Controller base class
    /// </summary>
    public abstract class ControllerBase<V>
                                where V : IViewBase
    {

        private V _view;

        /// <summary>
        ///  Access to the view
        /// </summary>
        protected V View
        {
            get { return _view; }
            set { _view = value; }
        }

        /// <summary>
        ///  OnWireUp - where events are wired up
        /// </summary>
        virtual public void OnWireUp()
        {
        }

        /// <summary>
        ///  OnFirstLoad - where the initial presentation of the view is handled
        /// </summary>
        virtual public void OnFirstLoad()
        {
        }

    }

}
