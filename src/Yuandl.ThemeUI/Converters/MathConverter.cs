// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.Converters;

public sealed class MathConverter : IValueConverter
{
    public MathOperation Operation { get; set; }

    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        try
        {
            double value1 = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
            double value2 = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            switch (Operation)
            {
                case MathOperation.Add:
                    return value1 + value2;
                case MathOperation.Divide:
                    return value1 / value2;
                case MathOperation.Multiply:
                    return value1 * value2;
                case MathOperation.Subtract:
                    return value1 - value2;
                case MathOperation.Pow:
                    return Math.Pow(value1, value2);
                default:
                    return Binding.DoNothing;
            }
        }
        catch (FormatException)
        {
            return Binding.DoNothing;
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Binding.DoNothing;
}