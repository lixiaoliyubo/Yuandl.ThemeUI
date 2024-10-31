// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// DataGridPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Data Display of GridGrid.", SymbolRegular.DocumentDatabase20)]
public partial class DataGridPage : INavigableView<DataGridPageViewModel>
{
    public DataGridPageViewModel ViewModel { get; }

    public DataGridPage(DataGridPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}