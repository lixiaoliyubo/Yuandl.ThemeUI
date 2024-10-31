// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Extensions;
using Yuandl.ThemeUI.Helpers;

namespace Yuandl.ThemeUI.Controls;

public static class Loading
{
    public static void ShowLoading(this UIElement element, UIElement child)
    {
        Border border = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(150, 0, 0, 0)),
            Child = child
        };
        var loadingAdorner = new AdornerContainer(element)
        {
            Child = border,
        };
        element.ShowAdorner(loadingAdorner);
    }

    public static void ShowLoading(this UIElement element, string Text = "加载中.....", LoadingIndicatorMode indicatorMode = LoadingIndicatorMode.Ring)
    {
        StackPanel _stackPanel = new StackPanel()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        _ = _stackPanel.Children.Add(new LoadingIndicator
        {
            PenSize = 8,
            Height = 80,
            IsActive = true,
            Foreground = Brushes.White,
            Mode = indicatorMode
        });
        _ = _stackPanel.Children.Add(new TextBlock
        {
            Text = Text,
            Foreground = Brushes.White,
            FontSize = 16,
            Margin = new Thickness(0, 10, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Center
        });
        Border border = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(150, 0, 0, 0)),
            Child = _stackPanel
        };
        var loadingAdorner = new AdornerContainer(element)
        {
            Child = border,
        };
        element.ShowAdorner(loadingAdorner);
    }

    public static void HideLoading(this UIElement element)
    {
        element.HideAdorner<AdornerContainer>();
    }
}