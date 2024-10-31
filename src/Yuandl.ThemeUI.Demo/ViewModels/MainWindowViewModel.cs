// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;
using System.ComponentModel;
using Yuandl.ThemeUI.Controls;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Demo.Pages;
using Yuandl.ThemeUI.Input;

namespace Yuandl.ThemeUI.Demo.ViewModels;
public class MainWindowViewModel: INotifyPropertyChanged
{
    public IRelayCommand ItemClickCommand => new RelayCommand<object>(OnItemClick);
    public MainWindowViewModel()
    {

        MenuItems = LoadData();
    }

    private void OnItemClick(object t)
    {

    }

    private ObservableCollection<PageViewItem> _menuItems = new ObservableCollection<PageViewItem>();
    public ObservableCollection<PageViewItem> MenuItems
    {
        get { return _menuItems; }
        set
        {
            if (null != value)
            {
                _menuItems = value;
                OnPropertyChanged(nameof(MenuItems));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    private ObservableCollection<PageViewItem> LoadData()
    {
        return
        [
            new PageViewItem("Home", SymbolRegular.Home24, typeof(HomePage)),
            new PageViewItem("Settings", SymbolRegular.Settings24,  typeof(SettingsPage)),
            new PageViewItem
            {
                Content = "Collections",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Collections24 },
                PageType = typeof(ChildrenPage),
                ItemsSource = new object[]
                {
                    new  PageViewItem(){
                        Content = "ChildrenPage1",
                        PageType = typeof(ChildrenPage1),
                        Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 }
                    }
                }
            },
        ];
    }
}
