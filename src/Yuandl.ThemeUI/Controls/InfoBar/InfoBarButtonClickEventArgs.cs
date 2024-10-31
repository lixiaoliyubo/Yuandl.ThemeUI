// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

// ReSharper disable once CheckNamespace
namespace Yuandl.ThemeUI.Controls;

public class InfoBarButtonClickEventArgs : RoutedEventArgs
{
    public InfoBarButtonClickEventArgs(RoutedEvent routedEvent, object source)
        : base(routedEvent, source) { }

    public required string Button { get; init; }

    public bool Cancel { get; set; }
}