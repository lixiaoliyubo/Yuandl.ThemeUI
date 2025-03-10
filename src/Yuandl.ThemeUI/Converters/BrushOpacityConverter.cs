﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;

namespace Yuandl.ThemeUI.Converters;

public class BrushOpacityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
        {
            var opacity = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            SolidColorBrush rv = new(brush.Color)
            {
                Opacity = opacity
            };
            rv.Freeze();
            return rv;
        }

        return null;
    }

    public object? ConvertBack(object value, Type targetType, object? parameter, CultureInfo culture)
        => Binding.DoNothing;
}