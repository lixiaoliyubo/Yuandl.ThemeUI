// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// PinBoxPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("A password input control.", SymbolRegular.Password20)]
public partial class PinBoxPage : INavigableView<PinBoxPageViewModel>
{
    public PinBoxPageViewModel ViewModel { get; }

    public PinBoxPage(PinBoxPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }

    private void NumberBox_ValueChanged(object sender, RoutedEventArgs e)
    {
        NumberBox numberBox = (NumberBox)sender;
        if (pinBox != null)
        {
            pinBox.PasswordLength = Convert.ToInt32(numberBox.Value);
        }
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Slider numberBox = (Slider)sender;

        if (pinBox != null)
        {
            pinBox.ItemSpacing = e.NewValue;
        }
    }
}