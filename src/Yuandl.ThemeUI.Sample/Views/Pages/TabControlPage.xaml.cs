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

    private void TabControl_Closing(object sender, ClosingEventArgs e)
    {
        Debug.WriteLine($"TabControl_Closing: {e.Item}");
    }

    private void TabControl_Closed(object sender, ClosedEventArgs e)
    {
        Debug.WriteLine($"TabControl_Closed: {e.Item}");
    }

    private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        Debug.WriteLine($"TabControl_SelectionChanged: ");
    }

    private void TabControl_Selected(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine($"TabControl_Add: {((ThemeUI.Controls.TabItem)e.Source).Header}");
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        ApplicationTheme currentTheme = ApplicationThemeManager.GetAppTheme();

        ApplicationThemeManager.Apply(
            currentTheme == ApplicationTheme.Light ? ApplicationTheme.Dark : ApplicationTheme.Light
        );
    }
}