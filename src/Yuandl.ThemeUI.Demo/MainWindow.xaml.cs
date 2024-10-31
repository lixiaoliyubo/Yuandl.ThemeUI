// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Diagnostics;
using System.Windows;
using Yuandl.ThemeUI.Controls;
using Yuandl.ThemeUI.Demo.ViewModels;

namespace Yuandl.ThemeUI.Demo;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : INavigableView<MainWindowViewModel>
{
    public MainWindowViewModel ViewModel { get; }

    public MainWindow()
    {
        DataContext = ViewModel = new MainWindowViewModel();
        InitializeComponent();
    }
    private void SidebarMenu_OnClosed(SidebarMenu sender, RoutedEventArgs args)
    {
        Debug.WriteLine($"SidebarMenu_OnClosed");
    }

    private void SidebarMenu_OnOpened(SidebarMenu sender, RoutedEventArgs args)
    {
        Debug.WriteLine($"SidebarMenu_OnOpened");
    }

    private void SidebarMenu_ItemClick(SidebarMenu? sender, RoutedEventArgs args)
    {
        Debug.WriteLine($"SidebarMenu_ItemInvoked={sender?.SelectedItem?.PageTag}");
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        _ = RootNavigation.Navigate(ViewModel.MenuItems.First());
    }
}