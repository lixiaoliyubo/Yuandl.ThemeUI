﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// Control changing its properties or appearance depending on the theme.
/// </summary>
public interface IThemeControl
{
    /// <summary>
    /// Gets the theme that is currently set.
    /// </summary>
    public ApplicationTheme ApplicationTheme { get; }
}