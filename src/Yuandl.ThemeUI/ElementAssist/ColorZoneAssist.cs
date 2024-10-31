// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.ElementAssist;

public static class ColorZoneAssist
{
    public static readonly DependencyProperty AppearanceProperty = DependencyProperty.RegisterAttached(
        "Appearance", typeof(ControlAppearance), typeof(ColorZoneAssist), new FrameworkPropertyMetadata(default(ControlAppearance), FrameworkPropertyMetadataOptions.Inherits));

    public static void SetAppearance(DependencyObject element, ControlAppearance value)
        => element.SetValue(AppearanceProperty, value);

    public static ControlAppearance GetAppearance(DependencyObject element)
        => (ControlAppearance)element.GetValue(AppearanceProperty);

    public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached(
        "Background", typeof(Brush), typeof(ColorZoneAssist), new FrameworkPropertyMetadata(default(Brush)));

    public static void SetBackground(DependencyObject element, Brush value)
        => element.SetValue(BackgroundProperty, value);

    public static Brush GetBackground(DependencyObject element)
        => (Brush)element.GetValue(BackgroundProperty);

    public static readonly DependencyProperty ForegroundProperty = DependencyProperty.RegisterAttached(
        "Foreground", typeof(Brush), typeof(ColorZoneAssist), new FrameworkPropertyMetadata(default(Brush)));

    public static void SetForeground(DependencyObject element, Brush value)
        => element.SetValue(ForegroundProperty, value);

    public static Brush GetForeground(DependencyObject element)
        => (Brush)element.GetValue(ForegroundProperty);
}