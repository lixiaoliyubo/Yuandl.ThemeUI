// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Yuandl.ThemeUI.Controls;

public class TabItem : System.Windows.Controls.TabItem
{
    /// <summary>
    /// 是否显示关闭按钮。
    /// </summary>
    public static readonly DependencyProperty ShowCloseButtonProperty = TabControl.ShowCloseButtonProperty.AddOwner(typeof(TabItem));

    public bool ShowCloseButton
    {
        get => (bool)GetValue(ShowCloseButtonProperty);
        set => SetValue(ShowCloseButtonProperty, value);
    }

    /// <summary>
    /// 是否显示上下文菜单。
    /// </summary>
    public static readonly DependencyProperty ShowContextMenuProperty =
        TabControl.ShowContextMenuProperty.AddOwner(
            typeof(TabItem),
            new FrameworkPropertyMetadata(OnShowContextMenuChanged));

    public bool ShowContextMenu
    {
        get => (bool)GetValue(ShowContextMenuProperty);
        set => SetValue(ShowContextMenuProperty, value);
    }

    /// <summary>
    /// 上下文菜单。
    /// </summary>
    public static readonly DependencyProperty MenuProperty = DependencyProperty.Register(
        nameof(Menu),
        typeof(ContextMenu),
        typeof(TabItem),
        new PropertyMetadata(default(ContextMenu), OnMenuChanged));

    public ContextMenu? Menu
    {
        get => (ContextMenu?)GetValue(MenuProperty);
        set => SetValue(MenuProperty, value);
    }

    // 私有字段：用于拖动逻辑的状态管理
    private bool _isWaiting; // 是否正在等待拖动
    private bool _isMouseDownDown = false; // 鼠标是否按下
    private bool _isDragged = false; // 是否正在拖动
    private Point _dragPoint; // 拖动起始点
    private Point _mouseDownPoint; // 鼠标按下时的位置
    private double _mouseDownOffsetX; // 鼠标按下时的水平偏移
    private double _mouseDownOffsetY; // 鼠标按下时的垂直偏移
    private double _scrollHorizontalOffset; // 水平滚动偏移
    private double _scrollVerticalOffset; // 垂直滚动偏移
    private double _maxMoveRight; // 向右移动的最大边界
    private double _maxMoveButtom; // 向下移动的最大边界
    private double _maxMoveLeft; // 向左移动的最大边界
    private double _maxMoveTop; // 向上移动的最大边界

    /// <summary>
    /// Gets 获取父级 TabControl。
    /// </summary>
    private TabControl TabControlParent => (TabControl)ItemsControl.ItemsControlFromItemContainer(this);

    /// <summary>
    /// 当前 TabItem 的索引
    /// </summary>
    private int _currentIndex;

    /// <summary>
    /// Gets or sets 当前 TabItem 的索引
    /// </summary>
    internal int CurrentIndex
    {
        get => _currentIndex;
        set
        {
            if (_currentIndex == value || value < 0) return;
            var oldIndex = _currentIndex;
            _currentIndex = value;
            UpdateItemOffset(oldIndex);
        }
    }

    public TabItem()
    {
        _ = CommandBindings.Add(new CommandBinding(ControlCommands.Close, (s, e) => TabControlParent.Close(this)));
        _ = CommandBindings.Add(new CommandBinding(ControlCommands.CloseAll, (s, e) => TabControlParent.CloseAllItems()));
        _ = CommandBindings.Add(new CommandBinding(ControlCommands.CloseOther, (s, e) => TabControlParent.CloseOtherItems(this), (s, e) => e.CanExecute = TabControlParent.Items.Count > 1));
        Loaded += (s, e) => OnMenuChanged(Menu);
    }

    /// <summary>
    /// 当 ShowContextMenu 属性变化时的回调。
    /// </summary>
    /// <param name="e">属性变化事件参数。</param>
    protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseRightButtonDown(e);
        if (!IsMouseOverHeader(e)) return;
        IsSelected = true;
        Focus();
    }

    /// <summary>
    /// 鼠标按下。
    /// </summary>
    /// <param name="e">MouseButtonEventArgs 事件</param>
    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        if (e is { ChangedButton: MouseButton.Middle, ButtonState: MouseButtonState.Pressed } &&

            IsMouseOverHeader(e) &&
            (ShowCloseButton || ShowContextMenu))
        {
            TabControlParent.Close(this);
        }
    }

    /// <summary>
    /// 当 ShowContextMenu 属性变化时的回调。
    /// </summary>
    /// <param name="d">依赖对象。</param>
    /// <param name="e">属性变化事件参数。</param>
    private static void OnShowContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TabItem item && item.Menu != null)
        {
            var show = (bool)e.NewValue;
            item.Menu.SetCurrentValue(IsEnabledProperty, show);
            if (show)
            {
                item.Menu.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                item.Menu.SetCurrentValue(IsEnabledProperty, true);
            }
            else
            {
                item.Menu.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                item.Menu.SetCurrentValue(IsEnabledProperty, false);
            }
        }
    }

    /// <summary>
    /// 当菜单变化时的处理逻辑。
    /// </summary>
    /// <param name="menu">新的上下文菜单。</param>
    private void OnMenuChanged(ContextMenu menu)
    {
        if (!IsLoaded || menu == null)
        {
            return;
        }

        TabControl parent = TabControlParent;
        if (parent == null)
        {
            return;
        }

        var item = parent.ItemContainerGenerator.ItemFromContainer(this);
        menu.DataContext = item;

        _ = menu.SetBinding(IsEnabledProperty, new Binding(ShowContextMenuProperty.Name) { Source = this });
        _ = menu.SetBinding(VisibilityProperty, new Binding(ShowContextMenuProperty.Name)
        {
            Source = this,
            Converter = (IValueConverter)FindResource("BooleanToVisibilityConverter")
        });
    }

    /// <summary>
    /// 当 Menu 属性变化时的回调。
    /// </summary>
    /// <param name="d">依赖对象。</param>
    /// <param name="e">属性变化事件参数。</param>
    private static void OnMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TabItem item)
        {
            item.OnMenuChanged(e.NewValue as ContextMenu);
        }
    }

    /// <summary>
    /// 鼠标左键按下事件处理。
    /// </summary>
    /// <param name="e">鼠标事件参数。</param>
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        if (!IsParentTabControlAllowDrag())
        {
            return;
        }

        if (!IsMouseOverHeader(e) || _isMouseDownDown)
        {
            return;
        }

        TabControl parent = TabControlParent;
        if (parent == null)
        {
            return;
        }

        // 更改鼠标图标
        Mouse.OverrideCursor = Cursors.Hand;

        parent.UpdateScroll();
        _dragPoint = e.GetPosition(parent);
        var isVertical = parent.TabStripPlacement == Dock.Left || parent.TabStripPlacement == Dock.Right;
        if (isVertical)
        {
            _scrollHorizontalOffset = parent.GetVerticalOffset(); // 获取垂直滚动偏移量
            _maxMoveTop = -TransformToAncestor(parent).Transform(new Point(0, 0)).Y;
            _maxMoveButtom = parent.ActualHeight - ActualHeight + _maxMoveTop;
            _mouseDownOffsetY = RenderTransform.Value.OffsetY;
            _dragPoint = new Point(_dragPoint.X, _dragPoint.Y + _scrollVerticalOffset);
        }
        else
        {
            _scrollHorizontalOffset = parent.GetHorizontalOffset(); // 获取水平滚动偏移量
            _maxMoveLeft = -TransformToAncestor(parent).Transform(new Point(0, 0)).X;
            _maxMoveRight = parent.ActualWidth - ActualWidth + _maxMoveLeft;
            _mouseDownOffsetX = RenderTransform.Value.OffsetX;
            _dragPoint = new Point(_dragPoint.X + _scrollHorizontalOffset, _dragPoint.Y);
        }

        _mouseDownPoint = _dragPoint;
        _isWaiting = true;
        _isMouseDownDown = true;
        _ = CaptureMouse();
    }

    /// <summary>
    /// 鼠标移动事件处理。
    /// </summary>
    /// <param name="e">鼠标事件参数。</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (!_isMouseDownDown || !IsParentTabControlAllowDrag())
        {
            return;
        }

        TabControl parent = TabControlParent; // 获取父级 TabControl
        if (parent == null)
        {
            return;
        }

        var isVertical = parent.TabStripPlacement == Dock.Left || parent.TabStripPlacement == Dock.Right; // 判断 TabControl 的方向

        _scrollHorizontalOffset = parent.GetHorizontalOffset(); // 获取水平滚动偏移量
        _scrollVerticalOffset = parent.GetVerticalOffset(); // 获取水平滚动偏移量
        Point p = e.GetPosition(parent); // 获取鼠标在父级中的位置
        p = isVertical ? new Point(p.X, p.Y + _scrollVerticalOffset) : new Point(p.X + _scrollHorizontalOffset, p.Y);

        double offset;
        if (isVertical)
        {
            var subTop = p.Y - _dragPoint.Y;
            if (Math.Abs(subTop) <= (ActualHeight * 0.1))
            {
                return;
            }

            var subY = TranslatePoint(default(Point), parent).Y + _scrollVerticalOffset;
            CurrentIndex = CalLocationIndex(subY);
            _isWaiting = false;
            _isDragged = true;

            var totalTop = p.Y - _mouseDownPoint.Y;
            offset = subTop + RenderTransform.Value.OffsetY;
            if (totalTop < _maxMoveTop)
            {
                offset = _maxMoveTop + _mouseDownOffsetY;
            }
            else if (totalTop > _maxMoveButtom)
            {
                offset = _maxMoveButtom + _mouseDownOffsetY;
            }
        }
        else
        {
            var subLeft = p.X - _dragPoint.X;
            if (Math.Abs(subLeft) <= (ActualWidth * 0.1))
            {
                return;
            }

            var subX = TranslatePoint(default(Point), parent).X + _scrollHorizontalOffset;
            CurrentIndex = CalLocationIndex(subX);
            _isWaiting = false;
            _isDragged = true;

            var totalLeft = p.X - _mouseDownPoint.X;
            offset = subLeft + RenderTransform.Value.OffsetX;
            if (totalLeft < _maxMoveLeft)
            {
                offset = _maxMoveLeft + _mouseDownOffsetX;
            }
            else if (totalLeft > _maxMoveRight)
            {
                offset = _maxMoveRight + _mouseDownOffsetX;
            }
        }

        // 更改鼠标图标
        Mouse.OverrideCursor = Cursors.SizeAll;
        var t = new TranslateTransform(isVertical ? 0 : offset, !isVertical ? 0 : offset);
        RenderTransform = t;
        _dragPoint = p;
    }

    /// <summary>
    /// 更新 TabItem 的位置。
    /// </summary>
    /// <param name="oldIndex">旧的索引。</param>
    private void UpdateItemOffset(int oldIndex)
    {
        TabControl parent = TabControlParent;
        if (parent == null)
        {
            return;
        }

        IList list = parent.GetActualList();
        if (list.Count == 0)
        {
            return;
        }

        if (!_isMouseDownDown || CurrentIndex >= list.Count)
        {
            return;
        }

        var moveItem = list[CurrentIndex] as TabItem;
        if (moveItem == null)
        {
            return;  // 如果无法获取容器项，则退出
        }

        moveItem.CurrentIndex -= CurrentIndex - oldIndex;
        var isVertical = parent.TabStripPlacement == Dock.Left || parent.TabStripPlacement == Dock.Right;
        list.Remove(this);
        list.Insert(CurrentIndex, this);
        if (CurrentIndex > moveItem.CurrentIndex)
        {
            if (isVertical)
            {
                _maxMoveTop -= ActualHeight;
                _maxMoveButtom -= ActualHeight;
                _mouseDownPoint.Y += ActualHeight;
                SetCurrentValue(RenderTransformProperty, new TranslateTransform(0, RenderTransform.Value.OffsetY - ActualHeight));
            }
            else
            {
                _maxMoveLeft -= ActualWidth;
                _maxMoveRight -= ActualWidth;
                _mouseDownPoint.X += ActualWidth;
                SetCurrentValue(RenderTransformProperty, new TranslateTransform(RenderTransform.Value.OffsetX - ActualWidth, 0));
            }
        }
        else
        {
            if (isVertical)
            {
                _maxMoveTop += ActualHeight;
                _maxMoveButtom += ActualHeight;
                _mouseDownPoint.Y -= ActualHeight;
                SetCurrentValue(RenderTransformProperty, new TranslateTransform(0, RenderTransform.Value.OffsetY + ActualHeight));
            }
            else
            {
                _maxMoveLeft += ActualWidth;
                _maxMoveRight += ActualWidth;
                _mouseDownPoint.X -= ActualWidth;
                SetCurrentValue(RenderTransformProperty, new TranslateTransform(RenderTransform.Value.OffsetX + ActualWidth, 0));
            }
        }

        _ = Focus();

        SetCurrentValue(IsSelectedProperty, true);

        if (!IsMouseCaptured)
        {
            parent.SetCurrentValue(Selector.SelectedIndexProperty, _currentIndex);
        }

        parent.UpdateScroll();
    }

    /// <summary>
    /// 鼠标左键松开事件处理。
    /// </summary>
    /// <param name="e">鼠标事件参数。</param>
    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);
        ReleaseMouseCapture();
        Mouse.OverrideCursor = null;// 还原鼠标图标
        if (_isDragged)
        {
            SetCurrentValue(RenderTransformProperty, new TranslateTransform(0, 0));
        }

        _isDragged = false;
        _isMouseDownDown = false;
    }

    /// <summary>
    /// 当应用模板时调用，初始化当前索引。
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        IList list = TabControlParent.GetActualList();
        if (list.Count == 0)
        {
            return;
        }

        var item = TabControlParent.ItemContainerGenerator.ItemFromContainer(this);
        CurrentIndex = list.IndexOf(item);
    }

    /// <summary>
    /// 计算当前 TabItem 的位置索引。
    /// </summary>
    /// <param name="position">当前位置。</param>
    /// <returns>计算后的索引。</returns>
    private int CalLocationIndex(double position)
    {
        TabControl parent = TabControlParent;
        if (_isWaiting)
        {
            return CurrentIndex;
        }

        var isVertical = parent.TabStripPlacement == Dock.Left || parent.TabStripPlacement == Dock.Right;
        var maxIndex = TabControlParent.Items.Count - 1;
        var div = (int)(position / (isVertical ? ActualHeight : ActualWidth));

        return Math.Min(div, maxIndex);
    }

    /// <summary>
    /// 检查父级 TabControl 是否允许拖动。
    /// </summary>
    /// <returns>是否允许拖动。</returns>
    private bool IsParentTabControlAllowDrag()
    {
        return TabControlParent?.AllowDrag ?? true;
    }

    /// <summary>
    /// 检查鼠标是否在 TabItem 头部。
    /// </summary>
    /// <param name="e">鼠标事件参数。</param>
    /// <returns>是否在头部。</returns>
    private bool IsMouseOverHeader(MouseButtonEventArgs e)
    {
        return VisualTreeHelper.HitTest(this, e.GetPosition(this)) is not null;
    }
}
