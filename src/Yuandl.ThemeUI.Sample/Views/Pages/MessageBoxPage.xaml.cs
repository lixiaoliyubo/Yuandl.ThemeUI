// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// MessageBoxPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Message box.", SymbolRegular.CalendarInfo20)]
public partial class MessageBoxPage : INavigableView<MessageBoxPageViewModel>
{
    public MessageBoxPageViewModel ViewModel { get; }

    public MessageBoxPage(MessageBoxPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}