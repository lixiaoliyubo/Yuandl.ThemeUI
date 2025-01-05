// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.ComponentModel;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yuandl.ThemeUI.Controls;
using Yuandl.ThemeUI.Mvvm.ViewModels;
using Yuandl.ThemeUI.Services;

namespace Yuandl.ThemeUI.Mvvm;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : IWindow
{

    // 事件服务
    private readonly IServiceProvider _serviceProvider;
    private readonly ISnackbarService _snackbarService;
    private readonly IContentDialogService _contentDialogService;


    public MainWindowViewModel ViewModel { get; }


    public MainWindow(
            MainWindowViewModel viewModel,
            IServiceProvider serviceProvider,
            ISnackbarService snackbarService,
            INavigationService navigationService,
            IContentDialogService contentDialogService
        )
    {
        Title = viewModel.PageTitle;
        DataContext = ViewModel = viewModel;

        Closing += MainWindow_Closing;
        Closed += MainWindow_Closed;
        Loaded += MainWindow_Loaded;
        Deactivated += MainWindow_Deactivated;
        _serviceProvider = serviceProvider;
        _snackbarService = snackbarService;
        _contentDialogService = contentDialogService;
        //Bootstrapper.InitializeContainer(this);

        InitializeComponent();
        navigationService.SetNavigationControl(RootNavigation);
        RootNavigation.SetServiceProvider(_serviceProvider);
        _snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        _contentDialogService.SetDialogHost(RootContentDialog);
    }

    private void MainWindow_Closed(object? sender, EventArgs e)
    {
    }

    private void MainWindow_Closing(object? sender, CancelEventArgs e)
    {
        this.Hide();
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
    }
    /// <summary>
    /// 丢失焦点事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainWindow_Deactivated(object? sender, EventArgs e)
    {
        //丢失焦点时主界面不隐藏
        bool StayMainViewWhenLoseFocus = true;
        if (Topmost || StayMainViewWhenLoseFocus) return;

    }
    private void BackBtn_Click(object sender, RoutedEventArgs e)
    {
        GoBack();
    }
    private void Reload_Click(object sender, RoutedEventArgs e)
    {
        Refresh();
    }

    private void Flyout_Closed(Flyout sender, RoutedEventArgs args)
    {
        ViewModel.IsFlyoutOpen = false;
    }

    public void GoBack()
    {
        _ = RootNavigation.GoBack();
    }
    public void Refresh()
    {
        _ = RootNavigation.Refresh();
    }

    public bool Navigate(Type pageType)
    {
        return RootNavigation.Navigate(pageType);
    }

    private string GetAssemblyVersion()
    {
        return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            ?? string.Empty;
    }

    private void RestartBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    public void Close(bool isClose = true)
    {
        this.Close();
    }
}