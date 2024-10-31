// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// The UniformGrid control presents information within a Grid with even spacing.
/// </summary>
public partial class UniformGrid : Grid
{
    // Internal list we use to keep track of items that we don't have space to layout.
    private List<UIElement> _overflow = new List<UIElement>();

    /// <summary>
    /// The <see cref="TakenSpotsReferenceHolder"/> instance in use, if any.
    /// </summary>
    private TakenSpotsReferenceHolder? _spotref;

    /// <inheritdoc/>
    protected override Size MeasureOverride(Size availableSize)
    {
        // Get all Visible FrameworkElement Children
        FrameworkElement?[] visible = Children.ConvertUIElementCollectionToList().Where(item => item.Visibility != Visibility.Collapsed && item is FrameworkElement).Select(item => item as FrameworkElement).ToArray();
        (int rows, int columns) = GetDimensions(visible!, Rows, Columns, FirstColumn);

        // Now that we know size, setup automatic rows/columns
        // to utilize Grid for UniformGrid behavior.
        // We also interleave any specified rows/columns with fixed sizes.
        SetupRowDefinitions(rows);
        SetupColumnDefinitions(columns);

        TakenSpotsReferenceHolder spotref;

        // If the last spot holder matches the size currently in use, just reset
        // that instance and reuse it to avoid allocating a new bit array.
        if (_spotref != null && _spotref.Height == rows && _spotref.Width == columns)
        {
            spotref = _spotref;

            spotref.Reset();
        }
        else
        {
            spotref = _spotref = new TakenSpotsReferenceHolder(rows, columns);
        }

        // Figure out which children we should automatically layout and where available openings are.
        foreach (FrameworkElement? child in visible)
        {
            if (child != null)
            {
                var row = GetRow(child);
                var col = GetColumn(child);
                var rowspan = GetRowSpan(child);
                var colspan = GetColumnSpan(child);

                // If an element needs to be forced in the 0, 0 position,
                // they should manually set UniformGrid.AutoLayout to False for that element.
                if ((row == 0 && col == 0 && GetAutoLayout(child) == null) ||
                    GetAutoLayout(child) == true)
                {
                    SetAutoLayout(child, true);
                }
                else
                {
                    SetAutoLayout(child, false);

                    spotref.Fill(true, row, col, colspan, rowspan);
                }
            }
        }

        // Setup available size with our known dimensions now.
        // UniformGrid expands size based on largest singular item.
        double columnSpacingSize = 0;
        double rowSpacingSize = 0;

        columnSpacingSize = ColumnSpacing * (columns - 1);
        rowSpacingSize = RowSpacing * (rows - 1);

        Size childSize = new Size(
            (availableSize.Width - columnSpacingSize) / columns,
            (availableSize.Height - rowSpacingSize) / rows);

        double maxWidth = 0.0;
        double maxHeight = 0.0;

        // Set Grid Row/Col for every child with autolayout = true
        // Backwards with FlowDirection
        IEnumerator<(int row, int column)> freespots = GetFreeSpot(spotref, FirstColumn, Orientation == Orientation.Vertical).GetEnumerator();
        foreach (FrameworkElement? child in visible)
        {
            if (child != null)
            {
                // Set location if we're in charge
                if (GetAutoLayout(child) == true)
                {
                    if (freespots.MoveNext())
                    {
                        (int row, int column) = freespots.Current;

                        SetRow(child, row);
                        SetColumn(child, column);

                        var rowspan = GetRowSpan(child);
                        var colspan = GetColumnSpan(child);

                        if (rowspan > 1 || colspan > 1)
                        {
                            // TODO: Need to tie this into iterator
                            spotref.Fill(true, row, column, colspan, rowspan);
                        }
                    }
                    else
                    {
                        // We've run out of spots as the developer has
                        // most likely given us a fixed size and too many elements
                        // Therefore, tell this element it has no size and move on.
                        child.Measure(Size.Empty);

                        _overflow.Add(child);

                        continue;
                    }
                }
                else if (GetRow(child) < 0 || GetRow(child) >= rows ||
                         GetColumn(child) < 0 || GetColumn(child) >= columns)
                {
                    // A child is specifying a location, but that location is outside
                    // of our grid space, so we should hide it instead.
                    child.Measure(Size.Empty);

                    _overflow.Add(child);

                    continue;
                }

                // Get measurement for max child
                child.Measure(childSize);

                maxWidth = Math.Max(child.DesiredSize.Width, maxWidth);
                maxHeight = Math.Max(child.DesiredSize.Height, maxHeight);
            }
        }

        // Return our desired size based on the largest child we found, our dimensions, and spacing.
        var desiredSize = new Size((maxWidth * columns) + columnSpacingSize, (maxHeight * rows) + rowSpacingSize);

        // Required to perform regular grid measurement, but ignore result.
        _ = base.MeasureOverride(desiredSize);

        return desiredSize;
    }

    /// <inheritdoc/>
    protected override Size ArrangeOverride(Size finalSize)
    {
        // Have grid to the bulk of our heavy lifting.
        Size size = base.ArrangeOverride(finalSize);

        // Make sure all overflown elements have no size.
        foreach (UIElement child in _overflow)
        {
            child.Arrange(default);
        }

        _overflow = new List<UIElement>(); // Reset for next time.

        return size;
    }


    // Provides the next spot in the boolean array with a 'false' value.
    internal static IEnumerable<(int row, int column)> GetFreeSpot(TakenSpotsReferenceHolder arrayref, int firstcolumn, bool topdown)
    {
        if (topdown)
        {
            var rows = arrayref.Height;

            // Layout spots from Top-Bottom, Left-Right (right-left handled automatically by Grid with Flow-Direction).
            // Effectively transpose the Grid Layout.
            for (int c = 0; c < arrayref.Width; c++)
            {
                int start = (c == 0 && firstcolumn > 0 && firstcolumn < rows) ? firstcolumn : 0;
                for (int r = start; r < rows; r++)
                {
                    if (!arrayref[r, c])
                    {
                        yield return (r, c);
                    }
                }
            }
        }
        else
        {
            var columns = arrayref.Width;

            // Layout spots as normal from Left-Right.
            // (right-left handled automatically by Grid with Flow-Direction
            // during its layout, internal model is always left-right).
            for (int r = 0; r < arrayref.Height; r++)
            {
                int start = (r == 0 && firstcolumn > 0 && firstcolumn < columns) ? firstcolumn : 0;
                for (int c = start; c < columns; c++)
                {
                    if (!arrayref[r, c])
                    {
                        yield return (r, c);
                    }
                }
            }
        }
    }

    // Based on the number of visible children,
    // returns the dimensions of the
    // grid we need to hold all elements.
#pragma warning disable SA1008 // Opening parenthesis must be spaced correctly
    internal static (int rows, int columns) GetDimensions(FrameworkElement[] visible, int rows, int cols, int firstColumn)
#pragma warning restore SA1008 // Opening parenthesis must be spaced correctly
    {
        // If a dimension isn't specified, we need to figure out the other one (or both).
        if (rows == 0 || cols == 0)
        {
            // Calculate the size & area of all objects in the grid to know how much space we need.
            var count = Math.Max(1, visible.Sum(item => GetRowSpan(item) * GetColumnSpan(item)));

            if (rows == 0)
            {
                if (cols > 0)
                {
                    // Bound check
                    var first = (firstColumn >= cols || firstColumn < 0) ? 0 : firstColumn;

                    // If we have columns but no rows, calculate rows based on column offset and number of children.
                    rows = (count + first + (cols - 1)) / cols;
                    return (rows, cols);
                }
                else
                {
                    // Otherwise, determine square layout if both are zero.
                    var size = (int)Math.Ceiling(Math.Sqrt(count));

                    // Figure out if firstColumn is in bounds
                    var first = (firstColumn >= size || firstColumn < 0) ? 0 : firstColumn;

                    rows = (int)Math.Ceiling(Math.Sqrt(count + first));
                    return (rows, rows);
                }
            }
            else if (cols == 0)
            {
                // If we have rows and no columns, then calculate columns needed based on rows
                cols = (count + (rows - 1)) / rows;

                // Now that we know a rough size of our shape, see if the FirstColumn effects that:
                var first = (firstColumn >= cols || firstColumn < 0) ? 0 : firstColumn;

                cols = (count + first + (rows - 1)) / rows;
            }
        }

        return (rows, cols);
    }

    // Used to interleave specified row dimensions with automatic rows added to use
    // underlying Grid layout for main arrange of UniformGrid.
    internal void SetupRowDefinitions(int rows)
    {
        // Mark initial definitions so we don't erase them.
        foreach (RowDefinition? rd in RowDefinitions)
        {
            if (GetAutoLayout(rd) == null)
            {
                SetAutoLayout(rd, false);
            }
        }

        // Remove non-autolayout rows we've added and then add them in the right spots.
        if (rows != RowDefinitions.Count)
        {
            for (int r = RowDefinitions.Count - 1; r >= 0; r--)
            {
                if (GetAutoLayout(RowDefinitions[r]) == true)
                {
                    RowDefinitions.RemoveAt(r);
                }
            }

            for (int r = this.RowDefinitions.Count; r < rows; r++)
            {
                var rd = new RowDefinition();
                SetAutoLayout(rd, true);
                this.RowDefinitions.Insert(r, rd);
            }
        }
    }

    // Used to interleave specified column dimensions with automatic columns added to use
    // underlying Grid layout for main arrange of UniformGrid.
    internal void SetupColumnDefinitions(int columns)
    {
        // Mark initial definitions so we don't erase them.
        foreach (ColumnDefinition? cd in ColumnDefinitions)
        {
            if (GetAutoLayout(cd) == null)
            {
                SetAutoLayout(cd, false);
            }
        }

        // Remove non-autolayout columns we've added and then add them in the right spots.
        if (columns != ColumnDefinitions.Count)
        {
            for (int c = ColumnDefinitions.Count - 1; c >= 0; c--)
            {
                if (GetAutoLayout(ColumnDefinitions[c]) == true)
                {
                    this.ColumnDefinitions.RemoveAt(c);
                }
            }

            for (int c = ColumnDefinitions.Count; c < columns; c++)
            {
                var cd = new ColumnDefinition();
                SetAutoLayout(cd, true);
                ColumnDefinitions.Insert(c, cd);
            }
        }
    }


    /// <summary>
    /// Determines if this element in the grid participates in the auto-layout algorithm.
    /// </summary>
    public static readonly DependencyProperty AutoLayoutProperty =
        DependencyProperty.RegisterAttached(
          "AutoLayout",
          typeof(bool?),
          typeof(UniformGrid),
          new PropertyMetadata(null));

    /// <summary>
    /// Sets the AutoLayout Attached Property Value. Used internally to track items which need to be arranged vs. fixed in place.
    /// Though it its required to use this property to force an element to the 0, 0 position.
    /// </summary>
    /// <param name="element"><see cref="FrameworkElement"/></param>
    /// <param name="value">A true value indicates this item should be automatically arranged.</param>
    public static void SetAutoLayout(FrameworkElement element, bool? value)
    {
        element.SetValue(AutoLayoutProperty, value);
    }

    /// <summary>
    /// Gets the AutoLayout Attached Property Value. Used internally to track items which need to be arranged vs. fixed in place.
    /// </summary>
    /// <param name="element"><see cref="FrameworkElement"/></param>
    /// <returns>A true value indicates this item should be automatically arranged.</returns>
    public static bool? GetAutoLayout(FrameworkElement element)
    {
        return (bool?)element.GetValue(AutoLayoutProperty);
    }

    /// <summary>
    /// Sets the AutoLayout Attached Property Value. Used internally to track items which need to be arranged vs. fixed in place.
    /// </summary>
    /// <param name="element"><see cref="ColumnDefinition"/></param>
    /// <param name="value">A true value indicates this item should be automatically arranged.</param>
    internal static void SetAutoLayout(ColumnDefinition element, bool? value)
    {
        element.SetValue(AutoLayoutProperty, value);
    }

    /// <summary>
    /// Gets the AutoLayout Attached Property Value. Used internally to track items which need to be arranged vs. fixed in place.
    /// </summary>
    /// <param name="element"><see cref="ColumnDefinition"/></param>
    /// <returns>A true value indicates this item should be automatically arranged.</returns>
    internal static bool? GetAutoLayout(ColumnDefinition element)
    {
        return (bool?)element.GetValue(AutoLayoutProperty);
    }

    /// <summary>
    /// Sets the AutoLayout Attached Property Value. Used internally to track items which need to be arranged vs. fixed in place.
    /// </summary>
    /// <param name="element"><see cref="RowDefinition"/></param>
    /// <param name="value">A true value indicates this item should be automatically arranged.</param>
    internal static void SetAutoLayout(RowDefinition element, bool? value)
    {
        element.SetValue(AutoLayoutProperty, value);
    }

    /// <summary>
    /// Gets the AutoLayout Attached Property Value. Used internally to track items which need to be arranged vs. fixed in place.
    /// </summary>
    /// <param name="element"><see cref="RowDefinition"/></param>
    /// <returns>A true value indicates this item should be automatically arranged.</returns>
    internal static bool? GetAutoLayout(RowDefinition element)
    {
        return (bool?)element.GetValue(AutoLayoutProperty);
    }

    /// <summary>
    /// Identifies the <see cref="Columns"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register(nameof(Columns), typeof(int), typeof(UniformGrid), new PropertyMetadata(0, OnPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="FirstColumn"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FirstColumnProperty =
        DependencyProperty.Register(nameof(FirstColumn), typeof(int), typeof(UniformGrid), new PropertyMetadata(0, OnPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="Orientation"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(UniformGrid), new PropertyMetadata(Orientation.Horizontal, OnPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="Rows"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RowsProperty =
        DependencyProperty.Register(nameof(Rows), typeof(int), typeof(UniformGrid), new PropertyMetadata(0, OnPropertyChanged));

    private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = d as UniformGrid;

        if (self != null)
        {
            self.InvalidateMeasure();
        }
    }

    public static readonly DependencyProperty ColumnSpacingProperty =
        DependencyProperty.Register(nameof(ColumnSpacing), typeof(double), typeof(UniformGrid), new PropertyMetadata(default(double), OnPropertyChanged));

    public double ColumnSpacing
    {
        get { return (double)GetValue(ColumnSpacingProperty); }
        set { SetValue(ColumnSpacingProperty, value); }
    }

    public static readonly DependencyProperty RowSpacingProperty =
        DependencyProperty.Register(nameof(RowSpacing), typeof(double), typeof(UniformGrid), new PropertyMetadata(default(double), OnPropertyChanged));

    public double RowSpacing
    {
        get { return (double)GetValue(RowSpacingProperty); }
        set { SetValue(RowSpacingProperty, value); }
    }

    /// <summary>
    /// Gets or sets the number of columns in the UniformGrid.
    /// </summary>
    public int Columns
    {
        get { return (int)GetValue(ColumnsProperty); }
        set { SetValue(ColumnsProperty, value); }
    }

    /// <summary>
    /// Gets or sets the starting column offset for the first row of the UniformGrid.
    /// </summary>
    public int FirstColumn
    {
        get { return (int)GetValue(FirstColumnProperty); }
        set { SetValue(FirstColumnProperty, value); }
    }

    /// <summary>
    /// Gets or sets the orientation of the grid. When <see cref="Orientation.Vertical"/>,
    /// will transpose the layout of automatically arranged items such that they start from
    /// top to bottom then based on <see cref="FlowDirection"/>.
    /// Defaults to <see cref="Orientation.Horizontal"/>.
    /// </summary>
    public Orientation Orientation
    {
        get { return (Orientation)GetValue(OrientationProperty); }
        set { SetValue(OrientationProperty, value); }
    }

    /// <summary>
    /// Gets or sets the number of rows in the UniformGrid.
    /// </summary>
    public int Rows
    {
        get { return (int)GetValue(RowsProperty); }
        set { SetValue(RowsProperty, value); }
    }
}