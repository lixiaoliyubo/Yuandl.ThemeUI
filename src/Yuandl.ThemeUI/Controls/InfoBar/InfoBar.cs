// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Input;

// ReSharper disable once CheckNamespace
namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// An <see cref="InfoBar" /> is an inline notification for essential app-
/// wide messages. The InfoBar will take up space in a layout and will not
/// cover up other content or float on top of it. It supports rich content
/// (including titles, messages, and icons) and can be configured to be
/// user-dismissable or persistent.
/// </summary>
public class InfoBar : System.Windows.Controls.ContentControl
{
    /// <summary>Identifies the <see cref="IsIconVisible"/> dependency property.</summary>
    public static readonly DependencyProperty IsIconVisibleProperty = DependencyProperty.Register(
        nameof(IsIconVisible),
        typeof(Visibility),
        typeof(InfoBar),
        new PropertyMetadata(Visibility.Collapsed)
    );

    /// <summary>Identifies the <see cref="IsClosable"/> dependency property.</summary>
    public static readonly DependencyProperty IsClosableProperty = DependencyProperty.Register(
        nameof(IsClosable),
        typeof(bool),
        typeof(InfoBar),
        new PropertyMetadata(true)
    );

    /// <summary>Identifies the <see cref="IsOpen"/> dependency property.</summary>
    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
        nameof(IsOpen),
        typeof(bool),
        typeof(InfoBar),
        new PropertyMetadata(false)
    );

    /// <summary>Identifies the <see cref="Title"/> dependency property.</summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(InfoBar),
        new PropertyMetadata(string.Empty)
    );

    /// <summary>Identifies the <see cref="Message"/> dependency property.</summary>
    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
        nameof(Message),
        typeof(string),
        typeof(InfoBar),
        new PropertyMetadata(string.Empty)
    );

    /// <summary>Identifies the <see cref="Appearance"/> dependency property.</summary>
    public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
        nameof(Appearance),
        typeof(ControlAppearance),
        typeof(InfoBar),
        new PropertyMetadata(ControlAppearance.Primary)
    );

    /// <summary>Identifies the <see cref="TemplateButtonCommand"/> dependency property.</summary>
    public static readonly DependencyProperty TemplateButtonCommandProperty = DependencyProperty.Register(
        nameof(TemplateButtonCommand),
        typeof(IRelayCommand),
        typeof(InfoBar),
        new PropertyMetadata(null)
    );

    /// <summary>Identifies the <see cref="Icon"/> dependency property.</summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(object),
        typeof(InfoBar),
        new PropertyMetadata(null, null)
    );

    /// <summary>Identifies the <see cref="ButtonClickedEvent"/> routed event.</summary>
    public static readonly RoutedEvent ButtonClickedEvent = EventManager.RegisterRoutedEvent(
        nameof(ButtonClicked),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<InfoBar, InfoBarButtonClickEventArgs>),
        typeof(InfoBar)
    );

    public event TypedEventHandler<InfoBar, InfoBarButtonClickEventArgs> ButtonClicked
    {
        add => AddHandler(ButtonClickedEvent, value);
        remove => RemoveHandler(ButtonClickedEvent, value);
    }

    /// <summary>Identifies the <see cref="Closing"/> routed event.</summary>
    public static readonly RoutedEvent ClosingEvent = EventManager.RegisterRoutedEvent(
        nameof(Closing),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<InfoBar, InfoBarButtonClickEventArgs>),
        typeof(InfoBar)
    );

    /// <summary>
    /// Occurs after the dialog starts to close, but before it is closed and before the <see cref="Closed"/> event occurs.
    /// </summary>
    public event TypedEventHandler<InfoBar, InfoBarButtonClickEventArgs> Closing
    {
        add => AddHandler(ClosingEvent, value);
        remove => RemoveHandler(ClosingEvent, value);
    }

    /// <summary>Identifies the <see cref="Closed"/> routed event.</summary>
    public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
        nameof(Closed),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<InfoBar, InfoBarButtonClickEventArgs>),
        typeof(InfoBar)
    );

    /// <summary>
    /// Occurs after the dialog is closed.
    /// </summary>
    public event TypedEventHandler<InfoBar, InfoBarButtonClickEventArgs> Closed
    {
        add => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    /// <summary>
    /// Gets or sets displayed <see cref="SymbolRegular"/>.
    /// </summary>
    [Bindable(true)]
    [Category("Appearance")]
    public SymbolRegular? Icon
    {
        get => (SymbolRegular?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user can close the <see cref="InfoBar" />. Defaults to <c>true</c>.
    /// </summary>
    public bool IsClosable
    {
        get => (bool)GetValue(IsClosableProperty);
        set => SetValue(IsClosableProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user can close the <see cref="InfoBar" />. Defaults to <c>true</c>.
    /// </summary>
    public Visibility IsIconVisible
    {
        get => (Visibility)GetValue(IsIconVisibleProperty);
        set => SetValue(IsIconVisibleProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="InfoBar" /> is open.
    /// </summary>
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>
    /// Gets or sets the title of the <see cref="InfoBar" />.
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets the message of the <see cref="InfoBar" />.
    /// </summary>
    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    /// <summary>
    /// Gets or sets the type of the <see cref="InfoBar" /> to apply
    /// consistent status color, icon, and assistive technology settings
    /// dependent on the criticality of the notification.
    /// </summary>
    public ControlAppearance Appearance
    {
        get => (ControlAppearance)GetValue(AppearanceProperty);
        set => SetValue(AppearanceProperty, value);
    }

    /// <summary>
    /// Gets the <see cref="RelayCommand{T}"/> triggered after clicking
    /// the close button.
    /// </summary>
    public IRelayCommand TemplateButtonCommand => (IRelayCommand)GetValue(TemplateButtonCommandProperty);

    /// <summary>
    /// Initializes a new instance of the <see cref="InfoBar"/> class.
    /// </summary>
    public InfoBar()
    {
        SetValue(
            TemplateButtonCommandProperty,
            new RelayCommand<object>(Params =>
            {
                string button = Params?.ToString() ?? "Closed";
                RaiseEvent(new InfoBarButtonClickEventArgs(ButtonClickedEvent, this) { Button = button });

                var argsClosing = new InfoBarButtonClickEventArgs(ClosingEvent, this) { Button = button };
                RaiseEvent(argsClosing);
                if (argsClosing.Cancel)
                {
                    return;
                }

                SetCurrentValue(IsOpenProperty, false);
                RaiseEvent(new InfoBarButtonClickEventArgs(ClosedEvent, this) { Button = button });
            })
        );
    }
}