// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Markup;
using Yuandl.ThemeUI.Controls;

namespace Yuandl.ThemeUI.Markup;

[ContentProperty(nameof(Source))]
[MarkupExtensionReturnType(typeof(ImageIcon))]
public class ImageIconExtension : MarkupExtension
{
    public ImageIconExtension(ImageSource? source)
    {
        Source = source;
    }

    [ConstructorArgument("source")]
    public ImageSource? Source { get; set; }

    public double Width { get; set; } = 16D;

    public double Height { get; set; } = 16D;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var imageIcon = new ImageIcon
        {
            Source = Source,
            Width = Width,
            Height = Height
        };

        return imageIcon;
    }
}