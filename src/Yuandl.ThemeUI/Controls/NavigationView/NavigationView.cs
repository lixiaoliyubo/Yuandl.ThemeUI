// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using Yuandl.ThemeUI.ElementAssist;

namespace Yuandl.ThemeUI.Controls;

/// <content>
/// Defines the template part of the navigation view control.
/// </content>
[TemplatePart(
    Name = TemplateElementNavigationViewContentPresenter,
    Type = typeof(NavigationViewContentPresenter)
)]
public partial class NavigationView : System.Windows.Controls.Control, INavigationView
{
    private const string TemplateElementNavigationViewContentPresenter = "PART_NavigationViewContentPresenter";

    /// <summary>
    /// Gets or sets the control responsible for rendering content.
    /// </summary>
    protected NavigationViewContentPresenter NavigationViewContentPresenter { get; set; } = null!;

    protected SidebarMenu SidebarMenu { get; } = null!;

    /// <summary>
    /// Initializes static members of the <see cref="NavigationView"/> class.
    /// </summary>
    static NavigationView()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationView),
            new FrameworkPropertyMetadata(typeof(NavigationView))
        );
        MarginProperty.OverrideMetadata(
            typeof(NavigationView),
            new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0))
        );
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationView"/> class.
    /// </summary>
    public NavigationView()
    {
        NavigationParent = this;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    /// <summary>
    /// Applies the template to the control.
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        NavigationViewContentPresenter = GetTemplateChild<NavigationViewContentPresenter>(
            TemplateElementNavigationViewContentPresenter
        );
        if (Sidebar is SidebarMenu sidebar)
        {
            sidebar.Opened += OnIsPaneOpenChanged;
            sidebar.Closed += OnIsPaneOpenChanged;
            sidebar.ItemClick += OnSidebarItemClick;
            sidebar.ItemsChanged += OnSidebarItemsChanged;
        }
    }

    /// <summary>
    /// Handles the unloaded event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;
        Unloaded -= OnUnloaded;

        ClearJournal();

        NavigationPages.Clear();

        if (Sidebar is SidebarMenu sidebar)
        {
            sidebar.Opened -= OnIsPaneOpenChanged;
            sidebar.Closed -= OnIsPaneOpenChanged;
            sidebar.ItemClick -= OnSidebarItemClick;
            sidebar.ItemsChanged -= OnSidebarItemsChanged;
        }
    }

    /// <summary>
    /// Handles the loaded event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
    }

    /// <summary>
    /// Handles the ItemsChanged event of the SidebarMenu.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="args">The event arguments.</param>
    private void OnSidebarItemsChanged(SidebarMenu sender, RoutedEventArgs args)
    {
        IList<object> list = (IList<object>)sender.MenuItems;
        AddItemsToDictionaries(list);
    }

    /// <summary>
    /// Handles the ItemClick event of the SidebarMenu.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="args">The event arguments.</param>
    private void OnSidebarItemClick(SidebarMenu sender, RoutedEventArgs args)
    {
        IPageViewItem? item = sender.SelectedItem;
        if (item != null)
        {
            if (Navigate(item))
            {
            }
        }
    }

    /// <summary>
    /// Identifies the dependency property of the navigation parent control.
    /// </summary>
    internal static readonly DependencyProperty NavigationParentProperty =
        DependencyProperty.RegisterAttached(
            nameof(NavigationParent),
            typeof(NavigationView),
            typeof(NavigationView),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits)
        );

    /// <summary>
    /// Gets the dependency property value of the navigation parent control.
    /// </summary>
    internal NavigationView? NavigationParent
    {
        get => (NavigationView?)GetValue(NavigationParentProperty);
        private set => SetValue(NavigationParentProperty, value);
    }

    /// <summary>
    /// Gets the dependency property value of the navigation parent control.
    /// </summary>
    /// <param name="navigationItem">The control to read the dependency property value from.</param>
    /// <returns>The dependency property value of the navigation parent control.</returns>
    [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
    internal static NavigationView? GetNavigationParent(DependencyObject navigationItem) =>
        navigationItem.GetValue(NavigationParentProperty) as NavigationView;

    /// <summary>
    /// Identifies the dependency property of the SidebarMenu.
    /// </summary>
    public static readonly DependencyProperty SidebarProperty = DependencyProperty.Register(
        nameof(Sidebar),
        typeof(SidebarMenu),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(null, (d, baseValue) => (SidebarMenu?)baseValue)
    );

    /// <summary>
    /// Handles the IsPaneOpenChanged event of the SidebarMenu.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="args">The event arguments.</param>
    private void OnIsPaneOpenChanged(SidebarMenu sender, RoutedEventArgs args)
    {
        _ = VisualStateManager.GoToState(
            NavigationParent,
            sender.IsOpen ? "PaneOpen" : "PaneCompact",
            true
        );
    }

    /// <summary>
    /// Gets or sets the SidebarMenu instance.
    /// </summary>
    public SidebarMenu? Sidebar
    {
        get => (SidebarMenu?)GetValue(SidebarProperty);
        set => SetValue(SidebarProperty, value);
    }

    /// <summary>
    /// Identifies the dependency property of the transition duration.
    /// </summary>
    public static readonly DependencyProperty TransitionDurationProperty = DependencyProperty.Register(
        nameof(TransitionDuration),
        typeof(int),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(200)
    );

    /// <summary>
    /// Identifies the dependency property of the open pane length.
    /// </summary>
    public static readonly DependencyProperty OpenPaneLengthProperty = DependencyProperty.Register(
        nameof(OpenPaneLength),
        typeof(double),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(0D)
    );

    /// <summary>
    /// Gets or sets the open pane length.
    /// </summary>
    public double OpenPaneLength
    {
        get => (double)GetValue(OpenPaneLengthProperty);
        set => SetValue(OpenPaneLengthProperty, value);
    }

    /// <summary>
    /// Gets or sets the compact pane length.
    /// </summary>
    public double CompactPaneLength
    {
        get => (double)GetValue(CompactPaneLengthProperty);
        set => SetValue(CompactPaneLengthProperty, value);
    }

    /// <summary>
    /// Identifies the dependency property of the compact pane length.
    /// </summary>
    public static readonly DependencyProperty CompactPaneLengthProperty = DependencyProperty.Register(
        nameof(CompactPaneLength),
        typeof(double),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(60.0)
    );

    /// <summary>
    /// Gets or sets the transition duration.
    /// </summary>
    [Bindable(true)]
    [Category("Appearance")]
    public int TransitionDuration
    {
        get => (int)GetValue(TransitionDurationProperty);
        set => SetValue(TransitionDurationProperty, value);
    }

    /// <summary>
    /// Identifies the dependency property of the transition.
    /// </summary>
    public static readonly DependencyProperty TransitionProperty = DependencyProperty.Register(
        nameof(Transition),
        typeof(Transition),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(Transition.FadeInWithSlide)
    );

    /// <summary>
    /// Gets or sets the transition.
    /// </summary>
    public Transition Transition
    {
        get => (Transition)GetValue(TransitionProperty);
        set => SetValue(TransitionProperty, value);
    }

    /// <summary>
    /// Identifies the dependency property of the frame margin.
    /// </summary>
    public static readonly DependencyProperty FrameMarginProperty = DependencyProperty.Register(
        nameof(FrameMargin),
        typeof(Thickness),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(default(Thickness))
    );

    /// <summary>
    /// Gets or sets the frame margin.
    /// </summary>
    public Thickness FrameMargin
    {
        get => (Thickness)GetValue(FrameMarginProperty);
        set => SetValue(FrameMarginProperty, value);
    }

    /// <summary>
    /// The navigating event.
    /// </summary>
    public event TypedEventHandler<NavigationView, NavigatingCancelEventArgs> Navigating
    {
        add => AddHandler(NavigatingEvent, value);
        remove => RemoveHandler(NavigatingEvent, value);
    }

    /// <summary>
    /// The navigated event.
    /// </summary>
    public event TypedEventHandler<NavigationView, NavigatedEventArgs> Navigated
    {
        add => AddHandler(NavigatedEvent, value);
        remove => RemoveHandler(NavigatedEvent, value);
    }

    /// <summary>
    /// Identifies the navigating event.
    /// </summary>
    public static readonly RoutedEvent NavigatingEvent = EventManager.RegisterRoutedEvent(
        nameof(Navigating),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<NavigationView, NavigatingCancelEventArgs>),
        typeof(NavigationView)
    );

    /// <summary>
    /// Identifies the navigated event.
    /// </summary>
    public static readonly RoutedEvent NavigatedEvent = EventManager.RegisterRoutedEvent(
        nameof(Navigated),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<NavigationView, NavigatedEventArgs>),
        typeof(NavigationView)
    );

    /// <summary>
    /// The selection changed event.
    /// </summary>
    public event TypedEventHandler<NavigationView, RoutedEventArgs> SelectionChanged
    {
        add => AddHandler(SelectionChangedEvent, value);
        remove => RemoveHandler(SelectionChangedEvent, value);
    }

    /// <summary>
    /// Identifies the selection changed event.
    /// </summary>
    public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(SelectionChanged),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<NavigationView, RoutedEventArgs>),
        typeof(NavigationView)
    );

    /// <summary>
    /// Raises the selection changed event.
    /// </summary>
    protected virtual void OnSelectionChanged()
    {
        RaiseEvent(new RoutedEventArgs(SelectionChangedEvent, this));
    }

    /// <summary>
    /// Raises the navigating event.
    /// </summary>
    /// <param name="sourcePage">The source page.</param>
    /// <returns>True if the navigation was canceled; otherwise, false.</returns>
    protected virtual bool OnNavigating(object sourcePage)
    {
        var eventArgs = new NavigatingCancelEventArgs(NavigatingEvent, this) { Page = sourcePage };

        RaiseEvent(eventArgs);

        return eventArgs.Cancel;
    }

    /// <summary>
    /// Raises the navigated event.
    /// </summary>
    /// <param name="page">The page.</param>
    protected virtual void OnNavigated(object page)
    {
        var eventArgs = new NavigatedEventArgs(NavigatedEvent, this) { Page = page };

        RaiseEvent(eventArgs);
    }

    private IServiceProvider? _serviceProvider;

    /// <summary>
    /// Gets or sets the selected item.
    /// </summary>
    public IPageViewItem? SelectedItem { get; private set; }

    private int _currentIndexInJournal;

    /// <summary>
    /// Gets stores the navigation history.
    /// </summary>
    private List<string> Journal { get; } = new(50);

    private readonly NavigationCache _cache = new();

    /// <summary>
    /// Gets or sets all navigation pages.
    /// </summary>
    protected ConcurrentDictionary<string, IPageViewItem> NavigationPages { get; set; } = [];

    /// <summary>
    /// Gets a value indicating whether the navigation view can go back.
    /// </summary>
    public bool CanGoBack => Journal.Count > 1 && _currentIndexInJournal >= 0;

    /// <summary>
    /// Sets the service provider.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public void SetServiceProvider(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    /// <summary>
    /// Adds items to the dictionary.
    /// </summary>
    /// <param name="list">The list of items to add.</param>
    protected virtual void AddItemsToDictionaries(IEnumerable list)
    {
        foreach (IPageViewItem pageViewItem in list.OfType<IPageViewItem>())
        {
            bool isAdd = false;
            if (!string.IsNullOrEmpty(pageViewItem.Id) && !NavigationPages.ContainsKey(pageViewItem.Id))
            {
                isAdd = true;
                _ = NavigationPages.TryAdd(
                    pageViewItem.Id,
                    pageViewItem
                );
            }

            if (!isAdd && !string.IsNullOrEmpty(pageViewItem.PageTag) && !NavigationPages.ContainsKey(pageViewItem.PageTag))
            {
                _ = NavigationPages.TryAdd(
                    pageViewItem.PageTag,
                    pageViewItem
                );
            }

            if (pageViewItem.HasItems)
            {
                AddItemsToDictionaries(pageViewItem.MenuItems);
            }
        }
    }

    /// <summary>
    /// Navigates to the specified page ID or target tag.
    /// </summary>
    /// <param name="pageIdOrTargetTag">The page ID or target tag.</param>
    /// <param name="dataContext">The data context.</param>
    /// <returns>True if navigation is successful; otherwise, false.</returns>
    public bool Navigate(string pageIdOrTargetTag, object? dataContext = null)
    {
        if (
            NavigationPages.TryGetValue(
                pageIdOrTargetTag,
                out IPageViewItem? navigationViewItem
            )
        )
        {
            return Navigate(navigationViewItem?.PageType, dataContext);
        }

        return false;
    }

    /// <summary>
    /// Navigates to the specified page type.
    /// </summary>
    /// <param name="PageType">The page type.</param>
    /// <param name="dataContext">The data context.</param>
    /// <returns>True if navigation is successful; otherwise, false.</returns>
    public bool Navigate(Type? PageType, object? dataContext = null)
    {
        IPageViewItem navigationViewItem = NavigationPages.FirstOrDefault(a => a.Value.PageType == PageType).Value;
        if (navigationViewItem == null)
        {
            navigationViewItem = new PageViewItem() { PageType = PageType, PageTag = Guid.NewGuid().ToString(), DataContent = dataContext };
            _ = NavigationPages.TryAdd(navigationViewItem.PageTag, navigationViewItem);
        }

        return Navigate(navigationViewItem, dataContext);
    }

    /// <summary>
    /// Navigates to the specified page view item.
    /// </summary>
    /// <param name="viewItem">The page view item.</param>
    /// <param name="dataContext">The data context.</param>
    /// <param name="isBackwardsNavigated">A value indicating whether the navigation is backwards.</param>
    /// <returns>True if navigation is successful; otherwise, false.</returns>
    public bool Navigate(IPageViewItem viewItem, object? dataContext = null, bool isBackwardsNavigated = false)
    {
        if (SelectedItem == viewItem)
        {
            return false;
        }

        SelectedItem = viewItem;

        var pageInstance = GetNavigationItemInstance(viewItem);

        if (OnNavigating(pageInstance))
        {
            return false;
        }

        if (Sidebar != null)
        {
            Sidebar.OnNavigationViewItemClick(viewItem);
        }

        OnNavigated(pageInstance);

        _ = UpdateContent(pageInstance, dataContext);

        AddToJournal(viewItem, isBackwardsNavigated);

        OnSelectionChanged();

        return true;
    }

    /// <summary>
    /// Updates the content.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="dataContext">The data context.</param>
    /// <returns>True if the content update is successful; otherwise, false.</returns>
    private bool UpdateContent(object? content, object? dataContext = null)
    {
        if (dataContext is not null && content is FrameworkElement frameworkViewContent)
        {
            frameworkViewContent.DataContext = dataContext;
        }

        return NavigationViewContentPresenter.Navigate(content);
    }

    /// <summary>
    /// Gets the navigation item instance.
    /// </summary>
    /// <param name="pageViewItem">The navigation view item.</param>
    /// <returns>The navigation item instance.</returns>
    private object GetNavigationItemInstance(IPageViewItem? pageViewItem)
    {
        if (pageViewItem?.PageType is null)
        {
            throw new InvalidOperationException(
                $"The {nameof(pageViewItem.PageTag)} property cannot be null."
            );
        }

        if (_serviceProvider is not null)
        {
            return _serviceProvider.GetService(pageViewItem.PageType)
                ?? throw new InvalidOperationException(
            $"{nameof(_serviceProvider)}.{nameof(_serviceProvider.GetService)} returned null for type {pageViewItem.PageType}."
            );
        }

        return _cache.Remember(
                pageViewItem.PageType,
                pageViewItem.CacheMode,
                ComputeCachedNavigationInstance
            )
            ?? throw new InvalidOperationException(
                $"Unable to get or create instance of {pageViewItem.PageTag} from cache."
            );

        object? ComputeCachedNavigationInstance() => GetPageInstanceFromCache(pageViewItem.PageType);
    }

    /// <summary>
    /// Gets the page instance from the cache.
    /// </summary>
    /// <param name="pageType">The navigation view item.</param>
    /// <returns>The page instance.</returns>
    private object? GetPageInstanceFromCache(Type pageType)
    {
        if (pageType is null)
        {
            return default;
        }

        if (_serviceProvider is not null)
        {
            return _serviceProvider.GetService(pageType)
                ?? throw new InvalidOperationException(
                    $"{nameof(_serviceProvider.GetService)} returned null"
                );
        }

        return NavigationViewActivator.CreateInstance(pageType)
            ?? throw new InvalidOperationException("Failed to create instance of the page");
    }

    /// <summary>
    /// Invokes the element constructor.
    /// </summary>
    /// <param name="tPage">The page type.</param>
    /// <param name="dataContext">The data context.</param>
    /// <returns>The element instance.</returns>
    private FrameworkElement? InvokeElementConstructor(Type tPage, object? dataContext)
    {
        ConstructorInfo? ctor = dataContext is null
            ? tPage.GetConstructor(Type.EmptyTypes)
            : tPage.GetConstructor(new[] { dataContext.GetType() });

        var result = ctor?.Invoke(null) as FrameworkElement;

        return result;
    }

    /// <summary>
    /// Adds to the journal.
    /// </summary>
    /// <param name="viewItem">The view item.</param>
    /// <param name="isBackwardsNavigated">A value indicating whether the navigation is backwards.</param>
    private void AddToJournal(IPageViewItem viewItem, bool isBackwardsNavigated)
    {
        if (isBackwardsNavigated)
        {
            // If it's a backwards navigation, remove the last two records
            Journal.RemoveAt(Journal.LastIndexOf(Journal[^2]));
            Journal.RemoveAt(Journal.LastIndexOf(Journal[^1]));

            // Update the current index, decrease by 2
            _currentIndexInJournal -= 2;
        }

        // Add the current view item's tag to the journal
        Journal.Add(viewItem.PageTag);

        // Increase the current index
        _currentIndexInJournal++;
    }

    /// <summary>
    /// Goes back.
    /// </summary>
    /// <returns>True if navigation is successful; otherwise, false.</returns>
    public virtual bool GoBack()
    {
        // If there are not more than one page in the journal, it's not possible to go back
        if (Journal.Count <= 1)
        {
            return false;
        }

        // Get the ID of the second last page
        var itemId = Journal[^2];

        // Navigate to that page
        return Navigate(itemId, null);
    }

    /// <summary>
    /// Clears the journal.
    /// </summary>
    public virtual void ClearJournal()
    {
        // Reset the current index to 0
        _currentIndexInJournal = 0;

        // Clear the journal
        Journal.Clear();
    }

    /// <summary>
    /// Refreshes the current page.
    /// </summary>
    /// <returns>True if refresh is successful; otherwise, false.</returns>
    public virtual bool Refresh()
    {
        if (SelectedItem != null)
        {
            // 保存当前页面的状态（如果需要）
            IPageViewItem currentPage = SelectedItem;

            // 移除当前内容，模拟关闭
            SelectedItem = null;
            _ = UpdateContent(null, null);

            // 重新加载页面，模拟重新打开
            return Navigate(currentPage);
        }

        return false;
    }

    /// <summary>
    /// Gets the template child.
    /// </summary>
    /// <typeparam name="T">The type of the child.</typeparam>
    /// <param name="name">The name of the child.</param>
    /// <returns>The template child.</returns>
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
