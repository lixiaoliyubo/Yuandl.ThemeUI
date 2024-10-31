// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

public class ToggleSwitch : System.Windows.Controls.Primitives.ToggleButton
{
    // 声明一个RoutedEvent
    public static readonly RoutedEvent StateChangedEvent = EventManager.RegisterRoutedEvent("StateChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToggleSwitch));

    // 声明一个RoutedEvent

    // 将该事件包装成一个CLR事件
    public event RoutedEventHandler StateChanged
    {
        add { AddHandler(StateChangedEvent, value); }
        remove { RemoveHandler(StateChangedEvent, value); }
    }

    public ToggleSwitch()
    {
        this.Checked += ToggleSwitch_Checked;
        this.Unchecked += ToggleSwitch_Unloaded;
    }

    private void ToggleSwitch_Unloaded(object sender, RoutedEventArgs e)
    {
        RoutedEventArgs args = new RoutedEventArgs(StateChangedEvent, ToggleSwitchEventType.Unloaded);
        RaiseEvent(args);
    }

    private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
    {
        RoutedEventArgs args = new RoutedEventArgs(StateChangedEvent, ToggleSwitchEventType.Checked);
        RaiseEvent(args);
    }

    /// <summary>Identifies the <see cref="OffContent"/> dependency property.</summary>
    public static readonly DependencyProperty OffContentProperty = DependencyProperty.Register(
        nameof(OffContent),
        typeof(object),
        typeof(ToggleSwitch),
        new PropertyMetadata(null)
    );

    /// <summary>Identifies the <see cref="OnContent"/> dependency property.</summary>
    public static readonly DependencyProperty OnContentProperty = DependencyProperty.Register(
        nameof(OnContent),
        typeof(object),
        typeof(ToggleSwitch),
        new PropertyMetadata(null)
    );

    /// <summary>
    /// Gets or sets the content that should be displayed when the <see cref="ToggleSwitch"/> is in the "Off" state.
    /// </summary>
    [Bindable(true)]
    public object? OffContent
    {
        get => GetValue(OffContentProperty);
        set => SetValue(OffContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the content that should be displayed when the <see cref="ToggleSwitch"/> is in the "On" state.
    /// </summary>
    [Bindable(true)]
    public object? OnContent
    {
        get => GetValue(OnContentProperty);
        set => SetValue(OnContentProperty, value);
    }
}