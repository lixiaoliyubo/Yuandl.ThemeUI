// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Globalization;
using System.Windows.Data;
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.Mvvm.Converters;

internal sealed class ThemeToIndexConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ApplicationTheme.Dark)
        {
            return 1;
        }

        return 0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is 1)
        {
            return ApplicationTheme.Dark;
        }

        return ApplicationTheme.Light;
    }
}
