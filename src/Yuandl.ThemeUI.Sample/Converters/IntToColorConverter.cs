// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Globalization;
using System.Windows.Data;

namespace Yuandl.ThemeUI.Sample.Converters;

public class IntToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double intValue)
        {
            Color hsb = new Hsb(intValue, 0.8, 0.8).ToColor();
            return new SolidColorBrush(hsb);
        }

        return Brushes.Black;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}