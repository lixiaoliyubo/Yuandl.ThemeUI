// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Windows.Automation;
using System.Windows.Media.Animation;

namespace Yuandl.ThemeUI.Controls;

public partial class Pagination : Control
{
    private int lastSelectedPageIndex = -1;
    private int lastPageCountCount = 0;

    private Button? firstPageButton;
    private Button? previousPageButton;
    private Button? nextPageButton;
    private Button? lastPageButton;
    private ComboBox? comboBox;
    private NumberBox? numberBox;
    private StackPanel? numberPanelRepeater;
    private FrameworkElement? selectedPageIndicator;

    public Pagination()
    {
        DefaultStyleKey = typeof(Pagination);

        Loaded += Pagination_Loaded;
    }

    ~Pagination()
    {
        try
        {
            if (firstPageButton != null)
            {
                firstPageButton.Click -= FirstPageButton_Click;
                firstPageButton = null;
            }

            if (previousPageButton != null)
            {
                previousPageButton.Click -= PreviousPageButton_Click;
                previousPageButton = null;
            }

            if (nextPageButton != null)
            {
                nextPageButton.Click -= NextPageButton_Click;
                nextPageButton = null;
            }

            if (lastPageButton != null)
            {
                lastPageButton.Click -= LastPageButton_Click;
                lastPageButton = null;
            }

            if (comboBox != null)
            {
                comboBox.SelectionChanged -= ComboBox_SelectionChanged;
                comboBox = null;
            }

            if (numberBox != null)
            {
                numberBox.ValueChanged -= NumberBox_ValueChanged;
                numberBox = null;
            }

            numberPanelRepeater = null;
            selectedPageIndicator = null;
        }
        catch (Exception)
        {
        }
    }

    public override void OnApplyTemplate()
    {
        if (string.IsNullOrEmpty(PrefixText))
        {
            SetCurrentValue(PrefixTextProperty, "Page");
        }

        if (string.IsNullOrEmpty(SuffixText))
        {
            SetCurrentValue(SuffixTextProperty, "of");
        }

        if (GetTemplateChild(C_firstPageButtonName) is Button firstPageButtonTemplate)
        {
            firstPageButton = firstPageButtonTemplate;
            firstPageButton.Click += FirstPageButton_Click;
            AutomationProperties.SetName(firstPageButtonTemplate, "First page");
        }

        if (GetTemplateChild(C_previousPageButtonName) is Button previousPageButtonTemplate)
        {
            previousPageButton = previousPageButtonTemplate;
            previousPageButton.Click += PreviousPageButton_Click;
            AutomationProperties.SetName(previousPageButtonTemplate, "Previous page");
        }

        if (GetTemplateChild(C_nextPageButtonName) is Button nextPageButtonTemplate)
        {
            nextPageButton = nextPageButtonTemplate;
            nextPageButton.Click += NextPageButton_Click;
            AutomationProperties.SetName(nextPageButtonTemplate, "Next page");
        }

        if (GetTemplateChild(C_lastPageButtonName) is Button lastPageButtonTemplate)
        {
            lastPageButton = lastPageButtonTemplate;
            lastPageButton.Click += LastPageButton_Click;
            AutomationProperties.SetName(lastPageButtonTemplate, "Last page");
        }

        if (GetTemplateChild(C_comboBoxName) is ComboBox comboBoxTemplate)
        {
            comboBox = comboBoxTemplate;
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
            FillComboBoxCollectionToSize(PageCount);
            comboBox.SetCurrentValue(System.Windows.Controls.Primitives.Selector.SelectedIndexProperty, SelectedPageIndex - 1);
            AutomationProperties.SetName(comboBox, "Page");
        }

        if (GetTemplateChild(C_numberBoxName) is NumberBox numberBoxTemplate)
        {
            numberBox = numberBoxTemplate;
            numberBox.ValueChanged += NumberBox_ValueChanged;
            numberBox.SetCurrentValue(NumberBox.ValueProperty, (double?)SelectedPageIndex + 1);
            AutomationProperties.SetName(numberBox, "Page");
        }

        numberPanelRepeater = GetTemplateChild(C_numberPanelRepeaterName) as StackPanel;
        selectedPageIndicator = GetTemplateChild(C_numberPanelIndicatorName) as FrameworkElement;

        OnDisplayModeChanged();
        UpdateOnEdgeButtonVisualStates();
        OnPageCountChanged(0);

        OnButtonVisibilityChanged(
            FirstButtonVisibility,
            C_firstPageButtonVisibleVisualState,
            C_firstPageButtonNotVisibleVisualState,
            C_firstPageButtonHiddenVisualState,
            0);

        OnButtonVisibilityChanged(
            PreviousButtonVisibility,
            C_previousPageButtonVisibleVisualState,
            C_previousPageButtonNotVisibleVisualState,
            C_previousPageButtonHiddenVisualState,
            0);

        OnButtonVisibilityChanged(
            NextButtonVisibility,
            C_nextPageButtonVisibleVisualState,
            C_nextPageButtonNotVisibleVisualState,
            C_nextPageButtonHiddenVisualState,
            PageCount - 1);

        OnButtonVisibilityChanged(
            LastButtonVisibility,
            C_lastPageButtonVisibleVisualState,
            C_lastPageButtonNotVisibleVisualState,
            C_lastPageButtonHiddenVisualState,
            PageCount - 1);

        OnSelectedPageIndexChange(-1);
    }

    private void OnPropertyChangedEvent(DependencyPropertyChangedEventArgs args)
    {
        DependencyProperty property = args.Property;
        if (Template != null)
        {
            if (property == FirstButtonVisibilityProperty)
            {
                OnButtonVisibilityChanged(
                    FirstButtonVisibility,
                    C_firstPageButtonVisibleVisualState,
                    C_firstPageButtonNotVisibleVisualState,
                    C_firstPageButtonHiddenVisualState,
                    0);
            }
            else if (property == PreviousButtonVisibilityProperty)
            {
                OnButtonVisibilityChanged(
                    PreviousButtonVisibility,
                    C_previousPageButtonVisibleVisualState,
                    C_previousPageButtonNotVisibleVisualState,
                    C_previousPageButtonHiddenVisualState,
                    0);
            }
            else if (property == NextButtonVisibilityProperty)
            {
                OnButtonVisibilityChanged(
                    NextButtonVisibility,
                    C_nextPageButtonVisibleVisualState,
                    C_nextPageButtonNotVisibleVisualState,
                    C_nextPageButtonHiddenVisualState,
                    PageCount - 1);
            }
            else if (property == LastButtonVisibilityProperty)
            {
                OnButtonVisibilityChanged(
                    LastButtonVisibility,
                    C_lastPageButtonVisibleVisualState,
                    C_lastPageButtonNotVisibleVisualState,
                    C_lastPageButtonHiddenVisualState,
                    PageCount - 1);
            }
            else if (property == DisplayModeProperty)
            {
                OnDisplayModeChanged();
                UpdateTemplateSettingElementLists();
            }
            else if (property == PageCountProperty)
            {
                OnPageCountChanged((int)args.OldValue);
            }
            else if (property == SelectedPageIndexProperty)
            {
                OnSelectedPageIndexChange((int)args.OldValue);
            }
            else if (property == ButtonPanelAlwaysShowFirstLastPageIndexProperty)
            {
                UpdateNumberPanel(PageCount);
            }
        }
    }

    private void OnDisplayModeChanged()
    {
        PaginationDisplayMode displayMode = DisplayMode;

        if (displayMode == PaginationDisplayMode.ButtonPanel)
        {
            _ = VisualStateManager.GoToState(this, C_numberPanelVisibleVisualState, false);
        }
        else if (displayMode == PaginationDisplayMode.ComboBox)
        {
            _ = VisualStateManager.GoToState(this, C_comboBoxVisibleVisualState, false);
        }
        else if (displayMode == PaginationDisplayMode.NumberBox)
        {
            _ = VisualStateManager.GoToState(this, C_numberBoxVisibleVisualState, false);
        }
        else
        {
            UpdateDisplayModeAutoState();
        }
    }

    private void UpdateDisplayModeAutoState()
    {
        var pageCount = PageCount;
        if (pageCount > -1)
        {
            _ = VisualStateManager.GoToState(this, pageCount < C_AutoDisplayModePageCountThreshold ?
                C_comboBoxVisibleVisualState : C_numberBoxVisibleVisualState, false);
        }
        else
        {
            _ = VisualStateManager.GoToState(this, C_numberBoxVisibleVisualState, false);
        }
    }

    private void OnPageCountChanged(int oldValue)
    {
        lastPageCountCount = oldValue;
        var pageCount = PageCount;
        if (pageCount < SelectedPageIndex && pageCount > -1)
        {
            SetCurrentValue(SelectedPageIndexProperty, pageCount - 1);
        }

        UpdateTemplateSettingElementLists();
        if (DisplayMode == PaginationDisplayMode.Auto)
        {
            UpdateDisplayModeAutoState();
        }

        if (pageCount > -1)
        {
            _ = VisualStateManager.GoToState(this, C_finiteItemsModeState, false);
            if (numberBox != null)
            {
                numberBox.Maximum = pageCount;
            }
        }
        else
        {
            _ = VisualStateManager.GoToState(this, C_infiniteItemsModeState, false);
            if (numberBox != null)
            {
                numberBox.Maximum = double.PositiveInfinity;
            }
        }

        UpdateOnEdgeButtonVisualStates();
    }

    private void OnSelectedPageIndexChange(int oldValue)
    {
        if (SelectedPageIndex > PageCount - 1 && PageCount > 0)
        {
            SetCurrentValue(SelectedPageIndexProperty, PageCount - 1);
        }
        else if (SelectedPageIndex < 0)
        {
            SetCurrentValue(SelectedPageIndexProperty, 0);
        }

        lastSelectedPageIndex = oldValue;

        if (comboBox != null)
        {
            if (SelectedPageIndex < ComboBoxPage.Count)
            {
                comboBox.SetCurrentValue(System.Windows.Controls.Primitives.Selector.SelectedIndexProperty, SelectedPageIndex);
            }
        }

        if (numberBox != null)
        {
            numberBox.SetCurrentValue(NumberBox.ValueProperty, (double?)SelectedPageIndex + 1);
        }

        UpdateOnEdgeButtonVisualStates();
        UpdateTemplateSettingElementLists();

        if (DisplayMode == PaginationDisplayMode.ButtonPanel)
        {
            UpdateNumberPanel(PageCount);
        }

        UpdateSelectedIndexChanged();
    }

    /// <summary>
    /// 更新当前页
    /// </summary>
    private void UpdateSelectedIndexChanged()
    {
        var args = new PaginationSelectedIndexChangedEventArgs(SelectedIndexChangedEvent, this);
        args.NewPageIndex = SelectedPageIndex;
        args.PreviousPageIndex = SelectedPageIndex;
        RaiseEvent(args);
    }

    private void OnButtonVisibilityChanged(PaginationButtonVisibility visibility, string visibleStateName, string collapsedStateName, string hiddenStateName, int hiddenOnEdgePageCriteria)
    {
        if (visibility == PaginationButtonVisibility.Visible)
        {
            _ = VisualStateManager.GoToState(this, visibleStateName, false);
        }
        else if (visibility == PaginationButtonVisibility.Hidden)
        {
            _ = VisualStateManager.GoToState(this, collapsedStateName, false);
        }
        else
        {
            if (SelectedPageIndex != hiddenOnEdgePageCriteria)
            {
                _ = VisualStateManager.GoToState(this, visibleStateName, false);
            }
            else
            {
                _ = VisualStateManager.GoToState(this, hiddenStateName, false);
            }
        }
    }

    private void UpdateTemplateSettingElementLists()
    {
        PaginationDisplayMode displayMode = DisplayMode;
        var pageCount = PageCount;

        if (displayMode == PaginationDisplayMode.ComboBox || displayMode == PaginationDisplayMode.Auto)
        {
            if (pageCount > -1)
            {
                FillComboBoxCollectionToSize(pageCount);
            }
            else
            {
                if (ComboBoxPage.Count < C_infiniteModeComboBoxItemsIncrement)
                {
                    FillComboBoxCollectionToSize(C_infiniteModeComboBoxItemsIncrement);
                }
            }
        }
        else if (displayMode == PaginationDisplayMode.ButtonPanel)
        {
            UpdateNumberPanel(pageCount);
        }
    }

    private void FillComboBoxCollectionToSize(int pageCount)
    {
        var currentComboBoxItemsCount = ComboBoxPage.Count;
        if (currentComboBoxItemsCount <= pageCount)
        {
            for (int i = currentComboBoxItemsCount; i < pageCount; i++)
            {
                ComboBoxPage.Add(i + 1);
            }
        }
        else
        {
            for (int i = currentComboBoxItemsCount; i > pageCount; i--)
            {
                ComboBoxPage.RemoveAt(ComboBoxPage.Count - 1);
            }
        }
    }

    private void UpdateOnEdgeButtonVisualStates()
    {
        var selectedPageIndex = SelectedPageIndex;
        var pageCount = PageCount;

        if (selectedPageIndex == 0)
        {
            _ = VisualStateManager.GoToState(this, C_firstPageButtonDisabledVisualState, false);
            _ = VisualStateManager.GoToState(this, C_previousPageButtonDisabledVisualState, false);
            _ = VisualStateManager.GoToState(this, C_nextPageButtonEnabledVisualState, false);
            _ = VisualStateManager.GoToState(this, C_lastPageButtonEnabledVisualState, false);
        }
        else if (selectedPageIndex >= pageCount - 1)
        {
            _ = VisualStateManager.GoToState(this, C_firstPageButtonEnabledVisualState, false);
            _ = VisualStateManager.GoToState(this, C_previousPageButtonEnabledVisualState, false);
            if (pageCount > -1)
            {
                _ = VisualStateManager.GoToState(this, C_nextPageButtonDisabledVisualState, false);
            }
            else
            {
                _ = VisualStateManager.GoToState(this, C_nextPageButtonEnabledVisualState, false);
            }

            _ = VisualStateManager.GoToState(this, C_lastPageButtonDisabledVisualState, false);
        }
        else
        {
            _ = VisualStateManager.GoToState(this, C_firstPageButtonEnabledVisualState, false);
            _ = VisualStateManager.GoToState(this, C_previousPageButtonEnabledVisualState, false);
            _ = VisualStateManager.GoToState(this, C_nextPageButtonEnabledVisualState, false);
            _ = VisualStateManager.GoToState(this, C_lastPageButtonEnabledVisualState, false);
        }

        if (FirstButtonVisibility == PaginationButtonVisibility.HiddenOnEdge)
        {
            if (selectedPageIndex != 0)
            {
                _ = VisualStateManager.GoToState(this, C_firstPageButtonVisibleVisualState, false);
            }
            else
            {
                _ = VisualStateManager.GoToState(this, C_firstPageButtonHiddenVisualState, false);
            }
        }

        if (PreviousButtonVisibility == PaginationButtonVisibility.HiddenOnEdge)
        {
            if (selectedPageIndex != 0)
            {
                _ = VisualStateManager.GoToState(this, C_previousPageButtonVisibleVisualState, false);
            }
            else
            {
                _ = VisualStateManager.GoToState(this, C_previousPageButtonHiddenVisualState, false);
            }
        }

        if (NextButtonVisibility == PaginationButtonVisibility.HiddenOnEdge)
        {
            if (selectedPageIndex != pageCount - 1)
            {
                _ = VisualStateManager.GoToState(this, C_nextPageButtonVisibleVisualState, false);
            }
            else
            {
                _ = VisualStateManager.GoToState(this, C_nextPageButtonHiddenVisualState, false);
            }
        }

        if (LastButtonVisibility == PaginationButtonVisibility.HiddenOnEdge)
        {
            if (selectedPageIndex != pageCount - 1)
            {
                _ = VisualStateManager.GoToState(this, C_lastPageButtonVisibleVisualState, false);
            }
            else
            {
                _ = VisualStateManager.GoToState(this, C_lastPageButtonHiddenVisualState, false);
            }
        }
    }

    private void UpdateNumberPanel(int pageCount)
    {
        if (pageCount < 0)
        {
            UpdateNumberOfPanelCollectionInfiniteItems();
        }
        else if (pageCount < 8)
        {
            UpdateNumberPanelCollectionAllItems(pageCount);
        }
        else
        {
            var selectedIndex = SelectedPageIndex;

            // Idea: Choose correct "template" based on SelectedPageIndex (x) and PageCount (n)
            // 1 2 3 4 5 6 ... n <-- Items
            if (selectedIndex < 4)
            {
                // First four items selected, create following pattern:
                // 1 2 3 4 5... n
                UpdateNumberPanelCollectionStartWithEllipsis(pageCount, selectedIndex);
            }
            else if (selectedIndex >= pageCount - 4)
            {
                // Last four items selected, create following pattern:
                // 1 [...] n-4 n-3 n-2 n-1 n
                UpdateNumberPanelCollectionEndWithEllipsis(pageCount, selectedIndex);
            }
            else
            {
                // Neither start or end, so lets do this:
                // 1 [...] x-2 x-1 x x+1 x+2 [...] n
                // where x > 4 and x < n - 4
                UpdateNumberPanelCollectionCenterWithEllipsis(pageCount, selectedIndex);
            }
        }
    }

    private void UpdateNumberOfPanelCollectionInfiniteItems()
    {
        var selectedIndex = SelectedPageIndex;

        numberPanelRepeater?.Children.Clear();
        if (selectedIndex < 3)
        {
            AppendButtonToNumberPanelList(1, 0);
            AppendButtonToNumberPanelList(2, 0);
            AppendButtonToNumberPanelList(3, 0);
            AppendButtonToNumberPanelList(4, 0);
            AppendButtonToNumberPanelList(5, 0);
            MoveIdentifierToElement(selectedIndex);
        }
        else
        {
            AppendButtonToNumberPanelList(1, 0);
            AppendEllipsisIconToNumberPanelList();
            AppendButtonToNumberPanelList(selectedIndex, 0);
            AppendButtonToNumberPanelList(selectedIndex + 1, 0);
            AppendButtonToNumberPanelList(selectedIndex + 2, 0);
            MoveIdentifierToElement(3);
        }
    }

    private void UpdateNumberPanelCollectionAllItems(int pageCount)
    {
        if (lastPageCountCount != pageCount)
        {
            numberPanelRepeater?.Children.Clear();
            for (int i = 0; i < pageCount && i < 7; i++)
            {
                AppendButtonToNumberPanelList(i + 1, pageCount);
            }
        }

        MoveIdentifierToElement(SelectedPageIndex);
    }

    private void UpdateNumberPanelCollectionStartWithEllipsis(int pageCount, int selectedIndex)
    {
        if (lastPageCountCount != pageCount)
        {
            numberPanelRepeater?.Children.Clear();
            AppendButtonToNumberPanelList(1, pageCount);
            AppendButtonToNumberPanelList(2, pageCount);
            AppendButtonToNumberPanelList(3, pageCount);
            AppendButtonToNumberPanelList(4, pageCount);
            AppendButtonToNumberPanelList(5, pageCount);
            if (ButtonPanelAlwaysShowFirstLastPageIndex)
            {
                AppendEllipsisIconToNumberPanelList();
                AppendButtonToNumberPanelList(pageCount, pageCount);
            }
        }

        MoveIdentifierToElement(selectedIndex);
    }

    private void UpdateNumberPanelCollectionEndWithEllipsis(int pageCount, int selectedIndex)
    {
        if (lastPageCountCount != pageCount)
        {
            numberPanelRepeater?.Children.Clear();
            if (ButtonPanelAlwaysShowFirstLastPageIndex)
            {
                AppendButtonToNumberPanelList(1, pageCount);
                AppendEllipsisIconToNumberPanelList();
            }

            AppendButtonToNumberPanelList(pageCount - 4, pageCount);
            AppendButtonToNumberPanelList(pageCount - 3, pageCount);
            AppendButtonToNumberPanelList(pageCount - 2, pageCount);
            AppendButtonToNumberPanelList(pageCount - 1, pageCount);
            AppendButtonToNumberPanelList(pageCount, pageCount);
        }

        // We can only be either the last, the second from last or third from last
        // => SelectedIndex = PageCount - y with y in {1,2,3}
        if (ButtonPanelAlwaysShowFirstLastPageIndex)
        {
            // Last item sits at index 4.
            // SelectedPageIndex for the last page is PageCount - 1.
            // => SelectedItem = SelectedIndex - PageCount + 7;
            MoveIdentifierToElement(selectedIndex - pageCount + 7);
        }
        else
        {
            // Last item sits at index 6.
            // SelectedPageIndex for the last page is PageCount - 1.
            // => SelectedItem = SelectedIndex - PageCount + 5;
            MoveIdentifierToElement(selectedIndex - pageCount + 5);
        }
    }

    private void UpdateNumberPanelCollectionCenterWithEllipsis(int pageCount, int selectedIndex)
    {
        var showFirstLastPageIndex = ButtonPanelAlwaysShowFirstLastPageIndex;
        if (lastPageCountCount != pageCount)
        {
            numberPanelRepeater?.Children.Clear();
            if (showFirstLastPageIndex)
            {
                AppendButtonToNumberPanelList(1, pageCount);
                AppendEllipsisIconToNumberPanelList();
            }

            AppendButtonToNumberPanelList(selectedIndex, pageCount);
            AppendButtonToNumberPanelList(selectedIndex + 1, pageCount);
            AppendButtonToNumberPanelList(selectedIndex + 2, pageCount);
            if (showFirstLastPageIndex)
            {
                AppendEllipsisIconToNumberPanelList();
                AppendButtonToNumberPanelList(pageCount, pageCount);
            }
        }

        // "selectedIndex + 1" is our representation for SelectedIndex.
        if (showFirstLastPageIndex)
        {
            // SelectedIndex + 1 is the fifth element.
            // Collections are base zero, so the index to underline is 3.
            MoveIdentifierToElement(3);
        }
        else
        {
            // SelectedIndex + 1 is the third element.
            // Collections are base zero, so the index to underline is 1.
            MoveIdentifierToElement(1);
        }
    }

    private double x = 0;

    private void MoveIdentifierToElement(int index)
    {
        if (selectedPageIndicator != null && numberPanelRepeater != null)
        {
            numberPanelRepeater.UpdateLayout();

            if (numberPanelRepeater?.Children[index] is FrameworkElement element)
            {
                if (numberPanelRepeater.ActualWidth > 0)
                {
                    Rect boundingRect = element.TransformToVisual(numberPanelRepeater).TransformBounds(new Rect(0, 0, (float)numberPanelRepeater.ActualWidth, (float)numberPanelRepeater.ActualHeight));

                    var transform = new TranslateTransform { };

                    DoubleAnimation xAnimation = new DoubleAnimation
                    {
                        From = x,
                        To = boundingRect.X,
                        Duration = new Duration(TimeSpan.FromSeconds(0.2)),
                        AutoReverse = false
                    };
                    selectedPageIndicator.SetCurrentValue(RenderTransformProperty, transform);
                    transform.BeginAnimation(TranslateTransform.XProperty, xAnimation);
                    selectedPageIndicator.SetCurrentValue(WidthProperty, element.ActualWidth);
                    x = boundingRect.X;
                }
                else
                {
                    selectedPageIndicator.SetCurrentValue(WidthProperty, 40.00);
                }
            }
        }
    }

    private void AppendButtonToNumberPanelList(int pageNumber, int pageCount)
    {
        var button = new System.Windows.Controls.Button()
        {
            Width = 40,
            Padding = new Thickness(5, 5, 5, 5),
            Margin = new Thickness(2.5, 0, 2.5, 0),
            Content = pageNumber,

            // CornerRadius = new CornerRadius(0,0,0,0)
        };
        button.Click += (object sender, RoutedEventArgs args) =>
        {
            var unboxedValue = button.Content as int?;
            if (unboxedValue != null)
            {
                SetCurrentValue(SelectedPageIndexProperty, unboxedValue.Value - 1);
            }
        };

        // Set the default style of buttons
        if (ResourceLookup(C_numberPanelButtonStyleName) is Style style)
        {
            // button.Style = style;
        }

        AutomationProperties.SetName(button, "Page" + pageNumber);

        // AutomationProperties.SetPositionInSet(button, pageNumber);
        // AutomationProperties.SetSizeOfSet(button, pageCount);
        _ = numberPanelRepeater?.Children.Add(button);
    }

    private void AppendEllipsisIconToNumberPanelList()
    {
        var ellipsisIcon = new SymbolIcon()
        {
            Margin = new Thickness(0, 0, 5, 0),
            Symbol = Enums.SymbolRegular.MoreHorizontal24
        };
        _ = numberPanelRepeater?.Children.Add(ellipsisIcon);
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (comboBox != null)
        {
            SetCurrentValue(SelectedPageIndexProperty, comboBox.SelectedIndex);
        }
    }

    private void NumberBox_ValueChanged(object sender, RoutedEventArgs e)
    {
        NumberBox numberBox = sender as NumberBox;
        SetCurrentValue(SelectedPageIndexProperty, (int)(numberBox.Value - 1));
    }

    private void FirstPageButton_Click(object sender, RoutedEventArgs e)
    {
        SetCurrentValue(SelectedPageIndexProperty, 0);
        if (FirstButtonCommand != null)
        {
            FirstButtonCommand.Execute(null);
        }
    }

    private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
    {
        // In this method, SelectedPageIndex is always greater than 1.
        SetCurrentValue(SelectedPageIndexProperty, SelectedPageIndex - 1);
        if (PreviousButtonCommand != null)
        {
            PreviousButtonCommand.Execute(null);
        }
    }

    private void NextPageButton_Click(object sender, RoutedEventArgs e)
    {
        // In this method, SelectedPageIndex is always less than maximum.
        SetCurrentValue(SelectedPageIndexProperty, SelectedPageIndex + 1);
        if (NextButtonCommand != null)
        {
            NextButtonCommand.Execute(null);
        }
    }

    private void LastPageButton_Click(object sender, RoutedEventArgs e)
    {
        SetCurrentValue(SelectedPageIndexProperty, PageCount - 1);
        if (LastButtonCommand != null)
        {
            LastButtonCommand.Execute(null);
        }
    }

    private void Pagination_Loaded(object sender, RoutedEventArgs e)
    {
        // Needed so we can update the UI and selection indicator once we have actually rendered
        OnSelectedPageIndexChange(-1);
    }

    public object? ResourceLookup(string name)
    {
        if (Application.Current.Resources.Contains(name))
        {
            return Application.Current.Resources[name];
        }

        return null;
    }
}