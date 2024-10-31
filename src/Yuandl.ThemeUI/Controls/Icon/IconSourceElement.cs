// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Markup;

namespace Yuandl.ThemeUI.Controls;

[ContentProperty(nameof(IconSource))]
public class IconSourceElement : IconElement
{
    /// <summary>Identifies the <see cref="IconSource"/> dependency property.</summary>
    public static readonly DependencyProperty IconSourceProperty = DependencyProperty.Register(
        nameof(IconSource),
        typeof(IconSource),
        typeof(IconSourceElement),
        new FrameworkPropertyMetadata(null)
    );

    /// <summary>
    /// Gets or sets <see cref="IconSource"/>
    /// </summary>
    public IconSource? IconSource
    {
        get => (IconSource?)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    protected override UIElement InitializeChildren()
    {
        return CreateIconElement();

        // TODO: Come up with an elegant solution
        throw new InvalidOperationException($"Use {nameof(CreateIconElement)}");
    }

    public IconElement? CreateIconElement()
    {
        return IconSource?.CreateIconElement();
    }
}