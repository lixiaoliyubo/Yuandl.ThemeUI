// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// FormItemPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("FormItem form layout control.", SymbolRegular.Grid28)]
public partial class FormItemPage : INavigableView<FormItemPageViewModel>
{
    public FormItemPageViewModel ViewModel { get; }

    public FormItemPage(FormItemPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}