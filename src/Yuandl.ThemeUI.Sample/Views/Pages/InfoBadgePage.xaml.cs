// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// InfoBadgePage.xaml 的交互逻辑
/// </summary>
[GalleryPage("An non-intrusive UI to display notifications or bring focus to an area", SymbolRegular.Badge20)]
public partial class InfoBadgePage : INavigableView<InfoBadgePageViewModel>
{
    public InfoBadgePageViewModel ViewModel { get; }

    public InfoBadgePage(InfoBadgePageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}