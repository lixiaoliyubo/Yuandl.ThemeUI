// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// Items range.
/// <para>Based on <see href="https://github.com/sbaeumlisberger/VirtualizingWrapPanel"/>.</para>
/// </summary>
public readonly struct ItemRange
{
    public int StartIndex { get; }

    public int EndIndex { get; }

    public ItemRange(int startIndex, int endIndex)
        : this()
    {
        StartIndex = startIndex;
        EndIndex = endIndex;
    }

    public readonly bool Contains(int itemIndex) => itemIndex >= StartIndex && itemIndex <= EndIndex;
}