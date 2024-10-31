// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// DatePickerPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Control that lets pick a date.", SymbolRegular.CalendarSearch20)]
public partial class DatePickerPage : INavigableView<DatePickerPageViewModel>
{
    public DatePickerPageViewModel ViewModel { get; }

    public DatePickerPage(DatePickerPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}