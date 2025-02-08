// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// TreeSelect.xaml 的交互逻辑
/// </summary>
[TemplatePart(Name = "PART_TreeView", Type = typeof(TreeView))]
[TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
[StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(TreeViewItem))]
public class TreeSelect : Selector
{
    public bool IsMulti
    {
        get { return (bool)GetValue(IsMultiProperty); }
        set { SetValue(IsMultiProperty, value); }
    }

    public static readonly DependencyProperty IsMultiProperty =
        DependencyProperty.Register(nameof(IsMulti), typeof(bool), typeof(TreeSelect), new PropertyMetadata(false));

    /// <summary>Identifies the <see cref="PlaceholderText"/> dependency property.</summary>
    public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(
        nameof(PlaceholderText),
        typeof(string),
        typeof(TreeSelect),
        new PropertyMetadata(string.Empty)
    );

    /// <summary>
    /// Gets or sets numbers pattern.
    /// </summary>
    public string PlaceholderText
    {
        get => (string)GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    // SelectedItem property
    public static new readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(TreeSelect), new PropertyMetadata(null, OnSelectedItemChanged));

    public new object? SelectedItem
    {
        get { return GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    // SelectedIndex property
    public static new readonly DependencyProperty SelectedIndexProperty =
        DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(TreeSelect), new PropertyMetadata(-1, OnSelectedIndexChanged));

    public new int SelectedIndex
    {
        get { return (int)GetValue(SelectedIndexProperty); }
        set { SetValue(SelectedIndexProperty, value); }
    }

    // SelectedValue property
    public static new readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register(nameof(SelectedValue), typeof(object), typeof(TreeSelect), new PropertyMetadata(null, OnSelectedValueChanged));

    public new object? SelectedValue
    {
        get { return GetValue(SelectedValueProperty); }
        set { SetValue(SelectedValueProperty, value); }
    }

    // SelectedValuePath property
    public static new readonly DependencyProperty SelectedValuePathProperty =
        DependencyProperty.Register(nameof(SelectedValuePath), typeof(string), typeof(TreeSelect), new PropertyMetadata(string.Empty));

    public new string SelectedValuePath
    {
        get { return (string)GetValue(SelectedValuePathProperty); }
        set { SetValue(SelectedValuePathProperty, value); }
    }

    // 定义IsDropDownOpen依赖属性
    public static readonly DependencyProperty IsDropDownOpenProperty =
        DependencyProperty.Register(
            nameof(IsDropDownOpen),
            typeof(bool),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata(false));

    public bool IsDropDownOpen
    {
        get => (bool)GetValue(IsDropDownOpenProperty);
        set => SetValue(IsDropDownOpenProperty, value);
    }

    public static readonly DependencyProperty SelectionBoxItemProperty =
        DependencyProperty.Register(nameof(SelectionBoxItem), typeof(object), typeof(TreeSelect), new FrameworkPropertyMetadata(string.Empty));

    /// <summary>
    /// Gets used to display the selected item
    /// </summary>
    public object SelectionBoxItem
    {
        get => GetValue(SelectionBoxItemProperty);
        set => SetValue(SelectionBoxItemProperty, value);
    }

    // 定义SelectedItemTemplate依赖属性
    public static readonly DependencyProperty SelectionItemTemplateProperty =
        DependencyProperty.Register(
            nameof(SelectionItemTemplate),
            typeof(DataTemplate),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata(null));

    public DataTemplate? SelectionItemTemplate
    {
        get => (DataTemplate?)GetValue(SelectionItemTemplateProperty);
        set => SetValue(SelectionItemTemplateProperty, value);
    }

    /// <summary>
    ///     DependencyProperty for the IsReadOnlyProperty
    /// </summary>
    public static readonly DependencyProperty IsReadOnlyProperty =
            TextBox.IsReadOnlyProperty.AddOwner(typeof(TreeSelect));

    /// <summary>
    ///     When the TreeSelect is Editable, if the TextBox within it is read only.
    /// </summary>
    public bool IsReadOnly
    {
        get { return (bool)GetValue(IsReadOnlyProperty); }
        set { SetValue(IsReadOnlyProperty, value); }
    }

    /// <summary>
    /// DependencyProperty for IsEditable
    /// </summary>
    public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register(
                    "IsEditable",
                    typeof(bool),
                    typeof(TreeSelect),
                    new FrameworkPropertyMetadata(
                            false,
                            new PropertyChangedCallback(OnIsEditableChanged)));

    /// <summary>
    ///     True if this TreeSelect is editable.
    /// </summary>
    /// <value></value>
    public bool IsEditable
    {
        get { return (bool)GetValue(IsEditableProperty); }
        set { SetValue(IsEditableProperty, value); }
    }

    /// <summary>
    ///     DependencyProperty for MaxDropDownHeight
    /// </summary>
    // Need to figure out the right default value, and this should actually be a better value
    public static readonly DependencyProperty MaxDropDownHeightProperty
        = DependencyProperty.Register(nameof(MaxDropDownHeight), typeof(double), typeof(TreeSelect), new FrameworkPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3, OnVisualStatePropertyChanged));

    /// <summary>
    ///     The maximum height of the popup
    /// </summary>
    [Bindable(true), Category("Layout")]
    [TypeConverter(typeof(LengthConverter))]
    public double MaxDropDownHeight
    {
        get => (double)GetValue(MaxDropDownHeightProperty);
        set => SetValue(MaxDropDownHeightProperty, value);
    }

    static TreeSelect()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeSelect), new FrameworkPropertyMetadata(typeof(TreeSelect)));
    }

    private TreeView _treeView;

    protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
    {
        base.OnItemsSourceChanged(oldValue, newValue);

        if (_isUpdating)
        {
            return;
        }

        try
        {
            _isUpdating = true;

            // 如果有 SelectedValue，优先根据 SelectedValue 更新
            if (SelectedValue != null && !string.IsNullOrEmpty(SelectedValuePath))
            {
                var itemsSourcePath = GetItemsSourcePropertyPath(this);
                var item = FindItemByValue(newValue, SelectedValue, SelectedValuePath, itemsSourcePath);
                SelectedItem = item;
                UpdateSelectedIndex();
            }

            // 如果有 SelectedIndex，根据 SelectedIndex 更新
            else if (SelectedIndex >= 0)
            {
                UpdateSelectedItem();
                UpdateSelectedValue();
            }

            // 如果有 SelectedItem，验证 SelectedItem 是否在新的 ItemsSource 中
            else if (SelectedItem != null)
            {
                var itemsSourcePath = GetItemsSourcePropertyPath(this);
                var index = FindIndexByItem(newValue, SelectedItem, itemsSourcePath);
                if (index == -1)
                {
                    // SelectedItem 不在新的 ItemsSource 中，清除选择
                    SelectedItem = null;
                    SelectedValue = null;
                    SelectedIndex = -1;
                }
                else
                {
                    // SelectedItem 在新的 ItemsSource 中，更新其他值
                    UpdateSelectedIndex();
                    UpdateSelectedValue();
                }
            }
        }
        finally
        {
            _isUpdating = false;
        }
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        if (_treeView != null)
        {
            _treeView.SelectedItemChanged -= TreeView_SelectedItemChanged;
        }

        _treeView = GetTemplateChild("PART_TreeView") as TreeView;

        if (_treeView != null)
        {
            _treeView.SelectedItemChanged += TreeView_SelectedItemChanged;

            // 如果有初始选中项，展开到该项
            if (SelectedItem != null)
            {
                ExpandToItem(_treeView, SelectedItem);
            }
        }
    }

    private void ExpandToItem(ItemsControl itemsControl, object item)
    {
        if (itemsControl == null || item == null) return;

        var itemsSourcePath = GetItemsSourcePropertyPath(this);
        if (string.IsNullOrEmpty(itemsSourcePath)) return;

        foreach (var containerItem in itemsControl.Items)
        {
            var container = itemsControl.ItemContainerGenerator.ContainerFromItem(containerItem) as TreeViewItem;
            if (container == null) continue;

            if (containerItem == item)
            {
                // 找到目标项，选中它
                container.IsSelected = true;
                ExpandParents(container);
                return;
            }

            // 检查子项
            var children = GetItemChildren(containerItem, itemsSourcePath) as IEnumerable;
            if (children != null)
            {
                container.IsExpanded = true;
                ExpandToItem(container, item);
            }
        }
    }

    private void ExpandParents(TreeViewItem item)
    {
        TreeViewItem parent = GetParentTreeViewItem(item);
        while (parent != null)
        {
            parent.IsExpanded = true;
            parent = GetParentTreeViewItem(parent);
        }
    }

    private TreeViewItem GetParentTreeViewItem(DependencyObject item)
    {
        DependencyObject? parent = VisualTreeHelper.GetParent(item);
        while (parent != null && !(parent is TreeViewItem))
        {
            parent = VisualTreeHelper.GetParent(parent);
        }

        return parent as TreeViewItem;
    }

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (!_isUpdating)
        {
            try
            {
                _isUpdating = true;
                SelectedItem = e.NewValue;
                UpdateSelectedIndex();
                UpdateSelectedValue();
            }
            finally
            {
                _isUpdating = false;
            }
        }

        IsDropDownOpen = false; // 选择后关闭下拉框
    }

    private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (TreeSelect)d;
        if (control._isUpdating)
        {
            return;
        }

        try
        {
            control._isUpdating = true;

            // Update SelectedIndex
            control.UpdateSelectedIndex();

            // Update SelectedValue
            control.UpdateSelectedValue();
        }
        finally
        {
            control._isUpdating = false;
        }
    }

    private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (TreeSelect)d;
        if (control._isUpdating)
        {
            return;
        }

        try
        {
            control._isUpdating = true;
            if (!string.IsNullOrEmpty(control.SelectedValuePath))
            {
                // Update SelectedItem
                control.UpdateSelectedItem();

                // Update SelectedIndex
                control.UpdateSelectedIndex();
            }
        }
        finally
        {
            control._isUpdating = false;
        }
    }

    private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (TreeSelect)d;
        if (control._isUpdating)
        {
            return;
        }

        try
        {
            control._isUpdating = true;
            if (control.ItemsSource != null)
            {
                var selectedIndex = (int)e.NewValue;

                if (selectedIndex >= 0)
                {
                    // Update SelectedItem
                    control.UpdateSelectedItem();

                    // Update SelectedValue
                    control.UpdateSelectedValue();
                }
                else
                {
                    control.SelectedItem = null;
                    control.SelectedValue = null;
                }
            }
        }
        finally
        {
            control._isUpdating = false;
        }
    }

    private void UpdateSelectedIndex()
    {
        if (ItemsSource != null)
        {
            var itemsSourcePath = GetItemsSourcePropertyPath(this);
            var foundIndex = FindIndexByItem(ItemsSource, SelectedItem, itemsSourcePath);
            SelectedIndex = foundIndex;
        }
    }

    private void UpdateSelectedValue()
    {
        if (SelectedItem != null)
        {
            if (!string.IsNullOrEmpty(SelectedValuePath))
            {
                var propertyInfo = SelectedItem.GetType().GetProperty(SelectedValuePath);
                if (propertyInfo != null)
                {
                    SelectedValue = propertyInfo.GetValue(SelectedItem, null);
                }
            }
            else
            {
                SelectedValue = SelectedItem;
            }
        }
    }

    private void UpdateSelectedItem()
    {
        int currentIndex = 0;
        var itemsSourcePath = GetItemsSourcePropertyPath(this);
        var item = FindItemByIndex(ItemsSource, SelectedIndex, ref currentIndex, itemsSourcePath);

        // Update SelectedItem
        SelectedItem = item;
    }

    private static string GetItemsSourcePropertyPath(TreeSelect control)
    {
        if (control.ItemTemplate is HierarchicalDataTemplate hierarchicalTemplate)
        {
            var binding = hierarchicalTemplate.ItemsSource as Binding;
            return binding?.Path.Path ?? string.Empty;
        }

        return "Children";
    }

    private static object? GetItemChildren(object item, string itemsSourcePath)
    {
        if (string.IsNullOrEmpty(itemsSourcePath)) return null;

        var propertyInfo = item.GetType().GetProperty(itemsSourcePath);
        return propertyInfo?.GetValue(item);
    }

    private static object? FindItemByValue(IEnumerable items, object value, string valuePath, string itemsSourcePath)
    {
        foreach (var item in items)
        {
            if (item != null)
            {
                var propertyInfo = item.GetType().GetProperty(valuePath);
                if (propertyInfo != null)
                {
                    var itemValue = propertyInfo.GetValue(item, null);
                    if (Equals(itemValue, value))
                    {
                        return item;
                    }
                }

                var subItems = GetItemChildren(item, itemsSourcePath) as IEnumerable;
                if (subItems != null)
                {
                    var result = FindItemByValue(subItems, value, valuePath, itemsSourcePath);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
        }

        return null;
    }

    internal static void OnVisualStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Due to inherited properties, its safer not to cast to control because this might get fired for
        // non-controls.
        var control = d as Control;
        if (control != null)
        {
        }
    }

    private static int FindIndexByItem(IEnumerable items, object targetItem, string itemsSourcePath)
    {
        int index = 0;
        foreach (var item in items)
        {
            if (Equals(item, targetItem))
            {
                return index;
            }

            index++;

            var subItems = GetItemChildren(item, itemsSourcePath) as IEnumerable;
            if (subItems != null)
            {
                var subIndex = FindIndexByItem(subItems, targetItem, itemsSourcePath);
                if (subIndex != -1)
                {
                    return index + subIndex;
                }

                index += GetItemCount(subItems, itemsSourcePath) - 1;
            }
        }

        return -1;
    }

    private static int GetItemCount(IEnumerable items, string itemsSourcePath)
    {
        int count = 0;
        foreach (var item in items)
        {
            count++;
            var subItems = GetItemChildren(item, itemsSourcePath) as IEnumerable;
            if (subItems != null)
            {
                count += GetItemCount(subItems, itemsSourcePath);
            }
        }

        return count;
    }

    private static object? FindItemByIndex(IEnumerable items, int targetIndex, ref int currentIndex, string itemsSourcePath)
    {
        foreach (var item in items)
        {
            if (currentIndex == targetIndex)
            {
                currentIndex = 0;
                return item;
            }

            currentIndex++;

            var subItems = GetItemChildren(item, itemsSourcePath) as IEnumerable;
            if (subItems != null)
            {
                var result = FindItemByIndex(subItems, targetIndex, ref currentIndex, itemsSourcePath);
                if (result != null)
                {
                    return result;
                }
            }
        }

        return null;
    }

    private static void OnIsEditableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TreeSelect cb = d as TreeSelect;
    }

    private bool _isUpdating = false;
}
