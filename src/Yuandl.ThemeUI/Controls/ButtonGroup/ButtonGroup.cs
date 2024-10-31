// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Controls.Primitives;

namespace Yuandl.ThemeUI.Controls;

public class ButtonGroup : ItemsControl
{
    protected override bool IsItemItsOwnContainerOverride(object item) => item is Button or RadioButton or ToggleButton;

    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
        nameof(Orientation), typeof(Orientation), typeof(ButtonGroup), new PropertyMetadata(default(Orientation)));

    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    public static readonly DependencyProperty LayoutProperty = DependencyProperty.Register(
        nameof(Layout), typeof(LinearLayout), typeof(ButtonGroup), new PropertyMetadata(LinearLayout.Uniform));

    public LinearLayout Layout
    {
        get => (LinearLayout)GetValue(LayoutProperty);
        set => SetValue(LayoutProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, (object)value);
    }

    /// <summary>Identifies the <see cref="CornerRadius"/> dependency property.</summary>
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(ButtonGroup),
        new FrameworkPropertyMetadata(
            default(CornerRadius),
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender
        )
    );

    protected override void OnRender(DrawingContext drawingContext)
    {
        var count = Items.Count;
        for (var i = 0; i < count; i++)
        {
            var item = (ButtonBase)Items[i];
            item.Style = ItemContainerStyleSelector?.SelectStyle(item, this);
        }
    }
}