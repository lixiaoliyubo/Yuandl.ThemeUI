// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Markup;

namespace Yuandl.ThemeUI;

[TypeConverter(typeof(DynamicResourceExtensionConverter))]
[ContentProperty(nameof(ResourceKey))]
[MarkupExtensionReturnType(typeof(object))]
public class ThemeResourceExtension : DynamicResourceExtension
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeResourceExtension"/> class.
    /// </summary>
    public ThemeResourceExtension()
    {
        ResourceKey = ThemeResource.ApplicationFillBrush.ToString();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeResourceExtension"/> class.
    /// Takes the resource key that this is a static reference to.
    /// </summary>
    public ThemeResourceExtension(ThemeResource resourceKey)
    {
        if (resourceKey == ThemeResource.Unknown)
        {
            throw new ArgumentNullException(nameof(resourceKey));
        }

        ResourceKey = resourceKey.ToString();
    }
}