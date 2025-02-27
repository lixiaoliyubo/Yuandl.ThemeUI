// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// Tree selection control that supports single
/// </summary>
[TemplatePart(Name = TreeViewTemplateName, Type = typeof(TreeView))]
[StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(TreeViewItem))]
public class TreeSelect : ItemsControl
{
    private const string TreeViewTemplateName = "PART_TreeView";

    private bool _isUpdating; // Flag to prevent circular updates
    private TreeView _treeView;
    private bool _pendingTreeViewUpdate; // Flag to indicate if there is a pending TreeView selection update

    /// <summary>
    /// Dependency property for the placeholder text of the dropdown
    /// </summary>
    public static readonly DependencyProperty PlaceholderTextProperty =
        DependencyProperty.Register(
            nameof(PlaceholderText),
            typeof(string),
            typeof(TreeSelect),
            new PropertyMetadata(string.Empty)
        );

    /// <summary>
    /// Gets or sets the placeholder text of the dropdown
    /// </summary>
    public string PlaceholderText
    {
        get => (string)GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    /// <summary>
    /// Dependency property for whether the dropdown is open
    /// </summary>
    public static readonly DependencyProperty IsDropDownOpenProperty =
        DependencyProperty.Register(
            nameof(IsDropDownOpen),
            typeof(bool),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata(false, OnIsDropDownOpenChanged));

    /// <summary>
    /// Gets or sets a value indicating whether the dropdown is open
    /// </summary>
    public bool IsDropDownOpen
    {
        get => (bool)GetValue(IsDropDownOpenProperty);
        set => SetValue(IsDropDownOpenProperty, value);
    }

    /// <summary>
    /// Dependency property for whether the control is read-only
    /// </summary>
    public static readonly DependencyProperty IsReadOnlyProperty =
        TextBox.IsReadOnlyProperty.AddOwner(typeof(TreeSelect));

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets whether the control is in read-only mode
    /// When the control is editable, this property determines whether the text box can be edited
    /// </summary>
    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    /// <summary>
    /// Dependency property for whether the control is editable
    /// </summary>
    public static readonly DependencyProperty IsEditableProperty =
        DependencyProperty.Register(
            nameof(IsEditable),
            typeof(bool),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata(false));

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets whether the control is editable
    /// </summary>
    public bool IsEditable
    {
        get => (bool)GetValue(IsEditableProperty);
        set => SetValue(IsEditableProperty, value);
    }

    /// <summary>
    /// Dependency property for the maximum height of the dropdown
    /// </summary>
    public static readonly DependencyProperty MaxDropDownHeightProperty =
        DependencyProperty.Register(
            nameof(MaxDropDownHeight),
            typeof(double),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3));

    /// <summary>
    /// Gets or sets the maximum height of the dropdown
    /// </summary>
    [Bindable(true), Category("Layout")]
    [TypeConverter(typeof(LengthConverter))]
    public double MaxDropDownHeight
    {
        get => (double)GetValue(MaxDropDownHeightProperty);
        set => SetValue(MaxDropDownHeightProperty, value);
    }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemChanged));

    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    private static readonly DependencyPropertyKey SelectionBoxItemTemplatePropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(SelectionBoxItemTemplate),
            typeof(DataTemplate),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata((DataTemplate)null));

    /// <summary>
    /// The DependencyProperty for the SelectionBoxItemProperty
    /// </summary>
    public static readonly DependencyProperty SelectionBoxItemTemplateProperty = SelectionBoxItemTemplatePropertyKey.DependencyProperty;

    /// <summary>
    /// Gets used to set the item DataTemplate
    /// </summary>
    public DataTemplate? SelectionBoxItemTemplate
    {
        get { return (DataTemplate?)GetValue(SelectionBoxItemTemplateProperty); }
        private set { SetValue(SelectionBoxItemTemplatePropertyKey, value); }
    }

    private static readonly DependencyPropertyKey SelectionBoxItemStringFormatPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(SelectionBoxItemStringFormat),
            typeof(string),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata((string)null));

    /// <summary>
    /// The DependencyProperty for the SelectionBoxItemProperty
    /// </summary>
    public static readonly DependencyProperty SelectionBoxItemStringFormatProperty = SelectionBoxItemStringFormatPropertyKey.DependencyProperty;

    /// <summary>
    /// Gets used to set the item DataStringFormat
    /// </summary>
    public string? SelectionBoxItemStringFormat
    {
        get { return (string?)GetValue(SelectionBoxItemStringFormatProperty); }
        private set { SetValue(SelectionBoxItemStringFormatPropertyKey, value); }
    }

    public static readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register(
            nameof(SelectedValue),
            typeof(object),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedValueChanged));

    public object? SelectedValue
    {
        get => GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }

    public static readonly DependencyProperty SelectedIndexProperty =
        DependencyProperty.Register(
            nameof(SelectedIndex),
            typeof(int),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedIndexChanged));

    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    public static readonly DependencyProperty SelectedValuePathProperty =
        DependencyProperty.Register(
            nameof(SelectedValuePath),
            typeof(string),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata(null));

    public string? SelectedValuePath
    {
        get => (string?)GetValue(SelectedValuePathProperty);
        set => SetValue(SelectedValuePathProperty, value);
    }

    public static readonly DependencyProperty SelectionBoxItemProperty =
        DependencyProperty.Register(
            nameof(SelectionBoxItem),
            typeof(object),
            typeof(TreeSelect),
            new FrameworkPropertyMetadata(null)
        );

    /// <summary>
    /// Gets used to display the selected item
    /// </summary>
    public object? SelectionBoxItem
    {
        get { return GetValue(SelectionBoxItemProperty); }
        private set { SetValue(SelectionBoxItemProperty, value); }
    }

    /// <summary>
    /// Event for selection change
    /// </summary>
    public event SelectionChangedEventHandler SelectionChanged
    {
        add { AddHandler(SelectionChangedEvent, value); }
        remove { RemoveHandler(SelectionChangedEvent, value); }
    }

    /// <summary>
    /// Routed event for selection change
    /// </summary>
    public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(SelectionChanged),
        RoutingStrategy.Bubble,
        typeof(SelectionChangedEventHandler),
        typeof(TreeSelect));

    /// <summary>
    /// Event for dropdown open state change
    /// </summary>
    public event EventHandler<bool> DropDownOpenChanged;

    /// <summary>
    /// Static constructor to register default style and event handling
    /// </summary>
    static TreeSelect()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(TreeSelect),
            new FrameworkPropertyMetadata(typeof(TreeSelect)));
    }

    /// <summary>
    /// Called when the control template is applied, initializes the control
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        if (_treeView != null)
        {
            _treeView.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
        }

        _treeView = GetTemplateChild(TreeViewTemplateName) as TreeView ?? throw new InvalidOperationException("TreeView template part not found");
        if (_treeView != null)
        {
            _treeView.SelectedItemChanged += OnTreeViewSelectedItemChanged;
            if (_pendingTreeViewUpdate)
            {
                UpdateTreeViewSelection();
                _pendingTreeViewUpdate = false;
            }
        }
    }

    /// <summary>
    /// Updates the selection state of the TreeView
    /// </summary>
    private void UpdateTreeViewSelection()
    {
        if (_treeView == null || SelectedItem == null)
        {
            return;
        }

        // Wait for ItemContainer generation to complete
        if (_treeView.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
        {
            _treeView.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
            return;
        }

        ExpandToItem(_treeView, SelectedItem);
    }

    /// <summary>
    /// Event handler for ItemContainerGenerator status change
    /// </summary>
    /// <param name="sender">Event source</param>
    /// <param name="e">Event arguments</param>
    private void ItemContainerGenerator_StatusChanged(object? sender, EventArgs e)
    {
        if (_treeView?.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
        {
            _treeView.ItemContainerGenerator.StatusChanged -= ItemContainerGenerator_StatusChanged;
            // Flatten all items to find the path to the target item
            ExpandToItem(_treeView, SelectedItem);
        }
    }

    /// <summary>
    /// Handles the selected item change event
    /// </summary>
    /// <param name="d">Dependency object</param>
    /// <param name="e">Event arguments</param>
    private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (TreeSelect)d;
        if (!control._isUpdating)
        {
            control._isUpdating = true;
            try
            {
                if (control._treeView == null)
                {
                    control._pendingTreeViewUpdate = true;
                }
                else
                {
                    control.UpdateTreeViewSelection();
                }

                control.UpdateSelectionBoxItem();
                control.UpdateSelectedValue();
                control.UpdateSelectedIndex();

                // Trigger the selection change event
                var args = new SelectionChangedEventArgs(
                    SelectionChangedEvent,
                    e.OldValue != null ? new[] { e.OldValue } : Array.Empty<object>(),
                    e.NewValue != null ? new[] { e.NewValue } : Array.Empty<object>());
                control.RaiseEvent(args);
            }
            finally
            {
                control._isUpdating = false;
            }
        }
    }

    /// <summary>
    /// Handles the selected value change event
    /// </summary>
    /// <param name="d">Dependency object</param>
    /// <param name="e">Event arguments</param>
    private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (TreeSelect)d;
        if (control._isUpdating) return;

        control._isUpdating = true;
        try
        {
            control.UpdateSelectedItemFromValue();

            if (control._treeView == null)
            {
                control._pendingTreeViewUpdate = true;
            }
            else
            {
                control.UpdateTreeViewSelection();
            }

            control.UpdateSelectedIndex();
            control.UpdateSelectionBoxItem();
        }
        finally
        {
            control._isUpdating = false;
        }
    }

    /// <summary>
    /// Handles the selected index change event
    /// </summary>
    /// <param name="d">Dependency object</param>
    /// <param name="e">Event arguments</param>
    private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (TreeSelect)d;
        if (control._isUpdating) return;

        control._isUpdating = true;
        try
        {
            control.UpdateSelectedItemFromIndex();

            if (control._treeView == null)
            {
                control._pendingTreeViewUpdate = true;
            }
            else
            {
                control.UpdateTreeViewSelection();
            }

            control.UpdateSelectedValue();
            control.UpdateSelectionBoxItem();
        }
        finally
        {
            control._isUpdating = false;
        }
    }

    /// <summary>
    /// Handles the mouse down event to close the dropdown
    /// </summary>
    private void UpdateSelectionBoxItem()
    {
        if (SelectedItem == null)
        {
            SelectionBoxItem = null;
            SelectionBoxItemTemplate = null;
            return;
        }

        // If DisplayMemberPath is set, use the property value
        if (!string.IsNullOrEmpty(DisplayMemberPath))
        {
            System.Reflection.PropertyInfo prop = SelectedItem.GetType().GetProperty(DisplayMemberPath);
            SelectionBoxItem = prop?.GetValue(SelectedItem) ?? SelectedItem;
            SelectionBoxItemTemplate = null;
            return;
        }

        // If ItemTemplate is set, use the template
        if (ItemTemplate != null)
        {
            SelectionBoxItem = SelectedItem;
            SelectionBoxItemTemplate = ItemTemplate;
            return;
        }

        // By default, display the item directly
        SelectionBoxItem = SelectedItem;
        SelectionBoxItemTemplate = null;
    }

    /// <summary>
    /// Updates the SelectedValue based on the current selected item
    /// </summary>
    private void UpdateSelectedValue()
    {
        SelectedValue = GetValueFromItem(SelectedItem);
    }

    /// <summary>
    /// Updates the SelectedIndex based on the current selected item
    /// </summary>
    private void UpdateSelectedIndex()
    {
        if (SelectedItem == null)
        {
            SelectedIndex = -1;
            return;
        }

        List<object> flattenedItems = FlattenItems(Items);
        SelectedIndex = flattenedItems.IndexOf(SelectedItem);
    }

    /// <summary>
    /// Updates the selected item based on the SelectedValue
    /// </summary>
    private void UpdateSelectedItemFromValue()
    {
        SelectedItem = FindItemByValue(Items, SelectedValue);
    }

    /// <summary>
    /// Updates the selected item based on the SelectedIndex
    /// </summary>
    private void UpdateSelectedItemFromIndex()
    {
        // Get the total number of items
        var totalCount = GetTotalItemCount(Items);

        if (SelectedIndex < 0 || SelectedIndex >= totalCount)
        {
            SelectedItem = null;
            return;
        }

        // Recursively find the item at the specified index
        SelectedItem = FindItemByIndex(Items, SelectedIndex);
    }

    /// <summary>
    /// Handles the dropdown open state change event
    /// </summary>
    /// <param name="d">Dependency object</param>
    /// <param name="e">Event arguments</param>
    private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var treeSelect = (TreeSelect)d;
        var newValue = (bool)e.NewValue;

        if (newValue)
        {
            // When the dropdown is opened, capture the mouse
            _ = Mouse.Capture(treeSelect, CaptureMode.SubTree);
        }
        else if (treeSelect.IsMouseCaptured)
        {
            _ = Mouse.Capture(null);
        }

        // Trigger the state change event
        treeSelect.OnDropDownOpenChanged(newValue);
    }

    /// <summary>
    /// Handles the TreeView selected item change event
    /// </summary>
    /// <param name="sender">Event source</param>
    /// <param name="e">Event arguments</param>
    private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (_isUpdating) return;

        _isUpdating = true;
        try
        {
            SelectedItem = e.NewValue;
            UpdateSelectionBoxItem();
            UpdateSelectedValue();
            UpdateSelectedIndex();

            // Trigger the SelectionChanged event
            var args = new SelectionChangedEventArgs(
                SelectionChangedEvent,
                e.OldValue != null ? new[] { e.OldValue } : Array.Empty<object>(),
                e.NewValue != null ? new[] { e.NewValue } : Array.Empty<object>());
            RaiseEvent(args);

            // When the dropdown is closed, release the mouse capture
            if (_treeView.IsLoaded && IsDropDownOpen)
            {
                SetCurrentValue(IsDropDownOpenProperty, false);
            }
        }
        finally
        {
            _isUpdating = false;
        }
    }

    /// <summary>
    /// Called when the ItemsSource changes
    /// </summary>
    /// <param name="oldValue">Old value</param>
    /// <param name="newValue">New value</param>
    protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
    {
        base.OnItemsSourceChanged(oldValue, newValue);

        if (!_isUpdating)
        {
            _isUpdating = true;
            try
            {
                // If the new ItemsSource is null, clear all selections
                if (newValue == null)
                {
                    SelectedItem = null;
                    SelectedValue = null;
                    SelectedIndex = -1;
                    SelectionBoxItem = null;
                    return;
                }

                // If SelectedValue is set, try to find the corresponding item in the new ItemsSource
                if (SelectedValue != null)
                {
                    UpdateSelectedItemFromValue();
                }

                // If SelectedIndex is set, try to find the corresponding item in the new ItemsSource
                else if (SelectedIndex >= 0)
                {
                    UpdateSelectedItemFromIndex();
                }

                // Update other related properties
                UpdateSelectionBoxItem();
                UpdateSelectedValue();
                UpdateSelectedIndex();
            }
            finally
            {
                _isUpdating = false;
            }
        }
    }

    /// <summary>
    /// Called when the DisplayMemberPath changes
    /// </summary>
    /// <param name="oldDisplayMemberPath">Old display member path</param>
    /// <param name="newDisplayMemberPath">New display member path</param>
    protected override void OnDisplayMemberPathChanged(string oldDisplayMemberPath, string newDisplayMemberPath)
    {
        base.OnDisplayMemberPathChanged(oldDisplayMemberPath, newDisplayMemberPath);
        UpdateSelectionBoxItem();
    }

    /// <summary>
    /// Triggered when the ItemTemplate changes.
    /// </summary>
    /// <param name="oldItemTemplate">The old item template.</param>
    /// <param name="newItemTemplate">The new item template.</param>
    protected override void OnItemTemplateChanged(DataTemplate oldItemTemplate, DataTemplate newItemTemplate)
    {
        base.OnItemTemplateChanged(oldItemTemplate, newItemTemplate);

        if (_treeView != null)
        {
            _treeView.ItemTemplate = newItemTemplate;
        }

        UpdateSelectionBoxItem();
    }

    private static int positionCounter = -1;

    /// <summary>
    /// Expand to the specified item and select it.
    /// </summary>
    private void ExpandToItem(ItemsControl itemsControl, object? item)
    {
        if (itemsControl == null || item == null) return;

        // Wait for the ItemContainer generation to complete.
        if (itemsControl.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
        {
            itemsControl.ItemContainerGenerator.StatusChanged += (s, e) =>
            {
                if (itemsControl.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                {
                    ExpandToItemInternal(itemsControl, item);
                }
            };
            return;
        }

        ExpandToItemInternal(itemsControl, item);
    }

    /// <summary>
    /// The internal implementation of expanding to the specified item.
    /// </summary>
    /// <param name="itemsControl">The current ItemsControl.</param>
    /// <param name="item">The target item.</param>
    private void ExpandToItemInternal(ItemsControl itemsControl, object item)
    {
        var itemsSourcePath = GetItemsSourcePropertyPath();
        if (string.IsNullOrEmpty(itemsSourcePath))
        {
            return;
        }

        foreach (var containerItem in itemsControl.Items)
        {
            var container = itemsControl.ItemContainerGenerator.ContainerFromItem(containerItem) as TreeViewItem;
            if (container == null)
            {
                continue;
            }

            if (containerItem == item)
            {
                // Find the target item and select it.
                container.Focus();
                container.IsSelected = true;
                return;
            }

            // Check the child items.
            var children = GetItemChildren(containerItem, itemsSourcePath) as IEnumerable;
            if (children != null)
            {
                container.IsExpanded = true;

                // Wait for the child item containers to be generated.
                if (container.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                {
                    container.UpdateLayout();
                    container.ItemContainerGenerator.StatusChanged += (s, e) =>
                    {
                        if (container.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                        {
                            ExpandToItem(container, item);
                        }
                    };
                }
                else
                {
                    ExpandToItem(container, item);
                }
            }
        }
    }

    /// <summary>
    /// Get the child item collection of the specified item.
    /// </summary>
    /// <param name="item">The item to get the child items from.</param>
    /// <param name="itemsSourcePath">The property path of the child item collection.</param>
    /// <returns>The child item collection, or null if there are none.</returns>
    private static object? GetItemChildren(object item, string itemsSourcePath)
    {
        if (string.IsNullOrEmpty(itemsSourcePath))
        {
            return null;
        }

        System.Reflection.PropertyInfo propertyInfo = item.GetType().GetProperty(itemsSourcePath);
        return propertyInfo?.GetValue(item);
    }

    /// <summary>
    /// The internal implementation of recursively finding the index of the specified item in the tree structure.
    /// </summary>
    /// <param name="item">The collection at the current level.</param>
    /// <param name="targetItem">The target item.</param>
    /// <returns>The index of the target item, or -1 if not found.</returns>
    private int? FindItemIndex(object item, object targetItem)
    {
        positionCounter++;
        if (item == targetItem)
        {
            return positionCounter;
        }

        // Get the name of the ItemsSource property defined in the ItemTemplate.
        var itemsSourceProperty = GetItemsSourcePropertyPath();
        if (!string.IsNullOrEmpty(itemsSourceProperty))
        {
            System.Reflection.PropertyInfo prop = item.GetType().GetProperty(itemsSourceProperty);
            var children = prop?.GetValue(item) as IEnumerable;
            if (children != null)
            {
                foreach (var child in children)
                {
                    // Search in the child items, passing the next index.
                    var childIndex = FindItemIndex(child, targetItem);
                    if (childIndex.HasValue)
                    {
                        return childIndex;
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Get the name of the ItemsSource property of the item.
    /// </summary>
    private string GetItemsSourcePropertyPath()
    {
        if (_treeView?.ItemTemplate is HierarchicalDataTemplate hierarchicalTemplate)
        {
            var binding = hierarchicalTemplate.ItemsSource as Binding;
            return binding?.Path.Path ?? string.Empty;
        }

        return "Children";
    }

    /// <summary>
    /// Flatten the tree structure into a one-dimensional list.
    /// </summary>
    private List<object> FlattenItems(IEnumerable items)
    {
        var result = new List<object>();
        if (items == null)
        {
            return result;
        }

        foreach (var item in items)
        {
            result.Add(item);

            // Get and add the child items.
            var itemsSourceProperty = GetItemsSourcePropertyPath();
            if (!string.IsNullOrEmpty(itemsSourceProperty))
            {
                var prop = item.GetType().GetProperty(itemsSourceProperty);
                var children = prop?.GetValue(item) as IEnumerable;
                if (children != null)
                {
                    result.AddRange(FlattenItems(children));
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Modify the FindItemByIndex method to use the flattened list.
    /// </summary>
    /// <param name="items"></param>
    /// <param name="targetIndex"></param>
    /// <returns></returns>
    private object? FindItemByIndex(IEnumerable? items, int targetIndex)
    {
        if (items == null) return null;

        var flattenedItems = FlattenItems(items);
        return targetIndex >= 0 && targetIndex < flattenedItems.Count ? flattenedItems[targetIndex] : null;
    }

    /// <summary>
    /// Find an item based on the SelectedValue.
    /// </summary>
    /// <param name="items"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private object? FindItemByValue(IEnumerable? items, object? value)
    {
        if (items == null)
        {
            return null;
        }

        foreach (var item in items)
        {
            if (Equals(GetValueFromItem(item), value)) return item;

            // Get the name of the ItemsSource property defined in the ItemTemplate.
            var itemsSourceProperty = GetItemsSourcePropertyPath();
            if (!string.IsNullOrEmpty(itemsSourceProperty))
            {
                var prop = item.GetType().GetProperty(itemsSourceProperty);
                var children = prop?.GetValue(item) as IEnumerable;
                var found = FindItemByValue(children, value);
                if (found != null) return found;
            }
        }

        return null;
    }

    /// <summary>
    /// Get the value of the item according to the SelectedValuePath.
    /// </summary>
    /// <param name="item">The item to get the value from.</param>
    /// <returns>The value of the item, or the item itself if SelectedValuePath is not set.</returns>
    private object? GetValueFromItem(object? item)
    {
        if (item == null || string.IsNullOrEmpty(SelectedValuePath))
            return item;

        var prop = item.GetType().GetProperty(SelectedValuePath);
        return prop?.GetValue(item);
    }

    /// <summary>
    /// Get the total number of items in the tree structure.
    /// </summary>
    /// <param name="items">The item collection to calculate.</param>
    /// <returns>The total number of items.</returns>
    private int GetTotalItemCount(IEnumerable items)
    {
        return FlattenItems(items).Count;
    }

    /// <summary>
    /// Called when the selected item changes.
    /// </summary>
    /// <param name="e">The event arguments for the selected item change.</param>
    protected virtual void OnSelectionChanged(SelectionChangedEventArgs e)
    {
        RaiseEvent(e);
    }

    /// <summary>
    /// Called when the open state of the dropdown changes.
    /// </summary>
    /// <param name="isOpen">Whether it is open.</param>
    protected virtual void OnDropDownOpenChanged(bool isOpen)
    {
        DropDownOpenChanged?.Invoke(this, isOpen);
    }
}