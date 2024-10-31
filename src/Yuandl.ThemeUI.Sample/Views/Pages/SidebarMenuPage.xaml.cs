// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Diagnostics;
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// SidebarMenuPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Data display of SidebarMenu..", SymbolRegular.NavigationLocationTarget20)]
public partial class SidebarMenuPage : INavigableView<SidebarMenuPageViewModel>
{
    public SidebarMenuPageViewModel ViewModel { get; }

    public SidebarMenuPage(SidebarMenuPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }

    private void SidebarMenu_OnPaneClosed(SidebarMenu sender, RoutedEventArgs args)
    {
        Debug.WriteLine($"SidebarMenu_OnPaneClosed");
    }

    private void SidebarMenu_OnPaneOpened(SidebarMenu sender, RoutedEventArgs args)
    {
        Debug.WriteLine($"SidebarMenu_OnPaneOpened");
    }

    private void SidebarMenu_ItemClick(SidebarMenu? sender, RoutedEventArgs args)
    {
        Debug.WriteLine($"SidebarMenu_ItemInvoked={sender?.SelectedItem?.PageTag}");
    }
}