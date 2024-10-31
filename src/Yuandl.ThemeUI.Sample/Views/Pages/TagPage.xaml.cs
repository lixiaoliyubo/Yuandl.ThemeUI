// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// TagPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Used for marking and selection.", SymbolRegular.Tag24)]
public partial class TagPage : INavigableView<TagPageViewModel>
{
    public TagPageViewModel ViewModel { get; }

    public TagPage(TagPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}