// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Extensions;

namespace Yuandl.ThemeUI.Managers;

public static class ApplicationAccentColorManager
{
    /// <summary>
    /// 背景 HSV 亮度的最大值，超过该值后强调色上的文本将变为深色。
    /// </summary>
    private const double BackgroundBrightnessThresholdValue = 80d;

    /// <summary>
    /// Gets 获取系统强调色。
    /// </summary>
    public static Color SystemAccent
    {
        get
        {
            var resource = UiApplication.Current.Resources["SystemAccentColor"];

            if (resource is Color color)
            {
                return color;
            }

            return Colors.Transparent;
        }
    }

    /// <summary>
    /// Gets 获取系统强调色的<see cref="Brush"/>。
    /// </summary>
    public static Brush SystemAccentBrush => new SolidColorBrush(SystemAccent);

    /// <summary>
    /// Gets 获取系统主要强调色。
    /// </summary>
    public static Color PrimaryAccent
    {
        get
        {
            var resource = UiApplication.Current.Resources["SystemAccentPrimaryColor"];

            if (resource is Color color)
            {
                return color;
            }

            return Colors.Transparent;
        }
    }

    /// <summary>
    /// Gets 获取系统主要强调色的<see cref="Brush"/>。
    /// </summary>
    public static Brush PrimaryAccentBrush => new SolidColorBrush(PrimaryAccent);

    /// <summary>
    /// Gets 获取系统次要强调色。
    /// </summary>
    public static Color SecondaryAccent
    {
        get
        {
            var resource = UiApplication.Current.Resources["SystemAccentSecondaryColor"];

            if (resource is Color color)
            {
                return color;
            }

            return Colors.Transparent;
        }
    }

    /// <summary>
    /// Gets 获取系统次要强调色的<see cref="Brush"/>。
    /// </summary>
    public static Brush SecondaryAccentBrush => new SolidColorBrush(SecondaryAccent);

    /// <summary>
    /// Gets 获取系统第三强调色。
    /// </summary>
    public static Color TertiaryAccent
    {
        get
        {
            var resource = UiApplication.Current.Resources["SystemAccentTertiaryColor"];

            if (resource is Color color)
            {
                return color;
            }

            return Colors.Transparent;
        }
    }

    /// <summary>
    /// Gets 获取系统第三强调色的<see cref="Brush"/>。
    /// </summary>
    public static Brush TertiaryAccentBrush => new SolidColorBrush(TertiaryAccent);

    /// <summary>
    /// 根据输入的颜色更改应用程序的颜色强调。
    /// </summary>
    /// <param name="systemAccent">主要强调色。</param>
    /// <param name="applicationTheme">如果是<see cref="ApplicationTheme.Dark"/>，颜色将有所不同。</param>
    public static void Apply(Color systemAccent, ApplicationTheme applicationTheme = ApplicationTheme.Light)
    {
        Color primaryAccent;
        Color secondaryAccent;
        Color tertiaryAccent;

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

        Apply(systemAccent, primaryAccent, secondaryAccent, tertiaryAccent);
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
        UiApplication.Current.Resources["SystemAccentColor"] = systemAccent;
        UiApplication.Current.Resources["SystemAccentPrimaryColor"] = primaryAccent;
        UiApplication.Current.Resources["SystemAccentSecondaryColor"] = secondaryAccent;
        UiApplication.Current.Resources["SystemAccentTertiaryColor"] = tertiaryAccent;
        UiApplication.Current.Resources["SystemAccentColorBrush"] = systemAccent.ToBrush();
        UiApplication.Current.Resources["SystemAccentColorPrimaryBrush"] = primaryAccent.ToBrush();
        UiApplication.Current.Resources["SystemAccentSecondaryColorBrush"] = secondaryAccent.ToBrush();
        UiApplication.Current.Resources["SystemAccentTertiaryColorBrush"] = tertiaryAccent.ToBrush();
    }

    /// <summary>
    /// 应用系统强调色到应用程序。
    /// </summary>
    public static void ApplySystemAccent()
    {
        Apply(GetSystemAccent(), ApplicationThemeManager.GetAppTheme());
    }

    public static Color GetSystemAccent()
    {
        Color color = GetColorizationColor();
        bool isColor = false;
        foreach (ResourceDictionary resource in UiApplication.Current.Resources.MergedDictionaries)
        {
            if (resource is ThemesDictionary themes)
            {
                isColor = true;
                color = themes.Color ?? color;
                break;
            }
        }

        if (!isColor)
        {
            color = SystemAccent;
        }

        return color;
    }

    /// <summary>
    /// 获取当前桌面窗口管理器的颜色化颜色。
    /// <para>它应该是系统个性化设置中定义的颜色。</para>
    /// </summary>
    public static Color GetColorizationColor()
    {
        return NativeMethodsExtension.GetDwmColor();
    }
}