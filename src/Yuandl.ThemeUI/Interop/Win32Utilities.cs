// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Diagnostics;
using System.Runtime.InteropServices;
using Yuandl.ThemeUI.Helpers;

namespace Yuandl.ThemeUI.Interop;

/// <summary>
/// 常见的窗口实用工具类。
/// </summary>
// ReSharper disable InconsistentNaming
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
public class Win32Utilities
{
    private static readonly PlatformID _osPlatform = Environment.OSVersion.Platform;
    public static readonly Version Vista = new(6, 0);  // 定义 Windows Vista 的版本
    public static readonly Version Windows7 = new(6, 1);  // 定义 Windows 7 的版本
    public static readonly Version Windows8 = new(6, 2);  // 定义 Windows 8 的版本

    private static readonly Version _osVersion =
#if NET5_0_OR_GREATER
    Environment.OSVersion.Version;  // 如果是.NET 5.0 或更高版本，直接获取操作系统版本
#else
GetOSVersionFromRegistry();  // 否则，从注册表获取操作系统版本
#endif

    /// <summary>
    /// Gets a value indicating whether 获取一个值，指示操作系统是否为 NT 或更新版本。
    /// </summary>
    public static bool IsNT => _osPlatform == PlatformID.Win32NT;

    /// <summary>
    /// Gets a value indicating whether 获取一个值，指示操作系统版本是否大于或等于 Windows Vista。
    /// </summary>
    public static bool IsOSVistaOrNewer => _osVersion >= Vista;

    /// <summary>
    /// Gets a value indicating whether 获取一个值，指示操作系统版本是否大于或等于 Windows 7。
    /// </summary>
    public static bool IsOSWindows7OrNewer => _osVersion >= Windows7;

    /// <summary>
    /// Gets a value indicating whether 获取一个值，指示操作系统版本是否大于或等于 Windows 8。
    /// </summary>
    public static bool IsOSWindows8OrNewer => _osVersion >= Windows8;

    /// <summary>
    /// Gets a value indicating whether 获取一个值，指示操作系统版本是否大于或等于 Windows 10（构建 10240）。
    /// </summary>
    public static bool IsOSWindows10OrNewer => _osVersion.Build >= 10240;

    /// <summary>
    /// Gets a value indicating whether 获取一个值，指示操作系统版本是否大于或等于 Windows 11。
    /// </summary>
    public static bool IsOSWindows11OrNewer => _osVersion.Build >= 22000;

    /// <summary>
    /// Gets a value indicating whether 获取一个值，指示操作系统版本是否大于或等于 Windows 11 预览版 1。
    /// </summary>
    public static bool IsOSWindows11Insider1OrNewer => _osVersion.Build >= 22523;

    /// <summary>
    /// Gets a value indicating whether 获取一个值，指示操作系统版本是否大于或等于 Windows 11 预览版 2。
    /// </summary>
    public static bool IsOSWindows11Insider2OrNewer => _osVersion.Build >= 22557;

    /// <summary>
    /// Gets a value indicating whether 获取一个值，指示桌面窗口管理器（DWM）合成是否已启用。
    /// </summary>
    public static bool IsCompositionEnabled
    {
        get
        {
            if (!IsOSVistaOrNewer)
            {
                return false;
            }

            _ = NativeMethods.DwmIsCompositionEnabled(out var pfEnabled);  // 调用 Dwmapi 来获取合成是否启用

            return pfEnabled != 0;
        }
    }

    /// <summary>
    /// 安全地释放并处置可释放的对象。
    /// </summary>
    public static void SafeDispose<T>(ref T disposable)
        where T : IDisposable
    {
        // 处置操作可以安全地多次调用
        IDisposable t = disposable;
        disposable = default;

        if (t is null)
        {
            return;
        }

        t.Dispose();
    }

    /// <summary>
    /// 安全地释放 COM 对象。
    /// </summary>
    public static void SafeRelease<T>(ref T comObject)
        where T : class
    {
        T t = comObject;
        comObject = default;

        if (t is null)
        {
            return;
        }

        Debug.Assert(Marshal.IsComObject(t), "对象不是 COM 对象。");
        _ = Marshal.ReleaseComObject(t);
    }

#if !NET5_0_OR_GREATER
    /// <summary>
    /// 尝试从 Windows 注册表获取操作系统版本。
    /// </summary>
    private static Version GetOSVersionFromRegistry()
    {
        int major = 0;
        {
            // 在 Windows 10 中，'CurrentMajorVersionNumber' 字符串值是新的，希望在微软再次更改之前能持续一段时间...
            if (
                TryGetRegistryKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion",
                    "CurrentMajorVersionNumber",
                    out var majorObj
                )
            )
            {
                majorObj ??= 0;

                major = (int)majorObj;
            }

            // 如果 'CurrentMajorVersionNumber' 值不存在，回退到读取之前用于此目的的键：'CurrentVersion'
            else if (
                TryGetRegistryKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion",
                    "CurrentVersion",
                    out var version
                )
            )
            {
                version ??= string.Empty;

                var versionParts = ((string)version).Split('.');

                if (versionParts.Length >= 2)
                {
                    major = int.TryParse(versionParts[0], out int majorAsInt) ? majorAsInt : 0;
                }
            }
        }

        int minor = 0;
        {
            // 在 Windows 10 中，'CurrentMinorVersionNumber' 字符串值是新的，希望在微软再次更改之前能持续一段时间...
            if (
                TryGetRegistryKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion",
                    "CurrentMinorVersionNumber",
                    out var minorObj
                )
            )
            {
                minorObj ??= string.Empty;

                minor = (int)minorObj;
            }

            // 如果 'CurrentMinorVersionNumber' 值不存在，回退到读取之前用于此目的的键：'CurrentVersion'
            else if (
                TryGetRegistryKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion",
                    "CurrentVersion",
                    out var version
                )
            )
            {
                version ??= string.Empty;

                var versionParts = ((string)version).Split('.');

                if (versionParts.Length >= 2)
                {
                    minor = int.TryParse(versionParts[1], out int minorAsInt) ? minorAsInt : 0;
                }
            }
        }

        int build = 0;
        {
            if (
                TryGetRegistryKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion",
                    "CurrentBuildNumber",
                    out var buildObj
                )
            )
            {
                buildObj ??= string.Empty;

                build = int.TryParse((string)buildObj, out int buildAsInt) ? buildAsInt : 0;
            }
        }

        return new(major, minor, build);
    }

    /// <summary>
    /// 尝试从指定的注册表路径和键获取注册表值。
    /// </summary>
    private static bool TryGetRegistryKey(string path, string key, out object? value)
    {
        value = null;

        try
        {
            using Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(path);

            if (rk == null)
            {
                return false;
            }

            value = rk.GetValue(key);

            return value != null;
        }
        catch
        {
            return false;
        }
    }
#endif
}