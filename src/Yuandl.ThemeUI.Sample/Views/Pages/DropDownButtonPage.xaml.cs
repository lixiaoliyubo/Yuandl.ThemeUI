// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// DropDownButtonPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Button with drop down.", SymbolRegular.Filter16)]
public partial class DropDownButtonPage : INavigableView<DropDownButtonPageViewModel>
{
    public DropDownButtonPageViewModel ViewModel { get; }

    public DropDownButtonPage(DropDownButtonPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}