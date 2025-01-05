// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using Yuandl.ThemeUI.ElementAssist;

namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// Represents a container that enables navigation of app content. It has a header, a view for the main content, and a menu pane for navigation commands.
/// </summary>
public interface INavigationView
{
    /// <summary>
    /// Gets the selected item.
    /// </summary>
    IPageViewItem? SelectedItem { get; }

    /// <summary>
    /// Gets or sets the width of the NavigationView pane when it's fully expanded.
    /// </summary>
    double OpenPaneLength { get; set; }

    /// <summary>
    /// Gets or sets the width of the NavigationView pane in its compact display mode.
    /// </summary>
    double CompactPaneLength { get; set; }

    /// <summary>
    /// Gets or sets a value deciding how long the effect of the transition between the pages should take.
    /// </summary>
    int TransitionDuration { get; set; }

    /// <summary>
    /// Gets or sets type of <see cref="INavigationView"/> transitions during navigation.
    /// </summary>
    Transition Transition { get; set; }

    /// <summary>
    /// Gets or sets margin for a Frame of <see cref="INavigationView"/>
    /// </summary>
    Thickness FrameMargin { get; set; }

    /// <summary>
    /// Occurs when the currently selected item changes.
    /// </summary>
    event TypedEventHandler<NavigationView, RoutedEventArgs> SelectionChanged;

    /// <summary>
    /// Occurs when a new navigation is requested
    /// </summary>
    event TypedEventHandler<NavigationView, NavigatingCancelEventArgs> Navigating;

    /// <summary>
    /// Occurs when navigated to page
    /// </summary>
    event TypedEventHandler<NavigationView, NavigatedEventArgs> Navigated;

    /// <summary>
    /// Gets a value indicating whether there is at least one entry in back navigation history.
    /// </summary>
    bool CanGoBack { get; }

    /// <summary>
    /// Synchronously navigates current navigation Frame to the
    /// given Element.
    /// </summary>
    bool Navigate(Type pageType, object? dataContext = null);

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
    /// Refresh the current navigation page
    /// </summary>
    /// <returns><see langword="true"/> if successfully navigated refresh, otherwise <see langword="false"/>.</returns>
    bool Refresh();

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
    /// Allows you to assign a general <see cref="IServiceProvider"/> to the NavigationView that will be used to retrieve page instances and view models.
    /// </summary>
    void SetServiceProvider(IServiceProvider serviceProvider);
}
