// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// FlyoutPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Contextual popup.", SymbolRegular.AppTitle24)]
public partial class FlyoutPage : INavigableView<FlyoutPageViewModel>
{
    public FlyoutPageViewModel ViewModel { get; }

    public FlyoutPage(FlyoutPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}