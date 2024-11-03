// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Microsoft.Extensions.Logging;
using Yuandl.ThemeUI.Sample.ViewModels.Pages;
using static System.Net.Mime.MediaTypeNames;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// IconsPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("aLL Icons.", SymbolRegular.Icons24)]
public partial class IconsPage : INavigableView<IconsPageViewModel>
{
    private readonly ISnackbarService _snackbarService;

    public IconsPageViewModel ViewModel { get; }

    public IconsPage(IconsPageViewModel viewModel, ISnackbarService snackbarService)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
        _snackbarService = snackbarService;
    }

    private void TextBlock_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        System.Windows.Controls.TextBlock textBlock = (System.Windows.Controls.TextBlock)sender;

        try
        {
            Clipboard.SetText(textBlock.Text);
            _snackbarService.Show("友情提示", "复制成功", ControlAppearance.Primary);
        }
        catch (Exception ex)
        {
            _snackbarService.Show("友情提示", "复制时发生错误："+ ex.Message, ControlAppearance.Primary);
        }
        finally
        {
            Clipboard.Flush();
        }
    }
}