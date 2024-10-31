// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace Yuandl.ThemeUI.ElementAssist;

public static class ShadowAssist
{
    private static readonly DependencyPropertyKey LocalInfoPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
        "LocalInfo", typeof(ShadowLocalInfo), typeof(ShadowAssist), new PropertyMetadata(default(ShadowLocalInfo)));

    private static void SetLocalInfo(DependencyObject element, ShadowLocalInfo? value)
        => element.SetValue(LocalInfoPropertyKey, value);

    private static ShadowLocalInfo? GetLocalInfo(DependencyObject element)
        => (ShadowLocalInfo?)element.GetValue(LocalInfoPropertyKey.DependencyProperty);

    private class ShadowLocalInfo
    {
        public ShadowLocalInfo(double standardOpacity) => StandardOpacity = standardOpacity;

        public double StandardOpacity { get; }
    }

    public static readonly DependencyProperty DarkenProperty = DependencyProperty.RegisterAttached(
        "Darken", typeof(bool), typeof(ShadowAssist), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.AffectsRender, DarkenPropertyChangedCallback));

    private static void DarkenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
        var uiElement = dependencyObject as UIElement;
        var dropShadowEffect = uiElement?.Effect as DropShadowEffect;

        if (dropShadowEffect is null)
        {
            return;
        }

        double? toOpacity;
        if ((bool)dependencyPropertyChangedEventArgs.NewValue)
        {
            dropShadowEffect.BeginAnimation(DropShadowEffect.OpacityProperty, null);

            SetLocalInfo(dependencyObject, new ShadowLocalInfo(dropShadowEffect.Opacity));

            toOpacity = 1;
        }
        else
        {
            ShadowLocalInfo? shadowLocalInfo = GetLocalInfo(dependencyObject);
            if (shadowLocalInfo is null)
            {
                return;
            }

            toOpacity = shadowLocalInfo.StandardOpacity;
        }

        TimeSpan time = GetShadowAnimationDuration(dependencyObject);

        var doubleAnimation = new DoubleAnimation()
        {
            To = toOpacity,
            Duration = new Duration(time),
            FillBehavior = FillBehavior.HoldEnd,
            EasingFunction = new CubicEase(),
            AccelerationRatio = 0.4,
            DecelerationRatio = 0.2
        };
        dropShadowEffect.BeginAnimation(DropShadowEffect.OpacityProperty, doubleAnimation);
    }

    public static void SetDarken(DependencyObject element, bool value)
    {
        element.SetValue(DarkenProperty, value);
    }

    public static bool GetDarken(DependencyObject element)
    {
        return (bool)element.GetValue(DarkenProperty);
    }

    public static readonly DependencyProperty CacheModeProperty = DependencyProperty.RegisterAttached(
        "CacheMode", typeof(CacheMode), typeof(ShadowAssist), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    public static void SetCacheMode(DependencyObject element, CacheMode value)
    {
        element.SetValue(CacheModeProperty, value);
    }

    public static CacheMode GetCacheMode(DependencyObject element)
    {
        return (CacheMode)element.GetValue(CacheModeProperty);
    }

    public static readonly DependencyProperty ShadowAnimationDurationProperty =
       DependencyProperty.RegisterAttached(
           name: "ShadowAnimationDuration",
           propertyType: typeof(TimeSpan),
           ownerType: typeof(ShadowAssist),
           defaultMetadata: new FrameworkPropertyMetadata(
               defaultValue: new TimeSpan(0, 0, 0, 0, 180),
               flags: FrameworkPropertyMetadataOptions.Inherits)
           );

    public static TimeSpan GetShadowAnimationDuration(DependencyObject element) => (TimeSpan)element.GetValue(ShadowAnimationDurationProperty);

    public static void SetShadowAnimationDuration(DependencyObject element, TimeSpan value) => element.SetValue(ShadowAnimationDurationProperty, value);
}