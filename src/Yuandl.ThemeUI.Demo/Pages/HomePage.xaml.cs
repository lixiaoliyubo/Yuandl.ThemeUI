// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Diagnostics;
using System.Windows;

namespace Yuandl.ThemeUI.Demo.Pages;
/// <summary>
/// HomePage.xaml 的交互逻辑
/// </summary>
public partial class HomePage 
{
    public HomePage()
    {
        InitializeComponent();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine($"Page_Loaded");
    }

    private void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine($"Page_Unloaded");
    }
}
