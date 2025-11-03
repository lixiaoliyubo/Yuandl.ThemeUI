// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows;
using Yuandl.ThemeUI.Controls;
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.Managers;

/// <summary>
/// 管理应用程序主题的静态类
/// </summary>
public static class ApplicationThemeManager
{
    // 缓存当前应用程序的主题
    private static ApplicationTheme _cachedApplicationTheme = ApplicationTheme.Unknown;
    
    // 缓存上次使用的主题字典管理器，避免重复创建
    private static ResourceDictionaryManager _cachedDictionaryManager;
    
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
    /// <returns>操作是否成功</returns>
    public static bool Apply(ApplicationTheme applicationTheme, WindowBackdropType backgroundEffect = WindowBackdropType.Mica, bool updateAccent = false)
    {
        // 验证应用程序实例
        if (UiApplication.Current is null)
            return false;
        
        // 如果主题未知，则不进行操作
        if (applicationTheme == ApplicationTheme.Unknown)
            return false;

        try
        {            
            // 如果要更新强调色
            if (updateAccent)
            {
                try
                {
                    Color color = ApplicationAccentColorManager.GetSystemAccent();
                    ApplicationAccentColorManager.Apply(color, applicationTheme);
                }
                catch { }
            }

            // 获取或创建资源字典管理器
            var appDictionaries = GetOrCreateDictionaryManager();
            
            // 清除缓存以确保加载最新资源
            appDictionaries.ClearCache();

            // 确定要使用的主题字典名称
            var themeDictionaryName = applicationTheme == ApplicationTheme.Dark ? "Dark" : "Light";

            // 构建主题URI
            var themeUri = new Uri($"{ThemesDictionaryPath}{themeDictionaryName}.xaml", UriKind.Absolute);
            
            // 更新资源字典
            bool isUpdated = appDictionaries.UpdateDictionary("theme", themeUri);
            if (!isUpdated)
                return false;

            // 更新系统主题缓存
            SystemThemeManager.UpdateSystemThemeCache();

            // 保存当前应用程序主题
            _cachedApplicationTheme = applicationTheme;

            // 通知主题已更改（可用于需要响应主题变化的组件）
            ThemeChanged?.Invoke(null, new ThemeChangedEventArgs(applicationTheme));

            // 如果主窗口存在，更新背景
            UpdateMainWindowBackground(applicationTheme, backgroundEffect);
            
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 应用系统主题
    /// </summary>
    /// <returns>操作是否成功</returns>
    public static bool ApplySystemTheme()
    {
        return ApplySystemTheme(true);
    }

    /// <summary>
    /// 应用系统主题，并可选择是否更新强调色
    /// </summary>
    /// <param name="updateAccent">是否更新强调色</param>
    /// <returns>操作是否成功</returns>
    public static bool ApplySystemTheme(bool updateAccent)
    {
        try
        {            
            // 更新系统主题缓存
            SystemThemeManager.UpdateSystemThemeCache();

            // 获取系统主题
            SystemTheme systemTheme = GetSystemTheme();

            // 确定要设置的应用程序主题
            ApplicationTheme themeToSet = systemTheme == SystemTheme.Dark ? ApplicationTheme.Dark : ApplicationTheme.Light;

            // 应用主题
            return Apply(themeToSet, updateAccent: updateAccent);
        }
        catch
        {
            return false;
        }
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

        return appApplicationTheme == ApplicationTheme.Light && sysTheme == SystemTheme.Light;
    }
    
    /// <summary>
    /// 检查应用程序和操作系统是否当前处于深色主题
    /// </summary>
    public static bool IsMatchedDark()
    {
        ApplicationTheme appApplicationTheme = GetAppTheme();
        SystemTheme sysTheme = GetSystemTheme();

        return appApplicationTheme == ApplicationTheme.Dark && sysTheme == SystemTheme.Dark;
    }

    /// <summary>
    /// 尝试猜测当前设置的应用程序主题
    /// </summary>
    private static void FetchApplicationTheme()
    {
        try
        {            
            var appDictionaries = GetOrCreateDictionaryManager();
            ResourceDictionary themeDictionary = appDictionaries.GetDictionary("theme");

            if (themeDictionary == null || themeDictionary.Source == null)
                return;

            string themeUri = themeDictionary.Source.ToString().Trim().ToLower();

            // 优先检查更具体的条件
            if (themeUri.Contains("dark"))
            {
                _cachedApplicationTheme = ApplicationTheme.Dark;
            }
            else if (themeUri.Contains("light"))
            {
                _cachedApplicationTheme = ApplicationTheme.Light;
            }
        }
        catch
        {
            // 发生错误时保持默认值
        }
    }
    
    /// <summary>
    /// 获取或创建资源字典管理器
    /// </summary>
    private static ResourceDictionaryManager GetOrCreateDictionaryManager()
    {
        if (_cachedDictionaryManager == null)
        {
            _cachedDictionaryManager = new ResourceDictionaryManager(LibraryNamespace);
        }
        return _cachedDictionaryManager;
    }
    
    /// <summary>
    /// 更新主窗口背景
    /// </summary>
    private static void UpdateMainWindowBackground(ApplicationTheme theme, WindowBackdropType backdropType)
    {
        try
        {            
            if (UiApplication.Current?.MainWindow is Window mainWindow)
            {
                WindowBackgroundManager.UpdateBackground(mainWindow, theme, backdropType);
            }
        }
        catch { }
    }
    
    /// <summary>
    /// 主题更改事件
    /// </summary>
    public static event EventHandler<ThemeChangedEventArgs> ThemeChanged;
    
    /// <summary>
    /// 主题更改事件参数
    /// </summary>
    public class ThemeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 获取新的主题
        /// </summary>
        public ApplicationTheme NewTheme { get; }
        
        public ThemeChangedEventArgs(ApplicationTheme newTheme)
        {
            NewTheme = newTheme;
        }
    }
}