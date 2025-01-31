// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

// Modified based on WPFDevelopersOrg
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace Yuandl.ThemeUI.Controls;

public class ChartBase : Control
{
    private Popup? _popup;
    private Border? _border;
    private TextBlock? _textBlock;
    public IDictionary<Rect, string>? PointCache;
    private bool isPopupOpen = false;
    private KeyValuePair<Rect, string> _lastItem;

    public static readonly DependencyProperty DatasProperty =
        DependencyProperty.Register(nameof(Datas), typeof(IEnumerable<KeyValuePair<string, double>>),
            typeof(ChartBase), new UIPropertyMetadata(OnDatasChanged));

    static ChartBase()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ChartBase),
            new FrameworkPropertyMetadata(typeof(ChartBase)));
    }

    protected double EllipseSize { get; } = 7;

    protected double EllipsePadding { get; } = 20;

    protected double Rows { get; } = 5;

    protected double Interval { get; } = 80;

    protected double MaxY { get; }

    protected double StartY { get; set; }

    protected double StartX { get; set; } = 40;

    protected Brush NormalBrush => (Brush)Application.Current.TryFindResource("SystemAccentColorBrush");

    public IEnumerable<KeyValuePair<string, double>>? Datas
    {
        get => (IEnumerable<KeyValuePair<string, double>>?)GetValue(DatasProperty);
        set => SetValue(DatasProperty, value);
    }

    private static void OnDatasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = d as ChartBase;
        if (e.NewValue != null)
        {
            ctrl.InvalidateVisual();
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (Datas == null || Datas.Count() == 0 || isPopupOpen)
        {
            return;
        }

        if (_popup == null)
        {
            _popup = new Popup
            {
                AllowsTransparency = true,
                Placement = PlacementMode.MousePoint,
                PlacementTarget = this,
                StaysOpen = false,
            };
            _popup.MouseMove += (y, j) =>
            {
                Point point = j.GetPosition(this);
                if (isPopupOpen && _lastItem.Value != null)
                {
                    if (!_lastItem.Key.Contains(point))
                    {
                        _popup.IsOpen = false;
                        isPopupOpen = false;
                        _lastItem = default(KeyValuePair<Rect, string>);
                    }
                }
            };
            _popup.Closed += (sender, e1) =>
            {
                isPopupOpen = false;
            };
            _textBlock = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = (Brush)Application.Current.TryFindResource("ControlTextColorBrush")
            };
            _border = new Border
            {
                Child = _textBlock,
                Background = (Brush)Application.Current.TryFindResource("ControPopupFillColorBrush"),
                Effect = new DropShadowEffect()
                {
                    BlurRadius = 12,
                    Direction = 270,
                    Opacity = 0.42,
                    Color = (Color)Application.Current.TryFindResource("ControlTextColor"),
                    RenderingBias = RenderingBias.Performance,
                    ShadowDepth = 2,
                },
                Margin = new Thickness(10),
                CornerRadius = new CornerRadius(3),
                Padding = new Thickness(6)
            };
            _popup.Child = _border;
        }

        if (PointCache == null)
        {
            return;
        }

        Point currentPoint = e.GetPosition(this);
        if (PointCache.Any(x => x.Key.Contains(currentPoint)))
        {
            isPopupOpen = true;

            KeyValuePair<Rect, string>? currentItem = PointCache.FirstOrDefault(x => x.Key.Contains(currentPoint));
            if (currentItem == null)
            {
                return;
            }

            _textBlock.Text = currentItem?.Value ?? string.Empty;
            _popup.IsOpen = true;
            _lastItem = currentItem ?? default(KeyValuePair<Rect, string>);
        }
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
        if (Datas == null || Datas.Count() == 0)
        {
            return;
        }

        SetCurrentValue(SnapsToDevicePixelsProperty, true);
        SetCurrentValue(UseLayoutRoundingProperty, true);
    }

    public void DrawEllipse(IEnumerable<Rect> rects, DrawingContext drawingContext)
    {
        var drawingPen = new Pen
        {
            Thickness = 2,
            Brush = NormalBrush
        };
        drawingPen.Freeze();

        var backgroupBrush = new SolidColorBrush()
        {
            Color = (Color)Application.Current.TryFindResource("ControPopupFillColor")
        };
        backgroupBrush.Freeze();
        foreach (Rect item in rects)
        {
            var ellipseGeom = new EllipseGeometry(item);
            drawingContext.DrawGeometry(backgroupBrush, drawingPen, ellipseGeom);
        }
    }

    /// <summary>
    /// 绘制Line
    /// </summary>
    /// <param name="dc">dc</param>
    /// <param name="pen">pen</param>
    /// <param name="lineThickness">lineThickness</param>
    /// <param name="points">points</param>
    public void DrawSnappedLinesBetweenPoints(DrawingContext dc, Pen pen, double lineThickness, params Point[] points)
    {
        var guidelineSet = new GuidelineSet();
        foreach (Point point in points)
        {
            guidelineSet.GuidelinesX.Add(point.X);
            guidelineSet.GuidelinesY.Add(point.Y);
        }

        var half = lineThickness / 2;
        points = points.Select(p => new Point(p.X + half, p.Y + half)).ToArray();
        dc.PushGuidelineSet(guidelineSet);
        for (var i = 0; i < points.Length - 1; i = i + 2)
        {
            dc.DrawLine(pen, points[i], points[i + 1]);
        }

        dc.Pop();
    }

    /// <summary>
    /// 返回FormattedText
    /// </summary>
    /// <param name="text">text</param>
    /// <param name="color">color</param>
    /// <param name="flowDirection">flowDirection</param>
    /// <param name="textSize">textSize</param>
    /// <param name="fontWeight">fontWeight</param>
    /// <returns>FormattedText</returns>
    public FormattedText GetFormattedText(string text, Brush? color = null, FlowDirection flowDirection = FlowDirection.RightToLeft, double textSize = 12.0D, FontWeight fontWeight = default)
    {
        if (fontWeight == default)
        {
            fontWeight = FontWeights.Thin;
        }

        // 获取当前的 PixelsPerDip 值
        double pixelsPerDip = VisualTreeHelper.GetDpi(this).PixelsPerDip;
        return new FormattedText(text, CultureInfo.CurrentCulture, flowDirection, new Typeface(FontFamily, FontStyles.Normal, fontWeight, FontStretches.Normal), textSize, color ?? Brushes.Bisque, pixelsPerDip)
        {
            MaxLineCount = 1,
            TextAlignment = TextAlignment.Justify,
            Trimming = TextTrimming.CharacterEllipsis
        };
    }
}