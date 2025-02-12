﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace Yuandl.ThemeUI.ElementAssist;

public enum Elevation
{
    Dp0,
    Dp1,
    Dp2,
    Dp3,
    Dp4,
    Dp5,
    Dp6,
    Dp7,
    Dp8,
    Dp12,
    Dp16,
    Dp24
}

internal static class ElevationInfo
{
    private static readonly IDictionary<Elevation, DropShadowEffect?> ShadowsDictionary;

    static ElevationInfo()
    {
        const string shadowsUri = "pack://application:,,,/Yuandl.ThemeUI;component/Resources/Shadows.xaml";
        var resourceDictionary = new ResourceDictionary { Source = new Uri(shadowsUri, UriKind.Absolute) };

        ShadowsDictionary = new Dictionary<Elevation, DropShadowEffect?>
        {
            { Elevation.Dp0, null },
            { Elevation.Dp1, resourceDictionary["ThemeUIElevationShadow1"] as DropShadowEffect },
            { Elevation.Dp2, resourceDictionary["ThemeUIElevationShadow2"] as DropShadowEffect },
            { Elevation.Dp3, resourceDictionary["ThemeUIElevationShadow3"] as DropShadowEffect },
            { Elevation.Dp4, resourceDictionary["ThemeUIElevationShadow4"] as DropShadowEffect },
            { Elevation.Dp5, resourceDictionary["ThemeUIElevationShadow5"] as DropShadowEffect },
            { Elevation.Dp6, resourceDictionary["ThemeUIElevationShadow6"] as DropShadowEffect },
            { Elevation.Dp7, resourceDictionary["ThemeUIElevationShadow7"] as DropShadowEffect },
            { Elevation.Dp8, resourceDictionary["ThemeUIElevationShadow8"] as DropShadowEffect },
            { Elevation.Dp12, resourceDictionary["ThemeUIElevationShadow12"] as DropShadowEffect },
            { Elevation.Dp16, resourceDictionary["ThemeUIElevationShadow16"] as DropShadowEffect },
            { Elevation.Dp24, resourceDictionary["ThemeUIElevationShadow24"] as DropShadowEffect }
        };
    }

    public static DropShadowEffect? GetDropShadow(Elevation elevation) => ShadowsDictionary[elevation];
}

public static class ElevationAssist
{
    public static readonly DependencyProperty ElevationProperty = DependencyProperty.RegisterAttached(
        "Elevation",
        typeof(Elevation),
        typeof(ElevationAssist),
        new FrameworkPropertyMetadata(default(Elevation), FrameworkPropertyMetadataOptions.AffectsRender));

    public static void SetElevation(DependencyObject element, Elevation value) => element.SetValue(ElevationProperty, value);

    public static Elevation GetElevation(DependencyObject element) => (Elevation)element.GetValue(ElevationProperty);

    public static DropShadowEffect? GetDropShadow(Elevation elevation) => ElevationInfo.GetDropShadow(elevation);
}

public class ElevationMarginConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Elevation elevation && elevation != Elevation.Dp0)
        {
            return new Thickness(ElevationInfo.GetDropShadow(elevation)!.BlurRadius);
        }

        return new Thickness(0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

public class ElevationRadiusConverter : IValueConverter
{
    public double Multiplier { get; set; } = 1.0;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Elevation elevation && elevation != Elevation.Dp0)
        {
            return ElevationInfo.GetDropShadow(elevation)!.BlurRadius * Multiplier;
        }

        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}