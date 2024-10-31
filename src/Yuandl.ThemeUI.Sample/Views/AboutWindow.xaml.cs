// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Diagnostics;
using System.Windows.Navigation;

namespace Yuandl.ThemeUI.Sample.Views;

/// <summary>
/// AboutWindow.xaml 的交互逻辑
/// </summary>
public partial class AboutWindow : Window
{
    public AboutWindow()
    {
        InitializeComponent();
    }

    private void GithubHyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        _ = Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        e.Handled = true;
    }

    private void GiteeHyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        _ = Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        e.Handled = true;
    }

    private void QQHyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        var uri = new Uri(@"https://qm.qq.com/cgi-bin/qm/qr?k=f2zl3nvoetItho8kGfe1eys0jDkqvvcL&jump_from=webapi");
        _ = Process.Start(new ProcessStartInfo(uri.AbsoluteUri));
        e.Handled = true;
    }
}