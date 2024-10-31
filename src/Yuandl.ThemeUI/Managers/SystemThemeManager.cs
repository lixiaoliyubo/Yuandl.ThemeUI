// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Microsoft.Win32;
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.Managers;

public static class SystemThemeManager
{
    private static SystemTheme _cachedTheme = SystemTheme.Unknown;

    /// <summary>
    /// Gets the Windows glass color.
    /// </summary>
    public static Color GlassColor => SystemParameters.WindowGlassColor;

    /// <summary>
    /// Returns the Windows theme retrieved from the registry. If it has not been cached before, invokes the <see cref="UpdateSystemThemeCache"/> and then returns the currently obtained theme.
    /// </summary>
    /// <returns>Currently cached Windows theme.</returns>
    public static SystemTheme GetCachedSystemTheme()
    {
        if (_cachedTheme != SystemTheme.Unknown)
        {
            return _cachedTheme;
        }

        UpdateSystemThemeCache();

        return _cachedTheme;
    }

    /// <summary>
    /// Refreshes the currently saved system theme.
    /// </summary>
    public static void UpdateSystemThemeCache()
    {
        _cachedTheme = GetCurrentSystemTheme();
    }

    private static SystemTheme GetCurrentSystemTheme()
    {
        var currentTheme =
            Registry.GetValue(
                "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes",
                "CurrentTheme",
                "aero.theme"
            ) as string
            ?? string.Empty;

        if (!string.IsNullOrEmpty(currentTheme))
        {
            currentTheme = currentTheme.ToLower().Trim();

            // This may be changed in the next versions, check the Insider previews
            if (currentTheme.Contains("basic.theme"))
            {
                return SystemTheme.Light;
            }

            if (currentTheme.Contains("aero.theme"))
            {
                return SystemTheme.Light;
            }

            if (currentTheme.Contains("dark.theme"))
            {
                return SystemTheme.Dark;
            }
        }

        /*if (currentTheme.Contains("custom.theme"))
            return ; custom can be light or dark*/
        var rawAppsUseLightTheme = Registry.GetValue(
            "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize",
            "AppsUseLightTheme",
            1
        );

        if (rawAppsUseLightTheme is 0)
        {
            return SystemTheme.Dark;
        }
        else if (rawAppsUseLightTheme is 1)
        {
            return SystemTheme.Light;
        }

        var rawSystemUsesLightTheme =
            Registry.GetValue(
                "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize",
                "SystemUsesLightTheme",
                1
            ) ?? 1;

        return rawSystemUsesLightTheme is 0 ? SystemTheme.Dark : SystemTheme.Light;
    }
}