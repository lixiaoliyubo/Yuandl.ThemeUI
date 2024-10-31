// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;
using Yuandl.ThemeUI.Sample.Models;

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class MenuPageViewModel : ObservableObject
{
    [ObservableProperty]
    public ObservableCollection<MenuItemData> _menuItems = new ObservableCollection<MenuItemData>();

    public MenuPageViewModel()
    {
        string[] menuValue = new string[]
        {
            "File",
            "Edit",
            "View",
            "Tools",
            "Help",
            "Settings",
            "About",
            "New Document",
            "Open Project",
            "Exit"
        };

        // 初始化菜单项集合
        MenuItems = new ObservableCollection<MenuItemData>() { };
        foreach (var item in menuValue)
        {
            ObservableCollection<MenuItemData> SubItems = new ObservableCollection<MenuItemData>();
            foreach (var sub in menuValue)
            {
                SubItems.Add(new() { Command = new RelayCommand(() => OnGoCommand(item)), Name = sub });
            }

            MenuItems.Add(new() { Command = new RelayCommand(() => OnGoCommand(item)), Name = item, SubItems = SubItems, Icon = new SymbolIcon(SymbolRegular.Accessibility32) });
        }
    }

    private bool OnGoCommand(object obj)
    {
        // 处理菜单项点击事件
        _ = System.Windows.MessageBox.Show($"Clicked: {obj}");
        return true;
    }
}
