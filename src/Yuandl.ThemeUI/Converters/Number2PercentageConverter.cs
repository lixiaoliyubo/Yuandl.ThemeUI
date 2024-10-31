// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;
using Yuandl.ThemeUI.Extensions;

namespace Yuandl.ThemeUI.Converters;

public class Number2PercentageConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length != 2)
        {
            return .0;
        }

        var obj1 = values[0];
        var obj2 = values[1];

        if (obj1 == null || obj2 == null)
        {
            return .0;
        }

        var str1 = values[0].ToString();
        var str2 = values[1].ToString();

        var v1 = str1.Value<double>();
        var v2 = str2.Value<double>();

        if (Math.Abs(v2) < 1E-06)
        {
            return 100.0;
        }

        return v1 / v2 * 100;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}