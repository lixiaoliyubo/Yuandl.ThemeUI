// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// UniformGridPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("UniformGrid layout control.", SymbolRegular.Grid28)]
public partial class UniformGridPage : INavigableView<UniformGridPageViewModel>
{
    public UniformGridPageViewModel ViewModel { get; }

    public UniformGridPage(UniformGridPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}