// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using System.Drawing;
using Yuandl.ThemeUI.Controls;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Managers;

namespace Yuandl.ThemeUI.Mvvm.ViewModels;

public partial class SettingsPageViewModel : ViewModel, INavigationAware
{
    [ObservableProperty]
    private string pageTitle = "软件设置";

    private readonly IWindow _window;
    private readonly ILogger<SettingsPageViewModel> _logger;
    public SettingsPageViewModel(ILogger<SettingsPageViewModel> logger, IWindow window)
    {
        _window = window;
        _logger = logger;
    }
    private bool _isInitialized = false;

    [ObservableProperty]
    private string _appVersion = string.Empty;

    [ObservableProperty] 
    private bool _isChecking;

    /// <summary>
    /// 开机启动
    /// </summary>
    [ObservableProperty] 
    private bool _isPowerOn;

    [ObservableProperty]
    private ApplicationTheme currentApplicationTheme = ApplicationTheme.Unknown;

    partial void OnCurrentApplicationThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue)
    {
        ApplicationThemeManager.Apply(newValue);
    }

    public override void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();
    }

    public override void OnNavigatedFrom() { }

    private void InitializeViewModel()
    {
        CurrentApplicationTheme = ApplicationThemeManager.GetAppTheme();

        _isInitialized = true;
    }

    private void OnThemeChanged(ApplicationTheme currentApplicationTheme, Color systemAccent)
    {
        // Update the theme if it has been changed elsewhere than in the settings.
        if (CurrentApplicationTheme != currentApplicationTheme)
        {
            CurrentApplicationTheme = currentApplicationTheme;
        }
    }
}
