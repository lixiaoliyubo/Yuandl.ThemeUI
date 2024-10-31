// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;

namespace Yuandl.ThemeUI.Converters;

internal class AnimationFactorToValueConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is not double completeValue)
        {
            return 0.0;
        }

        if (values[1] is not double factor)
        {
            return 0.0;
        }

        if (parameter is "negative")
        {
            factor = -factor;
        }

        return factor * completeValue;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}