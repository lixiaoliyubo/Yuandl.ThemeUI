// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;
using Yuandl.ThemeUI.Helpers;

namespace Yuandl.ThemeUI.Converters;

public class BrushRoundConverter : IValueConverter
{
    public Brush? HighValue { get; set; } = Brushes.White;

    public Brush? LowValue { get; set; } = Brushes.Black;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not SolidColorBrush solidColorBrush)
        {
            return null;
        }

        return solidColorBrush.Color.IsLightColor()
            ? HighValue
            : LowValue;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Binding.DoNothing;
}