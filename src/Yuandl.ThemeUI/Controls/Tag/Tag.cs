// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Input;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Input;

namespace Yuandl.ThemeUI.Controls;

public class Tag : ContentControl
{
    public Tag()
    {
        SetValue(TemplateButtonCommandProperty, new RelayCommand<Tag?>(OnButtonClick));
    }

    private void OnButtonClick(Tag? tag)
    {
        var argsClosing = new CancelRoutedEventArgs(ClosingEvent, this);
        RaiseEvent(argsClosing);
        if (argsClosing.Cancel)
        {
            return;
        }

        var parent = VisualTreeHelper.GetParent(tag) as Panel;
        if (parent != null)
        {
            parent.Children.Remove(tag);
        }

        RaiseEvent(new RoutedEventArgs(ClosedEvent, this));
    }

    /// <summary>Identifies the <see cref="Appearance"/> dependency property.</summary>
    public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
        nameof(Appearance),
        typeof(ControlAppearance),
        typeof(Tag),
        new PropertyMetadata(ControlAppearance.Custom)
    );

    public ControlAppearance Appearance
    {
        get => (ControlAppearance)GetValue(AppearanceProperty);
        set => SetValue(AppearanceProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(Tag),
        new PropertyMetadata(new CornerRadius(15))
    );

    /// <summary>
    /// Gets or sets the corner radius of the control.
    /// </summary>
    [Bindable(true)]
    [Category("Appearance")]
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>Identifies the <see cref="TemplateButtonCommand"/> dependency property.</summary>
    public static readonly DependencyProperty TemplateButtonCommandProperty = DependencyProperty.Register(
        nameof(TemplateButtonCommand),
        typeof(IRelayCommand),
        typeof(Tag),
        new PropertyMetadata(null)
    );

    public IRelayCommand TemplateButtonCommand => (IRelayCommand)GetValue(TemplateButtonCommandProperty);

    public static readonly DependencyProperty ShowCloseButtonProperty = DependencyProperty.Register(
        nameof(ShowCloseButton), typeof(bool), typeof(Tag), new PropertyMetadata(true));

    public bool ShowCloseButton
    {
        get => (bool)GetValue(ShowCloseButtonProperty);
        set => SetValue(ShowCloseButtonProperty, value);
    }

    public static readonly DependencyProperty SelectableProperty = DependencyProperty.Register(
        nameof(Selectable), typeof(bool), typeof(Tag), new PropertyMetadata(false));

    public bool Selectable
    {
        get => (bool)GetValue(SelectableProperty);
        set => SetValue(SelectableProperty, value);
    }

    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
        nameof(IsSelected), typeof(bool), typeof(Tag), new PropertyMetadata(false, (o, args) =>
        {
            var ctl = (Tag)o;
            ctl.RaiseEvent(new RoutedEventArgs(SelectedEvent, ctl));
        }));

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header), typeof(object), typeof(Tag), new PropertyMetadata(default, OnHeaderChanged));

    private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (Tag)d;

        ctrl.SetValue(HasHeaderPropertyKey, e.NewValue != null ? true : false);
        ctrl.OnHeaderChanged(e.OldValue, e.NewValue);
    }

    protected virtual void OnHeaderChanged(object oldHeader, object newHeader)
    {
        RemoveLogicalChild(oldHeader);
        AddLogicalChild(newHeader);
    }

    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    internal static readonly DependencyPropertyKey HasHeaderPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(HasHeader), typeof(bool), typeof(Tag), new PropertyMetadata(false));

    public static readonly DependencyProperty HasHeaderProperty = HasHeaderPropertyKey.DependencyProperty;

    [Bindable(false)]
    [Browsable(false)]
    public bool HasHeader => (bool)GetValue(HasHeaderProperty);

    public static readonly DependencyProperty HeaderStringFormatProperty = DependencyProperty.Register(
        nameof(HeaderStringFormat), typeof(string), typeof(Tag), new PropertyMetadata(default(string)));

    public string HeaderStringFormat
    {
        get => (string)GetValue(HeaderStringFormatProperty);
        set => SetValue(HeaderStringFormatProperty, value);
    }

    public static readonly DependencyProperty HeaderTemplateSelectorProperty = DependencyProperty.Register(
        nameof(HeaderTemplateSelector), typeof(DataTemplateSelector), typeof(Tag), new PropertyMetadata(default(DataTemplateSelector)));

    public DataTemplateSelector HeaderTemplateSelector
    {
        get => (DataTemplateSelector)GetValue(HeaderTemplateSelectorProperty);
        set => SetValue(HeaderTemplateSelectorProperty, value);
    }

    public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
        nameof(HeaderTemplate), typeof(DataTemplate), typeof(Tag), new PropertyMetadata(default(DataTemplate)));

    public DataTemplate HeaderTemplate
    {
        get => (DataTemplate)GetValue(HeaderTemplateProperty);
        set => SetValue(HeaderTemplateProperty, value);
    }

    /// <summary>Identifies the <see cref="Closing"/> routed event.</summary>
    public static readonly RoutedEvent SelectedEvent = EventManager.RegisterRoutedEvent(
        nameof(Selected),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<Tag, RoutedEventArgs>),
        typeof(Tag)
    );

    public event TypedEventHandler<Tag, RoutedEventArgs> Selected
    {
        add => AddHandler(SelectedEvent, value);
        remove => RemoveHandler(SelectedEvent, value);
    }

    /// <summary>Identifies the <see cref="Closing"/> routed event.</summary>
    public static readonly RoutedEvent ClosingEvent = EventManager.RegisterRoutedEvent(
        nameof(Closing),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<Tag, RoutedEventArgs>),
        typeof(Tag)
    );

    /// <summary>
    /// Occurs after the dialog starts to close, but before it is closed and before the <see cref="Closed"/> event occurs.
    /// </summary>
    public event TypedEventHandler<Tag, RoutedEventArgs> Closing
    {
        add => AddHandler(ClosingEvent, value);
        remove => RemoveHandler(ClosingEvent, value);
    }

    /// <summary>Identifies the <see cref="Closed"/> routed event.</summary>
    public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
        nameof(Closed),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<Tag, RoutedEventArgs>),
        typeof(Tag)
    );

    /// <summary>
    /// Occurs after the dialog is closed.
    /// </summary>
    public event TypedEventHandler<Tag, RoutedEventArgs> Closed
    {
        add => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        if (Selectable)
        {
            IsSelected = true;
        }
    }
}