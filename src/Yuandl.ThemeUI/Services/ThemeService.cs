// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Managers;

namespace Yuandl.ThemeUI.Services;

/// <summary>
/// Lets you set the app theme.
/// </summary>
public partial class ThemeService : IThemeService
{
    /// <inheritdoc />
    public virtual ApplicationTheme GetTheme() => ApplicationThemeManager.GetAppTheme();

    /// <inheritdoc />
    public virtual SystemTheme GetNativeSystemTheme() => ApplicationThemeManager.GetSystemTheme();

    /// <inheritdoc />
    public virtual ApplicationTheme GetSystemTheme()
    {
        SystemTheme systemTheme = ApplicationThemeManager.GetSystemTheme();

        return systemTheme switch
        {
            SystemTheme.Light => ApplicationTheme.Light,
            SystemTheme.Dark => ApplicationTheme.Dark,
            _ => ApplicationTheme.Unknown
        };
    }

    /// <inheritdoc />
    public virtual bool SetTheme(ApplicationTheme applicationTheme)
    {
        if (ApplicationThemeManager.GetAppTheme() == applicationTheme)
        {
            return false;
        }

        ApplicationThemeManager.Apply(applicationTheme);

        return true;
    }

    /// <inheritdoc />
    public bool SetSystemAccent()
    {
        ApplicationAccentColorManager.ApplySystemAccent();

        return true;
    }

    /// <inheritdoc />
    public bool SetAccent(Color accentColor)
    {
        ApplicationAccentColorManager.Apply(accentColor);

        return true;
    }

    /// <inheritdoc />
    public bool SetAccent(SolidColorBrush accentSolidBrush)
    {
        Color color = accentSolidBrush.Color;
        color.A = (byte)Math.Round(accentSolidBrush.Opacity * byte.MaxValue);

        ApplicationAccentColorManager.Apply(color);

        return true;
    }
}