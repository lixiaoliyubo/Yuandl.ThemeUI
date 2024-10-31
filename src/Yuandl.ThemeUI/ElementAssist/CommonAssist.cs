// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.ElementAssist;

public static class CommonAssist
{
    private static readonly CornerRadius DefaultCornerRadius = new(2.0);

    /// <summary>
    /// Controls the corner radius of the selection box.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty
        = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(CommonAssist), new PropertyMetadata(DefaultCornerRadius));

    public static CornerRadius GetCornerRadius(DependencyObject element)
        => (CornerRadius)element.GetValue(CornerRadiusProperty);

    public static void SetCornerRadius(DependencyObject element, CornerRadius value)
        => element.SetValue(CornerRadiusProperty, value);

    public static Brush? GetHoverBackground(DependencyObject obj)
        => (Brush?)obj.GetValue(HoverBackgroundProperty);

    public static void SetHoverBackground(DependencyObject obj, Brush? value)
        => obj.SetValue(HoverBackgroundProperty, value);

    public static readonly DependencyProperty HoverBackgroundProperty =
        DependencyProperty.RegisterAttached("HoverBackground", typeof(Brush), typeof(CommonAssist), new PropertyMetadata(null));

    public static Brush? GetCheckedBackground(DependencyObject obj)
        => (Brush?)obj.GetValue(CheckedBackgroundProperty);

    public static void SetCheckedBackground(DependencyObject obj, Brush? value)
        => obj.SetValue(CheckedBackgroundProperty, value);

    public static readonly DependencyProperty CheckedBackgroundProperty =
        DependencyProperty.RegisterAttached("CheckedBackground", typeof(Brush), typeof(CommonAssist), new PropertyMetadata(null));

    public static bool GetIsCheckedBackground(DependencyObject obj)
        => (bool)obj.GetValue(IsCheckedBackgroundProperty);

    public static void SetIsCheckedBackground(DependencyObject obj, bool value)
        => obj.SetValue(IsCheckedBackgroundProperty, value);

    public static readonly DependencyProperty IsCheckedBackgroundProperty =
        DependencyProperty.RegisterAttached("IsCheckedBackground", typeof(bool), typeof(CommonAssist), new PropertyMetadata(true));

    public static bool GetIsMouseOverBackground(DependencyObject obj)
        => (bool)obj.GetValue(IsMouseOverBackgroundProperty);

    public static void SetIsMouseOverBackground(DependencyObject obj, bool value)
        => obj.SetValue(IsMouseOverBackgroundProperty, value);

    public static readonly DependencyProperty IsMouseOverBackgroundProperty =
        DependencyProperty.RegisterAttached("IsMouseOverBackground", typeof(bool), typeof(CommonAssist), new PropertyMetadata(true));
    public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
        "Text", typeof(string), typeof(CommonAssist), new PropertyMetadata(default(string)));

    public static void SetText(DependencyObject element, string value)
        => element.SetValue(TextProperty, value);

    public static string GetText(DependencyObject element)
        => (string)element.GetValue(TextProperty);

    public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.RegisterAttached(
        "PlaceholderText", typeof(string), typeof(CommonAssist), new PropertyMetadata(default(string)));

    public static void SetPlaceholderText(DependencyObject element, string value)
        => element.SetValue(PlaceholderTextProperty, value);

    public static string GetPlaceholderText(DependencyObject element)
        => (string)element.GetValue(PlaceholderTextProperty);
}