// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

public static class EnumerableExtension
{
    public static IEnumerable<TSource> Do<TSource>(this IEnumerable<TSource> source, Action<TSource> predicate)
    {
        IList<TSource> enumerable = source as IList<TSource> ?? source.ToList();
        foreach (TSource? item in enumerable)
        {
            predicate.Invoke(item);
        }

        return enumerable;
    }

    public static IEnumerable<TSource> DoWithIndex<TSource>(this IEnumerable<TSource> source, Action<TSource, int> predicate)
    {
        IList<TSource> enumerable = source as IList<TSource> ?? source.ToList();
        for (var i = 0; i < enumerable.Count; i++)
        {
            predicate.Invoke(enumerable[i], i);
        }

        return enumerable;
    }

    public static List<UIElement> ConvertUIElementCollectionToList(this UIElementCollection collection)
    {
        List<UIElement> list = new List<UIElement>();
        foreach (UIElement element in collection)
        {
            list.Add(element);
        }

        return list;
    }
}