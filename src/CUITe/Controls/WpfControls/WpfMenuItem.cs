﻿using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace CUITe.Controls.WpfControls
{
    /// <summary>
    /// Wrapper class for WpfMenuItem
    /// </summary>
    public class WpfMenuItem : WpfControl<CUITControls.WpfMenuItem>
    {
        public WpfMenuItem() : base() { }
        public WpfMenuItem(string searchParameters) : base(searchParameters) { }

        public bool Checked
        {
            get { return this.UnWrap().Checked; }
            set { this.UnWrap().Checked = value; }
        }

        public bool Expanded
        {
            get { return this.UnWrap().Expanded; }
            set { this.UnWrap().Expanded = value; }
        }

        public bool HasChildNodes
        {
            get { return this.UnWrap().HasChildNodes; }
        }

        public string Header
        {
            get { return this.UnWrap().Header; }
        }

        public bool IsTopLevelMenu
        {
            get { return this.UnWrap().IsTopLevelMenu; }
        }
    }
}