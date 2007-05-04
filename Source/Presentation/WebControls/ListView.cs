
using System.Collections.Generic;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Atlanta.Presentation.WebControls
{

    /// <summary>
    ///  ListView server control
    /// </summary>
    public class ListView : WebControl
    {

        private IList<string>   _columnTexts = new List<string>();

        private void RenderListHeaders(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewColumnTable");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewColumnRow");
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    for (int i=0; i<_columnTexts.Count; i++)
                    {
                        string columnText = _columnTexts[i];

                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewColumnText");
                        writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, null);
                        writer.AddAttribute(HtmlTextWriterAttribute.Title, columnText);
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write(columnText);
                        writer.RenderEndTag();
                    }
                writer.RenderEndTag();

            writer.RenderEndTag();
        }

        /// <summary>
        /// Renders the control
        /// </summary>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            RenderListHeaders(writer);
        }

        /// <summary>
        /// Saves the control's view state
        /// </summary>
        protected override object SaveViewState()
        {
            object[] state = new object[1];

            state[0] = _columnTexts;

            return state;
        }

        /// <summary>
        /// Loads the control's stored view state
        /// </summary>
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                object[] state = (object []) savedState;

                _columnTexts = (IList<string>) state[0];
            }
        }

        /// <summary>
        ///  Add a column based on a percentage of the width of the control
        /// </summary>
        public void AddPercentageColumn(string  columnText,
                                        int     percentageWidth)
        {
            _columnTexts.Add(columnText);
        }

        /// <summary>
        ///  Add a column that takes up the remaining space of the control.
        /// </summary>
        public void AddRemainderColumn(string columnText)
        {
            _columnTexts.Add(columnText);
        }

    }
}


