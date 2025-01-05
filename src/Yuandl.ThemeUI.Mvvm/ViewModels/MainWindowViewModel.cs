// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Yuandl.ThemeUI.Appearance;
using Yuandl.ThemeUI.Controls;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Managers;

namespace Yuandl.ThemeUI.Mvvm.ViewModels;

public partial class MainWindowViewModel: ViewModel
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private string pageTitle = "源动力开发工具";

    [ObservableProperty]
    public bool _isFlyoutOpen;

    [ObservableProperty]
    private ObservableCollection<PageViewItem> navigationItems = [];

    public Color _systemColor;
    public Color SystemColor
    {
        get => _systemColor;
        set
        {

            ApplicationAccentColorManager.Apply(value);

            _ = SetProperty(ref _systemColor, value);
        }
    }
    public MainWindowViewModel()
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
        }
    }
    private void InitializeViewModel()
    {
        SystemColor = ApplicationAccentColorManager.GetSystemAccent();
        ObservableCollection<PageViewItem> pages = new ObservableCollection<PageViewItem>();
        pages.Add(new PageViewItem()
        {

            Name = string.Empty,
            Content = "Home",
            ToolTip = "首页",
            CacheMode = NavigationCacheMode.Enabled,
            Icon = new SymbolIcon { Symbol = Enums.SymbolRegular.Home24 },
            PageType = typeof(Views.HomePage)
        });
        pages.Add(new PageViewItem()
        {
            Name = string.Empty,
            Content = "Settings",
            ToolTip = "设置",
            CacheMode = NavigationCacheMode.Enabled,
            Icon = new SymbolIcon { Symbol = Enums.SymbolRegular.Settings24 },
            PageType = typeof(Views.SettingsPage)
        });
        pages.Add(new PageViewItem()
        {
            Name = string.Empty,
            Content = "CachePage",
            ToolTip = "缓存页",
            CacheMode = NavigationCacheMode.Enabled,
            Icon = new SymbolIcon { Symbol = Enums.SymbolRegular.Database24 },
            PageType = typeof(Views.CachePage)
        });

        NavigationItems = new ObservableCollection<PageViewItem>(pages);

        _isInitialized = true;
    }

    [RelayCommand]
    private void OnResetFolders()
    {
        OnPropertyChanged(nameof(NavigationItems));
    }

    [RelayCommand]
    private void OnToggleThemeButton()
    {
        ApplicationTheme currentTheme = ApplicationThemeManager.GetAppTheme();

        ApplicationThemeManager.Apply(
            currentTheme == ApplicationTheme.Light ? ApplicationTheme.Dark : ApplicationTheme.Light
        );
    }

    [RelayCommand]
    private void OnColorPickerButton()
    {
        IsFlyoutOpen = true;
    }
}
