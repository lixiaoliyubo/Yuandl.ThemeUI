// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;

namespace Yuandl.ThemeUI.Converters;

internal class GridLinesVisibilityBorderToThicknessConverter : IValueConverter
{
    private const double GridLinesThickness = 1;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is DataGridGridLinesVisibility visibility))
        {
            return Binding.DoNothing;
        }

        var thickness = parameter as double? ?? GridLinesThickness;

        return visibility switch
        {
            DataGridGridLinesVisibility.All => new Thickness(0, 0, thickness, thickness),
            DataGridGridLinesVisibility.Horizontal => new Thickness(0, 0, 0, thickness),
            DataGridGridLinesVisibility.Vertical => new Thickness(0, 0, thickness, 0),
            DataGridGridLinesVisibility.None => new Thickness(0),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}