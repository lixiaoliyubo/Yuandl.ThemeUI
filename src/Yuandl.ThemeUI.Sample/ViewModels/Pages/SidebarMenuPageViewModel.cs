// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using Yuandl.ThemeUI.Sample.Views.Pages;

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class SidebarMenuPageViewModel : ObservableObject
{
    public SidebarMenuPageViewModel()
    {
        MenuItems1 = LoadData();
        MenuItems2 = LoadData();
    }

    [ObservableProperty]
    private ObservableCollection<object> _menuItems1 = new ObservableCollection<object> { };
    [ObservableProperty]
    private ObservableCollection<object> _menuItems2 = new ObservableCollection<object> { };

    private ObservableCollection<object> LoadData()
    {
        return
        [
            new PageViewItem("Home", SymbolRegular.Home24, typeof(HomePage)),
            new PageViewItem()
            {
                Content = "Design guidance",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DesignIdeas24 },
                ItemsSource = new object[]
                {
                    new PageViewItem("Icons", SymbolRegular.Diversity24, typeof(IconsPage)),
                }
            },
            new PageViewItem("All samples", SymbolRegular.List24, typeof(HomePage)),
            new PageViewItem("Basic Input", SymbolRegular.CheckboxChecked24, typeof(HomePage))
            {
                ItemsSource = new object[]
                {
                    new PageViewItem(nameof(Anchor), typeof(AnchorPage)),
                    new PageViewItem(nameof(Yuandl.ThemeUI.Controls.Button), typeof(ButtonPage)),
                    new PageViewItem(nameof(DropDownButton), typeof(DropDownButtonPage)),
                    new PageViewItem(nameof(HyperlinkButton), typeof(HyperlinkButtonPage)),
                    new PageViewItem(nameof(ToggleButton), typeof(ToggleButtonPage)),
                    new PageViewItem(nameof(ToggleSwitch), typeof(ToggleSwitchPage)),
                    new PageViewItem(nameof(CheckBox), typeof(CheckBoxPage)),
                    new PageViewItem(nameof(ComboBox), typeof(ComboBoxPage)),
                    new PageViewItem(nameof(System.Windows.Controls.RadioButton), typeof(RadioButtonPage)),
                    new PageViewItem(nameof(Slider), typeof(SliderPage)),
                }
            },
            new PageViewItem
            {
                Content = "Collections",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Table24 },
                PageType = typeof(HomePage),
                ItemsSource = new object[]
                {
                    new PageViewItem(nameof(System.Windows.Controls.DataGrid), typeof(DataGridPage)),
                    new PageViewItem(nameof(ListBox), typeof(ListBoxPage)),
                    new PageViewItem(nameof(TreeView), typeof(TreePage)),
                }
            },
        ];
    }
}