// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// PaginationPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Data pagination display.", SymbolRegular.DocumentPageBreak20)]
public partial class PaginationPage : INavigableView<PaginationPageViewModel>
{
    public PaginationPageViewModel ViewModel { get; }

    public PaginationPage(PaginationPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}