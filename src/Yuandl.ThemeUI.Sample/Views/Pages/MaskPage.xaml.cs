// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.Helper;
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// MaskPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Button which opens a link.", SymbolRegular.CubeLink20)]
public partial class MaskPage : INavigableView<MaskPageViewModel>
{
    public MaskPageViewModel ViewModel { get; }

    public MaskPage(MaskPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }

    private void MaskShowButton_Click(object sender, RoutedEventArgs e)
    {
        Window parentWindow = App.Current.MainWindow;
        var window = new AboutWindow();
        window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        window.Owner = parentWindow;
        window.CreateMask();
        window.Show();
    }
}