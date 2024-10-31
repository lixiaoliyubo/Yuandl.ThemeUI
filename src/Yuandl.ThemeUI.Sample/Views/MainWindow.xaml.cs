// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Diagnostics;
using System.Windows.Input;
using Yuandl.ThemeUI.Sample.ViewModels;

namespace Yuandl.ThemeUI.Sample;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : IWindow
{
    private readonly IServiceProvider _serviceProvider;

    public MainWindowViewModel ViewModel { get; set; }

    public MainWindow(
        MainWindowViewModel viewModel,
        IServiceProvider serviceProvider,
        ISnackbarService snackbarService,
        IContentDialogService contentDialogService)
    {
        _serviceProvider = serviceProvider;
        DataContext = ViewModel = viewModel;
        InitializeComponent();
        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        contentDialogService.SetDialogHost(RootContentDialog);
    }

    private void OnCopy(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Parameter is string stringValue)
        {
            try
            {
                Clipboard.SetDataObject(stringValue);
                Notification.Success("友情提示", "复制成功");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }
    }

    public void GoBack()
    {
        RootNavigation.GoBack();
    }

    public void Navigate(Type pageType)
    {
        object content = _serviceProvider.GetService(pageType);

        _ = RootNavigation.Navigate(content);
    }
}