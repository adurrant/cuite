﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting;
using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.WinControls;

namespace CUITe.Controls.WinControls
{
    /// <summary>
    /// Wrapper class for WinMenu
    /// </summary>
    public class WinMenu : WinControl<CUITControls.WinMenu>
    {
        public WinMenu() : base() { }
        public WinMenu(string searchParameters) : base(searchParameters) { }

        public UITestControlCollection Items
        {
            get { return this.UnWrap().Items; }
        }

        public List<WinMenuItem> ItemsAsCUITe
        {
            get
            {
                List<WinMenuItem> list = new List<WinMenuItem>();
                foreach (CUITControls.WinMenuItem item in this.UnWrap().Items)
                {
                    WinMenuItem cuiteItem = new WinMenuItem();
                    cuiteItem.WrapReady(item);
                    list.Add(cuiteItem);
                }
                return list;
            }
        }

        public List<string> ItemsAsList
        {
            get { return (from x in this.UnWrap().Items select ((CUITControls.WinMenuItem)x).DisplayText).ToList<string>(); }
        }
    }
}