// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// ToggleSwitchPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Switchable button with a ball.", SymbolRegular.ToggleLeft24)]
public partial class ToggleSwitchPage : INavigableView<ToggleSwitchPageViewModel>
{
    public ToggleSwitchPageViewModel ViewModel { get; }

    public ToggleSwitchPage(ToggleSwitchPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}