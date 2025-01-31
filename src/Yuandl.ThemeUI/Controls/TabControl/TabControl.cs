// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections;
using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Yuandl.ThemeUI.Controls;


[TemplatePart(Name = OverflowScrollviewer, Type = typeof(ScrollViewer))]
[TemplatePart(Name = HeaderBorder, Type = typeof(Border))]
[TemplatePart(Name = ScrollBorder, Type = typeof(Border))]
[TemplatePart(Name = HeaderPanelKey, Type = typeof(TabPanel))]
[TemplatePart(Name = ScrollButtonLeft, Type = typeof(ButtonBase))]
[TemplatePart(Name = ScrollButtonRight, Type = typeof(ButtonBase))]
public class TabControl : System.Windows.Controls.TabControl
{
    // 模板部件名称常量
    private const string OverflowScrollviewer = "PART_OverflowScrollviewer";
    private const string HeaderPanelKey = "PART_HeaderPanel";
    private const string ScrollButtonLeft = "PART_ScrollButtonLeft";
    private const string ScrollButtonRight = "PART_ScrollButtonRight";
    private const string HeaderBorder = "PART_HeaderBorder";
    private const string ScrollBorder = "PART_ScrollBorder";

    // 私有字段
    private ScrollViewer _scrollViewerOverflow; // 滚动视图
    private ButtonBase _buttonScrollLeft; // 左滚动按钮
    private ButtonBase _buttonScrollRight; // 右滚动按钮
    private Border _headerBorder; // 头部边框

    /// <summary>
    /// 获取或设置 TabPanel 头部面板。
    /// </summary>
    internal TabPanel HeaderPanel { get; private set; }

    /// <summary>
    /// 获取或设置滚动边框。
    /// </summary>
    internal Border BorderScroll { get; private set; }

    /// <summary>
    /// 当应用模板时调用，初始化模板部件。
    /// </summary>
    public override void OnApplyTemplate()
    {
        if (_buttonScrollLeft != null) _buttonScrollLeft.Click -= ButtonScrollLeft_Click;
        if (_buttonScrollRight != null) _buttonScrollRight.Click -= ButtonScrollRight_Click;

        base.OnApplyTemplate();
        BorderScroll = GetTemplateChild(ScrollBorder) as Border;
        _headerBorder = GetTemplateChild(HeaderBorder) as Border;
        _scrollViewerOverflow = GetTemplateChild(OverflowScrollviewer) as ScrollViewer;
        HeaderPanel = GetTemplateChild(HeaderPanelKey) as TabPanel;
        _buttonScrollLeft = GetTemplateChild(ScrollButtonLeft) as ButtonBase;
        _buttonScrollRight = GetTemplateChild(ScrollButtonRight) as ButtonBase;

        if (_buttonScrollLeft != null) _buttonScrollLeft.Click += ButtonScrollLeft_Click;
        if (_buttonScrollRight != null) _buttonScrollRight.Click += ButtonScrollRight_Click;
    }

    /// <summary>
    /// 依赖属性：是否显示关闭按钮。
    /// </summary>
    public static readonly DependencyProperty ShowCloseButtonProperty = DependencyProperty.RegisterAttached(
        nameof(ShowCloseButton),
        typeof(bool),
        typeof(TabControl),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// 设置是否显示关闭按钮。
    /// </summary>
    /// <param name="element">目标元素。</param>
    /// <param name="value">是否显示。</param>
    public static void SetShowCloseButton(DependencyObject element, bool value)
        => element.SetValue(ShowCloseButtonProperty, value);

    /// <summary>
    /// 获取是否显示关闭按钮。
    /// </summary>
    /// <param name="element">目标元素。</param>
    /// <returns>是否显示。</returns>
    public static bool GetShowCloseButton(DependencyObject element)
        => (bool)element.GetValue(ShowCloseButtonProperty);

    /// <summary>
    /// 获取或设置是否显示关闭按钮。
    /// </summary>
    public bool ShowCloseButton
    {
        get { return (bool)GetValue(ShowCloseButtonProperty); }
        set { SetValue(ShowCloseButtonProperty, value); }
    }

    /// <summary>
    /// 是否允许拖动。
    /// </summary>
    public static readonly DependencyProperty AllowDragProperty = DependencyProperty.Register(
        nameof(AllowDrag), typeof(bool), typeof(TabControl), new PropertyMetadata(true));


    /// <summary>
    /// 获取或设置是否允许拖动。
    /// </summary>
    public bool AllowDrag
    {
        get { return (bool)GetValue(AllowDragProperty); }
        set { SetValue(AllowDragProperty, value); }
    }

    /// <summary>
    /// 是否显示上下文菜单
    /// </summary>
    public static readonly DependencyProperty ShowContextMenuProperty = DependencyProperty.RegisterAttached(
        nameof(ShowContextMenu), typeof(bool), typeof(TabControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// 设置是否显示上下文菜单。
    /// </summary>
    /// <param name="element">目标元素。</param>
    /// <param name="value">是否显示。</param>
    public static void SetShowContextMenu(DependencyObject element, bool value)
        => element.SetValue(ShowContextMenuProperty, value);

    /// <summary>
    /// 获取是否显示上下文菜单。
    /// </summary>
    /// <param name="element">目标元素。</param>
    /// <returns>是否显示。</returns>
    public static bool GetShowContextMenu(DependencyObject element)
        => (bool)element.GetValue(ShowContextMenuProperty);

    /// <summary>
    /// 是否显示上下文菜单
    /// </summary>
    public bool ShowContextMenu
    {
        get => (bool)GetValue(ShowContextMenuProperty);
        set => SetValue(ShowContextMenuProperty, value);
    }

    /// <summary>
    /// 标签宽度
    /// </summary>
    public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(
        nameof(ItemWidth), typeof(double), typeof(TabControl), new PropertyMetadata(130.0));

    /// <summary>
    /// 标签宽度
    /// </summary>
    public double ItemWidth
    {
        get => (double)GetValue(ItemWidthProperty);
        set => SetValue(ItemWidthProperty, value);
    }

    /// <summary>
    /// 标签高度
    /// </summary>
    public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(
        nameof(ItemHeight), typeof(double), typeof(TabControl), new PropertyMetadata(35.0));

    /// <summary>
    /// 标签高度
    /// </summary>
    public double ItemHeight
    {
        get => (double)GetValue(ItemHeightProperty);
        set => SetValue(ItemHeightProperty, value);
    }

    #region Events

    /// <summary>
    /// 标签页关闭前的路由事件
    /// </summary>
    public static readonly RoutedEvent ClosingEvent = EventManager.RegisterRoutedEvent(nameof(Closing), RoutingStrategy.Bubble, typeof(ClosingEventHandler), typeof(TabControl));

    /// <summary>
    /// 标签页关闭后的路由事件
    /// </summary>
    public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(nameof(Closed), RoutingStrategy.Bubble, typeof(ClosedEventHandler), typeof(TabControl));

    /// <summary>
    /// 标签页关闭前事件
    /// </summary>
    public event ClosingEventHandler Closing
    {
        add { AddHandler(ClosingEvent, value); }
        remove { RemoveHandler(ClosingEvent, value); }
    }

    /// <summary>
    /// 标签页关闭后事件
    /// </summary>
    public event ClosedEventHandler Closed
    {
        add { AddHandler(ClosedEvent, value); }
        remove { RemoveHandler(ClosedEvent, value); }
    }

    #endregion

    /// <summary>
    /// 获取水平滚动偏移量。
    /// </summary>
    /// <returns>水平偏移量。</returns>
    internal double GetHorizontalOffset() => _scrollViewerOverflow?.HorizontalOffset ?? 0;

    /// <summary>
    /// 获取垂直滚动偏移量。
    /// </summary>
    /// <returns>垂直偏移量。</returns>
    internal double GetVerticalOffset() => _scrollViewerOverflow?.VerticalOffset ?? 0;

    /// <summary>
    /// 更新滚动视图。
    /// </summary>
    internal void UpdateScroll() => _scrollViewerOverflow?.RaiseEvent(new MouseWheelEventArgs(Mouse.PrimaryDevice, Environment.TickCount, 0)
    {
        RoutedEvent = MouseWheelEvent
    });

    /// <summary>
    /// 右滚动按钮点击事件处理。
    /// </summary>
    /// <param name="sender">事件源。</param>
    /// <param name="e">事件参数。</param>
    private void ButtonScrollRight_Click(object sender, RoutedEventArgs e)
    {
        if (_scrollViewerOverflow == null)
        {
            return;
        }

        if (TabStripPlacement == Dock.Left || TabStripPlacement == Dock.Right)
        {
            var offseHeight = Math.Min(_scrollViewerOverflow.ContentVerticalOffset + ItemHeight, _scrollViewerOverflow.ScrollableHeight);
            _scrollViewerOverflow.ScrollToVerticalOffsetWithAnimation(offseHeight);
        }
        else
        {
            var offsetWidth = Math.Min(_scrollViewerOverflow.CurrentHorizontalOffset + ItemWidth, _scrollViewerOverflow.ScrollableWidth);
            _scrollViewerOverflow.ScrollToHorizontalOffsetWithAnimation(offsetWidth);
        }
    }

    /// <summary>
    /// 左滚动按钮点击事件处理。
    /// </summary>
    /// <param name="sender">事件源。</param>
    /// <param name="e">事件参数。</param>
    private void ButtonScrollLeft_Click(object sender, RoutedEventArgs e)
    {
        if (_scrollViewerOverflow == null) return;
        if (TabStripPlacement == Dock.Left || TabStripPlacement == Dock.Right)
        {
            var offsetHeight = Math.Max(_scrollViewerOverflow.ContentVerticalOffset - ItemHeight, 0);
            _scrollViewerOverflow.ScrollToVerticalOffsetWithAnimation(offsetHeight);
        }
        else
        {
            var offsetWidth = Math.Max(_scrollViewerOverflow.CurrentHorizontalOffset - ItemWidth, 0);
            _scrollViewerOverflow.ScrollToHorizontalOffsetWithAnimation(offsetWidth);
        }
    }

    /// <summary>
    /// 关闭指定 TabItem。
    /// </summary>
    /// <param name="tabItem">要关闭的 TabItem。</param>
    internal void Close(TabItem tabItem)
    {
        if (!tabItem.ShowCloseButton)
        {
            return;
        }

        var item = ItemContainerGenerator.ItemFromContainer(tabItem);
        var argsClosing = new ClosingEventArgs(item);
        RaiseEvent(argsClosing);
        if (argsClosing.Cancel)
        {
            return;
        }

        RaiseEvent(new ClosedEventArgs(item));
        IList list = GetActualList();
        if (list.Count == 0)
        {
            return;
        }

        list.Remove(item);
    }

    /// <summary>
    /// 关闭所有 TabItem。
    /// </summary>
    internal void CloseAllItems() => CloseOtherItems();

    /// <summary>
    /// 关闭除指定 TabItem 外的所有 TabItem。
    /// </summary>
    /// <param name="parameter">指定的 TabItem。</param>
    internal void CloseOtherItems(object? parameter = null)
    {
        TabItem? currentItem = (TabItem?)parameter;
        var actualItem = currentItem != null ? ItemContainerGenerator.ItemFromContainer(currentItem) : null;

        IList list = GetActualList();
        if (list.Count == 0)
        {
            return;
        }

        for (var i = 0; i < list.Count; i++)
        {
            var item = list[i];
            if (!Equals(item, actualItem) && item != null)
            {
                var argsClosing = new ClosingEventArgs(item);

                if (ItemContainerGenerator.ContainerFromItem(item) is not TabItem tabItem)
                {
                    continue;
                }

                if (!tabItem.ShowCloseButton)
                {
                    continue;
                }

                tabItem.RaiseEvent(argsClosing);
                if (argsClosing.Cancel)
                {
                    continue;
                }

                tabItem.RaiseEvent(new ClosedEventArgs(item));
                list.Remove(item);

                i--;
            }
        }

        SetCurrentValue(SelectedIndexProperty, list.Count == 0 ? -1 : 0);
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.Property == TabStripPlacementProperty)
        {
            // Reset scroll position
            if (_scrollViewerOverflow != null)
            {
                _scrollViewerOverflow.ScrollToHorizontalOffsetWithAnimation(0);
                _scrollViewerOverflow.ScrollToVerticalOffsetWithAnimation(0);
            }

            // Force layout update
            _headerBorder?.InvalidateMeasure();
            _headerBorder?.InvalidateArrange();
            BorderScroll?.InvalidateMeasure();
            BorderScroll?.InvalidateArrange();
            HeaderPanel?.InvalidateMeasure();
            HeaderPanel?.InvalidateArrange();

            UpdateLayout();
        }
    }

    /// <summary>
    /// 当 Items 集合发生变化时调用。
    /// </summary>
    /// <param name="e">集合变化事件参数。</param>
    protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    {
        base.OnItemsChanged(e);
        if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (ItemContainerGenerator.ContainerFromIndex(i) is not TabItem item) return;
                item.CurrentIndex = i;
                item.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }
        }

        _headerBorder?.InvalidateMeasure();
    }

    internal IList GetActualList()
    {
        IList list;
        if (ItemsSource != null)
        {
            list = ItemsSource as IList;
        }
        else
        {
            list = Items;
        }

        return list;
    }

    protected override bool IsItemItsOwnContainerOverride(object item) => item is TabItem;

    protected override DependencyObject GetContainerForItemOverride() => new TabItem();
}