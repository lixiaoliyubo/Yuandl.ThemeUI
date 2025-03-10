// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// CheckBoxPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Button with binary choice.", SymbolRegular.CheckmarkSquare24)]
public partial class CheckBoxPage : INavigableView<CheckBoxPageViewModel>
{
    public CheckBoxPageViewModel ViewModel { get; }

    public CheckBoxPage(CheckBoxPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}