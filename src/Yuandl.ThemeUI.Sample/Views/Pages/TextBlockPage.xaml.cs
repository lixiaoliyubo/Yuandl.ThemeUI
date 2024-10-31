// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// TextBlockPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Control for displaying text.", SymbolRegular.TextCaseLowercase24)]
public partial class TextBlockPage : INavigableView<TextBlockPageViewModel>
{
    public TextBlockPageViewModel ViewModel { get; }

    public TextBlockPage(TextBlockPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}