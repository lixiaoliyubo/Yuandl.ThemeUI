// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// User a colour zone to easily switch the background and foreground colours, from selected Material Design palette or custom ones.
/// </summary>
public class TabControl : System.Windows.Controls.TabControl
{
    public TabControl()
    {
    }

    public override void OnApplyTemplate()
    {
        foreach (var item in Items)
        {
            if (item as TabItem == null)
            {
            }
        }

        base.OnApplyTemplate();
    }

    private readonly object obj = new object();

    protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
    {
        base.OnItemsSourceChanged(oldValue, newValue);
    }

    public static readonly DependencyProperty FloatingContentProperty = DependencyProperty.RegisterAttached(
         nameof(FloatingContent), typeof(object), typeof(TabControl), new PropertyMetadata(null));

    [Bindable(true)]
    [Category("Appearance")]
    public object FloatingContent
    {
        get => GetValue(FloatingContentProperty);
        set => SetValue(FloatingContentProperty, value);
    }

    public static readonly DependencyProperty HasUniformTabWidthProperty = DependencyProperty.RegisterAttached(
        nameof(HasUniformTabWidth), typeof(bool), typeof(TabControl), new PropertyMetadata(false));

    [Bindable(true)]
    [Category("Appearance")]
    public bool HasUniformTabWidth
    {
        get => (bool)GetValue(HasUniformTabWidthProperty);
        set => SetValue(HasUniformTabWidthProperty, value);
    }

    /// <summary>
    /// Occurs after the dialog is closed.
    /// </summary>
    public event TypedEventHandler<TabControl, TabItemButtonClickEventArgs> Closed
    {
        add => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    /// <summary>Identifies the <see cref="Closed"/> routed event.</summary>
    public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
        nameof(Closed),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<TabControl, TabItemButtonClickEventArgs>),
        typeof(TabControl)
    );

    public static readonly DependencyProperty ShowCloseButtonProperty = DependencyProperty.Register(
        nameof(ShowCloseButton), typeof(bool), typeof(TabControl), new PropertyMetadata(default(bool)));

    [Bindable(true)]
    [Category("Appearance")]
    public bool ShowCloseButton
    {
        get => (bool)GetValue(ShowCloseButtonProperty);
        set => SetValue(ShowCloseButtonProperty, value);
    }

    public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
        nameof(Appearance), typeof(ControlAppearance), typeof(TabControl), new PropertyMetadata(ControlAppearance.Custom));

    [Bindable(true)]
    [Category("Appearance")]
    public ControlAppearance Appearance
    {
        get => (ControlAppearance)GetValue(AppearanceProperty);
        set => SetValue(AppearanceProperty, value);
    }
}