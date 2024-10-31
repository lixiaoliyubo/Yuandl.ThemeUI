// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Services;

/// <summary>
/// Represents a contract with the service that provides the pages for <see cref="INavigationView"/>.
/// </summary>
public interface IPageService
{
    /// <summary>
    /// Takes a page of the given type.
    /// </summary>
    /// <typeparam name="T">Page type.</typeparam>
    /// <returns>Instance of the registered page service.</returns>
    public T? GetPage<T>()
        where T : class;

    /// <summary>
    /// Takes a page of the given type.
    /// </summary>
    /// <param name="pageType">Page type.</param>
    /// <returns>Instance of the registered page service.</returns>
    public FrameworkElement? GetPage(Type pageType);
}