// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Windows.Input;

namespace Yuandl.ThemeUI.Controls;

public static class ControlCommands
{
    /// <summary>
    ///     Gets 关闭所有
    /// </summary>
    public static RoutedCommand CloseAll { get; } = new(nameof(CloseAll), typeof(ControlCommands));

    /// <summary>
    ///     Gets 关闭其他
    /// </summary>
    public static RoutedCommand CloseOther { get; } = new(nameof(CloseOther), typeof(ControlCommands));

    public static RoutedCommand Close { get; } = new (nameof(Close), typeof(ControlCommands));

    public static RoutedCommand Toggle { get; } = new (nameof(Toggle), typeof(ControlCommands));
}
