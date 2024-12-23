// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;

namespace Yuandl.ThemeUI.Converters;

public class ValueToSizeConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (double.Parse(value?.ToString() ?? "0") <= 0 && double.Parse(parameter?.ToString() ?? "0") < 0)
        {
            return 0;
        }

        return double.Parse(value?.ToString() ?? "0") + double.Parse(parameter?.ToString() ?? "0");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}