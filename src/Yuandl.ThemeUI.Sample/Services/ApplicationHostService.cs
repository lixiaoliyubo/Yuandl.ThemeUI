// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yuandl.ThemeUI.Sample;
using Yuandl.ThemeUI.Sample.Views.Pages;

namespace Yuandl.ThemeUI.Services;

/// <summary>
/// Managed host of the application.
/// </summary>
public class ApplicationHostService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly INavigationService _navigationService;
    private readonly IThemeService _themeService;

    public ApplicationHostService(IServiceProvider serviceProvider, INavigationService navigationService, IThemeService themeService)
    {
        _serviceProvider = serviceProvider;
        _navigationService = navigationService;
        _themeService = themeService;
    }

    /// <summary>
    /// Triggered when the application host is ready to start the service.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await HandleActivationAsync();
    }

    /// <summary>
    /// Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// Creates main window during activation.
    /// </summary>
    private Task HandleActivationAsync()
    {
        if (Application.Current.Windows.OfType<MainWindow>().Any())
        {
            return Task.CompletedTask;
        }

        IWindow mainWindow = _serviceProvider.GetRequiredService<IWindow>();
        mainWindow.Loaded += OnMainWindowLoaded;
        mainWindow?.Show();

        return Task.CompletedTask;
    }

    private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is not IWindow window)
        {
            return;
        }

        window.Navigate(typeof(HomePage));
    }
}