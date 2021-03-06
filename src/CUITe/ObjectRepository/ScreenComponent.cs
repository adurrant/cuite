﻿using System;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace CUITe.ObjectRepository
{
    /// <summary>
    /// Abstract class representing a screen or window component in a WPF or WinForms
    /// application.
    /// </summary>
    /// <remarks>
    /// A <see cref="Screen"/> with a overwhelming number of controls can be split into logical
    /// components, thus providing better test code maintainability.
    /// </remarks>
    /// <seealso cref="Screen"/>
    /// <seealso cref="ScreenComponent{T}"/>
    public abstract class ScreenComponent : ViewComponent
    {
        private ApplicationUnderTest application;

        /// <summary>
        /// Gets the application.
        /// </summary>
        public ApplicationUnderTest Application
        {
            get { return application; }
            internal set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                application = value;
            }
        }
        
        /// <summary>
        /// Gets the screen component of specified type.
        /// </summary>
        /// <remarks>
        /// A <see cref="Screen"/> with a overwhelming number of controls can be split into logical
        /// components, thus providing better test code maintainability.
        /// </remarks>
        /// <typeparam name="T">The type of the screen component.</typeparam>
        /// <returns>The screen component of specified type.</returns>
        protected T GetComponent<T>() where T : ScreenComponent, new()
        {
            return new T
            {
                Application = Application,
                SearchLimitContainer = SearchLimitContainer
            };
        }

        /// <summary>
        /// Navigates to a new screen.
        /// </summary>
        /// <typeparam name="T">The type of screen to navigate to.</typeparam>
        /// <returns>The new screen.</returns>
        protected T NavigateTo<T>() where T : Screen, new()
        {
            return new T
            {
                Application = Application,
                SearchLimitContainer = Application
            };
        }
    }
}