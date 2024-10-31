// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Helpers;

namespace Yuandl.ThemeUI.Sample.Helper;

public static class WindowHelper
{

    public static void CreateMask(this Window outputWindow)
    {
        Visual parent = ControlsHelper.GetDefaultWindow();
        System.Windows.Documents.AdornerLayer _layer = ControlsHelper.GetAdornerLayer(parent);
        if (_layer == null)
        {
            return;
        }

        var adornerContainer = new AdornerContainer(_layer)
        {
            Child = new MaskControl(parent) { Background = Brushes.Black }
        };
        _layer.Add(adornerContainer);
        if (outputWindow != null)
        {
            outputWindow.Closed += (sender, e) =>
            {
                if (parent != null)
                {
                    _layer.Remove(adornerContainer);
                }
            };
        }
    }
}