// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Diagnostics;
using Yuandl.ThemeUI.Extensions;

namespace Yuandl.ThemeUI.Controls;

public class NotificationWindow : Window
{
    private WindowInteropHelper? _interopHelper = null;

    /// <summary>
    /// Gets contains helper for accessing this window handle.
    /// </summary>
    protected WindowInteropHelper InteropHelper
    {
        get => _interopHelper ??= new WindowInteropHelper(this);
    }

    internal Panel NotificationPanel { get; set; }

    internal bool WindowClosed;

    private int NotificationWidth { get; set; }

    public NotificationWindow()
    {
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;

        // Background =  new SolidColorBrush(Color.FromArgb(0,0,0,0));
        // Topmost = true;
        // Width = 340;
        // MaxWidth = 340;
        // ShowActivated = false;
        // ResizeMode =  ResizeMode.NoResize;
        // ShowInTaskbar = false;
        // SizeToContent =  SizeToContent.WidthAndHeight;
        NotificationPanel = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(5, 5, 5, 5),
            Tag = "x"
        };

        NotificationPanel.SizeChanged += (sender, args) =>
        {
            Debug.WriteLine($"{args.NewSize}");
        };

        Content = new ScrollViewer
        {
            VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
            Content = NotificationPanel
        };

        Closed += NotificationWindow_Closed;
    }

    private void NotificationWindow_Closed(object sender, EventArgs args)
    {
        WindowClosed = true;
    }

    internal void Init()
    {
        Rect desktopWorkingArea = SystemParameters.WorkArea;
        Height = desktopWorkingArea.Height;
        Left = desktopWorkingArea.Right - Width;
        Top = 0;
        if (InteropHelper.Handle == IntPtr.Zero)
        {
            return;
        }

        _ = NativeMethodsExtension.SetWindowCornerRadius(InteropHelper.Handle, WindowCornerPreference.DoNotRound);
    }
}