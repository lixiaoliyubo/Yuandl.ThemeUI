// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Controls;

public interface IAppearanceControl
{
    /// <summary>
    /// Gets or sets the <see cref="Appearance"/> of the control, if available.
    /// </summary>
    public Enums.ControlAppearance Appearance { get; set; }
}