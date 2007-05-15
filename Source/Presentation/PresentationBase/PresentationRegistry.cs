
using System.Configuration;

namespace Atlanta.Presentation.PresentationBase
{


    /// <summary>
    /// Registry class to allow the presentation to find system settings and objects
    /// </summary>
    public class PresentationRegistry
    {

        static private string _webScriptDirectory;

        /// <summary> get/find the WebScriptDirectory </summary>
        static public string WebScriptDirectory
        {
            get
            {
                if (_webScriptDirectory == null)
                {
                    _webScriptDirectory = ConfigurationManager.AppSettings["WebScriptDirectory"];
                }

                return _webScriptDirectory;
            }
            set { _webScriptDirectory = value; }
        }

    }

}

