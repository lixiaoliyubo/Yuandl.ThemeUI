// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

public class MaskControl : ContentControl
{
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MaskControl),
            new PropertyMetadata(new CornerRadius(0)));

    private readonly Visual visual;

    static MaskControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(MaskControl),
            new FrameworkPropertyMetadata(typeof(MaskControl)));
    }

    public MaskControl(Visual _visual)
    {
        visual = _visual;
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}