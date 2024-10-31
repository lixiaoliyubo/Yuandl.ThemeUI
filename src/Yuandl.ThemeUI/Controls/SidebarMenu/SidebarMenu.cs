// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Yuandl.ThemeUI.Controls;

/// <content>
/// Defines the template parts for the <see cref="NavigationView"/> control
/// </content>
[TemplatePart(Name = TemplateElementMenuItemsItemsControl, Type = typeof(System.Windows.Controls.ItemsControl))]
[TemplatePart(Name = TemplateElementToggleButton, Type = typeof(System.Windows.Controls.Button))]
[TemplatePart(Name = TemplateElementAutoSuggestBoxSymbolButton, Type = typeof(System.Windows.Controls.Button))]
public partial class SidebarMenu : System.Windows.Controls.Control
{
    /// <summary>
    /// Template element represented by the <c>PART_MenuItemsItemsControl</c> name.
    /// </summary>
    private const string TemplateElementMenuItemsItemsControl = "PART_MenuItemsItemsControl";

    /// <summary>
    /// Template element represented by the <c>PART_ToggleButton</c> name.
    /// </summary>
    private const string TemplateElementToggleButton = "PART_ToggleButton";

    /// <summary>
    /// Template element represented by the <c>PART_AutoSuggestBoxSymbolButton</c> name.
    /// </summary>
    private const string TemplateElementAutoSuggestBoxSymbolButton = "PART_AutoSuggestBoxSymbolButton";

    /// <summary>
    /// Gets or sets the control located at the top of the pane with hamburger icon.
    /// </summary>
    protected Button? ToggleButton { get; set; }

    /// <summary>
    /// Gets or sets the control that is visitable if PaneDisplayMode="Left" and in compact state
    /// </summary>
    protected System.Windows.Controls.Button? AutoSuggestBoxSymbolButton { get; set; }

    /// <summary>
    /// Gets or sets the control located at the top of the pane with left arrow icon.
    /// </summary>
    protected System.Windows.Controls.ItemsControl MenuItemsItemsControl { get; set; } = null!;

    public IPageViewItem? SelectedItem { get; set; }

    public SidebarMenu()
    {
        NavigationParent = this;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;

        // Initialize MenuItems collection
        var menuItems = new ObservableCollection<object>();
        menuItems.CollectionChanged += OnMenuItems_CollectionChanged;
        SetValue(MenuItemsPropertyKey, menuItems);
    }

    /// <inheritdoc />
    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        UpdateAutoSuggestBoxSuggestions();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdateVisualState((SidebarMenu)sender);
    }

    /// <summary>
    /// This virtual method is called when this element is detached form a loaded tree.
    /// </summary>
    protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;
        Unloaded -= OnUnloaded;

        if (AutoSuggestBox is not null)
        {
            AutoSuggestBox.SuggestionChosen -= AutoSuggestBoxOnSuggestionChosen;
            AutoSuggestBox.QuerySubmitted -= AutoSuggestBoxOnQuerySubmitted;
        }

        if (ToggleButton is not null)
        {
            ToggleButton.Click -= OnToggleButtonClick;
        }

        if (AutoSuggestBoxSymbolButton is not null)
        {
            AutoSuggestBoxSymbolButton.Click -= AutoSuggestBoxSymbolButtonOnClick;
        }
    }

    /// <inheritdoc />
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        MenuItemsItemsControl = GetTemplateChild<System.Windows.Controls.ItemsControl>(TemplateElementMenuItemsItemsControl);

        MenuItemsItemsControl.SetCurrentValue(ItemsControl.ItemsSourceProperty, MenuItems);

        if (
            GetTemplateChild(TemplateElementAutoSuggestBoxSymbolButton)
            is System.Windows.Controls.Button autoSuggestBoxSymbolButton
        )
        {
            AutoSuggestBoxSymbolButton = autoSuggestBoxSymbolButton;

            AutoSuggestBoxSymbolButton.Click -= AutoSuggestBoxSymbolButtonOnClick;
            AutoSuggestBoxSymbolButton.Click += AutoSuggestBoxSymbolButtonOnClick;
        }

        if (GetTemplateChild(TemplateElementToggleButton) is Controls.Button toggleButton)
        {
            ToggleButton = toggleButton;

            ToggleButton.Click -= OnToggleButtonClick;
            ToggleButton.Click += OnToggleButtonClick;
        }
    }

    private void UpdateAutoSuggestBoxSuggestions()
    {
        if (AutoSuggestBox == null)
        {
            return;
        }

        _autoSuggestBoxItems.Clear();

        AddItemsToAutoSuggestBoxItems(MenuItems);
    }

    protected virtual void AddItemsToAutoSuggestBoxItems(IEnumerable list)
    {
        foreach (PageViewItem pageViewItem in list.OfType<PageViewItem>())
        {
            if (
                pageViewItem is { Content: string content, PageType: { } }
                && !string.IsNullOrWhiteSpace(content)
            )
            {
                _autoSuggestBoxItems.Add(content);
            }

            if (pageViewItem.HasItems)
            {
                AddItemsToAutoSuggestBoxItems(pageViewItem.MenuItems);
            }
        }
    }

    /// <summary>
    /// This virtual method is called when <see cref="ToggleButton"/> is clicked.
    /// </summary>
    protected virtual void OnToggleButtonClick(object sender, RoutedEventArgs e)
    {
        SetCurrentValue(IsOpenProperty, !IsOpen);
    }

    /// <summary>
    /// This virtual method is called when <see cref="AutoSuggestBoxSymbolButton"/> is clicked.
    /// </summary>
    protected virtual void AutoSuggestBoxSymbolButtonOnClick(object sender, RoutedEventArgs e)
    {
        SetCurrentValue(IsOpenProperty, !IsOpen);
        _ = AutoSuggestBox?.Focus();
    }

    private void OnMenuItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems is null)
        {
            return;
        }

        UpdateMenuItemsTemplate(e.NewItems);
    }

    protected virtual void UpdateMenuItemsTemplate(IEnumerable list)
    {
        if (ItemTemplate == null)
        {
            return;
        }

        foreach (var item in list)
        {
            if (item is PageViewItem pageViewItem)
            {
                pageViewItem.Template = ItemTemplate;
            }
        }
    }

    internal void OnNavigationViewItemClick(IPageViewItem pageViewItem)
    {
        SelectedItem?.Deactivate(this);
        if (SelectedItem != pageViewItem)
        {
            SelectedItem = pageViewItem;
            pageViewItem.Activate(this);
            OnItemInvoked();
        }
    }

    internal SidebarMenu? NavigationParent
    {
        get => (SidebarMenu?)GetValue(NavigationParentProperty);
        private set => SetValue(NavigationParentProperty, value);
    }

    /// <summary>Identifies the <see cref="NavigationParent"/> dependency property.</summary>
    internal static readonly DependencyProperty NavigationParentProperty =
        DependencyProperty.RegisterAttached(
            nameof(NavigationParent),
            typeof(SidebarMenu),
            typeof(SidebarMenu),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits)
        );

    [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
    internal static SidebarMenu? GetNavigationParent(DependencyObject navigationItem) =>
        navigationItem.GetValue(NavigationParentProperty) as SidebarMenu;


    private readonly ObservableCollection<string> _autoSuggestBoxItems = [];
    private static readonly Thickness AutoSuggestBoxMarginDefault = new(8, 8, 8, 16);

    /// <summary>Identifies the <see cref="Title"/> dependency property.</summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(SidebarMenu),
        new FrameworkPropertyMetadata(null)
    );

    /// <summary>Identifies the <see cref="IsOpen"/> dependency property.</summary>
    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
        nameof(IsOpen),
        typeof(bool),
        typeof(SidebarMenu),
        new FrameworkPropertyMetadata(true, OnIsOpenChanged)
    );

    /// <summary>Identifies the <see cref="IsVisible"/> dependency property.</summary>
    public static new readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(
        nameof(IsVisible),
        typeof(bool),
        typeof(SidebarMenu),
        new FrameworkPropertyMetadata(false)
    );

    /// <summary>Identifies the <see cref="IsToggleVisible"/> dependency property.</summary>
    public static readonly DependencyProperty IsToggleVisibleProperty = DependencyProperty.Register(
        nameof(IsToggleVisible),
        typeof(bool),
        typeof(SidebarMenu),
        new FrameworkPropertyMetadata(true)
    );

    /// <summary>Identifies the <see cref="AutoSuggestBox"/> dependency property.</summary>
    public static readonly DependencyProperty AutoSuggestBoxProperty = DependencyProperty.Register(
        nameof(AutoSuggestBox),
        typeof(AutoSuggestBox),
        typeof(SidebarMenu),
        new FrameworkPropertyMetadata(null, OnAutoSuggestBoxChanged)
    );

    private static readonly DependencyPropertyKey MenuItemsPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(MenuItems),
        typeof(ObservableCollection<object>),
        typeof(SidebarMenu),
        new PropertyMetadata(null)
    );

    /// <summary>Identifies the <see cref="MenuItems"/> dependency property.</summary>
    public static readonly DependencyProperty MenuItemsProperty = MenuItemsPropertyKey.DependencyProperty;

    /// <summary>Identifies the <see cref="ItemsSource"/> dependency property.</summary>
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
        nameof(ItemsSource),
        typeof(object),
        typeof(SidebarMenu),
        new FrameworkPropertyMetadata(null, OnItemsSourceChanged)
    );

    /// <summary>Identifies the <see cref="HeaderVisibility"/> dependency property.</summary>
    public static readonly DependencyProperty HeaderVisibilityProperty = DependencyProperty.Register(
        nameof(HeaderVisibility),
        typeof(Visibility),
        typeof(SidebarMenu),
        new FrameworkPropertyMetadata(Visibility.Visible)
    );

    /// <summary>Identifies the <see cref="ItemTemplate"/> dependency property.</summary>
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
        nameof(ItemTemplate),
        typeof(ControlTemplate),
        typeof(SidebarMenu),
        new FrameworkPropertyMetadata(
            null,
            FrameworkPropertyMetadataOptions.AffectsMeasure,
            OnItemTemplateChanged
        )
    );

    /// <summary>Identifies the <see cref="ItemClick"/> routed event.</summary>
    public static readonly RoutedEvent ItemClickEvent = EventManager.RegisterRoutedEvent(
        nameof(ItemClick),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<SidebarMenu, RoutedEventArgs>),
        typeof(SidebarMenu)
    );

    public event TypedEventHandler<SidebarMenu, RoutedEventArgs> ItemClick
    {
        add => AddHandler(ItemClickEvent, value);
        remove => RemoveHandler(ItemClickEvent, value);
    }

    public ControlTemplate? ItemTemplate
    {
        get => (ControlTemplate?)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    public Visibility HeaderVisibility
    {
        get => (Visibility)GetValue(HeaderVisibilityProperty);
        set => SetValue(HeaderVisibilityProperty, value);
    }

    /// <summary>Identifies the <see cref="ItemsChanged"/> routed event.</summary>
    public static readonly RoutedEvent ItemsChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(ItemsChanged),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<SidebarMenu, RoutedEventArgs>),
        typeof(SidebarMenu)
    );

    public event TypedEventHandler<SidebarMenu, RoutedEventArgs> ItemsChanged
    {
        add => AddHandler(ItemsChangedEvent, value);
        remove => RemoveHandler(ItemsChangedEvent, value);
    }

    /// <summary>Identifies the <see cref="Opened"/> routed event.</summary>
    public static readonly RoutedEvent OpenedEvent = EventManager.RegisterRoutedEvent(
        nameof(Opened),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<SidebarMenu, RoutedEventArgs>),
        typeof(SidebarMenu)
    );

    /// <summary>Identifies the <see cref="Closed"/> routed event.</summary>
    public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
        nameof(Closed),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<SidebarMenu, RoutedEventArgs>),
        typeof(SidebarMenu)
    );

    public event TypedEventHandler<SidebarMenu, RoutedEventArgs> Opened
    {
        add => AddHandler(OpenedEvent, value);
        remove => RemoveHandler(OpenedEvent, value);
    }

    public event TypedEventHandler<SidebarMenu, RoutedEventArgs> Closed
    {
        add => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    public IList MenuItems => (ObservableCollection<object>)GetValue(MenuItemsProperty);

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

    public AutoSuggestBox? AutoSuggestBox
    {
        get => (AutoSuggestBox?)GetValue(AutoSuggestBoxProperty);
        set => SetValue(AutoSuggestBoxProperty, value);
    }

    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public bool IsToggleVisible
    {
        get => (bool)GetValue(IsToggleVisibleProperty);
        set => SetValue(IsToggleVisibleProperty, value);
    }

    public new bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }

    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>
    /// Raises the item invoked event.
    /// </summary>
    protected virtual void OnItemInvoked()
    {
        RaiseEvent(new RoutedEventArgs(ItemClickEvent, this));
    }

    private static void OnItemTemplateChanged(DependencyObject? d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not SidebarMenu navigationView)
        {
            return;
        }

        navigationView.OnItemTemplateChanged();
    }

    /// <summary>
    /// This virtual method is called when <see cref="ItemTemplate"/> is changed.
    /// </summary>
    protected virtual void OnItemTemplateChanged()
    {
        UpdateMenuItemsTemplate();
    }

    protected virtual void UpdateMenuItemsTemplate()
    {
        UpdateMenuItemsTemplate(MenuItems);
    }

    private static void OnItemsSourceChanged(DependencyObject? d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not SidebarMenu navigationView)
        {
            return;
        }

        navigationView.MenuItems.Clear();

        if (e.NewValue is IEnumerable newItemsSource and not string)
        {
            foreach (var item in newItemsSource)
            {
                _ = navigationView.MenuItems.Add(item);
            }
        }
        else if (e.NewValue != null)
        {
            _ = navigationView.MenuItems.Add(e.NewValue);
        }

        navigationView.RaiseEvent(new RoutedEventArgs(ItemsChangedEvent, navigationView));

        navigationView.UpdateAutoSuggestBoxSuggestions();
    }

    private static void OnAutoSuggestBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not SidebarMenu navigationView)
        {
            return;
        }

        if (e.NewValue is null && e.OldValue is AutoSuggestBox oldValue)
        {
            oldValue.SuggestionChosen -= navigationView.AutoSuggestBoxOnSuggestionChosen;
            oldValue.QuerySubmitted -= navigationView.AutoSuggestBoxOnQuerySubmitted;
            return;
        }

        if (e.NewValue is not AutoSuggestBox autoSuggestBox)
        {
            return;
        }

        autoSuggestBox.OriginalItemsSource = navigationView._autoSuggestBoxItems;
        autoSuggestBox.SuggestionChosen += navigationView.AutoSuggestBoxOnSuggestionChosen;
        autoSuggestBox.QuerySubmitted += navigationView.AutoSuggestBoxOnQuerySubmitted;
    }

    /// <summary>
    /// Navigate to the page after its name is selected in <see cref="AutoSuggestBox"/>.
    /// </summary>
    private void AutoSuggestBoxOnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        if (sender.IsSuggestionListOpen)
        {
            return;
        }

        if (args.SelectedItem is not string selectedSuggestBoxItem)
        {
            return;
        }

        if (NavigateToMenuItemFromAutoSuggestBox(MenuItems, selectedSuggestBoxItem))
        {
            return;
        }
    }

    protected virtual bool NavigateToMenuItemFromAutoSuggestBox(IEnumerable list, string selectedSuggestBoxItem)
    {
        foreach (PageViewItem pageViewItem in list.OfType<PageViewItem>())
        {
            if (pageViewItem.Content is string content && content == selectedSuggestBoxItem)
            {
                pageViewItem.BringIntoView();
                _ = pageViewItem.Focus(); // TODO: Element or content?

                return true;
            }

            if (
                NavigateToMenuItemFromAutoSuggestBox(
                    pageViewItem.MenuItems,
                    selectedSuggestBoxItem
                )
            )
            {
                return true;
            }
        }

        return false;
    }

    private void AutoSuggestBoxOnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        var suggestions = new List<string>();
        var querySplit = args.QueryText.Split(' ');

        foreach (var item in _autoSuggestBoxItems)
        {
            bool isMatch = true;

            foreach (string queryToken in querySplit)
            {
                if (item.IndexOf(queryToken, StringComparison.CurrentCultureIgnoreCase) < 0)
                {
                    isMatch = false;
                }
            }

            if (isMatch)
            {
                suggestions.Add(item);
            }
        }

        if (suggestions.Count <= 0)
        {
            return;
        }

        var element = suggestions.First();

        if (NavigateToMenuItemFromAutoSuggestBox(MenuItems, element))
        {
            return;
        }
    }

    private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not SidebarMenu navigationView)
        {
            return;
        }

        if ((bool)e.NewValue == (bool)e.OldValue)
        {
            return;
        }

        if (navigationView.IsOpen)
        {
            navigationView.RaiseEvent(new RoutedEventArgs(OpenedEvent, navigationView));
        }
        else
        {
            navigationView.RaiseEvent(new RoutedEventArgs(ClosedEvent, navigationView));
        }

        UpdateVisualState(navigationView);

        navigationView.CloseNavigationViewItemMenus();
    }

    protected static void UpdateVisualState(SidebarMenu navigationView)
    {
        _ = VisualStateManager.GoToState(
            navigationView,
            navigationView.IsOpen ? "PaneOpen" : "PaneCompact",
            true
        );
    }

    protected virtual void CloseNavigationViewItemMenus()
    {
        foreach (var item in MenuItems)
        {
            if (item is PageViewItem singleNavigationViewItem)
            {
                singleNavigationViewItem.Deactivate(this);
            }
        }
    }

    protected T GetTemplateChild<T>(string name)
        where T : DependencyObject
    {
        if (GetTemplateChild(name) is not T dependencyObject)
        {
            throw new ArgumentNullException(name);
        }

        return dependencyObject;
    }
}