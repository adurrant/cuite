﻿using System.Windows.Input;
using CUITe.ObjectRepository;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sut.PeripheralInputTest.ObjectRepository;

namespace Sut.PeripheralInputTest
{
    [CodedUITest]
#if DEBUG
    [DeploymentItem(@"..\..\..\Sut.PeripheralInput\bin\Debug\Sut.PeripheralInput.exe")]
#else
    [DeploymentItem(@"..\..\..\Sut.PeripheralInput\bin\Release\Sut.PeripheralInput.exe")]
#endif
    public class KeyboardTest
    {
        private const string ApplicationFilePath = @"Sut.PeripheralInput.exe";
        private MainScreen mainScreen;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for
        /// the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Runs once before all tests.
        /// </summary>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            Playback.PlaybackSettings.SendKeysAsScanCode = true;
        }

        /// <summary>
        /// Runs before each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            mainScreen = Screen.Launch<MainScreen>(ApplicationFilePath);
        }

        [TestMethod]
        public void SendText()
        {
            // Arrange
            string input = "CUITe keyboard test";
            string expected = input;

            // Act
            mainScreen.KeyboardResult.SendKeys(input);
            
            // Assert
            Assert.AreEqual(expected, mainScreen.KeyboardResult.Text);
        }

        [TestMethod]
        public void SendTextWithModifier()
        {
            // Arrange
            string input = "cuite keyboard test";
            string expected = "CUITE KEYBOARD TEST";

            // Act
            mainScreen.KeyboardResult.SendKeys(input, ModifierKeys.Shift);

            // Assert
            Assert.AreEqual(expected, mainScreen.KeyboardResult.Text);
        }

        [TestMethod]
        public void PressAndReleaseModifierKeys()
        {
            // Arrange
            string input = "cuite keyboard test";
            string expected = "CUITE KEYBOARD TEST";

            // Act
            mainScreen.KeyboardResult.PressModifierKeys(ModifierKeys.Shift);
            mainScreen.KeyboardResult.SendKeys(input);
            mainScreen.KeyboardResult.ReleaseModifierKeys(ModifierKeys.Shift);

            // Assert
            Assert.AreEqual(expected, mainScreen.KeyboardResult.Text);
        }


        [TestMethod]
        public void HoldModifierKeys()
        {
            // Arrange
            string input = "cuite keyboard test";
            string expected = "CUITE KEYBOARD TEST";

            // Act
            using (mainScreen.KeyboardResult.HoldModifierKeys(ModifierKeys.Shift))
            {
                mainScreen.KeyboardResult.SendKeys(input);
            }

            // Assert
            Assert.AreEqual(expected, mainScreen.KeyboardResult.Text);
        }

        [TestMethod]
        public void SendTextAfterHoldingModifierKeys()
        {
            // Arrange
            string input = "CUITe keyboard test";
            string expected = input;

            using (mainScreen.KeyboardResult.HoldModifierKeys(ModifierKeys.Shift))
            {
            }

            // Act
            mainScreen.KeyboardResult.SendKeys(input);

            // Assert
            Assert.AreEqual(expected, mainScreen.KeyboardResult.Text);
        }
    }
}