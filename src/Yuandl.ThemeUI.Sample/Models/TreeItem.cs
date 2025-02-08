// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;

namespace Yuandl.ThemeUI.Sample.Models;

public class TreeItem
{
    public string Name { get; set; }

    public List<TreeItem> Children { get; set; } = new List<TreeItem>();
}