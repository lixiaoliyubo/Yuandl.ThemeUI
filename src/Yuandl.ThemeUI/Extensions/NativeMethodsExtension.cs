// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using Microsoft.Win32;
using System.Runtime.InteropServices;
using Yuandl.ThemeUI.Controls;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Helpers;
using Yuandl.ThemeUI.Interop;
using Yuandl.ThemeUI.Managers;

namespace Yuandl.ThemeUI.Extensions;

/// <summary>
/// NativeMethodsExtension 类提供了与 Windows 原生方法交互的各种方法，用于处理窗口的任务栏状态、背景效果、暗模式等属性。
/// </summary>
public class NativeMethodsExtension
{
    /// <summary>
    /// 尝试为选定的窗口句柄设置任务栏状态。
    /// </summary>
    /// <param name="hWnd">窗口句柄。</param>
    /// <param name="taskbarFlag">任务栏状态标志。</param>
    internal static bool SetTaskbarState(IntPtr hWnd, ShObjIdl.TBPFLAG taskbarFlag)
    {
        // 如果句柄为空，返回 false
        if (hWnd == IntPtr.Zero)
        {
            return false;
        }

        // 如果不是有效的窗口，返回 false
        if (!NativeMethods.IsWindow(hWnd))
        {
            return false;
        }

        // 如果创建任务栏列表对象失败，返回 false
        if (new ShObjIdl.CTaskbarList() is not ShObjIdl.ITaskbarList4 taskbarList)
        {
            return false;
        }

        taskbarList.HrInit();
        taskbarList.SetProgressState(hWnd, taskbarFlag);

        return true;
    }

    /// <summary>
    /// 更新选定窗口的任务栏进度条值。
    /// </summary>
    /// <param name="hWnd">窗口句柄。</param>
    /// <param name="taskbarFlag">进度条状态标志（例如暂停等）。</param>
    /// <param name="current">当前进度值。</param>
    /// <param name="total">最大进度值。</param>
    /// <returns>如果成功更新，返回 true；否则返回 false。</returns>
    internal static bool SetTaskbarValue(IntPtr hWnd, ShObjIdl.TBPFLAG taskbarFlag, int current, int total)
    {
        // 如果句柄为空，返回 false
        if (hWnd == IntPtr.Zero)
        {
            return false;
        }

        // 如果不是有效的窗口，返回 false
        if (!NativeMethods.IsWindow(hWnd))
        {
            return false;
        }

        // 创建任务栏列表对象
        if (new ShObjIdl.CTaskbarList() is not ShObjIdl.ITaskbarList4 taskbarList)
        {
            return false;
        }

        taskbarList.HrInit();
        taskbarList.SetProgressState(hWnd, taskbarFlag);

        // 如果标志不是不确定或无进度，设置进度值
        if (taskbarFlag is not ShObjIdl.TBPFLAG.TBPF_INDETERMINATE and not ShObjIdl.TBPFLAG.TBPF_NOPROGRESS)
        {
            taskbarList.SetProgressValue(hWnd, Convert.ToUInt64(current), Convert.ToUInt64(total));
        }

        return true;
    }

    /// <summary>
    /// 尝试为选定的窗口设置窗口角落偏好。
    /// </summary>
    /// <param name="handle">选定的窗口句柄。</param>
    /// <param name="cornerPreference">窗口角落偏好设置。</param>
    /// <returns>如果设置成功，返回 true；否则返回 false。</returns>
    public static bool SetWindowCornerRadius(IntPtr handle, WindowCornerPreference cornerPreference)
    {
        // 如果句柄为空，返回 false
        if (handle == IntPtr.Zero)
        {
            return false;
        }

        // 如果不是有效的窗口，返回 false
        if (!NativeMethods.IsWindow(handle))
        {
            return false;
        }

        int pvAttribute = (int)UnsafeReflection.Cast(cornerPreference);

        // 调用 Windows API 设置窗口属性
        _ = NativeMethods.DwmSetWindowAttribute(
            handle,
            Dwmapi.DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE,
            ref pvAttribute,
            Marshal.SizeOf(typeof(int))
        );

        return true;
    }

    /// <summary>
    /// 根据指定的背景类型将背景效果应用于选定的窗口句柄。
    /// </summary>
    /// <param name="hWnd">要应用背景效果的窗口句柄。</param>
    /// <param name="backdropType">要应用的背景效果类型。</param>
    /// <returns>如果操作成功，返回 true；否则，返回 false。</returns>
    public static bool ApplyBackdrop(IntPtr hWnd, WindowBackdropType backdropType)
    {
        // 如果句柄为空，返回 false
        if (hWnd == IntPtr.Zero)
        {
            return false;
        }

        // 如果不是有效的窗口，返回 false
        if (!NativeMethods.IsWindow(hWnd))
        {
            return false;
        }

        // 根据应用主题设置暗模式
        if (ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Dark)
        {
            _ = ApplyWindowDarkMode(hWnd);
        }
        else
        {
            _ = RemoveWindowDarkMode(hWnd);
        }

        _ = RemoveWindowCaption(hWnd);

        // 根据 Windows 版本进行不同的处理
        if (!Win32Utilities.IsOSWindows11Insider1OrNewer)
        {
            if (backdropType != WindowBackdropType.None)
            {
                return ApplyLegacyMicaBackdrop(hWnd);
            }

            return false;
        }

        return backdropType switch
        {
            WindowBackdropType.Auto => ApplyDwmwWindowAttrubute(hWnd, Dwmapi.DWMSBT.DWMSBT_AUTO),
            WindowBackdropType.Mica => ApplyDwmwWindowAttrubute(hWnd, Dwmapi.DWMSBT.DWMSBT_MAINWINDOW),
            WindowBackdropType.Acrylic => ApplyDwmwWindowAttrubute(hWnd, Dwmapi.DWMSBT.DWMSBT_TRANSIENTWINDOW),
            WindowBackdropType.Tabbed => ApplyDwmwWindowAttrubute(hWnd, Dwmapi.DWMSBT.DWMSBT_TABBEDWINDOW),
            _ => ApplyDwmwWindowAttrubute(hWnd, Dwmapi.DWMSBT.DWMSBT_DISABLE),
        };
    }

    /// <summary>
    /// 对选定的 <see cref="System.Windows.Window"/> 应用背景效果。
    /// </summary>
    /// <param name="window">要应用背景效果的窗口。</param>
    /// <param name="backdropType">要应用的背景效果类型。</param>
    /// <returns>如果操作成功，返回 true；否则，返回 false。</returns>
    public static bool ApplyBackdrop(System.Windows.Window window, WindowBackdropType backdropType)
    {
        // 如果窗口为空，返回 false
        if (window is null)
        {
            return false;
        }

        // 如果窗口已加载
        if (window.IsLoaded)
        {
            IntPtr windowHandle = new WindowInteropHelper(window).Handle;

            // 如果获取句柄失败，返回 false
            if (windowHandle == IntPtr.Zero)
            {
                return false;
            }

            return ApplyBackdrop(windowHandle, backdropType);
        }

        // 当窗口加载时执行应用背景效果的操作
        window.Loaded += (sender, _1) =>
        {
            IntPtr windowHandle =
                new WindowInteropHelper(sender as System.Windows.Window ?? null)?.Handle ?? IntPtr.Zero;

            // 如果获取句柄失败，直接返回
            if (windowHandle == IntPtr.Zero)
            {
                return;
            }

            _ = ApplyBackdrop(windowHandle, backdropType);
        };

        return true;
    }

    /// <summary>
    /// 尝试移除已应用于 <see cref="Window"/> 的背景效果。
    /// </summary>
    /// <param name="window">要移除效果的窗口。</param>
    public static bool RemoveBackdrop(System.Windows.Window window)
    {
        // 如果窗口为空，返回 false
        if (window is null)
        {
            return false;
        }

        IntPtr windowHandle = new WindowInteropHelper(window).Handle;

        return RemoveBackdrop(windowHandle);
    }

    /// <summary>
    /// 尝试移除已应用于 <c>hWnd</c> 的所有效果。
    /// </summary>
    /// <param name="hWnd">指向窗口句柄的指针。</param>
    public static bool RemoveBackdrop(IntPtr hWnd)
    {
        // 如果句柄为空，返回 false
        if (hWnd == IntPtr.Zero)
        {
            return false;
        }

        _ = RestoreContentBackground(hWnd);

        // 如果句柄为空或不是有效窗口，返回 false
        if (hWnd == IntPtr.Zero)
        {
            return false;
        }

        if (!NativeMethods.IsWindow(hWnd))
        {
            return false;
        }

        var pvAttribute = 0; // 禁用
        var backdropPvAttribute = (int)Dwmapi.DWMSBT.DWMSBT_DISABLE;

        // 调用 Windows API 设置窗口属性
        _ = NativeMethods.DwmSetWindowAttribute(
            hWnd,
            Dwmapi.DWMWINDOWATTRIBUTE.DWMWA_MICA_EFFECT,
            ref pvAttribute,
            Marshal.SizeOf(typeof(int))
        );

        _ = NativeMethods.DwmSetWindowAttribute(
            hWnd,
            Dwmapi.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE,
            ref backdropPvAttribute,
            Marshal.SizeOf(typeof(int))
        );

        return true;
    }

    private static bool RestoreContentBackground(IntPtr hWnd)
    {
        // 如果句柄为空，返回 false
        if (hWnd == IntPtr.Zero)
        {
            return false;
        }

        // 如果不是有效窗口，返回 false
        if (!NativeMethods.IsWindow(hWnd))
        {
            return false;
        }

        var windowSource = HwndSource.FromHwnd(hWnd);

        // 恢复客户区背景
        if (windowSource?.Handle != IntPtr.Zero && windowSource?.CompositionTarget != null)
        {
            windowSource.CompositionTarget.BackgroundColor = SystemColors.WindowColor;
        }

        if (windowSource?.RootVisual is System.Windows.Window window)
        {
            var backgroundBrush = window.Resources["ApplicationFillBrush"];

            // 手动处理背景刷的回退情况
            if (backgroundBrush is not SolidColorBrush)
            {
                backgroundBrush = GetFallbackBackgroundBrush();
            }

            window.Background = (SolidColorBrush)backgroundBrush;
        }

        return true;
    }

    private static SolidColorBrush GetFallbackBackgroundBrush()
    {
        return ApplicationThemeManager.GetAppTheme() switch
        {
            ApplicationTheme.Dark => new SolidColorBrush(Color.FromArgb(0xFF, 0x20, 0x20, 0x20)),
            ApplicationTheme.Light => new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0xFA, 0xFA)),
            _ => new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0xFA, 0xFA))
        };
    }

    /// <summary>
    /// 尝试为窗口句柄应用沉浸式暗模式效果
    /// </summary>
    /// <param name="handle">要应用效果的窗口句柄</param>
    /// <returns>如果调用原生 Windows 函数成功，返回 true；否则返回 false</returns>
    public static bool ApplyWindowDarkMode(IntPtr handle)
    {
        // 如果句柄为空，返回 false
        if (handle == IntPtr.Zero)
        {
            return false;
        }

        // 如果不是有效的窗口，返回 false
        if (!NativeMethods.IsWindow(handle))
        {
            return false;
        }

        var pvAttribute = 0x1; // 启用
        Dwmapi.DWMWINDOWATTRIBUTE dwAttribute = Dwmapi.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE;

        // 如果不是 Windows 11 或更新版本，使用旧的属性
        if (!Win32Utilities.IsOSWindows11Insider1OrNewer)
        {
            dwAttribute = Dwmapi.DWMWINDOWATTRIBUTE.DMWA_USE_IMMERSIVE_DARK_MODE_OLD;
        }

        // 调用 Windows API 设置属性
        // TODO: 验证 HRESULT
        _ = NativeMethods.DwmSetWindowAttribute(handle, dwAttribute, ref pvAttribute, Marshal.SizeOf(typeof(int)));

        return true;
    }

    /// <summary>
    /// 尝试为指定的窗口应用沉浸式暗模式效果
    /// </summary>
    /// <param name="window">要应用效果的窗口</param>
    /// <returns>如果获取窗口句柄并成功应用效果，返回 true；否则返回 false</returns>
    public static bool ApplyWindowDarkMode(Window window)
    {
        if (GetHandle(window, out IntPtr windowHandle))
        {
            return ApplyWindowDarkMode(windowHandle);
        }

        return false;
    }

    /// <summary>
    /// 尝试从指定的窗口移除沉浸式暗模式效果
    /// </summary>
    /// <param name="window">要移除效果的窗口</param>
    /// <returns>如果获取窗口句柄并成功移除效果，返回 true；否则返回 false</returns>
    public static bool RemoveWindowDarkMode(Window window)
    {
        if (GetHandle(window, out IntPtr windowHandle))
        {
            return RemoveWindowDarkMode(windowHandle);
        }

        return false;
    }

    /// <summary>
    /// 尝试从窗口句柄移除沉浸式暗模式效果
    /// </summary>
    /// <param name="handle">要操作的窗口句柄</param>
    /// <returns>如果调用原生 Windows 函数成功，返回 true；否则返回 false</returns>
    public static bool RemoveWindowDarkMode(IntPtr handle)
    {
        // 如果句柄为空，返回 false
        if (handle == IntPtr.Zero)
        {
            return false;
        }

        // 如果不是有效的窗口，返回 false
        if (!NativeMethods.IsWindow(handle))
        {
            return false;
        }

        var pvAttribute = 0x0; // 禁用
        Dwmapi.DWMWINDOWATTRIBUTE dwAttribute = Dwmapi.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE;

        // 如果不是 Windows 11 或更新版本，使用旧的属性
        if (!Win32Utilities.IsOSWindows11Insider1OrNewer)
        {
            dwAttribute = Dwmapi.DWMWINDOWATTRIBUTE.DMWA_USE_IMMERSIVE_DARK_MODE_OLD;
        }

        // 调用 Windows API 设置属性
        // TODO: 验证 HRESULT
        _ = NativeMethods.DwmSetWindowAttribute(handle, dwAttribute, ref pvAttribute, Marshal.SizeOf(typeof(int)));

        return true;
    }

    /// <summary>
    /// 尝试获取指定窗口的句柄
    /// </summary>
    /// <param name="window">要获取句柄的窗口</param>
    /// <param name="windowHandle">输出获取到的窗口句柄</param>
    /// <returns>如果成功获取到非空句柄，返回 true；否则返回 false</returns>
    private static bool GetHandle(Window window, out IntPtr windowHandle)
    {
        // 如果窗口为空，设置句柄为零并返回 false
        if (window is null)
        {
            windowHandle = IntPtr.Zero;

            return false;
        }

        windowHandle = new WindowInteropHelper(window).Handle;

        return windowHandle != IntPtr.Zero;
    }

    /// <summary>
    /// 应用传统的云母背景效果到指定的窗口句柄
    /// </summary>
    /// <param name="hWnd">要应用效果的窗口句柄</param>
    /// <returns>如果调用原生 Windows 函数成功（返回 HRESULT.S_OK），返回 true；否则返回 false</returns>
    private static bool ApplyLegacyMicaBackdrop(IntPtr hWnd)
    {
        var backdropPvAttribute = 1; // 启用

        // TODO: 验证 HRESULT
        var dwmApiResult = NativeMethods.DwmSetWindowAttribute(
            hWnd,
            Dwmapi.DWMWINDOWATTRIBUTE.DWMWA_MICA_EFFECT,
            ref backdropPvAttribute,
            Marshal.SizeOf(typeof(int))
        );

        return dwmApiResult == HRESULT.S_OK;
    }

    /// <summary>
    /// 设置指定窗口句柄的系统背景类型
    /// </summary>
    /// <param name="hWnd">要操作的窗口句柄</param>
    /// <param name="dwmSbt">指定的背景类型</param>
    /// <returns>如果调用原生 Windows 函数成功（返回 HRESULT.S_OK），返回 true；否则返回 false</returns>
    private static bool ApplyDwmwWindowAttrubute(IntPtr hWnd, Dwmapi.DWMSBT dwmSbt)
    {
        // 如果句柄为空，返回 false
        if (hWnd == IntPtr.Zero)
        {
            return false;
        }

        // 如果不是有效的窗口，返回 false
        if (!NativeMethods.IsWindow(hWnd))
        {
            return false;
        }

        var backdropPvAttribute = (int)dwmSbt;

        var dwmApiResult = NativeMethods.DwmSetWindowAttribute(
            hWnd,
            Dwmapi.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE,
            ref backdropPvAttribute,
            Marshal.SizeOf(typeof(int))
        );

        return dwmApiResult == HRESULT.S_OK;
    }

    /// <summary>
    /// 从指定的窗口句柄移除窗口标题栏
    /// </summary>
    /// <param name="hWnd">要操作的窗口句柄</param>
    /// <returns>如果操作成功，返回 true；否则返回 false</returns>
    public static bool RemoveWindowCaption(IntPtr hWnd)
    {
        // 如果句柄为空，返回 false
        if (hWnd == IntPtr.Zero)
        {
            return false;
        }

        // 如果不是有效的窗口，返回 false
        if (!NativeMethods.IsWindow(hWnd))
        {
            return false;
        }

        var wtaOptions = new Dwmapi.WTA_OPTIONS()
        {
            DwFlags = Dwmapi.WTNCA.NODRAWCAPTION,
            DwMask = Dwmapi.WTNCA.VALIDBITS
        };

        NativeMethods.SetWindowThemeAttribute(
            hWnd,
            Dwmapi.WINDOWTHEMEATTRIBUTETYPE.WTA_NONCLIENT,
            ref wtaOptions,
            (uint)Marshal.SizeOf(typeof(Dwmapi.WTA_OPTIONS))
        );

        return true;
    }

    /// <summary>
    /// 尝试获取当前选定窗口的强调色
    /// </summary>
    /// <returns>获取到的颜色，如果获取失败则返回默认的 Windows 强调色</returns>
    public static Color GetDwmColor()
    {
        try
        {
            NativeMethods.DwmGetColorizationParameters(out Dwmapi.DWMCOLORIZATIONPARAMS dwmParams);
            var values = BitConverter.GetBytes(dwmParams.ClrColor);

            return Color.FromArgb(255, values[2], values[1], values[0]);
        }
        catch
        {
            var colorizationColorValue = Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM",
                "ColorizationColor",
                null
            );

            if (colorizationColorValue is not null)
            {
                try
                {
                    var colorizationColor = (uint)(int)colorizationColorValue;
                    var values = BitConverter.GetBytes(colorizationColor);

                    return Color.FromArgb(255, values[2], values[1], values[0]);
                }
                catch
                {
                }
            }
        }

        return GetDefaultWindowsAccentColor();
    }

    /// <summary>
    /// 获取默认的 Windows 强调色
    /// </summary>
    /// <returns>默认的颜色</returns>
    private static Color GetDefaultWindowsAccentColor()
    {
        // Windows 默认的强调色
        // https://learn.microsoft.com/windows-hardware/customize/desktop/unattend/microsoft-windows-shell-setup-themes-windowcolor#values
        return Color.FromArgb(0xff, 0x00, 0x78, 0xd7);
    }

    /// <summary>
    /// 尝试从指定的窗口移除标题栏内容
    /// </summary>
    /// <param name="window">要操作的窗口</param>
    /// <returns>如果对本机 Windows 函数的调用成功，返回 true；否则返回 false</returns>
    public static bool RemoveWindowTitlebarContents(Window window)
    {
        // 如果窗口为空，返回 false
        if (window == null)
        {
            return false;
        }

        // 如果窗口已加载
        if (window.IsLoaded)
        {
            return GetHandle(window, out IntPtr windowHandle) && RemoveWindowTitlebarContents(windowHandle);
        }

        // 当窗口加载时执行移除标题栏内容的操作
        window.Loaded += (sender, _1) =>
        {
            _ = GetHandle(sender as Window, out IntPtr windowHandle);
            _ = RemoveWindowTitlebarContents(windowHandle);
        };

        return true;
    }

    /// <summary>
    /// 尝试从指定的窗口句柄移除标题栏
    /// </summary>
    /// <param name="handle">要操作的窗口句柄</param>
    /// <returns>如果调用本机 Windows 函数成功，返回 true；否则返回 false</returns>
    public static bool RemoveWindowTitlebarContents(IntPtr handle)
    {
        // 如果句柄为空，返回 false
        if (handle == IntPtr.Zero)
        {
            return false;
        }

        // 如果不是有效的窗口，返回 false
        if (!NativeMethods.IsWindow(handle))
        {
            return false;
        }

        var windowStyleLong = NativeMethods.GetWindowLong(handle, Dwmapi.GWL.GWL_STYLE);
        windowStyleLong &= ~(int)Dwmapi.WS.SYSMENU;

        IntPtr result = SetWindowLong(handle, Dwmapi.GWL.GWL_STYLE, windowStyleLong);

        return result.ToInt64() > 0x0;
    }

    /// <summary>
    /// 设置指定窗口句柄的窗口样式
    /// </summary>
    /// <param name="handle">要操作的窗口句柄</param>
    /// <param name="nIndex">窗口样式的索引</param>
    /// <param name="windowStyleLong">要设置的窗口样式值</param>
    /// <returns>设置后的窗口句柄，如果设置失败返回 IntPtr.Zero</returns>
    private static IntPtr SetWindowLong(IntPtr handle, Dwmapi.GWL nIndex, long windowStyleLong)
    {
        if (IntPtr.Size == 4)
        {
            return new IntPtr(NativeMethods.SetWindowLong(handle, (int)nIndex, (int)windowStyleLong));
        }

        return NativeMethods.SetWindowLongPtr(handle, (int)nIndex, checked((IntPtr)windowStyleLong));
    }
}