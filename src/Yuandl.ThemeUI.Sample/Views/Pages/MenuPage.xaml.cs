// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// MenuPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Contains a collection of MenuItem elements.", SymbolRegular.RowTriple24)]
public partial class MenuPage : INavigableView<MenuPageViewModel>
{
    public MenuPageViewModel ViewModel { get; }

    public MenuPage(MenuPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}