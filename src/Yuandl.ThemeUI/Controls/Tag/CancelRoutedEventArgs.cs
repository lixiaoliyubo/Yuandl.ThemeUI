// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

public class CancelRoutedEventArgs : RoutedEventArgs
{
    public CancelRoutedEventArgs(RoutedEvent routedEvent, object source)
        : base(routedEvent, source)
    {
    }

    public bool Cancel { get; set; }
}