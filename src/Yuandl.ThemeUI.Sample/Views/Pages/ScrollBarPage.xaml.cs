// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// ScrollBarPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Page All Scroll.", SymbolRegular.DualScreenVerticalScroll24)]
public partial class ScrollBarPage : INavigableView<ScrollBarPageViewModel>
{
    public ScrollBarPageViewModel ViewModel { get; }

    public ScrollBarPage(ScrollBarPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}