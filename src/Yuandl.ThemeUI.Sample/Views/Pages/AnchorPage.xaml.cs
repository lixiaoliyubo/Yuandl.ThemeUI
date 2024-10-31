// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// AnchorPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Button which opens a link.", SymbolRegular.CubeLink20)]
public partial class AnchorPage : INavigableView<AnchorPageViewModel>
{
    public AnchorPageViewModel ViewModel { get; }

    public AnchorPage(AnchorPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}