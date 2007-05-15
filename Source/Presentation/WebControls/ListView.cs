
using System;
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

        private IList<string>       _columnTexts = new List<string>();
        private IList<string>       _columnWidths = new List<string>();
        private IList<string []>    _items = new List<string []>();

        /// <summary>
        ///  Constructor
        /// </summary>
        public ListView() : base(HtmlTextWriterTag.Div)
        {
        }

        private void RenderListColumns(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Colgroup);

                foreach (string columnWidth in _columnWidths)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Width, columnWidth);
                    writer.RenderBeginTag(HtmlTextWriterTag.Col);
                    writer.RenderEndTag();
                }

            writer.RenderEndTag();
        }

        private void RenderListHeader(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewTableHeader");
            writer.RenderBeginTag(HtmlTextWriterTag.Thead);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewHeaderRow");
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    foreach (string columnText in _columnTexts)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewHeaderText");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write(columnText);
                        writer.RenderEndTag();
                    }
                writer.RenderEndTag();

            writer.RenderEndTag();
        }

        private void RenderListBody(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewTableBody");
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);

                foreach (string[] itemTexts in _items)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewBodyRow");
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                        foreach (string itemText in itemTexts)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewBodyText");
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                writer.Write(itemText);
                            writer.RenderEndTag();
                        }
                    writer.RenderEndTag();
                }

            writer.RenderEndTag();
        }

        /// <summary>
        /// Override the attributes on the outer HTML element
        /// </summary>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewOuterDiv");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, Width.ToString());
        }

        /// <summary>
        /// Renders the control
        /// </summary>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewInnerDiv");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Height, Height.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "listViewColumnTable");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);

                    RenderListColumns(writer);
                    RenderListHeader(writer);
                    RenderListBody(writer);

                writer.RenderEndTag();

            writer.RenderEndTag();
        }

        /// <summary>
        /// Saves the control's view state
        /// </summary>
        protected override object SaveViewState()
        {
            object[] state = new object[3];

            state[0] = _columnTexts;
            state[1] = _columnWidths;
            state[2] = _items;

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
                _columnWidths = (IList<string>) state[1];
                _items = (IList<string []>) state[2];
            }
        }
        
        /// <summary>
        ///  The number of items in the list
        /// </summary>
        public int ItemCount
        {
            get { return _items.Count; }
        }

        /// <summary>
        ///  Add a column based on a percentage of the width of the control
        /// </summary>
        public void AddPercentageColumn(string  columnText,
                                        int     percentageWidth)
        {
            _columnTexts.Add(columnText);
            _columnWidths.Add(percentageWidth.ToString() + "%");
        }

        /// <summary>
        ///  Add a column that takes up the remaining space of the control.
        /// </summary>
        public void AddRemainderColumn(string columnText)
        {
            _columnTexts.Add(columnText);
            _columnWidths.Add("*");
        }

        /// <summary>
        /// Adds an item to the end of the list
        /// </summary>
        public void AddListItem(params string[] itemTexts)
        {
            if (itemTexts.Length != _columnTexts.Count)
                throw new Exception("List item does not match list columns");

            _items.Add(itemTexts);
        }

    }
}


