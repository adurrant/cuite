﻿#if SILVERLIGHT_SUPPORT
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UITesting;
using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.SilverlightControls;

namespace CUITe.Controls.SilverlightControls
{
    public class SilverlightControl<T> : CUITe_ControlBase<T> where T : CUITControls.SilverlightControl
    {
        public SilverlightControl() : base() { }
        public SilverlightControl(string searchParameters) : base(searchParameters) { }

        /// <summary>
        /// Gets the control's label text.
        /// </summary>
        public string LabeledBy
        {
            get
            {
                this._control.WaitForControlReady();
                return this._control.LabeledBy;
            }
        }

        /// <summary>
        /// Gets the parent of the current CUITe control.
        /// </summary>
        public override ICUITe_ControlBase Parent
        {
            get
            {
                this._control.WaitForControlReady();
                ICUITe_ControlBase ret = null;
                try
                {
                    ret = WrapUtil((CUITControls.SilverlightControl)this._control.GetParent());
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    throw new CUITe_InvalidTraversal(string.Format("({0}).Parent", this._control.GetType().Name));
                }
                return ret;
            }
        }

        /// <summary>
        /// Gets the previous sibling of the current CUITe control.
        /// </summary>
        public override ICUITe_ControlBase PreviousSibling
        {
            get
            {
                this._control.WaitForControlReady();
                ICUITe_ControlBase ret = null;
                try
                {
                    ret = WrapUtil((CUITControls.SilverlightControl)this._control.GetParent().GetChildren()[GetMyIndexAmongSiblings() - 1]);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    throw new CUITe_InvalidTraversal(string.Format("({0}).PreviousSibling", this._control.GetType().Name));
                }
                return ret;
            }
        }

        /// <summary>
        /// Gets the next sibling of the current CUITe control.
        /// </summary>
        public override ICUITe_ControlBase NextSibling
        {
            get
            {
                this._control.WaitForControlReady();
                ICUITe_ControlBase ret = null;
                try
                {
                    ret = WrapUtil((CUITControls.SilverlightControl)this._control.GetParent().GetChildren()[GetMyIndexAmongSiblings() + 1]);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    throw new CUITe_InvalidTraversal(string.Format("({0}).NextSibling", this._control.GetType().Name));
                }
                return ret;
            }
        }

        /// <summary>
        /// Gets the first child of the current CUITe control.
        /// </summary>
        public override ICUITe_ControlBase FirstChild
        {
            get
            {
                this._control.WaitForControlReady();
                ICUITe_ControlBase ret = null;
                try
                {
                    ret = WrapUtil((CUITControls.SilverlightControl)this._control.GetChildren()[0]);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    throw new CUITe_InvalidTraversal(string.Format("({0}).FirstChild", this._control.GetType().Name));
                }
                return ret;
            }
        }

        /// <summary>
        /// Returns a list of all first level children of the current CUITe control.
        /// </summary>
        /// <returns>list of all first level children</returns>
        public override List<ICUITe_ControlBase> GetChildren()
        {
            this._control.WaitForControlReady();
            var uicol = new List<ICUITe_ControlBase>();
            foreach (UITestControl uitestcontrol in this._control.GetChildren())
            {
                uicol.Add(WrapUtil((CUITControls.SilverlightControl)uitestcontrol));
            }
            return uicol;
        }

        private ICUITe_ControlBase WrapUtil(CUITControls.SilverlightControl control)
        {
            ICUITe_ControlBase _con = null;
            if (control.GetType() == typeof(CUITControls.SilverlightButton))
            {
                _con = new SilverlightButton();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightCalendar))
            {
                _con = new SilverlightCalendar();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightCell))
            {
                _con = new SilverlightCell();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightCheckBox))
            {
                _con = new SilverlightCheckBox();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightComboBox))
            {
                _con = new SilverlightComboBox();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightDataPager))
            {
                _con = new SilverlightDataPager();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightDatePicker))
            {
                _con = new SilverlightDatePicker();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightEdit))
            {
                _con = new SilverlightEdit();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightHyperlink))
            {
                _con = new SilverlightHyperlink();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightImage))
            {
                _con = new SilverlightImage();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightLabel))
            {
                _con = new SilverlightLabel();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightList))
            {
                _con = new SilverlightList();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightRadioButton))
            {
                _con = new SilverlightRadioButton();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightSlider))
            {
                _con = new SilverlightSlider();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightTab))
            {
                _con = new SilverlightTab();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightTabItem))
            {
                _con = new SilverlightTabItem();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightTable))
            {
                _con = new SilverlightTable();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightText))
            {
                _con = new SilverlightText();
            }
            else if (control.GetType() == typeof(CUITControls.SilverlightTree))
            {
                _con = new SilverlightTree();
            }
            else
            {
                throw new Exception(string.Format("WrapUtil: '{0}' is not supported.", control.GetType().ToString()));
            }

            ((ICUITe_ControlBase)_con).WrapReady(control);
            return _con;
        }

        private int GetMyIndexAmongSiblings()
        {
            int i = -1;
            foreach (UITestControl uitestcontrol in this._control.GetParent().GetChildren())
            {
                i++;
                if (uitestcontrol == this._control)
                {
                    break;
                }
            }
            return i;
        }
    }
}
#endif