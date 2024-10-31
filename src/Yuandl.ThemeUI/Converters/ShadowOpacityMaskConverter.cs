// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;
using System.Windows.Media.Effects;
using Yuandl.ThemeUI.ElementAssist;

namespace Yuandl.ThemeUI.Converters;

public class ShadowOpacityMaskConverter : IMultiValueConverter
{
    public object? Convert(object[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        static double? GetValidSize(object? value)
            => value is double d && !double.IsNaN(d) && !double.IsInfinity(d) ? d : null;

        static DropShadowEffect? GetDropShadow(object? value) => value switch
        {
            Elevation elevation => ElevationAssist.GetDropShadow(elevation),
            _ => null
        };

        if (values is null
            || values.Length < 3
            || GetValidSize(values[0]) is not { } width
            || GetValidSize(values[1]) is not { } height
            || GetDropShadow(values[2]) is not { } dropShadow)
        {
            return null;
        }

        double blurRadius = dropShadow.BlurRadius;

        Rect rect = new Rect(
                -blurRadius,
                -blurRadius,
                width + blurRadius + blurRadius,
                height + blurRadius + blurRadius);

        var drawing = new GeometryDrawing(Brushes.White, null, new RectangleGeometry(rect));
        DrawingBrush rv = new(drawing)
        {
            Stretch = Stretch.None,
            TileMode = TileMode.None,
            Viewport = rect,
            ViewportUnits = BrushMappingMode.Absolute,
            Viewbox = rect,
            ViewboxUnits = BrushMappingMode.Absolute
        };
        rv.Freeze();
        return rv;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}