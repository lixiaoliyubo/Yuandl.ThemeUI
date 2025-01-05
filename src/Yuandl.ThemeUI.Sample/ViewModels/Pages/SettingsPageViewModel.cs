// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.


namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class SettingsPageViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private string pageTitle = "软件设置";

    public SettingsPageViewModel()
    {
    }

    private bool _isInitialized = false;

    [ObservableProperty]
    private string appVersion = string.Empty;

    [ObservableProperty]
    private ApplicationTheme currentApplicationTheme = ApplicationTheme.Unknown;

    partial void OnCurrentApplicationThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue)
    {
        ApplicationThemeManager.Apply(newValue);
    }

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
        }
    }

    public void OnNavigatedFrom() { }

    private void InitializeViewModel()
    {
        CurrentApplicationTheme = ApplicationThemeManager.GetAppTheme();
        AppVersion = $"Yuandl.WPF-Mvvm - {GetAssemblyVersion()}";

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

    private string GetAssemblyVersion()
    {
        return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            ?? string.Empty;
    }

}