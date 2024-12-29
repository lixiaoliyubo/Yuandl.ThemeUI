// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Input;

namespace Yuandl.ThemeUI.Controls;

public class FormItem : ContentControl
{
    static FormItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FormItem), new FrameworkPropertyMetadata(typeof(FormItem)));
    }

    public static readonly DependencyProperty PrefixProperty =
        DependencyProperty.Register(
            nameof(Prefix),
            typeof(string),
            typeof(FormItem),
            new PropertyMetadata("标签信息："));

    public static readonly DependencyProperty PrefixWidthProperty =
        DependencyProperty.Register(
            nameof(PrefixWidth),
            typeof(double),
            typeof(FormItem),
            new PropertyMetadata(100.0d));

    public string Prefix
    {
        get => (string)GetValue(PrefixProperty);
        set => SetValue(PrefixProperty, value);
    }

    public double PrefixWidth
    {
        get => (double)GetValue(PrefixWidthProperty);
        set => SetValue(PrefixWidthProperty, value);
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        if (Content is UIElement element)
        {
            _ = element.Focus();
        }

        base.OnMouseDown(e);
    }
}