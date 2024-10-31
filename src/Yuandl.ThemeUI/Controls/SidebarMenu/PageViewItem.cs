// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Yuandl.ThemeUI.Appearance;
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// Represents the container for an item in a NavigationView control.
/// When needed, it can be used as a normal button with a <see cref="System.Windows.Controls.Primitives.ButtonBase.Click"/> action.
/// </summary>
[TemplatePart(Name = TemplateElementChevronGrid, Type = typeof(Grid))]
public class PageViewItem : System.Windows.Controls.Primitives.ButtonBase,
        IPageViewItem,
        IIconControl
{
    protected const string TemplateElementChevronGrid = "PART_ChevronGrid";

    #region Property

    private static readonly DependencyPropertyKey MenuItemsPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(MenuItems),
        typeof(ObservableCollection<object>),
        typeof(PageViewItem),
        new PropertyMetadata(null)
    );

    /// <summary>Identifies the <see cref="MenuItems"/> dependency property.</summary>
    public static readonly DependencyProperty MenuItemsProperty = MenuItemsPropertyKey.DependencyProperty;

    /// <summary>Identifies the <see cref="ItemsSource"/> dependency property.</summary>
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
        nameof(ItemsSource),
        typeof(object),
        typeof(PageViewItem),
        new PropertyMetadata(null, OnItemsSourceChanged)
    );

    /// <summary>Identifies the <see cref="HasItems"/> dependency property.</summary>
    internal static readonly DependencyPropertyKey HasItemsPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(HasItems),
            typeof(bool),
            typeof(PageViewItem),
            new PropertyMetadata(false)
        );

    /// <summary>Identifies the <see cref="HasItems"/> dependency property.</summary>
    public static readonly DependencyProperty HasItemsProperty =
        HasItemsPropertyKey.DependencyProperty;

    /// <summary>Identifies the <see cref="IsActive"/> dependency property.</summary>
    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
        nameof(IsActive),
        typeof(bool),
        typeof(PageViewItem),
        new PropertyMetadata(false)
    );

    /// <summary>Identifies the <see cref="IsOpen"/> dependency property.</summary>
    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
        nameof(IsOpen),
        typeof(bool),
        typeof(PageViewItem),
        new PropertyMetadata(false)
    );

    /// <summary>Identifies the <see cref="IsExpanded"/> dependency property.</summary>
    public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(
        nameof(IsExpanded),
        typeof(bool),
        typeof(PageViewItem),
        new PropertyMetadata(false)
    );

    /// <summary>Identifies the <see cref="Icon"/> dependency property.</summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(IconElement),
        typeof(PageViewItem),
        new PropertyMetadata(null, null, IconElement.Coerce)
    );

    /// <summary>Identifies the <see cref="InfoBadge"/> dependency property.</summary>
    public static readonly DependencyProperty InfoBadgeProperty = DependencyProperty.Register(
        nameof(InfoBadge),
        typeof(InfoBadge),
        typeof(PageViewItem),
        new PropertyMetadata(null)
    );

    /// <summary>Identifies the <see cref="PageTag"/> dependency property.</summary>
    public static readonly DependencyProperty PageTagProperty = DependencyProperty.Register(
        nameof(PageTag),
        typeof(string),
        typeof(PageViewItem),
        new PropertyMetadata(string.Empty)
    );

    /// <summary>Identifies the <see cref="PageType"/> dependency property.</summary>
    public static readonly DependencyProperty PageTypeProperty = DependencyProperty.Register(
        nameof(PageType),
        typeof(Type),
        typeof(PageViewItem),
        new PropertyMetadata(null)
    );

    /// <summary>Identifies the <see cref="NavigationCacheMode"/> dependency property.</summary>
    public static new readonly DependencyProperty CacheModeProperty = DependencyProperty.Register(
        nameof(CacheMode),
        typeof(NavigationCacheMode),
        typeof(PageViewItem),
        new FrameworkPropertyMetadata(NavigationCacheMode.Disabled)
    );

    /// <summary>Identifies the <see cref="DataContent"/> dependency property.</summary>
    public static readonly DependencyProperty DataContentProperty = DependencyProperty.Register(
        nameof(DataContent),
        typeof(object),
        typeof(PageViewItem),
        new FrameworkPropertyMetadata(null)
    );

    /// <inheritdoc/>
    public IList MenuItems => (ObservableCollection<object>)GetValue(MenuItemsProperty);

    /// <inheritdoc/>
    public object? DataContent
    {
        get => GetValue(DataContentProperty);
        set => SetValue(DataContentProperty, value);
    }

    /// <inheritdoc/>
    [Bindable(true)]
    public object? ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set
        {
            if (value is null)
            {
                ClearValue(ItemsSourceProperty);
            }
            else
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether MenuItems.Count > 0
    /// </summary>
    [Browsable(false)]
    [ReadOnly(true)]
    public bool HasItems
    {
        get => (bool)GetValue(HasItemsProperty);
    }

    /// <inheritdoc />
    [Browsable(false)]
    [ReadOnly(true)]
    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    /// <inheritdoc />
    [Browsable(false)]
    [ReadOnly(true)]
    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    [Browsable(false)]
    [ReadOnly(true)]
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <inheritdoc />
    [Bindable(true)]
    [Category("Appearance")]
    public IconElement? Icon
    {
        get => (IconElement?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public InfoBadge? InfoBadge
    {
        get => (InfoBadge?)GetValue(InfoBadgeProperty);
        set => SetValue(InfoBadgeProperty, value);
    }

    /// <inheritdoc />
    public string PageTag
    {
        get => (string)GetValue(PageTagProperty);
        set => SetValue(PageTagProperty, value);
    }

    /// <inheritdoc />
    public Type? PageType
    {
        get => (Type?)GetValue(PageTypeProperty);
        set => SetValue(PageTypeProperty, value);
    }

    /// <inheritdoc/>
    public new NavigationCacheMode CacheMode
    {
        get => (NavigationCacheMode)GetValue(CacheModeProperty);
        set => SetValue(CacheModeProperty, value);
    }

    #endregion

    /// <inheritdoc />
    public IPageViewItem? NavigationViewItemParent { get; set; }

    /// <inheritdoc />
    public bool IsElement { get; set; }

    /// <inheritdoc />
    public string Id { get; }

    protected Grid? ChevronGrid { get; set; }


    static PageViewItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(PageViewItem),
            new FrameworkPropertyMetadata(typeof(PageViewItem))
        );
    }

    public PageViewItem(Type targetPageType)
        : this()
    {
        SetValue(PageTypeProperty, targetPageType);
    }

    public PageViewItem(string name, Type targetPageType)
        : this(targetPageType)
    {
        SetValue(ContentProperty, name);
    }

    public PageViewItem(string name, SymbolRegular icon, Type targetPageType)
        : this(targetPageType)
    {
        SetValue(ContentProperty, name);
        SetValue(IconProperty, new SymbolIcon { Symbol = icon });
    }

    public PageViewItem(string name, SymbolRegular icon, Type targetPageType, IList menuItems)
        : this(name, icon, targetPageType)
    {
        SetValue(ItemsSourceProperty, menuItems);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PageViewItem"/> class.
    /// </summary>
    public PageViewItem()
    {
        Id = Guid.NewGuid().ToString("n");

        Unloaded += static (sender, _) =>
        {
            ((PageViewItem)sender).NavigationViewItemParent = null;
        };

        Loaded += (_, _) => InitializeNavigationViewEvents();

        // Initialize the `Items` collection
        var menuItems = new ObservableCollection<object>();
        menuItems.CollectionChanged += OnMenuItems_CollectionChanged;
        SetValue(MenuItemsPropertyKey, menuItems);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PageViewItem"/> class.
    /// </summary>
    /// <param name="name">name</param>
    public PageViewItem(string name)
    {
        Id = Guid.NewGuid().ToString("n");
        SetValue(ContentProperty, name);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PageViewItem"/> class.
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="icon">icon</param>
    public PageViewItem(string name, SymbolRegular icon)
    {
        Id = Guid.NewGuid().ToString("n");
        SetValue(ContentProperty, name);
        SetValue(IconProperty, new SymbolIcon { Symbol = icon });
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PageViewItem"/> class.
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="icon">icon</param>
    /// <param name="menuItems">menuItems</param>
    public PageViewItem(string name, SymbolRegular icon, IList menuItems)
        : this(name, icon)
    {
        Id = Guid.NewGuid().ToString("n");
        SetValue(ItemsSourceProperty, menuItems);
    }

    /// <summary>
    /// Correctly activates
    /// </summary>
    public virtual void Activate(SidebarMenu navigationView)
    {
        SetCurrentValue(IsActiveProperty, true);

        if (!navigationView.IsOpen && NavigationViewItemParent is not null)
        {
            NavigationViewItemParent.Activate(navigationView);
        }

        if (NavigationViewItemParent is not null)
        {
            if (navigationView.IsOpen)
            {
                NavigationViewItemParent.IsExpanded = true;
            }
            else
            {
                NavigationViewItemParent.IsExpanded = false;
            }
        }

        if (
            Icon is SymbolIcon symbolIcon
        )
        {
            symbolIcon.Filled = true;
        }
    }

    /// <summary>
    /// Correctly deactivates
    /// </summary>
    public virtual void Deactivate(SidebarMenu navigationView)
    {
        SetCurrentValue(IsActiveProperty, false);
        NavigationViewItemParent?.Deactivate(navigationView);

        if (!navigationView.IsOpen && HasItems)
        {
            SetCurrentValue(IsExpandedProperty, false);
        }

        if (Icon is SymbolIcon symbolIcon)
        {
            symbolIcon.Filled = false;
        }
    }

    /// <inheritdoc />
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild(TemplateElementChevronGrid) is Grid chevronGrid)
        {
            ChevronGrid = chevronGrid;
        }
    }

    /// <inheritdoc />
    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        if (string.IsNullOrWhiteSpace(PageTag) && Content is not null)
        {
            SetCurrentValue(
                PageTagProperty,
                Content as string ?? Content.ToString()?.ToLower().Trim() ?? string.Empty
            );
        }
    }

    /// <inheritdoc />
    protected override void OnClick()
    {
        if (SidebarMenu.GetNavigationParent(this) is not { } navigationView)
        {
            return;
        }

        if (HasItems && navigationView.IsOpen)
        {
            SetCurrentValue(IsExpandedProperty, !IsExpanded);
        }

        if (PageType is not null)
        {
            navigationView.OnNavigationViewItemClick(this);
        }

        base.OnClick();
    }

    /// <summary>
    /// Is called when mouse is clicked down.
    /// </summary>
    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        if (!HasItems || e.LeftButton != MouseButtonState.Pressed)
        {
            base.OnMouseDown(e);
            return;
        }

        if (SidebarMenu.GetNavigationParent(this) is not { } navigationView)
        {
            return;
        }

        if (!navigationView.IsOpen || ChevronGrid is null)
        {
            base.OnMouseDown(e);
            return;
        }

        var mouseOverChevron = ActualWidth < e.GetPosition(this).X + ChevronGrid.ActualWidth;
        if (!mouseOverChevron)
        {
            base.OnMouseDown(e);
            return;
        }

        SetCurrentValue(IsExpandedProperty, !IsExpanded);

        for (int i = 0; i < MenuItems.Count; i++)
        {
            object? menuItem = MenuItems[i];

            if (menuItem is not IPageViewItem { IsActive: true })
            {
                continue;
            }

            if (IsExpanded)
            {
                Deactivate(navigationView);
            }
            else
            {
               Activate(navigationView);
            }

            break;
        }

        e.Handled = true;
    }

    private void OnMenuItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        SetValue(HasItemsPropertyKey, MenuItems.Count > 0);

        foreach (IPageViewItem item in MenuItems.OfType<IPageViewItem>())
        {
            item.NavigationViewItemParent = this;
        }
    }

    private static void OnItemsSourceChanged(DependencyObject? d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not PageViewItem navigationViewItem)
        {
            return;
        }

        navigationViewItem.MenuItems.Clear();

        if (e.NewValue is IEnumerable newItemsSource and not string)
        {
            foreach (var item in newItemsSource)
            {
                _ = navigationViewItem.MenuItems.Add(item);
            }
        }
        else if (e.NewValue != null)
        {
            _ = navigationViewItem.MenuItems.Add(e.NewValue);
        }
    }

    private void InitializeNavigationViewEvents()
    {
        if (SidebarMenu.GetNavigationParent(this) is { } navigationView)
        {
            SetCurrentValue(IsOpenProperty, navigationView.IsOpen);

            navigationView.Opened += (_, _) => SetCurrentValue(IsOpenProperty, true);
            navigationView.Closed += (_, _) => SetCurrentValue(IsOpenProperty, false);
        }
    }
}