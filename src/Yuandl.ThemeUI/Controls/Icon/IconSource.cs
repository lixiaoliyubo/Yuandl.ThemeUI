// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

public abstract class IconSource : DependencyObject
{
    /// <summary>Identifies the <see cref="Foreground"/> dependency property.</summary>
    public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
        nameof(Foreground),
        typeof(Brush),
        typeof(IconSource),
        new FrameworkPropertyMetadata(SystemColors.ControlTextBrush)
    );

    /// <inheritdoc cref="Control.Foreground"/>
    public Brush Foreground
    {
        get => (Brush)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }

    public abstract IconElement CreateIconElement();
}