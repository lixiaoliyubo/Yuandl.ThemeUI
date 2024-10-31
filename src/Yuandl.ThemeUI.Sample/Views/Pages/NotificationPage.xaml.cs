// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// NotificationPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Message notification control demonstration.", SymbolRegular.CommentNote24)]
public partial class NotificationPage : INavigableView<NotificationPageViewModel>
{
    public NotificationPageViewModel ViewModel { get; }

    public NotificationPage(NotificationPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}