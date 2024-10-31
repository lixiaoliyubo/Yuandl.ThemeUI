// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// RadioButtonPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Set of options as buttons.", SymbolRegular.RadioButton24)]
public partial class RadioButtonPage : INavigableView<RadioButtonPageViewModel>
{
    public RadioButtonPageViewModel ViewModel { get; }

    public RadioButtonPage(RadioButtonPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}