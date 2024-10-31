// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Shapes;

namespace Yuandl.ThemeUI.Controls;

[TemplatePart(Name = PART_ColumnStart, Type = typeof(ColumnDefinition))]
[TemplatePart(Name = PART_ColumnEnd, Type = typeof(ColumnDefinition))]
[TemplatePart(Name = PART_StretchLine, Type = typeof(Line))]
[TemplatePart(Name = PART_LeftLine, Type = typeof(Line))]
[TemplatePart(Name = PART_RightLine, Type = typeof(Line))]
[TemplatePart(Name = PART_Content, Type = typeof(ContentPresenter))]
public class Divider : System.Windows.Controls.ContentControl
{
    private const string PART_ColumnStart = "PART_ColumnStart";
    private const string PART_ColumnEnd = "PART_ColumnEnd";
    private const string PART_StretchLine = "PART_StretchLine";
    private const string PART_LeftLine = "PART_LeftLine";
    private const string PART_RightLine = "PART_RightLine";
    private const string PART_Content = "PART_Content";

    private ColumnDefinition _PART_ColumnStart;
    private ColumnDefinition _PART_ColumnEnd;
    private Line _PART_StretchLine;
    private Line _PART_LeftLine;
    private Line _PART_RightLine;
    private ContentPresenter _PART_Content;

    private Thickness _oldContentPadding;

    public Thickness ContentPadding
    {
        get { return (Thickness)GetValue(ContentPaddingProperty); }
        set { SetValue(ContentPaddingProperty, value); }
    }

    public static readonly DependencyProperty ContentPaddingProperty =
        DependencyProperty.Register(nameof(ContentPadding), typeof(Thickness), typeof(Divider), new PropertyMetadata(new Thickness(24, 0, 24, 0)));

    [Bindable(true)]
    [Category("Content")]
    public new object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public static new readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(Divider), new PropertyMetadata(null, OnContentChanged));

    private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (Divider)d;
        if (ctl != null)
        {
            ctl.UpdateContent(e.NewValue);
        }
    }

    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(Divider), new PropertyMetadata(Orientation.Horizontal, OnOrientationChanged));

    private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (Divider)d;
        if (ctl != null)
        {
            ctl.UpdateOrientation((Orientation)e.NewValue);
            ctl.UpdateHorizontalContentAlignment();
        }
    }

    public Brush LineStroke
    {
        get => (Brush)GetValue(LineStrokeProperty);
        set => SetValue(LineStrokeProperty, value);
    }

    public static readonly DependencyProperty LineStrokeProperty =
        DependencyProperty.Register(nameof(LineStroke), typeof(Brush), typeof(Divider), new PropertyMetadata(default(Brush)));

    public double LineStrokeThickness
    {
        get => (double)GetValue(LineStrokeThicknessProperty);
        set => SetValue(LineStrokeThicknessProperty, value);
    }

    public static readonly DependencyProperty LineStrokeThicknessProperty =
        DependencyProperty.Register(nameof(LineStrokeThickness), typeof(double), typeof(Divider), new PropertyMetadata(1.0));

    public DoubleCollection? LineStrokeDashArray
    {
        get { return (DoubleCollection?)GetValue(LineStrokeDashArrayProperty); }
        set { SetValue(LineStrokeDashArrayProperty, value); }
    }

    public static readonly DependencyProperty LineStrokeDashArrayProperty =
        DependencyProperty.Register(nameof(LineStrokeDashArray), typeof(DoubleCollection), typeof(Divider), new PropertyMetadata(default(DoubleCollection)));

    public static new readonly DependencyProperty HorizontalContentAlignmentProperty =
                DependencyProperty.Register(
                            nameof(HorizontalContentAlignment),
                            typeof(HorizontalAlignment),
                            typeof(Divider),
                            new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.AffectsRender,
                            new PropertyChangedCallback(OnHorizontalContentAlignmentChanged))
                );

    [Bindable(true)]
    [Category("Layout")]
    public new HorizontalAlignment HorizontalContentAlignment
    {
        get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
        set { SetValue(HorizontalContentAlignmentProperty, value); }
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _PART_ColumnStart = GetTemplateChild(PART_ColumnStart) as ColumnDefinition;
        _PART_ColumnEnd = GetTemplateChild(PART_ColumnEnd) as ColumnDefinition;
        _PART_StretchLine = GetTemplateChild(PART_StretchLine) as Line;
        _PART_LeftLine = GetTemplateChild(PART_LeftLine) as Line;
        _PART_RightLine = GetTemplateChild(PART_RightLine) as Line;
        _PART_Content = GetTemplateChild(PART_Content) as ContentPresenter;

        UpdateOrientation(Orientation);
        UpdateContent(Content);
    }

    private static void OnHorizontalContentAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Divider self)
        {
            return;
        }

        self.UpdateHorizontalContentAlignment();
    }

    private void UpdateHorizontalContentAlignment()
    {
        if (_PART_ColumnStart == null || _PART_ColumnEnd == null)
        {
            return;
        }

        if (Orientation == Orientation.Vertical || Content == null || (Content is string value && string.IsNullOrEmpty(value)))
        {
            _PART_ColumnStart.SetCurrentValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
            _PART_ColumnEnd.SetCurrentValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
            return;
        }

        switch (HorizontalContentAlignment)
        {
            case HorizontalAlignment.Left:
                _PART_ColumnStart.SetCurrentValue(ColumnDefinition.WidthProperty, new GridLength(20, GridUnitType.Pixel));
                _PART_ColumnEnd.SetCurrentValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
                break;
            case HorizontalAlignment.Right:
                _PART_ColumnStart.SetCurrentValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
                _PART_ColumnEnd.SetCurrentValue(ColumnDefinition.WidthProperty, new GridLength(20, GridUnitType.Pixel));
                break;
            case HorizontalAlignment.Center:
            case HorizontalAlignment.Stretch:
                _PART_ColumnStart.SetCurrentValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
                _PART_ColumnEnd.SetCurrentValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
                break;
        }
    }

    private void UpdateOrientation(Orientation orientation)
    {
        if (_PART_StretchLine == null || _PART_LeftLine == null || _PART_RightLine == null || _PART_Content == null)
        {
            return;
        }

        if (orientation == Orientation.Vertical)
        {
            // MinHeight = ActualWidth;
            SetCurrentValue(MarginProperty, new Thickness(6, 0, 6, 0));
            _PART_StretchLine.SetCurrentValue(VisibilityProperty, Visibility.Visible);
            _PART_LeftLine.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
            _PART_RightLine.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
            _PART_Content.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
        }
        else
        {
            SetCurrentValue(MarginProperty, new Thickness(0, 24, 0, 24));
            _PART_StretchLine.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
            _PART_LeftLine.SetCurrentValue(VisibilityProperty, Visibility.Visible);
            _PART_RightLine.SetCurrentValue(VisibilityProperty, Visibility.Visible);
            _PART_Content.SetCurrentValue(VisibilityProperty, Visibility.Visible);
        }
    }

    private void UpdateContent(object content)
    {
        if (ContentPadding != new Thickness(0))
        {
            _oldContentPadding = ContentPadding;
        }

        if (content == null || (content is string newContent && string.IsNullOrEmpty(newContent)))
        {
            ContentPadding = new Thickness(uniformLength: 0);
        }
        else
        {
            ContentPadding = _oldContentPadding;
        }

        UpdateHorizontalContentAlignment();
    }
}