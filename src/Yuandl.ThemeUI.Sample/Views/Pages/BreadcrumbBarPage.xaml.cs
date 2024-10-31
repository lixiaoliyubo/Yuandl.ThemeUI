// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// BreadcrumbBarPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Shows the trail of navigation taken to the current location.", SymbolRegular.Navigation24)]
public partial class BreadcrumbBarPage : INavigableView<BreadcrumbBarPageViewModel>
{
    public BreadcrumbBarPageViewModel ViewModel { get; }

    public BreadcrumbBarPage(BreadcrumbBarPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}