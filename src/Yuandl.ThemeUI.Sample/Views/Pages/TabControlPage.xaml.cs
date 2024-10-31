// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Diagnostics;
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// TabControlPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("Tab control like in browser.", SymbolRegular.Tab24)]
public partial class TabControlPage : INavigableView<TabControlPageViewModel>
{
    public TabControlPageViewModel ViewModel { get; }

    public TabControlPage(TabControlPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }

    private void TabControl_Closed(ThemeUI.Controls.TabControl sender, TabItemButtonClickEventArgs args)
    {
        if (args.OriginalSource is ThemeUI.Controls.TabItem item)
        {
            Debug.WriteLine(args.Button + $"={item.Header}");
        }
    }
}