// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Markup;
using Yuandl.ThemeUI.Controls;

namespace Yuandl.ThemeUI.Extensions;

[ContentProperty(nameof(Glyph))]
[MarkupExtensionReturnType(typeof(FontIcon))]
public class FontIconExtension : MarkupExtension
{
    public FontIconExtension(string glyph)
    {
        Glyph = glyph;
        FontFamily = new FontFamily("FluentSystemIcons");
    }

    public FontIconExtension(string glyph, FontFamily fontFamily)
        : this(glyph)
    {
        FontFamily = fontFamily;
    }

    [ConstructorArgument("glyph")]
    public string Glyph { get; set; }

    [ConstructorArgument("fontFamily")]
    public FontFamily FontFamily { get; set; }

    public double FontSize { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var fontIcon = new FontIcon { Glyph = Glyph, FontFamily = FontFamily };

        if (FontSize > 0)
        {
            fontIcon.FontSize = FontSize;
        }

        return fontIcon;
    }
}