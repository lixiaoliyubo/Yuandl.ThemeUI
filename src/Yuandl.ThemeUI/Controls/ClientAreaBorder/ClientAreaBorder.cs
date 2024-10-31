// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Shell;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Hardware;
using Yuandl.ThemeUI.Helpers;
using Yuandl.ThemeUI.Interop;
using Yuandl.ThemeUI.Managers;
using Size = System.Windows.Size;

// ReSharper disable once CheckNamespace
namespace Yuandl.ThemeUI.Controls;

public class ClientAreaBorder : System.Windows.Controls.Border, IThemeControl
{
    private static Thickness? _paddedBorderThickness;
    private static Thickness? _resizeFrameBorderThickness;
    private static Thickness? _windowChromeNonClientFrameThickness;
    private bool _borderBrushApplied = false;
    private System.Windows.Window? _oldWindow;

    public ApplicationTheme ApplicationTheme { get; set; } = ApplicationTheme.Unknown;

    /// <summary>
    /// Gets the system value for the padded border thickness (<see cref="User32.SM.CXPADDEDBORDER"/>) in WPF units.
    /// </summary>
    public Thickness PaddedBorderThickness
    {
        get
        {
            if (_paddedBorderThickness is not null)
            {
                return _paddedBorderThickness.Value;
            }

            var paddedBorder = NativeMethods.GetSystemMetrics(Dwmapi.SM.CXPADDEDBORDER);

            (double factorX, double factorY) = GetDpi();

            var frameSize = new Size(paddedBorder, paddedBorder);
            var frameSizeInDips = new Size(frameSize.Width / factorX, frameSize.Height / factorY);

            _paddedBorderThickness = new Thickness(
                frameSizeInDips.Width,
                frameSizeInDips.Height,
                frameSizeInDips.Width,
                frameSizeInDips.Height
            );

            return _paddedBorderThickness.Value;
        }
    }

    public static Thickness ResizeFrameBorderThickness =>
        _resizeFrameBorderThickness ??= new Thickness(
            SystemParameters.ResizeFrameVerticalBorderWidth,
            SystemParameters.ResizeFrameHorizontalBorderHeight,
            SystemParameters.ResizeFrameVerticalBorderWidth,
            SystemParameters.ResizeFrameHorizontalBorderHeight
        );

    /// <summary>
    /// Gets the thickness of the window's non-client frame used for maximizing the window with a custom chrome.
    /// </summary>
    /// <remarks>
    /// If you use a <see cref="WindowChrome"/> to extend the client area of a window to the non-client area, you need to handle the edge margin issue when the window is maximized.
    /// Use this property to get the correct margin value when the window is maximized, so that when the window is maximized, the client area can completely cover the screen client area by no less than a single pixel at any DPI.
    /// The<see cref="User32.GetSystemMetrics"/> method cannot obtain this value directly.
    /// </remarks>
    public Thickness WindowChromeNonClientFrameThickness =>
        _windowChromeNonClientFrameThickness ??= new Thickness(
            ResizeFrameBorderThickness.Left + PaddedBorderThickness.Left,
            ResizeFrameBorderThickness.Top + PaddedBorderThickness.Top,
            ResizeFrameBorderThickness.Right + PaddedBorderThickness.Right,
            ResizeFrameBorderThickness.Bottom + PaddedBorderThickness.Bottom
        );

    public ClientAreaBorder()
    {
        ApplicationTheme = ApplicationThemeManager.GetAppTheme();
    }

    private void OnThemeChanged(ApplicationTheme currentApplicationTheme, Color systemAccent)
    {
        ApplicationTheme = currentApplicationTheme;

        if (!_borderBrushApplied || _oldWindow == null)
        {
            return;
        }

        ApplyDefaultWindowBorder();
    }

    /// <inheritdoc />
    protected override void OnVisualParentChanged(DependencyObject oldParent)
    {
        base.OnVisualParentChanged(oldParent);

        if (_oldWindow is { } oldWindow)
        {
            oldWindow.StateChanged -= OnWindowStateChanged;
            oldWindow.Closing -= OnWindowClosing;
        }

        var newWindow = (System.Windows.Window?)System.Windows.Window.GetWindow(this);

        if (newWindow is not null)
        {
            newWindow.StateChanged -= OnWindowStateChanged; // Unsafe
            newWindow.StateChanged += OnWindowStateChanged;
            newWindow.Closing += OnWindowClosing;
        }

        _oldWindow = newWindow;

        ApplyDefaultWindowBorder();
    }

    private void OnWindowClosing(object? sender, CancelEventArgs e)
    {
        if (_oldWindow != null)
        {
            _oldWindow.Closing -= OnWindowClosing;
        }
    }

    private void OnWindowStateChanged(object? sender, EventArgs e)
    {
        if (sender is not System.Windows.Window window)
        {
            return;
        }

        Thickness padding =
            window.WindowState == WindowState.Maximized ? WindowChromeNonClientFrameThickness : default;
        SetCurrentValue(PaddingProperty, padding);
    }

    private void ApplyDefaultWindowBorder()
    {
        if (Win32Utilities.IsOSWindows11OrNewer || _oldWindow == null)
        {
            return;
        }

        _borderBrushApplied = true;

        // SystemParameters.WindowGlassBrush
        Color borderColor =
            ApplicationTheme == ApplicationTheme.Light
                ? Color.FromArgb(0xFF, 0x7A, 0x7A, 0x7A)
                : Color.FromArgb(0xFF, 0x3A, 0x3A, 0x3A);
        _oldWindow.SetCurrentValue(
            System.Windows.Controls.Control.BorderBrushProperty,
            new SolidColorBrush(borderColor)
        );
        _oldWindow.SetCurrentValue(System.Windows.Controls.Control.BorderThicknessProperty, new Thickness(1));
    }

    private (double FactorX, double FactorY) GetDpi()
    {
        if (PresentationSource.FromVisual(this) is { } source)
        {
            return (
                source.CompositionTarget.TransformToDevice.M11, // Possible null reference
                source.CompositionTarget.TransformToDevice.M22
            );
        }

        DisplayDpi systemDPi = DpiHelper.GetSystemDpi();

        return (systemDPi.DpiScaleX, systemDPi.DpiScaleY);
    }
}