// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Reflection;

namespace Yuandl.ThemeUI.Sample.Helper;

internal record GalleryPage(string Name, string Description, SymbolRegular Icon, Type PageType);

internal static class ControlPages
{
    private const string PageSuffix = "Page";

    public static IEnumerable<GalleryPage> All()
    {
        foreach (Type? type in GalleryAssembly.Asssembly.GetTypes().Where(t => t.IsDefined(typeof(GalleryPageAttribute))))
        {
            GalleryPageAttribute? galleryPageAttribute = type.GetCustomAttributes<GalleryPageAttribute>()
                .FirstOrDefault();

            if (galleryPageAttribute is not null)
            {
                yield return new GalleryPage(
                    type.Name[..type.Name.LastIndexOf(PageSuffix)],
                    galleryPageAttribute.Description,
                    galleryPageAttribute.Icon,
                    type
                );
            }
        }
    }

    public static IEnumerable<GalleryPage> FromNamespace(string namespaceName)
    {
        return All().Where(t => t.PageType?.Namespace?.StartsWith(namespaceName) ?? false);
    }
}