// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;
using System.Windows.Input;
using Yuandl.ThemeUI.Sample.ViewModels;

namespace Yuandl.ThemeUI.Sample.Models;

public class MenuItemData : ObservableObject
{
    public IconElement Icon { get; set; }

    public string Name { get; set; }

    public ICommand Command { get; set; }

    public ObservableCollection<MenuItemData> SubItems { get; set; } = new ObservableCollection<MenuItemData>();
}