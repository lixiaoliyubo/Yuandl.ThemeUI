// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Controls.Primitives;
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// User a colour zone to easily switch the background and foreground colours, from selected Material Design palette or custom ones.
/// </summary>
public class ColorZone : ButtonBase
{
    static ColorZone()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorZone), new FrameworkPropertyMetadata(typeof(ColorZone)));
    }
    ///// <summary>
    ///// Gets or sets background <see cref="Brush"/>.
    ///// </summary>
    // [Bindable(true)]
    // [Category("Appearance")]
    // public Brush MouseOverBackground { get; set; } = new SolidColorBrush(Color.FromArgb(0,0,0,0));

    ///// <summary>
    ///// Gets or sets border <see cref="Brush"/> when the user mouses over the button.
    ///// </summary>
    // [Bindable(true)]
    // [Category("Appearance")]
    // public Brush MouseOverBorderBrush { get; set; } = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));

    ///// <summary>
    ///// Gets or sets the foreground <see cref="Brush"/> when the user clicks the button.
    ///// </summary>
    // [Bindable(true)]
    // [Category("Appearance")]
    // public Brush PressedForeground { get; set; } = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));

    ///// <summary>
    ///// Gets or sets background <see cref="Brush"/> when the user clicks the button.
    ///// </summary>
    // [Bindable(true)]
    // [Category("Appearance")]
    // public Brush PressedBackground { get; set; } = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));

    ///// <summary>
    ///// Gets or sets border <see cref="Brush"/> when the user clicks the button.
    ///// </summary>
    // [Bindable(true)]
    // [Category("Appearance")]
    // public Brush PressedBorderBrush { get; set; } = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
    public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
        nameof(Appearance), typeof(ControlAppearance), typeof(ColorZone), new PropertyMetadata(ControlAppearance.Transparent));

    public ControlAppearance Appearance
    {
        get => (ControlAppearance)GetValue(AppearanceProperty);
        set => SetValue(AppearanceProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius), typeof(CornerRadius), typeof(ColorZone), new PropertyMetadata(default(CornerRadius)));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}