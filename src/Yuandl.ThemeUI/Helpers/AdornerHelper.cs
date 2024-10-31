// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Documents;

namespace Yuandl.ThemeUI.Helpers;

public static class AdornerHelper
{
    public static RoutedEventHandler? ClickEvent;

    public static void ShowAdorner<T>(this UIElement element, T adorner)
        where T : Adorner
    {
        AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(element);
        if (adornerLayer != null)
        {
            adornerLayer.RemoveHandler(Button.PreviewMouseDownEvent, new RoutedEventHandler(OnClick));
            adornerLayer.AddHandler(Button.PreviewMouseDownEvent, new RoutedEventHandler(OnClick), true);
            adornerLayer.Add(adorner);
        }
    }

    private static void OnClick(object sender, RoutedEventArgs e)
    {
        ClickEvent?.Invoke(sender, e);
    }

    public static void HideAdorner<T>(this UIElement element)
        where T : Adorner
    {
        AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(element);
        if (adornerLayer != null)
        {
            Adorner[] adorners = adornerLayer.GetAdorners(element);
            if (adorners != null)
            {
                foreach (Adorner? adorner in adorners)
                {
                    if (adorner is T)
                    {
                        adornerLayer.Remove(adorner);
                    }
                }
            }
        }
    }
}