// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Extensions;

internal static class ControlExtensions
{
    public static void Show(this ContextMenu menu, bool show)
    {
        if (show)
        {
            menu.Visibility = Visibility.Visible;
            menu.IsEnabled = true;
        }
        else
        {
            menu.Visibility = Visibility.Collapsed;
            menu.IsEnabled = false;
        }
    }
}
