// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

// Modified based on WPFDevelopersOrg

// ReSharper disable once CheckNamespace
namespace Yuandl.ThemeUI.Controls;

/// <summary>
///
/// </summary>
public class ChartRadar : ChartBase
{
    private PointCollection _points;
    private double _h;
    private double _w;
    private Pen _penXAxis;

    static ChartRadar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ChartRadar),
            new FrameworkPropertyMetadata(typeof(ChartRadar)));
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
        _penXAxis = new Pen
        {
            Thickness = 1,
            Brush = (Brush)Application.Current.TryFindResource("ControlBorderColorBrush")
        };
        _penXAxis.Freeze();

        var dicts = new Dictionary<Rect, string>();
        var rects = new List<Rect>();
        var max = Convert.ToInt32(Datas.Max(kvp => kvp.Value)) + 50;
        double v = StartX;
        for (var i = 0; i < Rows; i++)
        {
            DrawPoints(v, drawingContext, i == Rows - 1);
            v += StartX;
        }

        var myPen = new Pen
        {
            Thickness = 3,
            Brush = NormalBrush
        };
        myPen.Freeze();

        var streamGeometry = new StreamGeometry();
        using (StreamGeometryContext geometryContext = streamGeometry.Open())
        {
            var points = new PointCollection();
            short index = 0;
            foreach (KeyValuePair<string, double> item in Datas)
            {
                if (index < _points.Count)
                {
                    Point startPoint = _points[index];
                    var point = new Point(
                        ((startPoint.X - _w) / max * item.Value) + _w,
                        ((startPoint.Y - _h) / max * item.Value) + _h);
                    points.Add(point);
                    var ellipsePoint = new Point(point.X - (EllipseSize / 2), point.Y - (EllipseSize / 2));
                    var rect = new Rect(ellipsePoint, new Size(EllipseSize, EllipseSize));
                    rects.Add(rect);
                    var nRect = new Rect(rect.Left - EllipsePadding, rect.Top - EllipsePadding, rect.Width + EllipsePadding, rect.Height + EllipsePadding);
                    dicts.Add(nRect, $"{item.Key} : {item.Value}");
                }

                index++;
            }

            geometryContext.BeginFigure(points[points.Count - 1], true, true);
            geometryContext.PolyLineTo(points, true, true);
        }

        PointCache = dicts;
        streamGeometry.Freeze();
        var color = (Color)Application.Current.TryFindResource("SystemAccentPrimaryColor");
        var rectBrush = new SolidColorBrush(color);
        rectBrush.Opacity = 0.5;
        rectBrush.Freeze();
        drawingContext.DrawGeometry(rectBrush, myPen, streamGeometry);
        DrawEllipse(rects, drawingContext);
    }

    private void DrawPoints(double circleRadius, DrawingContext drawingContext, bool isDrawText = false)
    {
        var pieWidth = ActualWidth > ActualHeight ? ActualHeight : ActualWidth;
        var pieHeight = ActualWidth > ActualHeight ? ActualHeight : ActualWidth;
        _h = pieWidth / 2;
        _w = pieHeight / 2;
        var streamGeometry = new StreamGeometry();
        using (StreamGeometryContext geometryContext = streamGeometry.Open())
        {
            if (isDrawText)
            {
                _points = GetPolygonPoint(new Point(_w, _h), circleRadius, drawingContext);
            }
            else
            {
                _points = GetPolygonPoint(new Point(_w, _h), circleRadius);
            }

            geometryContext.BeginFigure(_points[_points.Count - 1], true, true);
            geometryContext.PolyLineTo(_points, true, true);
        }

        streamGeometry.Freeze();
        drawingContext.DrawGeometry(null, _penXAxis, streamGeometry);
    }

    private PointCollection GetPolygonPoint(Point center, double r,
        DrawingContext drawingContext = null)
    {
        double g = 18;
        double perangle = 360 / Datas.Count();
        var pi = Math.PI;
        var values = new List<Point>();
        foreach (KeyValuePair<string, double> item in Datas)
        {
            var p1 = new Point(_w, _h);
            var p2 = new Point((r * Math.Cos(g * pi / 180)) + center.X, (r * Math.Sin(g * pi / 180)) + center.Y);
            if (drawingContext != null)
            {
                drawingContext.DrawLine(_penXAxis, p1, p2);
                FormattedText formattedText = GetFormattedText(
                    item.Key,
                    (Brush)Application.Current.TryFindResource("ControlTextColorBrush"),
                    flowDirection: FlowDirection.LeftToRight, textSize: 20.001D);
                if (p2.Y > center.Y && p2.X < center.X)
                {
                    drawingContext.DrawText(
                        formattedText,
                        new Point(p2.X - formattedText.Width - 5, p2.Y - (formattedText.Height / 2)));
                }
                else if (p2.Y < center.Y && p2.X > center.X)
                    drawingContext.DrawText(formattedText, new Point(p2.X, p2.Y - formattedText.Height));
                else if (p2.Y < center.Y && p2.X < center.X)
                {
                    drawingContext.DrawText(
                        formattedText,
                        new Point(p2.X - formattedText.Width - 5, p2.Y - formattedText.Height));
                }
                else if (p2.Y < center.Y && p2.X == center.X)
                {
                    drawingContext.DrawText(
                        formattedText,
                        new Point(p2.X - formattedText.Width, p2.Y - formattedText.Height));
                }
                else
                    drawingContext.DrawText(formattedText, new Point(p2.X, p2.Y));
            }

            values.Add(p2);
            g += perangle;
        }

        var pcollect = new PointCollection(values);
        return pcollect;
    }
}