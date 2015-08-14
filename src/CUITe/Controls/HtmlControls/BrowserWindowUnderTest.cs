﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CUITe.Browsers;
using CUITe.Controls.TelerikControls;
using Microsoft.VisualStudio.TestTools.UITesting;
using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace CUITe.Controls.HtmlControls
{
    /// <summary>
    /// The browser window
    /// </summary>
    public class BrowserWindowUnderTest : BrowserWindow
    {
        public string sWindowTitle;
        private CUITControls.HtmlCustom mSlObjectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserWindowUnderTest"/> class.
        /// </summary>
        public BrowserWindowUnderTest()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserWindowUnderTest"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public BrowserWindowUnderTest(string title)
        {
            SearchProperties[PropertyNames.ClassName] = GetCurrentBrowser().WindowClassName;

            SetWindowTitle(title);
        }

        /// <summary>
        /// Gets the current browser.
        /// </summary>
        /// <returns></returns>
        public static IBrowser GetCurrentBrowser()
        {
            InternetExplorer ie = new InternetExplorer();

            string currentBrowserName = CurrentBrowser;

            if (currentBrowserName == null)
            {
                //default browser
                return ie;
            }

            List<IBrowser> supportedBrowsers = new List<IBrowser>
            {
                ie,
                new Firefox(),
                new Chrome()
            };

            IBrowser currentBrowser = supportedBrowsers.SingleOrDefault(x => currentBrowserName.StartsWith(x.Name, StringComparison.OrdinalIgnoreCase));

            if (currentBrowser == null)
            {
                //default browser
                return ie;
            }

            currentBrowser.Name = currentBrowserName;

            return currentBrowser;
        }

        /// <summary>
        /// Launches the specified url.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>The launched BrowserWindow</returns>
        public new static BrowserWindow Launch(string url)
        {
            return Launch(new Uri(url));
        }

        /// <summary>
        /// Launches the specified url.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="title">The title.</param>
        /// <returns>The BrowserWindowUnderTest that matches the title</returns>
        public new static BrowserWindowUnderTest Launch(string url, string title)
        {
            var browserWindowUnderTest = new BrowserWindowUnderTest();
            browserWindowUnderTest.CopyFrom(Launch(new Uri(url)));

            return browserWindowUnderTest;
        }

        /// <summary>
        /// Launches the specified url.
        /// </summary>
        /// <typeparam name="T">Object repository class</typeparam>
        /// <param name="url">The url.</param>
        /// <returns>An instance of the object repository class that matches the title</returns>
        public static T Launch<T>(string url)
            where T : BrowserWindowUnderTest, new()
        {
            T browserWindow = new T();
            browserWindow.CopyFrom(Launch(new Uri(url)));

            return browserWindow;
        }

        /// <summary>
        /// Gets the instance of T, which is an Object repository class (page definition).
        /// </summary>
        /// <typeparam name="T">Object repository class</typeparam>
        /// <returns>instance of T</returns>
        public static T GetBrowserWindow<T>()
        {
            return ObjectRepositoryManager.GetInstance<T>();
        }

        /// <summary>
        /// Sets the window title.
        /// </summary>
        /// <param name="title">The title.</param>
        public void SetWindowTitle(string title)
        {
            WindowTitles.Clear();
            WindowTitles.Add(title);
            sWindowTitle = title;
        }

        public CUITControls.HtmlCustom SlObjectContainer
        {
            get
            {
                if ((mSlObjectContainer == null))
                {
                    mSlObjectContainer = new CUITControls.HtmlCustom(this);
                    mSlObjectContainer.SearchProperties["TagName"] = "OBJECT";
                    mSlObjectContainer.WindowTitles.Add(sWindowTitle);
                }
                return mSlObjectContainer;
            }
        }

        /// <summary>
        /// Navigates to the specified URL.
        /// </summary>
        /// <param name="sUrl">The s URL.</param>
        public void NavigateToUrl(string sUrl)
        {
            NavigateToUrl(new Uri(sUrl));
        }

        /// <summary>
        /// Closes all instances of the current browser.
        /// </summary>
        public static void CloseAllBrowsers()
        {
            Process[] pro_list = Process.GetProcessesByName(GetCurrentBrowser().ProcessName);
            foreach (Process pro in pro_list)
            {
                //kill all open browsers
                pro.Kill();
            }
        }

        /// <summary>
        /// Run/evaluate JavaScript code in the DOM context.
        /// </summary>
        /// <param name="code">The JavaScript code</param>
        public void RunScript(string code)
        {
            InternetExplorer.RunScript(this, code);
        }

        /// <summary>
        /// Authenticates the user with the specified user name and password.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        public static void Authenticate(string userName, string password)
        {
            UIWindowsSecurityWindow winTemp2 = new UIWindowsSecurityWindow();
            if (winTemp2.UIUseAnotherAccountText.Exists)
            {
                Mouse.Click(winTemp2.UIUseAnotherAccountText);
            }
            winTemp2.UIUsernameEdit.Text = userName;
            winTemp2.UIPasswordEdit.Text = password;
            Mouse.Click(winTemp2.UIOKButton);
        }

        #region Objects initialized at runtime without ObjectRepository entries

        /// <summary>
        /// Gets the CUITe control object when search parameters are passed. 
        /// You don't have to create the object repository entry for this.
        /// </summary>
        /// <typeparam name="T">Pass the CUITe control you are looking for.</typeparam>
        /// <param name="searchParameters">In 'Key1=Value1;Key2=Value2' format. For example 'Id=firstname'</param>
        /// <returns>CUITe control object</returns>
        public T Get<T>(string searchParameters = null)
            where T : IControlBase
        {
            T control = ControlBaseFactory.Create<T>(searchParameters);

            if (typeof(T).Namespace.Equals("CUITe.Controls.SilverlightControls"))
            {
                var sourceControl = Activator.CreateInstance(control.SourceType, SlObjectContainer);
                control.Wrap(sourceControl);
            }
            else if (typeof(T).Namespace.Equals("CUITe.Controls.TelerikControls"))
            {
                (control as ComboBox).SetWindow(this);
            }
            else
            {
                var sourceControl = Activator.CreateInstance(control.SourceType, this);
                control.Wrap(sourceControl);
            }

            return control;
        }

        [Obsolete("GetHtmlButton(string) is deprecated, please use Get<HtmlButton>(string) instead.")]
        public HtmlButton GetHtmlButton(string searchParameters)
        {
            var button = new HtmlButton(searchParameters);
            button.Wrap(new CUITControls.HtmlButton(this));
            return button;
        }

        [Obsolete("GetHtmlCell(string) is deprecated, please use Get<HtmlCell>(string) instead.")]
        public HtmlCell GetHtmlCell(string searchParameters)
        {
            var cell = new HtmlCell(searchParameters);
            cell.Wrap(new CUITControls.HtmlCell(this));
            return cell;
        }

        [Obsolete("GetHtmlCheckBox(string) is deprecated, please use Get<HtmlCheckBox>(string) instead.")]
        public HtmlCheckBox GetHtmlCheckBox(string searchParameters)
        {
            var chk = new HtmlCheckBox(searchParameters);
            chk.Wrap(new CUITControls.HtmlCheckBox(this));
            return chk;
        }

        [Obsolete("GetHtmlComboBox(string) is deprecated, please use Get<HtmlComboBox>(string) instead.")]
        public HtmlComboBox GetHtmlComboBox(string searchParameters)
        {
            var cmb = new HtmlComboBox(searchParameters);
            cmb.Wrap(new CUITControls.HtmlComboBox(this));
            return cmb;
        }

        [Obsolete("GetHtmlDiv(string) is deprecated, please use Get<HtmlDiv>(string) instead.")]
        public HtmlDiv GetHtmlDiv(string searchParameters)
        {
            var div = new HtmlDiv(searchParameters);
            div.Wrap(new CUITControls.HtmlDiv(this));
            return div;
        }

        [Obsolete("GetHtmlEdit(string) is deprecated, please use Get<HtmlEdit>(string) instead.")]
        public HtmlEdit GetHtmlEdit(string searchParameters)
        {
            HtmlEdit edit = new HtmlEdit(searchParameters);
            edit.Wrap(new CUITControls.HtmlEdit(this));
            return edit;
        }

        [Obsolete("GetHtmlFileInput(string) is deprecated, please use Get<HtmlFileInput>(string) instead.")]
        public HtmlFileInput GetHtmlFileInput(string searchParameters)
        {
            HtmlFileInput fin = new HtmlFileInput(searchParameters);
            fin.Wrap(new CUITControls.HtmlFileInput(this));
            return fin;
        }

        [Obsolete("GetHtmlHyperlink(string) is deprecated, please use Get<HtmlHyperlink>(string) instead.")]
        public HtmlHyperlink GetHtmlHyperlink(string searchParameters)
        {
            var href = new HtmlHyperlink(searchParameters);
            href.Wrap(new CUITControls.HtmlHyperlink(this));
            return href;
        }

        [Obsolete("GetHtmlImage(string) is deprecated, please use Get<HtmlImage>(string) instead.")]
        public HtmlImage GetHtmlImage(string searchParameters)
        {
            var img = new HtmlImage(searchParameters);
            img.Wrap(new CUITControls.HtmlImage(this));
            return img;
        }

        [Obsolete("GetHtmlInputButton(string) is deprecated, please use Get<HtmlInputButton>(string) instead.")]
        public HtmlInputButton GetHtmlInputButton(string searchParameters)
        {
            var input = new HtmlInputButton(searchParameters);
            input.Wrap(new CUITControls.HtmlInputButton(this));
            return input;
        }

        [Obsolete("GetHtmlLabel(string) is deprecated, please use Get<HtmlLabel>(string) instead.")]
        public HtmlLabel GetHtmlLabel(string searchParameters)
        {
            var lbl = new HtmlLabel(searchParameters);
            lbl.Wrap(new CUITControls.HtmlLabel(this));
            return lbl;
        }

        [Obsolete("GetHtmlList(string) is deprecated, please use Get<HtmlList>(string) instead.")]
        public HtmlList GetHtmlList(string searchParameters)
        {
            var lst = new HtmlList(searchParameters);
            lst.Wrap(new CUITControls.HtmlList(this));
            return lst;
        }

        [Obsolete("GetHtmlPassword(string) is deprecated, please use Get<HtmlPassword>(string) instead.")]
        public HtmlPassword GetHtmlPassword(string searchParameters)
        {
            var pwd = new HtmlPassword(searchParameters);
            var tmp = new CUITControls.HtmlEdit(this);
            tmp.FilterProperties[CUITControls.HtmlControl.PropertyNames.Type] = "PASSWORD";
            pwd.Wrap(tmp);
            return pwd;
        }

        [Obsolete("GetHtmlRadioButton(string) is deprecated, please use Get<HtmlRadioButton>(string) instead.")]
        public HtmlRadioButton GetHtmlRadioButton(string searchParameters)
        {
            var rad = new HtmlRadioButton(searchParameters);
            rad.Wrap(new CUITControls.HtmlRadioButton(this));
            return rad;
        }

        [Obsolete("GetHtmlSpan(string) is deprecated, please use Get<HtmlSpan>(string) instead.")]
        public HtmlSpan GetHtmlSpan(string searchParameters)
        {
            var span = new HtmlSpan(searchParameters);
            span.Wrap(new CUITControls.HtmlSpan(this));
            return span;
        }

        [Obsolete("GetHtmlTable(string) is deprecated, please use Get<HtmlTable>(string) instead.")]
        public HtmlTable GetHtmlTable(string searchParameters)
        {
            HtmlTable tbl = new HtmlTable(searchParameters);
            tbl.Wrap(new CUITControls.HtmlTable(this));
            return tbl;
        }

        [Obsolete("GetHtmlTextArea(string) is deprecated, please use Get<HtmlTextArea>(string) instead.")]
        public HtmlTextArea GetHtmlTextArea(string searchParameters)
        {
            var tarea = new HtmlTextArea(searchParameters);
            tarea.Wrap(new CUITControls.HtmlTextArea(this));
            return tarea;
        }

        #endregion
    }
}