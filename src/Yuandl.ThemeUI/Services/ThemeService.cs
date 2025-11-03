// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System;
using System.Windows.Media;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Managers;

namespace Yuandl.ThemeUI.Services;

/// <summary>
/// 主题服务的具体实现，负责管理应用程序的主题切换和颜色设置
/// </summary>
public partial class ThemeService : IThemeService
{
    // 缓存当前主题状态以提高性能
    private static ApplicationTheme? _cachedCurrentTheme;
    private static Color? _cachedCurrentAccentColor;
    private static bool _isInitialized;

    /// <summary>
    /// 主题更改事件
    /// </summary>
    public event EventHandler<ApplicationTheme>? ThemeChanged;

    /// <summary>
    /// 强调色更改事件
    /// </summary>
    public event EventHandler<Color>? AccentColorChanged;

    /// <summary>
    /// 初始化主题服务
    /// </summary>
    public ThemeService()
    {
        InitializeService();
    }

    /// <summary>
    /// 初始化服务，注册相关事件处理
    /// </summary>
    private void InitializeService()
    {
        if (!_isInitialized)
        {
            // 初始化缓存
            _cachedCurrentTheme = ApplicationThemeManager.GetAppTheme();
            _isInitialized = true;
        }
    }

    /// <inheritdoc />
    public virtual ApplicationTheme GetTheme()
    {
        try
        {
            if (_cachedCurrentTheme == null)
            {
                _cachedCurrentTheme = ApplicationThemeManager.GetAppTheme();
            }

            return _cachedCurrentTheme.Value;
        }
        catch
        {
            return ApplicationTheme.Light; // 出错时返回浅色主题
        }
    }

    /// <inheritdoc />
    public virtual SystemTheme GetNativeSystemTheme()
    {
        try
        {
            return ApplicationThemeManager.GetSystemTheme();
        }
        catch
        {
            return SystemTheme.Light; // 出错时返回浅色系统主题
        }
    }

    /// <inheritdoc />
    public virtual ApplicationTheme GetSystemTheme()
    {
        try
        {
            SystemTheme systemTheme = ApplicationThemeManager.GetSystemTheme();

            return systemTheme switch
            {
                SystemTheme.Light => ApplicationTheme.Light,
                SystemTheme.Dark => ApplicationTheme.Dark,
                _ => ApplicationTheme.Unknown
            };
        }
        catch
        {
            return ApplicationTheme.Light; // 出错时返回浅色主题
        }
    }

    /// <inheritdoc />
    public virtual bool SetTheme(ApplicationTheme applicationTheme)
    {
        try
        {
            if (!Enum.IsDefined(typeof(ApplicationTheme), applicationTheme))
            {
                throw new ArgumentException("无效的主题枚举值", nameof(applicationTheme));
            }

            // 仅当主题发生变化时才执行切换
            if (_cachedCurrentTheme != applicationTheme)
            {
                bool result = ApplicationThemeManager.Apply(applicationTheme);
                if (result)
                {
                    _cachedCurrentTheme = applicationTheme;

                    // 触发主题更改事件
                    ThemeChanged?.Invoke(this, applicationTheme);
                }

                return result;
            }

            return true; // 主题未变化，视为成功
        }
        catch
        {
            return false; // 出错时返回失败
        }
    }

    /// <inheritdoc />
    public bool SetSystemAccent()
    {
        try
        {
            bool result = ApplicationAccentColorManager.ApplySystemAccent();
            if (result)
            {
                _cachedCurrentAccentColor = ApplicationAccentColorManager.GetSystemAccent();
                AccentColorChanged?.Invoke(this, _cachedCurrentAccentColor.Value);
            }

            return result;
        }
        catch
        {
            return false; // 出错时返回失败
        }
    }

    /// <inheritdoc />
    public bool SetAccent(Color accentColor)
    {
        try
        {
            // 仅当颜色发生变化时才执行设置
            if (!_cachedCurrentAccentColor.HasValue || _cachedCurrentAccentColor.Value != accentColor)
            {
                bool result = ApplicationAccentColorManager.Apply(accentColor);
                if (result)
                {
                    _cachedCurrentAccentColor = accentColor;
                    AccentColorChanged?.Invoke(this, accentColor);
                }

                return result;
            }

            return true; // 颜色未变化，视为成功
        }
        catch
        {
            return false; // 出错时返回失败
        }
    }

    /// <inheritdoc />
    public bool SetAccent(SolidColorBrush accentSolidBrush)
    {
        try
        {
            Color color = accentSolidBrush.Color;
            color.A = (byte)Math.Round(accentSolidBrush.Opacity * byte.MaxValue);
            return SetAccent(color);
        }
        catch
        {
            return false; // 出错时返回失败
        }
    }

    /// <summary>
    /// 切换明暗主题
    /// </summary>
    /// <returns>操作是否成功</returns>
    public bool ToggleTheme()
    {
        try
        {
            ApplicationTheme currentTheme = GetTheme();
            ApplicationTheme newTheme = currentTheme == ApplicationTheme.Light ? ApplicationTheme.Dark : ApplicationTheme.Light;
            return SetTheme(newTheme);
        }
        catch
        {
            return false; // 出错时返回失败
        }
    }

    /// <summary>
    /// 清除主题服务的内部缓存
    /// </summary>
    public void ClearCache()
    {
        try
        {
            _cachedCurrentTheme = null;
            _cachedCurrentAccentColor = null;

            // 重新初始化以获取最新状态
            InitializeService();
        }
        catch
        {
            // 忽略清理缓存时的错误
        }
    }
}