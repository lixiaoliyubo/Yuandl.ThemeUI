// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Controls;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Extensions;

namespace Yuandl.ThemeUI.Managers;

public static class WindowBackgroundManager
{
    /// <summary>
    /// Tries to apply dark theme to <see cref="Window"/>.
    /// </summary>
    public static void ApplyDarkThemeToWindow(Window window)
    {
        if (window is null)
        {
            return;
        }

        if (window.IsLoaded)
        {
            _ = NativeMethodsExtension.ApplyWindowDarkMode(window);
        }

        window.Loaded += (sender, _) => NativeMethodsExtension.ApplyWindowDarkMode(sender as Window);
    }

    /// <summary>
    /// Tries to remove dark theme from <see cref="Window"/>.
    /// </summary>
    public static void RemoveDarkThemeFromWindow(Window window)
    {
        if (window is null)
        {
            return;
        }

        if (window.IsLoaded)
        {
            _ = NativeMethodsExtension.RemoveWindowDarkMode(window);
        }

        window.Loaded += (sender, _) => NativeMethodsExtension.RemoveWindowDarkMode(sender as Window);
    }

    /// <summary>
    /// Forces change to application background. Required if custom background effect was previously applied.
    /// </summary>
    public static void UpdateBackground(Window window, ApplicationTheme applicationTheme, WindowBackdropType backdrop)
    {
        if (window is null)
        {
            return;
        }

        _ = NativeMethodsExtension.RemoveBackdrop(window);

        // This was required to update the background when moving from a HC theme to light/dark theme. However, this breaks theme proper light/dark theme changing on Windows 10.
        // else
        // {
        //    _ = WindowBackdrop.RemoveBackground(window);
        // }
        _ = NativeMethodsExtension.ApplyBackdrop(window, backdrop);
        if (applicationTheme is ApplicationTheme.Dark)
        {
            ApplyDarkThemeToWindow(window);
        }
        else
        {
            RemoveDarkThemeFromWindow(window);
        }

        foreach (object subWindow in window.OwnedWindows)
        {
            if (subWindow is Window windowSubWindow)
            {
                _ = NativeMethodsExtension.ApplyBackdrop(windowSubWindow, backdrop);

                if (applicationTheme is ApplicationTheme.Dark)
                {
                    ApplyDarkThemeToWindow(windowSubWindow);
                }
                else
                {
                    RemoveDarkThemeFromWindow(windowSubWindow);
                }
            }
        }
    }
}