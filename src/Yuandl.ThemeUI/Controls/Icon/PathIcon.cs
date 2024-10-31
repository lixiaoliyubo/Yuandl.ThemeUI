// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

public class PathIcon : FontIcon
{
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register(nameof(Data), typeof(Geometry), typeof(PathIcon));

    public Geometry Data
    {
        get { return (Geometry)GetValue(DataProperty); }
        set { SetValue(DataProperty, value); }
    }

    static PathIcon()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PathIcon), new FrameworkPropertyMetadata(typeof(PathIcon)));
    }
}