// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

// Modified based on WPFDevelopersOrg
namespace Yuandl.ThemeUI.Controls;

public class ChartBar : ChartRect
{
    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
        var dicts = new Dictionary<Rect, string>();
        var x = StartX;
        var rectWidth = 45;
        var interval = Interval;
        foreach (KeyValuePair<string, double> item in Datas)
        {
            FormattedText formattedText = GetFormattedText(
                item.Key,
                ChartFill, FlowDirection.LeftToRight);
            drawingContext.DrawText(formattedText, new Point(x + (interval / 2) - (formattedText.Width / 2), StartY + 4));
            var _value = item.Value;
            var rectHeight = (_value - 0) / (IntervalY - 0) * (ScaleFactor * Rows);
            var rect = new Rect(x + ((interval - rectWidth) / 2), StartY - rectHeight, rectWidth, rectHeight);
            drawingContext.DrawRectangle(NormalBrush, null, rect);
            x += interval;
            var nRect = new Rect(rect.Left - EllipsePadding, rect.Top - EllipsePadding, rect.Width + EllipsePadding, rect.Height + EllipsePadding);
            dicts.Add(nRect, $"{item.Key} : {item.Value}");
        }

        PointCache = dicts;
    }
}