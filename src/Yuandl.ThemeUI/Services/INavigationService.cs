// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using Yuandl.ThemeUI.Controls;

namespace Yuandl.ThemeUI.Services;

/// <summary>
/// Represents a contract with a <see cref="FrameworkElement"/> that contains <see cref="INavigationView"/>.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Lets you navigate to the selected page based on it's type.
    /// </summary>
    /// <param name="pageType"><see langword="Type"/> of the page.</param>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool Navigate(Type pageType);

    /// <summary>
    /// Lets you navigate to the selected page based on it's type.
    /// </summary>
    /// <param name="pageType"><see langword="Type"/> of the page.</param>
    /// <param name="dataContext">DataContext <see cref="object"/></param>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool Navigate(Type pageType, object? dataContext);

    /// <summary>
    /// Lets you navigate to the selected page based on it's tag.
    /// </summary>
    /// <param name="pageIdOrTargetTag">Id or tag of the page.</param>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool Navigate(string pageIdOrTargetTag);

    /// <summary>
    /// Lets you navigate to the selected page based on it's tag.
    /// </summary>
    /// <param name="pageIdOrTargetTag">Id or tag of the page.</param>
    /// <param name="dataContext">DataContext <see cref="object"/></param>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool Navigate(string pageIdOrTargetTag, object? dataContext);

    /// <summary>
    /// Provides direct access to the control responsible for navigation.
    /// </summary>
    /// <returns>Instance of the <see cref="INavigationView"/> control.</returns>
    INavigationView GetNavigationControl();

    /// <summary>
    /// Lets you attach the control that represents the <see cref="INavigationView"/>.
    /// </summary>
    /// <param name="navigation">Instance of the <see cref="INavigationView"/>.</param>
    void SetNavigationControl(INavigationView navigation);

    /// <summary>
    /// Navigates the NavigationView to the previous journal entry.
    /// </summary>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool GoBack();
}

