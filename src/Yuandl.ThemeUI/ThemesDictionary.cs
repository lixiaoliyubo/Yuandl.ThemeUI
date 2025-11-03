// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Markup;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Managers;

namespace Yuandl.ThemeUI;

[Localizability(LocalizationCategory.Ignore)]
[Ambient]
[UsableDuringInitialization(true)]
public class ThemesDictionary : ResourceDictionary
{
    private ApplicationTheme? _theme;

    /// <summary>
    /// Gets or sets the default application theme.
    /// </summary>
    public ApplicationTheme? Theme
    {
        get => _theme;
        set
        {
            if (_theme != value)
            {
                _theme = value;
                SetTheme();
            }
        }
    }

    private Color? _color;

    public Color? Color
    {
        get => _color;
        set
        {
            if (_color != value)
            {
                _color = value;
                SetTheme();
            }
        }
    }

    private void SetTheme()
    {
        if (Theme is ApplicationTheme baseTheme)
        {
            var themeUri = new Uri($"{ApplicationThemeManager.ThemesDictionaryPath}{baseTheme}.xaml", UriKind.Absolute);

            // 仅当URI发生变化时才重新加载资源
            Source = themeUri;

            // 应用主题
            ApplicationThemeManager.Apply(baseTheme);

            // 如果设置了颜色，则应用主色调
            if (Color is Color accentColor)
            {
                ApplicationAccentColorManager.Apply(accentColor, baseTheme);
            }
        }
    }
}