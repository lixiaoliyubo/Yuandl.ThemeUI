// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Windows.Data;
using System.Windows.Media.Effects;
using Yuandl.ThemeUI.ElementAssist;

namespace Yuandl.ThemeUI.Converters;

/// <summary>
///
/// </summary>
public class ShadowConverter : IValueConverter
{
    public static readonly ShadowConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value switch
        {
            Elevation elevation => Clone(Convert(elevation)),
            _ => null
        };

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();

    public static DropShadowEffect? Convert(Elevation elevation) => ElevationAssist.GetDropShadow(elevation);

    private static DropShadowEffect? Clone(DropShadowEffect? dropShadowEffect)
    {
        if (dropShadowEffect is null)
        {
            return null;
        }

        return new DropShadowEffect()
        {
            BlurRadius = dropShadowEffect.BlurRadius,
            Color = dropShadowEffect.Color,
            Direction = dropShadowEffect.Direction,
            Opacity = dropShadowEffect.Opacity,
            RenderingBias = dropShadowEffect.RenderingBias,
            ShadowDepth = dropShadowEffect.ShadowDepth
        };
    }
}