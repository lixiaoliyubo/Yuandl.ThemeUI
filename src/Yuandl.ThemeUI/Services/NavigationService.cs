// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Controls;

namespace Yuandl.ThemeUI.Services;

public class NavigationService : INavigationService
{
    /// <summary>
    /// Locally attached service provider.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Locally attached page service.
    /// </summary>
    private readonly IPageService _pageService;

    /// <summary>
    /// Gets or sets the control representing navigation.
    /// </summary>
    protected INavigationView? NavigationControl { get; set; }

    public NavigationService(IServiceProvider serviceProvider, IPageService pageService)
    {
        _pageService = pageService;
        _serviceProvider = serviceProvider;
    }

    public void SetNavigationControl(INavigationView navigation)
    {
        NavigationControl = navigation;

        if (_pageService != null)
        {
            NavigationControl.SetPageService(_pageService);

            return;
        }

        NavigationControl.SetServiceProvider(_serviceProvider);
    }

    /// <summary>
    /// Synchronously navigates current navigation Frame to the
    /// given Element.
    /// </summary>
    public bool Navigate(Type PageType, object? dataContext = null) => NavigationControl?.Navigate(PageType, dataContext) ?? false;

    /// <summary>
    /// Synchronously navigates current navigation Frame to the
    /// given Element.
    /// </summary>
    public bool Navigate(string pageIdOrTargetTag, object? dataContext = null) => NavigationControl?.Navigate(pageIdOrTargetTag, dataContext) ?? false;

    /// <summary>
    /// Synchronously navigates current navigation Frame to the
    /// given Element.
    /// </summary>
    public bool Navigate(IPageViewItem viewItem, object? dataContext = null, bool isBackwardsNavigated = false) => NavigationControl?.Navigate(viewItem, dataContext, isBackwardsNavigated) ?? false;

    /// <summary>
    /// Navigates the NavigationView to the previous journal entry.
    /// </summary>
    /// <returns><see langword="true"/> if successfully navigated backward, otherwise <see langword="false"/>.</returns>
    public bool GoBack() => NavigationControl?.GoBack() ?? false;

    /// <summary>
    /// Clears the NavigationView history.
    /// </summary>
    public void ClearJournal() => NavigationControl?.ClearJournal();

    /// <summary>
    /// Refresh the current navigation page
    /// </summary>
    /// <returns><see langword="true"/> if successfully navigated refresh, otherwise <see langword="false"/>.</returns>
    public bool Refresh() => NavigationControl?.Refresh() ?? false;
}