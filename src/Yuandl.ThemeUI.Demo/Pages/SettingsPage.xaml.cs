// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Windows.Controls;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Managers;
namespace Yuandl.ThemeUI.Demo.Pages;
/// <summary>
/// SettingsPage.xaml 的交互逻辑
/// </summary>
public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        ApplicationThemeManager.Apply(comboBox.SelectedIndex == 0 ? ApplicationTheme.Light: ApplicationTheme.Dark);
    }

    private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        ApplicationTheme Theme = ApplicationThemeManager.GetAppTheme();
        themeComboBox.SelectedIndex = Theme == ApplicationTheme.Light ? 0 : 1;
    }
}
