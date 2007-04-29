
namespace Atlanta.Presentation.PresentationBase
{

    /// <summary>
    ///  Controller base class
    /// </summary>
    public class ControllerBase
    {

        /// <summary>
        ///  OnInit - where dynamic controls are instatiated, and events are wired up
        /// </summary>
        virtual public void OnInit()
        {
        }

        /// <summary>
        ///  OnLoad - where the initial presentation of the view is handled
        /// </summary>
        virtual public void OnLoad()
        {
        }

        /// <summary>
        ///  OnRender - where preparation for rendering of the view is handled
        /// </summary>
        virtual public void OnRender()
        {
        }

    }

}
