// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Enums;

/// <summary>
/// 指定剩余空间的分布方式。
/// <para>基于 <see href="https://github.com/sbaeumlisberger/VirtualizingWrapPanel"/> 。</para>
/// </summary>
public enum SpacingMode
{
    /// <summary>
    /// 间距被禁用，所有项目将尽可能紧密排列。
    /// </summary>
    None,

    /// <summary>
    /// 每行上的项目之间以及每行的开头和结尾均匀分布剩余空间。
    /// </summary>
    Uniform,

    /// <summary>
    /// 剩余空间仅在每行的项目之间均匀分布，不包括每行的开头和结尾。
    /// </summary>
    BetweenItemsOnly,

    /// <summary>
    /// 剩余空间仅在每行的开头和结尾均匀分布。
    /// </summary>
    StartAndEndOnly
}