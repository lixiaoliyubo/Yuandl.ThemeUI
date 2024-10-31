// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Input;

namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// User a colour zone to easily switch the background and foreground colours, from selected Material Design palette or custom ones.
/// </summary>
public class TabItem : System.Windows.Controls.TabItem
{
    public TabItem()
    {
        SetValue(TemplateButtonCommandProperty, new RelayCommand<TabItemButton>(OnButtonClick));

        // Loaded += (s, e) => OnMenuChanged(Menu);
    }

    /// <summary>Identifies the <see cref="TemplateButtonCommand"/> dependency property.</summary>
    public static readonly DependencyProperty TemplateButtonCommandProperty = DependencyProperty.Register(
        nameof(TemplateButtonCommand),
        typeof(IRelayCommand),
        typeof(TabItem),
        new PropertyMetadata(null)
    );

    public IRelayCommand TemplateButtonCommand => (IRelayCommand)GetValue(TemplateButtonCommandProperty);

    /// <summary>Identifies the <see cref="ButtonClicked"/> routed event.</summary>
    public static readonly RoutedEvent ButtonClickedEvent = EventManager.RegisterRoutedEvent(
        nameof(ButtonClicked),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<TabItem, TabItemButtonClickEventArgs>),
        typeof(TabItem)
    );

    /// <summary>
    /// Occurs after the <see cref="TabItemButton"/> has been tapped.
    /// </summary>
    public event TypedEventHandler<TabItem, TabItemButtonClickEventArgs> ButtonClicked
    {
        add => AddHandler(ButtonClickedEvent, value);
        remove => RemoveHandler(ButtonClickedEvent, value);
    }

    /// <summary>
    /// Invoked when a <see cref="TabItemButton"/> is clicked.
    /// </summary>
    /// <param name="button">The button that was clicked.</param>
    protected virtual void OnButtonClick(TabItemButton button)
    {
        var buttonClickEventArgs = new TabItemButtonClickEventArgs(ButtonClickedEvent, this)
        {
            Button = button
        };
        RaiseEvent(buttonClickEventArgs);
        TabControl tabControl = (TabControl)GetParentObject<TabControl>(this);
        if (tabControl != null)
        {
            switch (button)
            {
                case TabItemButton.Close:
                    tabControl.RaiseEvent(new TabItemButtonClickEventArgs(TabControl.ClosedEvent, this) { Button = button });
                    tabControl.Items.Remove(this);
                    break;
                case TabItemButton.CloseAll:
                    break;
                case TabItemButton.CloseOther:
                    break;
                default:
                    break;
            }
        }

    }

    // 获得指定元素的父元素
    // </summary>
    // <typeparam name="T">父级元素类型</typeparam>
    // <param name="obj">指定查找元素</param>
    // <returns></returns>
    public T GetParentObject<T>(DependencyObject obj)
        where T : FrameworkElement
    {
        // 返回可视对象的父对象
        DependencyObject parent = VisualTreeHelper.GetParent(obj);

        // 按层、类型提取父级
        while (parent != null)
        {
            if (parent is T)
            {
                return (T)parent;
            }

            parent = VisualTreeHelper.GetParent(parent);
        }

        // 返回
        return null;
    }
}