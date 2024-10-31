// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// Provides event data for the <see cref="AutoSuggestBox.QuerySubmitted"/> event.
/// </summary>
public sealed class AutoSuggestBoxQuerySubmittedEventArgs : RoutedEventArgs
{
    public AutoSuggestBoxQuerySubmittedEventArgs(RoutedEvent eventArgs, object sender)
        : base(eventArgs, sender) { }

    public required string QueryText { get; init; }
}