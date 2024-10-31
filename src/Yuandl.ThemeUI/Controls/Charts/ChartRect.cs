// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

public class ChartRect : ChartBase
{
    protected Brush ChartFill { get; private set; }

    protected double IntervalY { get; private set; }

    protected short ScaleFactor { get; private set; } = 80;

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        ChartFill = (Brush)Application.Current.TryFindResource("ControlTextColorBrush");
        var myPen = new Pen
        {
            Thickness = 1,
            Brush = ChartFill
        };
        myPen.Freeze();

        var xAxiHeight = 4;
        StartY = ActualHeight - (xAxiHeight + myPen.Thickness) - 20;
        var w = ActualWidth;
        StartX = 40;
        var width = (Datas.Count() * Interval) + StartX;
        IntervalY = 0;
        var x = StartX;
        var y = StartY + myPen.Thickness;

        DrawSnappedLinesBetweenPoints(drawingContext, myPen, myPen.Thickness, new Point(StartX, StartY),
            new Point(width, StartY));

        var points = new List<Point>();
        for (var i = 0; i < Datas.Count() + 1; i++)
        {
            points.Add(new Point(x, y));
            points.Add(new Point(x, y + xAxiHeight));
            x += Interval;
        }

        DrawSnappedLinesBetweenPoints(drawingContext, myPen, myPen.Thickness, points.ToArray());
        FormattedText formattedText = GetFormattedText(
            IntervalY.ToString(),
            ChartFill, FlowDirection.LeftToRight);
        drawingContext.DrawText(
            formattedText,
            new Point(StartX - (formattedText.Width * 2), StartY - (formattedText.Height / 2)));

        var xAxisPen = new Pen
        {
            Thickness = 1,
            Brush = (Brush)Application.Current.TryFindResource("ControlBorderColorBrush")
        };
        xAxisPen.Freeze();

        var max = Convert.ToInt32(Datas.Max(kvp => kvp.Value));
        var min = Convert.ToInt32(Datas.Min(kvp => kvp.Value));
        ScaleFactor = Convert.ToInt16(StartY / Rows);
        var yAxis = StartY - ScaleFactor;
        points.Clear();
        var average = Convert.ToInt32(max / Rows);
        IEnumerable<int> result = Enumerable.Range(0, ((Convert.ToInt32(max) - average) / average) + 1)
            .Select(i => average + (i * average));
        foreach (var item in result)
        {
            points.Add(new Point(StartX, yAxis));
            points.Add(new Point(width, yAxis));
            IntervalY = item;
            formattedText = GetFormattedText(
                IntervalY.ToString(),
                ChartFill, FlowDirection.LeftToRight);
            drawingContext.DrawText(
                formattedText,
                new Point(StartX - formattedText.Width - 10, yAxis - (formattedText.Height / 2)));
            yAxis -= ScaleFactor;
        }

        DrawSnappedLinesBetweenPoints(drawingContext, xAxisPen, xAxisPen.Thickness, points.ToArray());
    }
}