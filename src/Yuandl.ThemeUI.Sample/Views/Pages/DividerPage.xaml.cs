// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// DividerPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("A lightweight Diver widget.", SymbolRegular.MoreHorizontal24)]
public partial class DividerPage : INavigableView<DividerPageViewModel>
{
    public DividerPageViewModel ViewModel { get; }

    public DividerPage(DividerPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}