// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Collections;
using Yuandl.ThemeUI.Appearance;

namespace Yuandl.ThemeUI.Controls;

public interface IPageViewItem
{
    /// <summary>
    /// Gets the unique identifier that allows the item to be located in the navigation.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets or sets the content
    /// </summary>
    object Content { get; set; }

    /// <summary>
    /// Gets or sets the icon displayed in the MenuItem object.
    /// </summary>
    IconElement? Icon { get; set; }

    /// <summary>
    /// Gets a value indicating whether the current element is active.
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the sub-<see cref="MenuItems"/> are expanded.
    /// </summary>
    bool IsExpanded { get; internal set; }

    /// <summary>
    /// Gets or sets a value indicating whether the NavigationView pane is expanded to its full width.
    /// </summary>
    bool IsOpen { get; set; }

    /// <summary>
    /// Gets or sets the unique tag used by the parent navigation system for the purpose of searching and navigating.
    /// </summary>
    string PageTag { get; set; }

    /// <summary>
    /// Gets a value indicating whether MenuItems.Count > 0
    /// </summary>
    bool HasItems { get; }

    /// <summary>
    /// Retrieve the menu IList collection
    /// </summary>
    IList MenuItems { get; }

    /// <summary>
    ///  Gets or sets a collection used to generate the content of the.
    /// </summary>
    object? ItemsSource { get; set; }

    /// <summary>
    ///  Gets or sets the data context for an element when it participates in data binding.
    /// </summary>
    object? DataContent { get; set; }

    /// <summary>
    /// Gets or sets the type of the page to be navigated. (Should be derived from <see cref="FrameworkElement"/>).
    /// </summary>
    Type? PageType { get; set; }

    /// <summary>
    /// Gets or sets the caching characteristics for a page involved in a navigation.
    /// </summary>
    NavigationCacheMode CacheMode { get; set; }

    InfoBadge? InfoBadge { get; set; }

    /// <summary>
    /// Gets or sets the template property
    /// </summary>
    ControlTemplate? Template { get; set; }

    /// <summary>
    /// Add / Remove ClickEvent handler.
    /// </summary>
    [Category("Behavior")]
    event RoutedEventHandler Click;

    internal bool IsElement { get; set; }

    /// <summary>
    /// Correctly activates
    /// </summary>
    void Activate(SidebarMenu navigationView);

    /// <summary>
    /// Correctly deactivates
    /// </summary>
    void Deactivate(SidebarMenu navigationView);

    /// <summary>
    /// Gets or sets the parent if it's in <see cref="MenuItems"/> collection
    /// </summary>
    IPageViewItem? NavigationViewItemParent { get; internal set; }
}