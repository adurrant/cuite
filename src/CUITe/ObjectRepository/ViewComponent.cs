﻿using System;
using CUITe.Controls;
using CUITe.SearchConfigurations;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace CUITe.ObjectRepository
{
    /// <summary>
    /// Base class for all view components, indifferent of application technology.
    /// </summary>
    public abstract class ViewComponent
    {
        private UITestControl searchLimitContainer;

        /// <summary>
        /// Gets or sets the search limit container.
        /// </summary>
        internal virtual UITestControl SearchLimitContainer
        {
            get { return searchLimitContainer; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                searchLimitContainer = value;
            }
        }

        /// <summary>
        /// Finds the control object from the descendants of this control using the specified
        /// search configuration.
        /// </summary>
        /// <typeparam name="T">The type of control to find.</typeparam>
        /// <param name="searchConfiguration">The search configuration.</param>
        /// <exception cref="InvalidSearchPropertyNamesException">
        /// Search configuration contains a property namely that isn't applicable on the control.
        /// </exception>
        protected T Find<T>(By searchConfiguration = null) where T : ControlBase
        {
            if (searchLimitContainer == null)
                throw new InvalidOperationException("The view component has not been configured with a search limit container.");

            return searchLimitContainer.Find<T>(searchConfiguration);
        }
    }
}