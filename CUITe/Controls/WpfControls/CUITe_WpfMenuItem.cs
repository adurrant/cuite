﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace CUITe.Controls.WpfControls
{
    /// <summary>
    /// Wrapper class for WpfMenuItem
    /// </summary>
    public class CUITe_WpfMenuItem : CUITe_WpfControl<WpfMenuItem>
    {
        public CUITe_WpfMenuItem() : base() { }
        public CUITe_WpfMenuItem(string sSearchParameters) : base(sSearchParameters) { }

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