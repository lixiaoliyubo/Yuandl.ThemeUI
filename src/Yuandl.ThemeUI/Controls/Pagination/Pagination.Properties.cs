// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Input;

namespace Yuandl.ThemeUI.Controls;

public partial class Pagination
{
    public PaginationDisplayMode DisplayMode
    {
        get { return (PaginationDisplayMode)GetValue(DisplayModeProperty); }
        set { SetValue(DisplayModeProperty, value); }
    }

    public static readonly DependencyProperty DisplayModeProperty =
        DependencyProperty.Register(nameof(DisplayMode), typeof(PaginationDisplayMode), typeof(Pagination), new PropertyMetadata(PaginationDisplayMode.Auto, OnPropertyChanged));

    public int PageCount
    {
        get { return (int)GetValue(PageCountProperty); }
        set { SetValue(PageCountProperty, value); }
    }

    public static readonly DependencyProperty PageCountProperty =
        DependencyProperty.Register(nameof(PageCount), typeof(int), typeof(Pagination), new PropertyMetadata(0, OnPropertyChanged));

    public int SelectedPageIndex
    {
        get { return (int)GetValue(SelectedPageIndexProperty); }
        set { SetValue(SelectedPageIndexProperty, value); }
    }

    public static readonly DependencyProperty SelectedPageIndexProperty =
        DependencyProperty.Register(nameof(SelectedPageIndex), typeof(int), typeof(Pagination), new PropertyMetadata(0, OnPropertyChanged));

    public bool ButtonPanelAlwaysShowFirstLastPageIndex
    {
        get { return (bool)GetValue(ButtonPanelAlwaysShowFirstLastPageIndexProperty); }
        set { SetValue(ButtonPanelAlwaysShowFirstLastPageIndexProperty, value); }
    }

    public static readonly DependencyProperty ButtonPanelAlwaysShowFirstLastPageIndexProperty =
        DependencyProperty.Register(nameof(ButtonPanelAlwaysShowFirstLastPageIndex), typeof(bool), typeof(Pagination), new PropertyMetadata(true, OnPropertyChanged));

    /// <summary>Identifies the <see cref="SelectedIndexChangedEvent"/> routed event.</summary>
    public static readonly RoutedEvent SelectedIndexChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(SelectedIndexChanged),
        RoutingStrategy.Bubble,
        typeof(TypedEventHandler<Pagination, PaginationSelectedIndexChangedEventArgs>),
        typeof(Pagination)
    );


    /// <summary>
    /// Event occurs when the user selects an item from the recommended ones.
    /// </summary>
    public event TypedEventHandler<Pagination, PaginationSelectedIndexChangedEventArgs> SelectedIndexChanged
    {
        add => AddHandler(SelectedIndexChangedEvent, value);
        remove => RemoveHandler(SelectedIndexChangedEvent, value);
    }

    public PaginationButtonVisibility FirstButtonVisibility
    {
        get { return (PaginationButtonVisibility)GetValue(FirstButtonVisibilityProperty); }
        set { SetValue(FirstButtonVisibilityProperty, value); }
    }

    public static readonly DependencyProperty FirstButtonVisibilityProperty =
        DependencyProperty.Register(nameof(FirstButtonVisibility), typeof(PaginationButtonVisibility), typeof(Pagination), new PropertyMetadata(PaginationButtonVisibility.Visible, OnPropertyChanged));

    public PaginationButtonVisibility PreviousButtonVisibility
    {
        get { return (PaginationButtonVisibility)GetValue(PreviousButtonVisibilityProperty); }
        set { SetValue(PreviousButtonVisibilityProperty, value); }
    }

    public static readonly DependencyProperty PreviousButtonVisibilityProperty =
        DependencyProperty.Register(nameof(PreviousButtonVisibility), typeof(PaginationButtonVisibility), typeof(Pagination), new PropertyMetadata(PaginationButtonVisibility.Visible, OnPropertyChanged));

    public PaginationButtonVisibility NextButtonVisibility
    {
        get { return (PaginationButtonVisibility)GetValue(NextButtonVisibilityProperty); }
        set { SetValue(NextButtonVisibilityProperty, value); }
    }

    public static readonly DependencyProperty NextButtonVisibilityProperty =
        DependencyProperty.Register(nameof(NextButtonVisibility), typeof(PaginationButtonVisibility), typeof(Pagination), new PropertyMetadata(PaginationButtonVisibility.Visible, OnPropertyChanged));

    public PaginationButtonVisibility LastButtonVisibility
    {
        get { return (PaginationButtonVisibility)GetValue(LastButtonVisibilityProperty); }
        set { SetValue(LastButtonVisibilityProperty, value); }
    }

    public static readonly DependencyProperty LastButtonVisibilityProperty =
        DependencyProperty.Register(nameof(LastButtonVisibility), typeof(PaginationButtonVisibility), typeof(Pagination), new PropertyMetadata(PaginationButtonVisibility.Visible, OnPropertyChanged));

    public ICommand FirstButtonCommand
    {
        get { return (ICommand)GetValue(FirstButtonCommandProperty); }
        set { SetValue(FirstButtonCommandProperty, value); }
    }

    public static readonly DependencyProperty FirstButtonCommandProperty = DependencyProperty.Register(nameof(FirstButtonCommand), typeof(ICommand), typeof(Pagination), new PropertyMetadata(null));

    public ICommand PreviousButtonCommand
    {
        get { return (ICommand)GetValue(PreviousButtonCommandProperty); }
        set { SetValue(PreviousButtonCommandProperty, value); }
    }

    public static readonly DependencyProperty PreviousButtonCommandProperty = DependencyProperty.Register(nameof(PreviousButtonCommand), typeof(ICommand), typeof(Pagination), new PropertyMetadata(null));

    public ICommand NextButtonCommand
    {
        get { return (ICommand)GetValue(NextButtonCommandProperty); }
        set { SetValue(NextButtonCommandProperty, value); }
    }

    public static readonly DependencyProperty NextButtonCommandProperty = DependencyProperty.Register(nameof(NextButtonCommand), typeof(ICommand), typeof(Pagination), new PropertyMetadata(null));

    public ICommand LastButtonCommand
    {
        get { return (ICommand)GetValue(LastButtonCommandProperty); }
        set { SetValue(LastButtonCommandProperty, value); }
    }

    public static readonly DependencyProperty LastButtonCommandProperty = DependencyProperty.Register(nameof(LastButtonCommand), typeof(ICommand), typeof(Pagination), new PropertyMetadata(null));

    public string PrefixText
    {
        get { return (string)GetValue(PrefixTextProperty); }
        set { SetValue(PrefixTextProperty, value); }
    }

    public static readonly DependencyProperty PrefixTextProperty =
        DependencyProperty.Register(nameof(PrefixText), typeof(string), typeof(Pagination), new PropertyMetadata(null));

    public string SuffixText
    {
        get { return (string)GetValue(SuffixTextProperty); }
        set { SetValue(SuffixTextProperty, value); }
    }

    public static readonly DependencyProperty SuffixTextProperty =
        DependencyProperty.Register(nameof(SuffixText), typeof(string), typeof(Pagination), new PropertyMetadata(null));

    public IList<object> ComboBoxPage
    {
        get { return (IList<object>)GetValue(ComboBoxPageProperty); }
        set { SetValue(ComboBoxPageProperty, value); }
    }

    public static readonly DependencyProperty ComboBoxPageProperty =
        DependencyProperty.Register("Pages", typeof(IList<object>), typeof(Pagination), new PropertyMetadata(new List<object>()));

    private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is Pagination pagerControl)
        {
            pagerControl.OnPropertyChangedEvent(args);
        }
    }
}