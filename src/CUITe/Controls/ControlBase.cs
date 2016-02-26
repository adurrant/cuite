﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using CUITe.Caches;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace CUITe.Controls
{
    /// <summary>
    /// Base class for all UI test controls. It provides properties and methods which are generic
    /// to controls across technologies.
    /// </summary>
    public abstract class ControlBase
    {
        private readonly UITestControl sourceControl;

        protected static readonly PropertyNamesCache PropertyNamesCache;

        /// <summary>
        /// Initializes the <see cref="ControlBase"/> class.
        /// </summary>
        static ControlBase()
        {
            PropertyNamesCache = new PropertyNamesCache();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlBase"/> class.
        /// </summary>
        /// <param name="sourceControl">The source control.</param>
        protected ControlBase(UITestControl sourceControl)
        {
            if (sourceControl == null)
                throw new ArgumentNullException("sourceControl");

            this.sourceControl = sourceControl;
        }

        /// <summary>
        /// Waits for the control to be ready and gets a value indicating whether this element is
        /// enabled in the user interface.
        /// </summary>
        public bool Enabled
        {
            get
            {
                WaitForControlReady();
                return sourceControl.Enabled;
            }
        }

        /// <summary>
        /// Waits for the control to be ready and gets a value indicating whether this element
        /// exists in the user interface.
        /// </summary>
        public bool Exists
        {
            get { return sourceControl.Exists; }
        }

        /// <summary>
        /// Gets the source control.
        /// </summary>
        internal UITestControl SourceControl
        {
            get { return sourceControl; }
        }

        /// <summary>
        /// Gets the <see cref="Type"/> of the source control.
        /// </summary>
        /// <returns>The exact runtime type of the source control.</returns>
        public Type SourceControlType
        {
            get { return sourceControl.GetType(); }
        }
        
        /// <summary>
        /// Gets the parent of the control.
        /// </summary>
        /// <exception cref="InvalidTraversalException">
        /// Error occurred when traversing the control tree.
        /// </exception>
        public abstract ControlBase Parent { get; }

        /// <summary>
        /// Gets the previous sibling of the control.
        /// </summary>
        /// <exception cref="InvalidTraversalException">
        /// Error occurred when traversing the control tree.
        /// </exception>
        public abstract ControlBase PreviousSibling { get; }

        /// <summary>
        /// Gets the next sibling of the control.
        /// </summary>
        /// <exception cref="InvalidTraversalException">
        /// Error occurred when traversing the control tree.
        /// </exception>
        public abstract ControlBase NextSibling { get; }

        /// <summary>
        /// Gets the first child of the control.
        /// </summary>
        /// <exception cref="InvalidTraversalException">
        /// Error occurred when traversing the control tree.
        /// </exception>
        public abstract ControlBase FirstChild { get; }

        /// <summary>
        /// Returns a sequence of all first level children of the control.
        /// </summary>
        public abstract IEnumerable<ControlBase> GetChildren();

        /// <summary>
        /// Waits for the control to be ready.
        /// </summary>
        public void WaitForControlReady()
        {
            sourceControl.WaitForControlReady();
        }
        /// <summary>
        /// Checks to see if the control has focus
        /// </summary>
        public bool HasFocus
        {
            get { return sourceControl.HasFocus; }
        }

        /// <summary>
        /// Waits for the control to be ready and attempts to set focus.
        /// </summary>
        public void SetFocus()
        {
            WaitForControlReady();
            sourceControl.SetFocus();
        }

        /// <summary>
        /// Waits for the control to be ready and then clicks the specified mouse button.
        /// </summary>
        /// <param name="button">
        /// The <see cref="MouseButtons"/> that will be used for clicking.
        /// </param>
        public void Click(MouseButtons button = MouseButtons.Left)
        {
            WaitForControlReady();
            Mouse.Click(sourceControl, button);
        }

        /// <summary>
        /// Waits for the control to be ready and then clicks the default mouse button while
        /// holding the specified modifier keys.
        /// </summary>
        /// <param name="modifierKeys">
        /// <see cref="ModifierKeys"/> to be pressed while clicking.
        /// </param>
        public void Click(ModifierKeys modifierKeys)
        {
            WaitForControlReady();
            Mouse.Click(sourceControl, modifierKeys);
        }

        /// <summary>
        /// Waits for the control to be ready and then double-clicks the specified mouse button.
        /// </summary>
        /// <param name="button">
        /// The <see cref="MouseButtons"/> that will be used for double-clicking.
        /// </param>
        public void DoubleClick(MouseButtons button = MouseButtons.Left)
        {
            WaitForControlReady();
            Mouse.DoubleClick(sourceControl, button);
        }

        /// <summary>
        /// Waits for the control to be ready and then double-clicks the default mouse button while
        /// holding the specified modifier keys.
        /// </summary>
        /// <param name="modifierKeys">
        /// <see cref="ModifierKeys"/> that will be used for double-clicking.
        /// </param>
        public void DoubleClick(ModifierKeys modifierKeys)
        {
            WaitForControlReady();
            Mouse.DoubleClick(sourceControl, modifierKeys);
        }

        /// <summary>
        /// Waits for the control to be ready and then clicks at the center of the control based on
        /// its point on the screen.
        /// </summary>
        /// <remarks>
        /// This may be a "work-around" for Coded UI tests on third-party controls that throw the
        /// <see cref="FailedToPerformActionOnBlockedControlException"/> with the message:
        /// Another control is blocking the control. Please make the blocked control visible and
        /// retry the action.
        /// </remarks>
        public void PointAndClick()
        {
            WaitForControlReady();
            int centerX = sourceControl.BoundingRectangle.X + sourceControl.BoundingRectangle.Width / 2;
            int centerY = sourceControl.BoundingRectangle.Y + sourceControl.BoundingRectangle.Height / 2;
            Mouse.Click(new Point(centerX, centerY));
        }

        /// <summary>
        /// Waits for the control to be ready and then presses the specified modifier keys without
        /// releasing them.
        /// </summary>
        /// <param name="keys">
        /// The sum of one or more values of the <see cref="ModifierKeys"/> enumeration.
        /// </param>
        /// <remarks>
        /// Modifier keys that have been pressed must be explicitly released by using the
        /// <see cref="ReleaseModifierKeys"/>.
        /// </remarks>
        public void PressModifierKeys(ModifierKeys keys)
        {
            WaitForControlReady();
            Keyboard.PressModifierKeys(sourceControl, keys);
        }

        /// <summary>
        /// Waits for the control to be ready and then releases the specified keys that were
        /// previously pressed by using the <see cref="PressModifierKeys"/> method.
        /// </summary>
        /// <param name="keys">
        /// The sum of one or more values of the <see cref="ModifierKeys"/> enumeration.
        /// </param>
        public void ReleaseModifierKeys(ModifierKeys keys)
        {
            WaitForControlReady();
            Keyboard.ReleaseModifierKeys(sourceControl, keys);
        }

        /// <summary>
        /// Waits for the control to be ready and then holds the specified modifier keys until
        /// the returned instance is disposed.
        /// </summary>
        /// <param name="keys">
        /// The sum of one or more values of the <see cref="ModifierKeys"/> enumeration.
        /// </param>
        /// <returns>
        /// An instance that releases the modifier keys when disposed.
        /// </returns>
        /// <remarks>
        /// This method is an alternative to using <see cref="PressModifierKeys"/> and
        /// <see cref="ReleaseModifierKeys"/>.
        /// </remarks>
        public IDisposable HoldModifierKeys(ModifierKeys keys)
        {
            WaitForControlReady();
            return new ModifierKeysLifetime(this, keys);
        }

        /// <summary>
        /// Waits for the control to be ready and then sends keystrokes to generate the specified
        /// text string.
        /// </summary>
        /// <param name="text">The text for which to generate keystrokes.</param>
        /// <param name="modifierKeys">
        /// The sum of one or more values of the <see cref="ModifierKeys"/> enumeration.
        /// </param>
        /// <param name="isEncoded">true if the text is encoded; otherwise, false.</param>
        /// <param name="isUnicode">true if the text is Unicode text; otherwise, false.</param>
        /// <remarks>
        /// The string may contain key modifiers.
        /// 
        /// Control     ^
        /// Shift       +
        /// Alt         %
        /// Windows     #
        /// 
        /// To send a Control+A keyboard sequence, use <code>SendKeys("^a")</code>.
        /// 
        /// To send a character that represents a key modifier, enclose the character in a pair of
        /// braces. For example, to send a plus sign, use <code>SendKeys("{+}")</code>.
        /// 
        /// To send a brace, enclose the brace in a pair of braces. For example, to send an opening
        /// or closing brace, use <code>SendKeys("{{}")</code> or <code>SendKeys("{}}")</code>,
        /// respectively.
        /// </remarks>
        public void SendKeys(
            string text,
            ModifierKeys modifierKeys = ModifierKeys.None,
            bool isEncoded = false,
            bool isUnicode = true)
        {
            WaitForControlReady();
            Keyboard.SendKeys(sourceControl, text, modifierKeys, isEncoded, isUnicode);
        }
    }
}