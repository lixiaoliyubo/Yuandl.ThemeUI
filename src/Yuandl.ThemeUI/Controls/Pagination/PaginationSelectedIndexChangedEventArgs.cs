// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

// </summary>
public sealed class PaginationSelectedIndexChangedEventArgs : RoutedEventArgs
{
    public PaginationSelectedIndexChangedEventArgs(RoutedEvent eventArgs, object sender)
        : base(eventArgs, sender) { }

    public PaginationSelectedIndexChangedEventArgs(int newPageIndex, int previousPageIndex)
    {
        NewPageIndex = newPageIndex;
        PreviousPageIndex = previousPageIndex;
    }

    public int NewPageIndex { get; set; }

    public int PreviousPageIndex { get; set; }
}