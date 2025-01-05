// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Controls;
using Yuandl.ThemeUI.Mvvm.ViewModels;

namespace Yuandl.ThemeUI.Mvvm.Views;

/// <summary>
/// DashboardPage.xaml 的交互逻辑
/// </summary>
public partial class CachePage : INavigableView<CachePageViewModel>
{
    public CachePageViewModel ViewModel { get; }

    public CachePage(CachePageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }

    private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {

    }

    private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e)
    {

    }
}
