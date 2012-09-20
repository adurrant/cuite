﻿using System.Threading;
using CUITe.Controls.HtmlControls;

namespace CUITe.Controls.TelerikControls
{
    public class Telerik_ComboBox
    {
        private string id;
        private CUITe_BrowserWindow _window;

        public Telerik_ComboBox(string searchParameters) 
        {
            this.id = searchParameters.Trim().Split('=')[1];
        }

        internal void SetWindow(CUITe_BrowserWindow window) 
        {
            this._window = window;
        }

        public void SelectItemByText(string sText, int milliseconds)
        {
            this._window.RunScript("var obj = window.$find('" + id + "');obj.showDropDown();");
            Thread.Sleep(milliseconds);
            this._window.RunScript("var obj = window.$find('" + id + "');obj.findItemByText('" + sText + "').select();obj.hideDropDown();");
        }
    }
}
