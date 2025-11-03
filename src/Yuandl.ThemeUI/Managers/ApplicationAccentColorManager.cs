// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Media;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Extensions;

namespace Yuandl.ThemeUI.Managers;

/// <summary>
/// 管理应用程序强调色的静态类
/// </summary>
public static class ApplicationAccentColorManager
{
    /// <summary>
    /// 背景 HSV 亮度的最大值，超过该值后强调色上的文本将变为深色。
    /// </summary>
    private const double BackgroundBrightnessThresholdValue = 80d;

    // 缓存当前强调色以避免重复计算
    private static Color? _cachedSystemAccent;
    private static Color? _cachedPrimaryAccent;
    private static Color? _cachedSecondaryAccent;
    private static Color? _cachedTertiaryAccent;

    // 缓存对应的Brush对象
    private static Brush? _cachedSystemAccentBrush;
    private static Brush? _cachedPrimaryAccentBrush;
    private static Brush? _cachedSecondaryAccentBrush;
    private static Brush? _cachedTertiaryAccentBrush;

    /// <summary>
    /// Gets 获取系统强调色。
    /// </summary>
    public static Color SystemAccent
    {
        get
        {
            // 检查缓存
            if (_cachedSystemAccent.HasValue)
            {
                return _cachedSystemAccent.Value;
            }

            try
            {
                if (UiApplication.Current?.Resources != null)
                {
                    var resource = UiApplication.Current.Resources["SystemAccentColor"];
                    if (resource is Color color && color != Colors.Transparent)
                    {
                        _cachedSystemAccent = color;
                        return color;
                    }
                }
            }
            catch { }

            // 返回默认值
            return Colors.Transparent;
        }
    }

    /// <summary>
    /// Gets 获取系统强调色的<see cref="Brush"/>。
    /// </summary>
    public static Brush SystemAccentBrush
    {
        get
        {
            if (_cachedSystemAccentBrush == null)
            {
                _cachedSystemAccentBrush = CreateFrozenBrush(SystemAccent);
            }
            return _cachedSystemAccentBrush;
        }
    }

    /// <summary>
    /// Gets 获取系统主要强调色。
    /// </summary>
    public static Color PrimaryAccent
    {
        get
        {
            // 检查缓存
            if (_cachedPrimaryAccent.HasValue)
            {
                return _cachedPrimaryAccent.Value;
            }

            try
            {
                if (UiApplication.Current?.Resources != null)
                {
                    var resource = UiApplication.Current.Resources["SystemAccentPrimaryColor"];
                    if (resource is Color color && color != Colors.Transparent)
                    {
                        _cachedPrimaryAccent = color;
                        return color;
                    }
                }
            }
            catch { }

            return Colors.Transparent;
        }
    }

    /// <summary>
    /// Gets 获取系统主要强调色的<see cref="Brush"/>。
    /// </summary>
    public static Brush PrimaryAccentBrush
    {
        get
        {
            if (_cachedPrimaryAccentBrush == null)
            {
                _cachedPrimaryAccentBrush = CreateFrozenBrush(PrimaryAccent);
            }
            return _cachedPrimaryAccentBrush;
        }
    }

    /// <summary>
    /// Gets 获取系统次要强调色。
    /// </summary>
    public static Color SecondaryAccent
    {
        get
        {
            // 检查缓存
            if (_cachedSecondaryAccent.HasValue)
            {
                return _cachedSecondaryAccent.Value;
            }

            try
            {
                if (UiApplication.Current?.Resources != null)
                {
                    var resource = UiApplication.Current.Resources["SystemAccentSecondaryColor"];
                    if (resource is Color color && color != Colors.Transparent)
                    {
                        _cachedSecondaryAccent = color;
                        return color;
                    }
                }
            }
            catch { }

            return Colors.Transparent;
        }
    }

    /// <summary>
    /// Gets 获取系统次要强调色的<see cref="Brush"/>。
    /// </summary>
    public static Brush SecondaryAccentBrush
    {
        get
        {
            if (_cachedSecondaryAccentBrush == null)
            {
                _cachedSecondaryAccentBrush = CreateFrozenBrush(SecondaryAccent);
            }
            return _cachedSecondaryAccentBrush;
        }
    }

    /// <summary>
    /// Gets 获取系统第三强调色。
    /// </summary>
    public static Color TertiaryAccent
    {
        get
        {
            // 检查缓存
            if (_cachedTertiaryAccent.HasValue)
            {
                return _cachedTertiaryAccent.Value;
            }

            try
            {
                if (UiApplication.Current?.Resources != null)
                {
                    var resource = UiApplication.Current.Resources["SystemAccentTertiaryColor"];
                    if (resource is Color color && color != Colors.Transparent)
                    {
                        _cachedTertiaryAccent = color;
                        return color;
                    }
                }
            }
            catch { }

            return Colors.Transparent;
        }
    }

    /// <summary>
    /// Gets 获取系统第三强调色的<see cref="Brush"/>。
    /// </summary>
    public static Brush TertiaryAccentBrush
    {
        get
        {
            if (_cachedTertiaryAccentBrush == null)
            {
                _cachedTertiaryAccentBrush = CreateFrozenBrush(TertiaryAccent);
            }
            return _cachedTertiaryAccentBrush;
        }
    }

    /// <summary>
    /// 根据输入的颜色更改应用程序的颜色强调。
    /// </summary>
    /// <param name="systemAccent">主要强调色。</param>
    /// <param name="applicationTheme">如果是<see cref="ApplicationTheme.Dark"/>，颜色将有所不同。</param>
    /// <returns>操作是否成功</returns>
    public static bool Apply(Color systemAccent, ApplicationTheme applicationTheme = ApplicationTheme.Light)
    {
        try
        {
            Color primaryAccent;
            Color secondaryAccent;
            Color tertiaryAccent;

            // 根据主题生成不同的强调色变体
            if (applicationTheme == ApplicationTheme.Dark)
            {
                primaryAccent = systemAccent.Update(15f, -12f);
                secondaryAccent = systemAccent.Update(30f, -24f);
                tertiaryAccent = systemAccent.Update(45f, -36f);
            }
            else
            {
                primaryAccent = systemAccent.UpdateBrightness(-5f);
                secondaryAccent = systemAccent.UpdateBrightness(-10f);
                tertiaryAccent = systemAccent.UpdateBrightness(-15f);
            }

            // 应用强调色
            Apply(systemAccent, primaryAccent, secondaryAccent, tertiaryAccent);

            // 通知颜色已更改
            AccentColorChanged?.Invoke(null, EventArgs.Empty);

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 根据输入的颜色更改应用程序的颜色强调。
    /// </summary>
    /// <param name="systemAccent">主要颜色。</param>
    /// <param name="primaryAccent">替代的浅色或深色。</param>
    /// <param name="secondaryAccent">第二个替代的浅色或深色（最常用）。</param>
    /// <param name="tertiaryAccent">第三个替代的浅色或深色。</param>
    private static void Apply(Color systemAccent, Color primaryAccent, Color secondaryAccent, Color tertiaryAccent)
    {
        try
        {
            if (UiApplication.Current?.Resources != null)
            {
                // 更新资源字典
                UiApplication.Current.Resources["SystemAccentColor"] = systemAccent;
                UiApplication.Current.Resources["SystemAccentPrimaryColor"] = primaryAccent;
                UiApplication.Current.Resources["SystemAccentSecondaryColor"] = secondaryAccent;
                UiApplication.Current.Resources["SystemAccentTertiaryColor"] = tertiaryAccent;

                // 更新画刷资源，使用冻结的画刷提高性能
                UiApplication.Current.Resources["SystemAccentColorBrush"] = CreateFrozenBrush(systemAccent);
                UiApplication.Current.Resources["SystemAccentColorPrimaryBrush"] = CreateFrozenBrush(primaryAccent);
                UiApplication.Current.Resources["SystemAccentSecondaryColorBrush"] = CreateFrozenBrush(secondaryAccent);
                UiApplication.Current.Resources["SystemAccentTertiaryColorBrush"] = CreateFrozenBrush(tertiaryAccent);

                // 更新缓存
                UpdateCachedValues(systemAccent, primaryAccent, secondaryAccent, tertiaryAccent);
            }
        }
        catch { }
    }

    /// <summary>
    /// 应用系统强调色到应用程序。
    /// </summary>
    /// <returns>操作是否成功</returns>
    public static bool ApplySystemAccent()
    {
        try
        {
            Color accentColor = GetSystemAccent();
            return Apply(accentColor, ApplicationThemeManager.GetAppTheme());
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 获取系统强调色
    /// </summary>
    public static Color GetSystemAccent()
    {
        try
        {
            // 首先尝试获取DWM颜色
            Color color = GetColorizationColor();

            // 然后检查ThemesDictionary中是否设置了自定义颜色
            bool isCustomColorSet = false;
            if (UiApplication.Current?.Resources?.MergedDictionaries != null)
            {
                foreach (ResourceDictionary resource in UiApplication.Current.Resources.MergedDictionaries)
                {
                    if (resource is ThemesDictionary themes && themes.Color.HasValue)
                    {
                        isCustomColorSet = true;
                        color = themes.Color.Value;
                        break;
                    }
                }
            }

            // 如果没有自定义颜色，尝试获取已设置的系统强调色
            if (!isCustomColorSet && color == Colors.Transparent)
            {
                color = SystemAccent;
            }

            return color;
        }
        catch
        {
            // 出错时返回默认蓝色
            return Color.FromRgb(0, 120, 215);
        }
    }

    /// <summary>
    /// 获取当前桌面窗口管理器的颜色化颜色。
    /// <para>它应该是系统个性化设置中定义的颜色。</para>
    /// </summary>
    public static Color GetColorizationColor()
    {
        try
        {
            return NativeMethodsExtension.GetDwmColor();
        }
        catch
        {
            return Colors.Transparent;
        }
    }

    /// <summary>
    /// 创建冻结的SolidColorBrush以提高性能
    /// </summary>
    private static Brush CreateFrozenBrush(Color color)
    {
        var brush = new SolidColorBrush(color);
        brush.Freeze(); // 冻结以提高性能
        return brush;
    }

    /// <summary>
    /// 更新缓存的颜色和画刷值
    /// </summary>
    private static void UpdateCachedValues(Color systemAccent, Color primaryAccent, Color secondaryAccent, Color tertiaryAccent)
    {
        // 更新颜色缓存
        _cachedSystemAccent = systemAccent;
        _cachedPrimaryAccent = primaryAccent;
        _cachedSecondaryAccent = secondaryAccent;
        _cachedTertiaryAccent = tertiaryAccent;

        // 更新画刷缓存
        _cachedSystemAccentBrush = CreateFrozenBrush(systemAccent);
        _cachedPrimaryAccentBrush = CreateFrozenBrush(primaryAccent);
        _cachedSecondaryAccentBrush = CreateFrozenBrush(secondaryAccent);
        _cachedTertiaryAccentBrush = CreateFrozenBrush(tertiaryAccent);
    }

    /// <summary>
    /// 清除颜色缓存
    /// </summary>
    public static void ClearCache()
    {
        _cachedSystemAccent = null;
        _cachedPrimaryAccent = null;
        _cachedSecondaryAccent = null;
        _cachedTertiaryAccent = null;

        _cachedSystemAccentBrush = null;
        _cachedPrimaryAccentBrush = null;
        _cachedSecondaryAccentBrush = null;
        _cachedTertiaryAccentBrush = null;
    }

    /// <summary>
    /// 强调色更改事件
    /// </summary>
    public static event EventHandler AccentColorChanged;
}
