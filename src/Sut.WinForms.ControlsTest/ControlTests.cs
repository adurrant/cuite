﻿using CUITe.ObjectRepository;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sut.WinForms.ControlsTest.ObjectRepository;

namespace Sut.WinForms.ControlsTest
{
    [CodedUITest]
#if DEBUG
    [DeploymentItem(@"..\..\..\Sut.WinForms.Controls\bin\Debug\Sut.WinForms.Controls.exe")]
#else
    [DeploymentItem(@"..\..\..\Sut.WinForms.Controls\bin\Release\Sut.WinForms.Controls.exe")]
#endif
    public class ControlTests
    {
        private const string ApplicationFilePath = @"Sut.WinForms.Controls.exe";
        private MainScreen mainScreen;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for
        /// the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            mainScreen = Screen.Launch<MainScreen>(ApplicationFilePath);
        }

        [TestMethod]
        public void Button()
        {
            // Assert
            Assert.IsTrue(mainScreen.ButtonExists);
        }

        [TestMethod]
        public void CheckBox()
        {
            // Assert
            Assert.IsTrue(mainScreen.CheckBoxExists);
        }

        [TestMethod]
        public void ComboBox()
        {
            // Assert
            Assert.IsTrue(mainScreen.ComboBoxExists);
        }

        [TestMethod]
        public void DateTimePicker()
        {
            // Assert
            Assert.IsTrue(mainScreen.DateTimePickerExists);
        }

        [TestMethod]
        public void GroupBox()
        {
            // Assert
            Assert.IsTrue(mainScreen.GroupBoxExists);
        }

        [TestMethod]
        public void Label()
        {
            // Assert
            Assert.IsTrue(mainScreen.LabelExists);
        }

        [TestMethod]
        public void LinkLabel()
        {
            // Assert
            Assert.IsTrue(mainScreen.LinkLabelExists);
        }

        [TestMethod]
        public void ListBox()
        {
            // Assert
            Assert.IsTrue(mainScreen.ListBoxExists);
        }

        [TestMethod]
        public void ListView()
        {
            // Assert
            Assert.IsTrue(mainScreen.ListViewExists);
        }

        [TestMethod]
        public void MaskedTextBox()
        {
            // Assert
            Assert.IsTrue(mainScreen.MaskedTextBoxExists);
        }

        [TestMethod]
        public void MonthCalendar()
        {
            // Assert
            Assert.IsTrue(mainScreen.MonthCalendarExists);
        }

        [TestMethod]
        public void NumericUpDown()
        {
            // Assert
            Assert.IsTrue(mainScreen.NumericUpDownExists);
        }

        [TestMethod]
        public void ProgressBar()
        {
            // Assert
            Assert.IsTrue(mainScreen.ProgressBarExists);
        }

        [TestMethod]
        public void RadioButton()
        {
            // Assert
            Assert.IsTrue(mainScreen.RadioButtonExists);
        }

        [TestMethod]
        public void RichTextBox()
        {
            // Assert
            Assert.IsTrue(mainScreen.RichTextBoxExists);
        }

        [TestMethod]
        public void TabControl()
        {
            // Assert
            Assert.IsTrue(mainScreen.TabControlExists);
        }

        [TestMethod]
        public void TextBox()
        {
            // Assert
            Assert.IsTrue(mainScreen.TextBoxExists);
        }

        [TestMethod]
        public void TreeView()
        {
            // Assert
            Assert.IsTrue(mainScreen.TreeViewExists);
        }
    }
}