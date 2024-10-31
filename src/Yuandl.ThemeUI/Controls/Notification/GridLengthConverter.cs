// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Windows.Data;

namespace Yuandl.ThemeUI.Converters;

public class GridLengthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    {
        if (value is Visibility buttonVisibility)
        {
            if (buttonVisibility == Visibility.Visible)
            {
                return new GridLength(1, GridUnitType.Star);
            }
            else
            {
                return new GridLength(1, GridUnitType.Auto);
            }
        }

        return default(GridLength);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    {
        throw new NotImplementedException();
    }
}