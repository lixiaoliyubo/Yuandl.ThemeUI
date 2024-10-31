// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;
using Yuandl.ThemeUI.Controls;

namespace Yuandl.ThemeUI.Converters;

[ValueConversion(typeof(int), typeof(StepItem))]
public class StepIndexConverter : IValueConverter
{
    public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
    {
        var item = (StepItem)value;
        var step = ItemsControl.ItemsControlFromItemContainer(item) as Step;
        var index = step.ItemContainerGenerator.IndexFromContainer(item) + 1;
        item.Index = index;
        return index;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}