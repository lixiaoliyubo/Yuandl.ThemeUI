// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Controls;
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.Managers;

public static class ApplicationThemeManager
{
    // 缓存当前应用程序的主题
    private static ApplicationTheme _cachedApplicationTheme = ApplicationTheme.Unknown;

    // 库的命名空间
    internal const string LibraryNamespace = "yuandl.themeui;";

    // 主题字典的路径
    internal const string ThemesDictionaryPath = "pack://application:,,,/Yuandl.ThemeUI;component/Resources/Themes/";

    /// <summary>
    /// 更改当前应用程序的主题
    /// </summary>
    /// <param name="applicationTheme">要设置的主题</param>
    /// <param name="backgroundEffect">是否应用自定义背景效果</param>
    /// <param name="updateAccent">是否更改颜色强调</param>
    public static void Apply(ApplicationTheme applicationTheme, WindowBackdropType backgroundEffect = WindowBackdropType.Mica, bool updateAccent = false)
    {
        // 如果要更新强调色
        if (updateAccent)
        {
            Color color = ApplicationAccentColorManager.GetSystemAccent();
            ApplicationAccentColorManager.Apply(color, applicationTheme);
        }

        // 如果主题未知，则不进行操作
        if (applicationTheme == ApplicationTheme.Unknown)
        {
            return;
        }

        // 管理资源字典
        var appDictionaries = new ResourceDictionaryManager(LibraryNamespace);

        // 确定要使用的主题字典名称
        var themeDictionaryName = "Light";

        switch (applicationTheme)
        {
            case ApplicationTheme.Dark:
                themeDictionaryName = "Dark";
                break;
        }

        // 更新资源字典
        bool isUpdated = appDictionaries.UpdateDictionary("theme", new Uri(ThemesDictionaryPath + themeDictionaryName + ".xaml", UriKind.Absolute));
        if (!isUpdated)
        {
            return;
        }

        // 更新系统主题缓存
        SystemThemeManager.UpdateSystemThemeCache();

        // 保存当前应用程序主题
        _cachedApplicationTheme = applicationTheme;

        // 如果主窗口存在，更新背景
        if (UiApplication.Current.MainWindow is Window mainWindow)
        {
            WindowBackgroundManager.UpdateBackground(mainWindow, applicationTheme, backgroundEffect);
        }
    }

    /// <summary>
    /// 应用系统主题
    /// </summary>
    public static void ApplySystemTheme()
    {
        ApplySystemTheme(true);
    }

    /// <summary>
    /// 应用系统主题，并可选择是否更新强调色
    /// </summary>
    /// <param name="updateAccent">是否更新强调色</param>
    public static void ApplySystemTheme(bool updateAccent)
    {
        // 更新系统主题缓存
        SystemThemeManager.UpdateSystemThemeCache();

        // 获取系统主题
        SystemTheme systemTheme = GetSystemTheme();

        // 确定要设置的应用程序主题
        ApplicationTheme themeToSet = ApplicationTheme.Light;

        if (systemTheme is SystemTheme.Dark)
        {
            themeToSet = ApplicationTheme.Dark;
        }

        // 应用主题
        Apply(themeToSet, updateAccent: updateAccent);
    }

    /// <summary>
    /// 获取当前设置的应用程序主题
    /// </summary>
    /// <returns>如果出现问题，则返回 ApplicationTheme.Unknown</returns>
    public static ApplicationTheme GetAppTheme()
    {
        // 如果缓存的主题未知，尝试获取
        if (_cachedApplicationTheme == ApplicationTheme.Unknown)
        {
            FetchApplicationTheme();
        }

        return _cachedApplicationTheme;
    }

    /// <summary>
    /// 获取当前设置的系统主题
    /// </summary>
    /// <returns>如果出现问题，则返回 SystemTheme.Unknown</returns>
    public static SystemTheme GetSystemTheme()
    {
        return SystemThemeManager.GetCachedSystemTheme();
    }

    /// <summary>
    /// 检查应用程序和操作系统是否当前处于明亮主题
    /// </summary>
    public static bool IsMatchedLight()
    {
        ApplicationTheme appApplicationTheme = GetAppTheme();
        SystemTheme sysTheme = GetSystemTheme();

        if (appApplicationTheme != ApplicationTheme.Light)
        {
            return false;
        }

        return sysTheme is SystemTheme.Light;
    }

    /// <summary>
    /// 尝试猜测当前设置的应用程序主题
    /// </summary>
    private static void FetchApplicationTheme()
    {
        ResourceDictionaryManager appDictionaries = new(LibraryNamespace);
        ResourceDictionary themeDictionary = appDictionaries.GetDictionary("themes");

        if (themeDictionary == null)
        {
            return;
        }

        string themeUri = themeDictionary.Source.ToString().Trim().ToLower();

        if (themeUri.Contains("light"))
        {
            _cachedApplicationTheme = ApplicationTheme.Light;
        }

        if (themeUri.Contains("dark"))
        {
            _cachedApplicationTheme = ApplicationTheme.Dark;
        }
    }
}