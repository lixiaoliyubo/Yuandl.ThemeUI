// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Documents;
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

    /// <summary>
    /// DependencyProperty for <see cref="Orientation" /> property.
    /// </summary>
    public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(
                    nameof(Orientation),
                    typeof(Orientation),
                    typeof(FormItem),
                    new FrameworkPropertyMetadata(
                            Orientation.Horizontal,
                            FrameworkPropertyMetadataOptions.AffectsMeasure,
                            new PropertyChangedCallback(OnOrientationChanged)));

    /// <summary>
    /// DependencyProperty for <see cref="TextAlignment" /> property.
    /// </summary>
    public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register(
                    nameof(TextAlignment),
                    typeof(TextAlignment),
                    typeof(FormItem),
                    new FrameworkPropertyMetadata(TextAlignment.Left));

    /// <summary>
    /// The TextAlignment property specifies horizontal alignment of the content.
    /// </summary>
    public TextAlignment TextAlignment
    {
        get { return (TextAlignment)GetValue(TextAlignmentProperty); }
        set { SetValue(TextAlignmentProperty, value); }
    }

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

    /// <summary>
    /// Specifies dimension of children stacking.
    /// </summary>
    public Orientation Orientation
    {
        get { return (Orientation)GetValue(OrientationProperty); }
        set { SetValue(OrientationProperty, value); }
    }


    //-----------------------------------------------------------
    // Avalon Property Callbacks/Overrides
    //-----------------------------------------------------------
    #region Avalon Property Callbacks/Overrides

    /// <summary>
    /// <see cref="PropertyMetadata.PropertyChangedCallback"/>
    /// </summary>
    private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Since Orientation is so essential to logical scrolling/virtualization, we synchronously check if
        // the new value is different and clear all scrolling data if so.
        ResetScrolling(d as FormItem);
    }

    #endregion

    private static void ResetScrolling(FormItem element)
    {
        element.InvalidateMeasure();
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