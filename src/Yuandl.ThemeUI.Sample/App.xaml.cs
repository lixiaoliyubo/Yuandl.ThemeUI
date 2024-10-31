// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShowMeTheXAML;
using System.IO;
using System.Reflection;
using System.Windows.Threading;
using Yuandl.ThemeUI.Sample.Extensions;
using Yuandl.ThemeUI.Sample.Services;
using Yuandl.ThemeUI.Sample.ViewModels;

namespace Yuandl.ThemeUI.Sample;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly DateTime S_startTime = DateTime.Now;

    public App()
    {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
    }

    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static readonly IHost _host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.Sources.Clear();
            _ = config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
        })
            .ConfigureLogging((hostingContext, builder) =>
            {
                _ = builder.ClearProviders(); // 清除已配置的其他日志提供程序
                _ = builder.AddConsole(); // 添加控制台日志提供程序

                // 1.过滤掉系统默认的一些日志
                _ = builder.AddFilter("System", LogLevel.Error);
                _ = builder.AddFilter("Microsoft", LogLevel.Error);
                _ = builder.SetMinimumLevel(LogLevel.Error);
            })
        .ConfigureServices(
            (context, services) =>
            {
                _ = services.AddSingleton(services);
                _ = services.AddLogSetup();

                // App Host
                _ = services.AddHostedService<ApplicationHostService>();

                // Theme manipulation
                _ = services.AddSingleton<IThemeService, ThemeService>();
                _ = services.AddSingleton<ITaskBarService, TaskBarService>();
                _ = services.AddSingleton<ISnackbarService, SnackbarService>();
                _ = services.AddSingleton<ISnackbarService, SnackbarService>();
                _ = services.AddSingleton<IContentDialogService, ContentDialogService>();

                // Main window with navigation
                _ = services.AddSingleton<MainWindowViewModel>();
                _ = services.AddSingleton<IWindow, MainWindow>();

                // All other pages and view models
                _ = services.AddTransientFromNamespace("Yuandl.ThemeUI.Sample.Views.Pages", Assembly.GetExecutingAssembly());
                _ = services.AddTransientFromNamespace("Yuandl.ThemeUI.Sample.ViewModels.Pages", Assembly.GetExecutingAssembly());
            }
        )
        .Build();

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T GetService<T>()
        where T : class
    {
        return _host.Services.GetRequiredService<T>();
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        XamlDisplay.Init();
        _host.Start();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private void OnExit(object sender, ExitEventArgs e)
    {
        _host.StopAsync().Wait();

        _host.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}