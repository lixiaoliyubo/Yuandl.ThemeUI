// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

// Modified based on WPFDevelopersOrg
namespace Yuandl.ThemeUI.Controls;

public class ChartLine : ChartRect
{
    protected override void OnRender(DrawingContext drawingContext)
    {
        if (Datas == null || Datas.Count() == 0)
        {
            return;
        }

        base.OnRender(drawingContext);
        var dicts = new Dictionary<Rect, string>();
        var rects = new List<Rect>();
        var points = new List<Point>();
        var x = StartX;
        var interval = Interval;
        var drawingPen = new Pen
        {
            Thickness = 2,
            Brush = NormalBrush
        };
        drawingPen.Freeze();
        KeyValuePair<string, double> firstDataPoint = Datas.FirstOrDefault();
        if (firstDataPoint.Equals(default(KeyValuePair<string, double>)))
        {
            return;
        }

        double proportion = firstDataPoint.Value / IntervalY;
        double yPositionFromBottom = StartY - (proportion * (ScaleFactor * Rows));
        var startPoint = new Point(x + (Interval / 2), yPositionFromBottom);
        points.Add(startPoint);
        foreach (KeyValuePair<string, double> item in Datas)
        {
            FormattedText formattedText = GetFormattedText(item.Key, ChartFill, FlowDirection.LeftToRight);
            var point = new Point(x + (interval / 2) - (formattedText.Width / 2), StartY + 4);
            drawingContext.DrawText(formattedText, point);

            var y = StartY - ((item.Value / IntervalY) * (ScaleFactor * Rows));
            var endPoint = new Point(x + (Interval / 2), y);
            points.Add(endPoint);
            drawingContext.DrawLine(drawingPen, startPoint, endPoint);
            var ellipsePoint = new Point(endPoint.X - (EllipseSize / 2), endPoint.Y - (EllipseSize / 2));
            var rect = new Rect(ellipsePoint, new Size(EllipseSize, EllipseSize));
            rects.Add(rect);
            startPoint = endPoint;
            x += interval;
            var nRect = new Rect(rect.Left - EllipsePadding, rect.Top - EllipsePadding, rect.Width + EllipsePadding, rect.Height + EllipsePadding);
            dicts.Add(nRect, $"{item.Key} : {item.Value}");
        }

        PointCache = dicts;
        if (points.Count > 1)
        {
            var color = (Color)Application.Current.TryFindResource("SystemAccentColor");
            var rectBrush = new SolidColorBrush(color);
            rectBrush.Opacity = 0.3;
            rectBrush.Freeze();
            var streamGeometry = new StreamGeometry();
            using (StreamGeometryContext geometryContext = streamGeometry.Open())
            {
                var sPoint = new Point(points[0].X, StartY);
                geometryContext.BeginFigure(sPoint, true, true);

                foreach (Point point in points)
                {
                    geometryContext.LineTo(point, true, true);
                }

                var ePoint = new Point(points[points.Count - 1].X, StartY);
                geometryContext.LineTo(ePoint, true, true);
                geometryContext.Close();
            }

            drawingContext.DrawGeometry(rectBrush, null, streamGeometry);
        }

        DrawEllipse(rects, drawingContext);
    }
}