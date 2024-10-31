// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// InfoBarPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Inline message card.", SymbolRegular.ErrorCircle24)]
public partial class InfoBarPage : INavigableView<InfoBarPageViewModel>
{
    public InfoBarPageViewModel ViewModel { get; }

    public InfoBarPage(InfoBarPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }

    private void InfoBar_Closed(InfoBar sender, InfoBarButtonClickEventArgs args)
    {
        Notification.SuccessGlobal("关闭后事件");
    }

    private void InfoBar_Closing(InfoBar sender, InfoBarButtonClickEventArgs args)
    {
        Notification.SuccessGlobal("关闭前事件");
    }
}