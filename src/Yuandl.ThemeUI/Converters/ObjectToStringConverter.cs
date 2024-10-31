// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;

namespace Yuandl.ThemeUI.Converters;

public class ObjectToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return string.Empty;
        }

        if (value is string value1)
        {
            return (int)Math.Round(System.Convert.ToDouble(value1), 0);
        }
        else if (value is double _double)
        {
            return (int)Math.Round(System.Convert.ToDouble(_double), 0);
        }

        return value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public bool IsEntityClass(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        Type type = obj.GetType();
        return type.IsClass && !type.IsAbstract && !type.IsGenericType &&
               type.GetProperties().Any();
    }
}