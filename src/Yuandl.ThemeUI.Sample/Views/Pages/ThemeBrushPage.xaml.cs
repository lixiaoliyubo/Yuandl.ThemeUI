// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Reflection;
using Yuandl.ThemeUI.Sample.Models;
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// ThemeBrushPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("System brush.", SymbolRegular.CubeLink20)]
public partial class ThemeBrushPage : Page, INavigableView<ThemeBrushPageViewModel>
{
    public CancellationTokenSource? Token = null;

    public ThemeBrushPageViewModel ViewModel { get; }

    public ThemeBrushPage(ThemeBrushPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        Loaded += Page_Loaded;
        Unloaded += Page_Unloaded;
        InitializeComponent();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        Token = new CancellationTokenSource();
        _ = Task.Run(async () =>
        {
            while (true)
            {
                if (Token?.Token.IsCancellationRequested ?? false)
                {
                    break;
                }

                this.Dispatcher.Invoke(() =>
                {
                    ViewModel.Items.Clear();
                    LoadButtons();
                });
                await Task.Delay(1000);
            }
        });
    }

    private void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        Token?.Cancel();
    }

    private void LoadButtons()
    {
        Type themeResourceType = typeof(ThemeResource);
        FieldInfo[] fields = themeResourceType.GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (FieldInfo field in fields)
        {
            if (field.Name.Contains("Elevation"))
            {
                ViewModel.Items.Add(new ThemeColorEntity
                {
                    Name = field.Name,
                    ColorType = 4,
                    ColorBrush = (Brush)UiApplication.Current.Resources[field.Name]
                });
            }
            else if (field.Name.EndsWith("Color"))
            {
                var type = field.Name.StartsWith("Application") ? 1 : field.Name.StartsWith("SystemAccent") ? 2 : field.Name.StartsWith("Control") ? 3 : 0;

                ViewModel.Items.Add(new ThemeColorEntity
                {
                    Name = field.Name,
                    ColorType = type,
                    ColorBrush = new SolidColorBrush((Color)UiApplication.Current.Resources[field.Name])
                });
            }
        }
    }
}