// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Controls;

namespace Yuandl.ThemeUI.Services;

public interface INavigationService
{
    void SetNavigationControl(INavigationView navigationControl);

    /// <summary>
    /// Synchronously navigates current navigation Frame to the
    /// given Element.
    /// </summary>
    bool Navigate(Type PageType, object? dataContext = null);

    /// <summary>
    /// Synchronously navigates current navigation Frame to the
    /// given Element.
    /// </summary>
    bool Navigate(string pageIdOrTargetTag, object? dataContext = null);

    /// <summary>
    /// Synchronously navigates current navigation Frame to the
    /// given Element.
    /// </summary>
    bool Navigate(IPageViewItem viewItem, object? dataContext = null, bool isBackwardsNavigated = false);

    /// <summary>
    /// Navigates the NavigationView to the previous journal entry.
    /// </summary>
    /// <returns><see langword="true"/> if successfully navigated backward, otherwise <see langword="false"/>.</returns>
    bool GoBack();

    /// <summary>
    /// Clears the NavigationView history.
    /// </summary>
    void ClearJournal();

    /// <summary>
    /// Refresh the current navigation page
    /// </summary>
    /// <returns><see langword="true"/> if successfully navigated refresh, otherwise <see langword="false"/>.</returns>
    bool Refresh();
}