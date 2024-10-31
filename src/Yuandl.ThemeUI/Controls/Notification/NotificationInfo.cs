// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.Controls;

public class NotificationInfo
{
    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string DateTime { get; set; } = string.Empty;

    public string ConfirmButtonText { get; set; } = "Confirm";

    public string CloseButtonText { get; set; } = "Close";

    public string Token { get; set; } = string.Empty;

    public object Content { get; set; } = string.Empty;

    public ControlAppearance Appearance { get; set; }

    public SymbolRegular IconSource { get; set; }

    public bool IsClosable { get; set; } = true;

    public bool ShowCloseButton { get; set; }

    public bool ShowConfirmButton { get; set; }

    public bool ShowDateTime { get; set; } = true;

    public bool StaysOpen { get; set; }

    public Visibility IsIconVisible { get; set; } = Visibility.Collapsed;

    public bool UseBlueColorForInfo { get; set; }

    public TimeSpan WaitTime { get; set; } = TimeSpan.FromSeconds(6);

    public Func<object, RoutedEventArgs, bool>? ConfirmButtonClicked { get; set; }

    public Func<object, RoutedEventArgs, bool>? CloseButtonClicked { get; set; }
}