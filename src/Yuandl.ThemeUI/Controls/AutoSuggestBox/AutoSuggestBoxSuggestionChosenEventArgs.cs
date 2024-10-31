// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// Provides data for the <see cref="AutoSuggestBoxSuggestionChosenEventArgs"/> event.
/// </summary>
public sealed class AutoSuggestBoxSuggestionChosenEventArgs : RoutedEventArgs
{
    public AutoSuggestBoxSuggestionChosenEventArgs(RoutedEvent eventArgs, object sender)
        : base(eventArgs, sender) { }

    public required object SelectedItem { get; init; }
}