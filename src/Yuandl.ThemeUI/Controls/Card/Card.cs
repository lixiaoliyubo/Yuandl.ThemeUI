// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// Simple Card with content and <see cref="Footer"/>.
/// </summary>
public class Card : System.Windows.Controls.ContentControl
{
    /// <summary>Identifies the <see cref="Header"/> dependency property.</summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header), typeof(object), typeof(Card), new PropertyMetadata(null, OnHeaderChanged)
    );

    /// <summary>Identifies the <see cref="HasHeader"/> dependency property.</summary>
    public static readonly DependencyProperty HasHeaderProperty = DependencyProperty.Register(
        nameof(HasHeader), typeof(bool), typeof(Card), new PropertyMetadata(false)
    );

    public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
        nameof(HeaderTemplate), typeof(DataTemplate), typeof(Card), new PropertyMetadata(default(DataTemplate))
    );

    public static readonly DependencyProperty HeaderTemplateSelectorProperty = DependencyProperty.Register(
        nameof(HeaderTemplateSelector), typeof(DataTemplateSelector), typeof(Card), new PropertyMetadata(default(DataTemplateSelector))
    );

    public static readonly DependencyProperty HeaderStringFormatProperty = DependencyProperty.Register(
        nameof(HeaderStringFormat), typeof(string), typeof(Card), new PropertyMetadata(default(string))
    );

    /// <summary>Identifies the <see cref="Footer"/> dependency property.</summary>
    public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(
        nameof(Footer), typeof(object), typeof(Card), new PropertyMetadata(null, OnFooterChanged)
    );

    /// <summary>Identifies the <see cref="HasFooter"/> dependency property.</summary>
    public static readonly DependencyProperty HasFooterProperty = DependencyProperty.Register(
        nameof(HasFooter), typeof(bool), typeof(Card), new PropertyMetadata(false)
    );

    public static readonly DependencyProperty FooterTemplateProperty = DependencyProperty.Register(
        nameof(FooterTemplate), typeof(DataTemplate), typeof(Card), new PropertyMetadata(default(DataTemplate))
    );

    public static readonly DependencyProperty FooterTemplateSelectorProperty = DependencyProperty.Register(
        nameof(FooterTemplateSelector), typeof(DataTemplateSelector), typeof(Card), new PropertyMetadata(default(DataTemplateSelector))
    );

    public static readonly DependencyProperty FooterStringFormatProperty = DependencyProperty.Register(
        nameof(FooterStringFormat), typeof(string), typeof(Card), new PropertyMetadata(default(string))
    );

    /// <summary>
    /// Gets or sets additional content displayed at the bottom.
    /// </summary>
    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Card"/> has a <see cref="HasHeader"/>.
    /// </summary>
    public bool HasHeader
    {
        get => (bool)GetValue(HasHeaderProperty);
        internal set => SetValue(HasHeaderProperty, value);
    }

    [Bindable(true)]
    [Category("Content")]
    public string HeaderStringFormat
    {
        get => (string)GetValue(HeaderStringFormatProperty);
        set => SetValue(HeaderStringFormatProperty, value);
    }

    [Bindable(true)]
    [Category("Content")]
    public DataTemplate HeaderTemplate
    {
        get => (DataTemplate)GetValue(HeaderTemplateProperty);
        set => SetValue(HeaderTemplateProperty, value);
    }

    [Bindable(true)]
    [Category("Content")]
    public DataTemplateSelector HeaderTemplateSelector
    {
        get => (DataTemplateSelector)GetValue(HeaderTemplateSelectorProperty);
        set => SetValue(HeaderTemplateSelectorProperty, value);
    }

    /// <summary>
    /// Gets or sets additional content displayed at the bottom.
    /// </summary>
    public object? Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Card"/> has a <see cref="Footer"/>.
    /// </summary>
    public bool HasFooter
    {
        get => (bool)GetValue(HasFooterProperty);
        internal set => SetValue(HasFooterProperty, value);
    }

    [Bindable(true)]
    [Category("Content")]
    public DataTemplate FooterTemplate
    {
        get => (DataTemplate)GetValue(FooterTemplateProperty);
        set => SetValue(FooterTemplateProperty, value);
    }

    [Bindable(true)]
    [Category("Content")]
    public DataTemplateSelector FooterTemplateSelector
    {
        get => (DataTemplateSelector)GetValue(FooterTemplateSelectorProperty);
        set => SetValue(FooterTemplateSelectorProperty, value);
    }

    [Bindable(true)]
    [Category("Content")]
    public string FooterStringFormat
    {
        get => (string)GetValue(FooterStringFormatProperty);
        set => SetValue(FooterStringFormatProperty, value);
    }

    private static void OnFooterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Card control)
        {
            return;
        }

        control.SetValue(HasFooterProperty, control.Footer != null);
    }

    private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Card control)
        {
            return;
        }

        control.SetValue(HasHeaderProperty, control.Header != null);
    }
}