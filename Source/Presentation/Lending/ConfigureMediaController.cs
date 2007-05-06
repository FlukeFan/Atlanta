
using Atlanta.Presentation.PresentationBase;
using Atlanta.Presentation.WebControls;

namespace Atlanta.Presentation.Lending
{


    /// <summary>
    ///  Interface for ConfigureMedia view
    /// </summary>
    public interface IConfigureMediaView : IViewBase
    {

        /// <summary> MediaList </summary>
        ListView MediaList { get; }

    }


    /// <summary>
    ///  Controller for ConfigureMedia view
    /// </summary>
    public class ConfigureMediaController : ControllerBase<IConfigureMediaView>
    {

        /// <summary>
        ///  OnFirstLoad
        /// </summary>
        override public void OnFirstLoad()
        {
        }

    }


}
