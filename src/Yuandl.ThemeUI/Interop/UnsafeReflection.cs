// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Controls;

namespace Yuandl.ThemeUI.Interop;

/// <summary>
/// A set of dangerous methods to modify the appearance.
/// </summary>
internal static class UnsafeReflection
{
    /// <summary>
    /// Casts <see cref="WindowBackdropType"/> to <see cref="Dwmapi.DWMSBT"/>.
    /// </summary>
    public static Dwmapi.DWMSBT Cast(WindowBackdropType backgroundType)
    {
        return backgroundType switch
        {
            WindowBackdropType.Auto => Dwmapi.DWMSBT.DWMSBT_AUTO,
            WindowBackdropType.Mica => Dwmapi.DWMSBT.DWMSBT_MAINWINDOW,
            WindowBackdropType.Acrylic => Dwmapi.DWMSBT.DWMSBT_TRANSIENTWINDOW,
            WindowBackdropType.Tabbed => Dwmapi.DWMSBT.DWMSBT_TABBEDWINDOW,
            _ => Dwmapi.DWMSBT.DWMSBT_DISABLE
        };
    }

    /// <summary>
    /// Casts <see cref="WindowCornerPreference"/> to <see cref="Dwmapi.DWM_WINDOW_CORNER_PREFERENCE"/>.
    /// </summary>
    public static Dwmapi.DWM_WINDOW_CORNER_PREFERENCE Cast(WindowCornerPreference cornerPreference)
    {
        return cornerPreference switch
        {
            WindowCornerPreference.Round => Dwmapi.DWM_WINDOW_CORNER_PREFERENCE.ROUND,
            WindowCornerPreference.RoundSmall => Dwmapi.DWM_WINDOW_CORNER_PREFERENCE.ROUNDSMALL,
            WindowCornerPreference.DoNotRound => Dwmapi.DWM_WINDOW_CORNER_PREFERENCE.DONOTROUND,
            _ => Dwmapi.DWM_WINDOW_CORNER_PREFERENCE.DEFAULT
        };
    }
}