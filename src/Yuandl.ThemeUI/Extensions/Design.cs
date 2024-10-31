// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Markup;

public static class Design
{
    private static readonly string DesignProcessName = "devenv";
    private static bool? _inDesignMode;

    /// <summary>
    /// Gets a value indicating whether the framework is in design-time mode. (Caliburn.Micro implementation)
    /// </summary>
    private static bool InDesignMode =>
        _inDesignMode ??=
            (bool)DependencyPropertyDescriptor
                    .FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement))
                    .Metadata.DefaultValue
            || System
                .Diagnostics.Process.GetCurrentProcess()
                .ProcessName.StartsWith(DesignProcessName, StringComparison.Ordinal);

    public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached(
        "Background",
        typeof(System.Windows.Media.Brush),
        typeof(Design),
        new PropertyMetadata(OnBackgroundChanged)
    );

    public static readonly DependencyProperty ForegroundProperty = DependencyProperty.RegisterAttached(
        "Foreground",
        typeof(System.Windows.Media.Brush),
        typeof(Design),
        new PropertyMetadata(OnForegroundChanged)
    );

    /// <summary>Helper for getting <see cref="BackgroundProperty"/> from <paramref name="dependencyObject"/>.</summary>
    /// <param name="dependencyObject"><see cref="DependencyObject"/> to read <see cref="BackgroundProperty"/> from.</param>
    /// <returns>Background property value.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "WpfAnalyzers.DependencyProperty",
        "WPF0033:Add [AttachedPropertyBrowsableForType]",
        Justification = "Because"
    )]
    public static System.Windows.Media.Brush? GetBackground(DependencyObject dependencyObject) =>
        (System.Windows.Media.Brush)dependencyObject.GetValue(BackgroundProperty);

    /// <summary>Helper for setting <see cref="BackgroundProperty"/> on <paramref name="dependencyObject"/>.</summary>
    /// <param name="dependencyObject"><see cref="DependencyObject"/> to set <see cref="BackgroundProperty"/> on.</param>
    /// <param name="value">Background property value.</param>
    public static void SetBackground(DependencyObject dependencyObject, System.Windows.Media.Brush? value) =>
        dependencyObject.SetValue(BackgroundProperty, value);

    /// <summary>Helper for getting <see cref="ForegroundProperty"/> from <paramref name="dependencyObject"/>.</summary>
    /// <param name="dependencyObject"><see cref="DependencyObject"/> to read <see cref="ForegroundProperty"/> from.</param>
    /// <returns>Foreground property value.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "WpfAnalyzers.DependencyProperty",
        "WPF0033:Add [AttachedPropertyBrowsableForType]",
        Justification = "Because"
    )]
    public static System.Windows.Media.Brush? GetForeground(DependencyObject dependencyObject) =>
        (System.Windows.Media.Brush)dependencyObject.GetValue(ForegroundProperty);

    /// <summary>Helper for setting <see cref="ForegroundProperty"/> on <paramref name="dependencyObject"/>.</summary>
    /// <param name="dependencyObject"><see cref="DependencyObject"/> to set <see cref="ForegroundProperty"/> on.</param>
    /// <param name="value">Foreground property value.</param>
    public static void SetForeground(DependencyObject dependencyObject, System.Windows.Media.Brush? value) =>
        dependencyObject.SetValue(ForegroundProperty, value);

    private static void OnBackgroundChanged(DependencyObject? d, DependencyPropertyChangedEventArgs e)
    {
        if (!InDesignMode)
        {
            return;
        }

        d?.GetType()?.GetProperty("Background")?.SetValue(d, e.NewValue, null);
    }

    private static void OnForegroundChanged(DependencyObject? d, DependencyPropertyChangedEventArgs e)
    {
        if (!InDesignMode)
        {
            return;
        }

        d?.GetType()?.GetProperty("Foreground")?.SetValue(d, e.NewValue, null);
    }
}