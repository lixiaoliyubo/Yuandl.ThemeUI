// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Windows;

namespace Yuandl.ThemeUI.Controls;

public delegate void ClosingEventHandler(object sender, ClosingEventArgs e);
public delegate void ClosedEventHandler(object sender, ClosedEventArgs e);

public class ClosingEventArgs : RoutedEventArgs
{
    public object Item { get; private set; }
    public bool Cancel { get; set; }

    public ClosingEventArgs(object item)
    {
        Item = item;
        Cancel = false;
        RoutedEvent = TabControl.ClosingEvent;
    }
}

public class ClosedEventArgs : RoutedEventArgs
{
    public object Item { get; private set; }

    public ClosedEventArgs(object item)
    {
        Item = item;
        RoutedEvent = TabControl.ClosedEvent;
    }
}