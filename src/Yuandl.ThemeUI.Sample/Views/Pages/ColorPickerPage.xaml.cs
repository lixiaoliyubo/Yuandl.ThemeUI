// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Diagnostics;
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// ColorPickerPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("ColorPicker control.", SymbolRegular.Color24)]
public partial class ColorPickerPage : INavigableView<ColorPickerPageViewModel>
{
    public ColorPickerPageViewModel ViewModel { get; }

    public ColorPickerPage(ColorPickerPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
    }

    private void ColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color> e)
    {
        Debug.WriteLine($"NewValue：{e.NewValue}，OldValue：{e.OldValue}，时间：{DateTime.Now}");
    }
}