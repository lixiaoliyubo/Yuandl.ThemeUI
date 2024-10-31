// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Yuandl.ThemeUI.Controls;

public class GridViewColumnThumb : Thumb
{
    public GridViewColumnThumb()
        : base() { }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.SizeWE;
        base.OnMouseLeftButtonDown(e);
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);
        Mouse.OverrideCursor = null;
    }
}