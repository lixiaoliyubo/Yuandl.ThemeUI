// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using Yuandl.ThemeUI.ElementAssist;

namespace Yuandl.ThemeUI.Controls;

/// <content>
/// Defines the template parts for the <see cref="NavigationView"/> control
/// </content>
[TemplatePart(
    Name = TemplateElementNavigationViewContentPresenter,
    Type = typeof(NavigationViewContentPresenter)
)]
public partial class NavigationView : System.Windows.Controls.Control, INavigationView
{
    private const string TemplateElementNavigationViewContentPresenter = "PART_NavigationViewContentPresenter";

    /// <summary>
    /// Gets or sets the control responsible for rendering the content.
    /// </summary>
    protected NavigationViewContentPresenter NavigationViewContentPresenter { get; set; } = null!;

    protected SidebarMenu SidebarMenu { get; } = null!;

    /// <summary>
    /// Initializes static members of the <see cref="NavigationView"/> class and overrides default property metadata.
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

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;
        Unloaded -= OnUnloaded;

        ClearJournal();

        PageIdOrTargetTagNavigationViewsDictionary.Clear();

        if (Sidebar is SidebarMenu sidebar)
        {
            sidebar.Opened -= OnIsPaneOpenChanged;
            sidebar.Closed -= OnIsPaneOpenChanged;
            sidebar.ItemClick -= OnSidebarItemClick;
            sidebar.ItemsChanged -= OnSidebarItemsChanged;
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
    }

    private void OnSidebarItemsChanged(SidebarMenu sender, RoutedEventArgs args)
    {
        IList<object> list = (IList<object>)sender.MenuItems;
        AddItemsToDictionaries(list);
    }

    protected virtual void AddItemsToDictionaries(IEnumerable list)
    {
        foreach (IPageViewItem pageViewItem in list.OfType<IPageViewItem>())
        {
            if (!PageIdOrTargetTagNavigationViewsDictionary.ContainsKey(pageViewItem.Id))
            {
                PageIdOrTargetTagNavigationViewsDictionary.Add(
                    pageViewItem.Id,
                    pageViewItem
                );
            }

            if (
                !PageIdOrTargetTagNavigationViewsDictionary.ContainsKey(
                    pageViewItem.PageTag
                )
            )
            {
                PageIdOrTargetTagNavigationViewsDictionary.Add(
                    pageViewItem.PageTag,
                    pageViewItem
                );
            }

            if (pageViewItem.PageType != null && !NavigationPages.ContainsKey(pageViewItem.PageType))
            {
                _ = NavigationPages.TryAdd(
                    pageViewItem.PageType,
                    pageViewItem
                );
            }

            if (pageViewItem.HasItems)
            {
                AddItemsToDictionaries(pageViewItem.MenuItems);
            }
        }
    }

    private void OnSidebarItemClick(SidebarMenu sender, RoutedEventArgs args)
    {
        IPageViewItem? item = sender.SelectedItem;
        if (item != null)
        {
            if (NavigationParent?.Navigate(item) ?? false) 
            {
                SelectedItem = item;
            }
        }
    }

    /// <summary>Identifies the <see cref="NavigationParent"/> dependency property.</summary>
    internal static readonly DependencyProperty NavigationParentProperty =
        DependencyProperty.RegisterAttached(
            nameof(NavigationParent),
            typeof(NavigationView),
            typeof(NavigationView),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits)
        );

    /// <summary>
    /// Gets the parent <see cref="NavigationView"/> for its <see cref="PageViewItem"/> children.
    /// </summary>
    internal NavigationView? NavigationParent
    {
        get => (NavigationView?)GetValue(NavigationParentProperty);
        private set => SetValue(NavigationParentProperty, value);
    }

    /// <summary>Helper for getting <see cref="NavigationParentProperty"/> from <paramref name="navigationItem"/>.</summary>
    /// <param name="navigationItem"><see cref="DependencyObject"/> to read <see cref="NavigationParentProperty"/> from.</param>
    /// <returns>NavigationParent property value.</returns>
    [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
    internal static NavigationView? GetNavigationParent(DependencyObject navigationItem) =>
        navigationItem.GetValue(NavigationParentProperty) as NavigationView;

    /// <summary>Identifies the <see cref="SidebarMenu"/> dependency property.</summary>
    public static readonly DependencyProperty SidebarProperty = DependencyProperty.Register(
        nameof(Sidebar),
        typeof(SidebarMenu),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(null, (d, baseValue) => (SidebarMenu?)baseValue)
    );

    private void OnIsPaneOpenChanged(SidebarMenu sender, RoutedEventArgs args)
    {
        _ = VisualStateManager.GoToState(
            NavigationParent,
            sender.IsOpen ? "PaneOpen" : "PaneCompact",
            true
        );
    }

    public SidebarMenu? Sidebar
    {
        get => (SidebarMenu?)GetValue(SidebarProperty);
        set => SetValue(SidebarProperty, value);
    }

    /// <summary>Identifies the <see cref="TransitionDuration"/> dependency property.</summary>
    public static readonly DependencyProperty TransitionDurationProperty = DependencyProperty.Register(
        nameof(TransitionDuration),
        typeof(int),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(200)
    );

    /// <summary>Identifies the <see cref="OpenPaneLength"/> dependency property.</summary>
    public static readonly DependencyProperty OpenPaneLengthProperty = DependencyProperty.Register(
        nameof(OpenPaneLength),
        typeof(double),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(0D)
    );

    public double OpenPaneLength
    {
        get => (double)GetValue(OpenPaneLengthProperty);
        set => SetValue(OpenPaneLengthProperty, value);
    }

    public double CompactPaneLength
    {
        get => (double)GetValue(CompactPaneLengthProperty);
        set => SetValue(CompactPaneLengthProperty, value);
    }

    /// <summary>Identifies the <see cref="CompactPaneLength"/> dependency property.</summary>
    public static readonly DependencyProperty CompactPaneLengthProperty = DependencyProperty.Register(
        nameof(CompactPaneLength),
        typeof(double),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(60.0)
    );

    [Bindable(true)]
    [Category("Appearance")]
    public int TransitionDuration
    {
        get => (int)GetValue(TransitionDurationProperty);
        set => SetValue(TransitionDurationProperty, value);
    }

    /// <summary>Identifies the <see cref="Transition"/> dependency property.</summary>
    public static readonly DependencyProperty TransitionProperty = DependencyProperty.Register(
        nameof(Transition),
        typeof(Transition),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(Transition.FadeInWithSlide)
    );

    public Transition Transition
    {
        get => (Transition)GetValue(TransitionProperty);
        set => SetValue(TransitionProperty, value);
    }

    /// <summary>Identifies the <see cref="FrameMargin"/> dependency property.</summary>
    public static readonly DependencyProperty FrameMarginProperty = DependencyProperty.Register(
        nameof(FrameMargin),
        typeof(Thickness),
        typeof(NavigationView),
        new FrameworkPropertyMetadata(default(Thickness))
    );

    public Thickness FrameMargin
    {
        get => (Thickness)GetValue(FrameMarginProperty);
        set => SetValue(FrameMarginProperty, value);
    }

    public event TypedEventHandler<NavigationView, NavigatingCancelEventArgs> Navigating
    {
        add => AddHandler(NavigatingEvent, value);
        remove => RemoveHandler(NavigatingEvent, value);
    }

    public event TypedEventHandler<NavigationView, NavigatedEventArgs> Navigated
    {
        add => AddHandler(NavigatedEvent, value);
        remove => RemoveHandler(NavigatedEvent, value);
    }

    /// <summary>Identifies the <see cref="Navigating"/> routed event.</summary>
    public static readonly RoutedEvent NavigatingEvent = EventManager.RegisterRoutedEvent(
        nameof(Navigating),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<NavigationView, NavigatingCancelEventArgs>),
        typeof(NavigationView)
    );

    /// <summary>Identifies the <see cref="Navigated"/> routed event.</summary>
    public static readonly RoutedEvent NavigatedEvent = EventManager.RegisterRoutedEvent(
        nameof(Navigated),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<NavigationView, NavigatedEventArgs>),
        typeof(NavigationView)
    );

    public event TypedEventHandler<NavigationView, RoutedEventArgs> SelectionChanged
    {
        add => AddHandler(SelectionChangedEvent, value);
        remove => RemoveHandler(SelectionChangedEvent, value);
    }

    /// <summary>Identifies the <see cref="SelectionChanged"/> routed event.</summary>
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
    /// Raises the navigating requested event.
    /// </summary>
    protected virtual bool OnNavigating(object sourcePage)
    {
        var eventArgs = new NavigatingCancelEventArgs(NavigatingEvent, this) { Page = sourcePage };

        RaiseEvent(eventArgs);

        return eventArgs.Cancel;
    }

    /// <summary>
    /// Raises the navigated requested event.
    /// </summary>
    protected virtual void OnNavigated(object page)
    {
        var eventArgs = new NavigatedEventArgs(NavigatedEvent, this) { Page = page };

        RaiseEvent(eventArgs);
    }

    private IServiceProvider? _serviceProvider;

    /// <inheritdoc />
    public IPageViewItem? SelectedItem { get; private set; }

    private int _currentIndexInJournal;

    private List<string> Journal { get; } = new(50);

    private readonly NavigationCache _cache = new();

    protected ConcurrentDictionary<Type, IPageViewItem> NavigationPages { get; set; } = [];

    protected Dictionary<string, IPageViewItem> PageIdOrTargetTagNavigationViewsDictionary { get; } = [];

    /// <inheritdoc />
    public bool CanGoBack => Journal.Count > 1 && _currentIndexInJournal >= 0;

    /// <inheritdoc />
    public void SetServiceProvider(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    /// <inheritdoc />
    public bool Navigate(string pageIdOrTargetTag, object? dataContext = null)
    {
        if (
            PageIdOrTargetTagNavigationViewsDictionary.TryGetValue(
                pageIdOrTargetTag,
                out IPageViewItem? navigationViewItem
            )
        )
        {
            return Navigate(navigationViewItem, dataContext);
        }

        return false;
    }

    /// <inheritdoc />
    public bool Navigate(Type PageType, object? dataContext = null)
    {
        if (
            NavigationPages.TryGetValue(
                PageType,
                out IPageViewItem? navigationViewItem
            )
        )
        {
            return Navigate(navigationViewItem, dataContext);
        }

        navigationViewItem = new PageViewItem() { PageType = PageType, PageTag = Guid.NewGuid().ToString(), DataContent = dataContext };

        PageIdOrTargetTagNavigationViewsDictionary.Add(navigationViewItem.PageTag, navigationViewItem);

        _ = NavigationPages.TryAdd(PageType, navigationViewItem);

        return Navigate(navigationViewItem, dataContext);
    }

    /// <inheritdoc />
    public bool Navigate(IPageViewItem viewItem, object? dataContext = null, bool isBackwardsNavigated = false)
    {
        var pageInstance = GetNavigationItemInstance(viewItem);

        if (OnNavigating(pageInstance))
        {
            return false;
        }

        OnNavigated(pageInstance);

        _ = UpdateContent(pageInstance, dataContext);

        AddToJournal(viewItem, isBackwardsNavigated);

        SelectedItem = viewItem;

        OnSelectionChanged();

        return true;
    }

    private bool UpdateContent(object? content, object? dataContext = null)
    {
        if (dataContext is not null && content is FrameworkElement frameworkViewContent)
        {
            frameworkViewContent.DataContext = dataContext;
        }

        return NavigationViewContentPresenter.Navigate(content);
    }

    /// <summary>
    /// Get navigation item instance.
    /// </summary>
    /// <param name="NavViewItem">PageViewItem</param>
    private object GetNavigationItemInstance(IPageViewItem NavViewItem)
    {
        if (NavViewItem.PageType is null)
        {
            throw new InvalidOperationException(
                $"The {nameof(NavViewItem.PageType)} property cannot be null."
            );
        }

        if (_serviceProvider is not null)
        {
            return _serviceProvider.GetService(NavViewItem.PageType)
                ?? throw new InvalidOperationException(
            $"{nameof(_serviceProvider)}.{nameof(_serviceProvider.GetService)} returned null for type {NavViewItem.PageType}."
            );
        }

        return _cache.Remember(
                NavViewItem.PageType,
                NavViewItem.CacheMode,
                ComputeCachedNavigationInstance
            )
            ?? throw new InvalidOperationException(
                $"Unable to get or create instance of {NavViewItem.PageType} from cache."
            );

        object? ComputeCachedNavigationInstance() => GetPageInstanceFromCache(NavViewItem);
    }

    private object? GetPageInstanceFromCache(IPageViewItem NavViewItem)
    {
        if (NavViewItem.PageType is null)
        {
            return default;
        }

        if (_serviceProvider is not null)
        {
            return _serviceProvider.GetService(NavViewItem.PageType)
                ?? throw new InvalidOperationException(
                    $"{nameof(_serviceProvider.GetService)} returned null"
                );
        }

        return InvokeElementConstructor(NavViewItem.PageType, NavViewItem.DataContent)
            ?? throw new InvalidOperationException("Failed to create instance of the page");
    }

    private FrameworkElement? InvokeElementConstructor(Type tPage, object? dataContext)
    {
        ConstructorInfo? ctor = dataContext is null
            ? tPage.GetConstructor(Type.EmptyTypes)
            : tPage.GetConstructor(new[] { dataContext.GetType() });

        var result = ctor?.Invoke(null) as FrameworkElement;

        return result;
    }

    private void AddToJournal(IPageViewItem viewItem, bool isBackwardsNavigated)
    {
        if (isBackwardsNavigated)
        {
            Journal.RemoveAt(Journal.LastIndexOf(Journal[^2]));
            Journal.RemoveAt(Journal.LastIndexOf(Journal[^1]));

            _currentIndexInJournal -= 2;
        }

        Journal.Add(viewItem.PageTag);
        _currentIndexInJournal++;
    }

    /// <inheritdoc />
    public virtual bool GoBack()
    {
        if (Journal.Count <= 1)
        {
            return false;
        }

        var itemId = Journal[^2];

        return Navigate(itemId, null);
    }

    /// <inheritdoc />
    public virtual void ClearJournal()
    {
        _currentIndexInJournal = 0;

        Journal.Clear();
    }

    /// <inheritdoc />
    public virtual bool Refresh()
    {
        if (SelectedItem != null)
        {
            IPageViewItem page = GetNavigationItemInstance(SelectedItem) as IPageViewItem;
            return NavigationViewContentPresenter.Navigate(page);
        }

        return false;
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
