// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.ComponentModel;
using System.Windows;

namespace Yuandl.ThemeUI.Mvvm;

public interface IWindow
{
    event CancelEventHandler Closing;
    event RoutedEventHandler Loaded;
    string Title { get; }
    void Close(bool isClose = true);
    void Show();

    void Refresh();

    void GoBack();

    bool Navigate(Type pageType);
}