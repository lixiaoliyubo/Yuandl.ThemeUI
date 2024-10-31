// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Text;
using System.Windows.Media.Animation;

namespace Yuandl.ThemeUI.Helpers;

public class AnimationHelper
{
    /// <summary>
    ///     创建一个Thickness动画
    /// </summary>
    /// <param name="thickness">thickness</param>
    /// <param name="milliseconds">milliseconds</param>
    /// <returns></returns>
    public static ThicknessAnimation CreateAnimation(Thickness thickness = default, double milliseconds = 200)
    {
        return new(thickness, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
        {
            EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
        };
    }

    /// <summary>
    ///     创建一个Double动画
    /// </summary>
    /// <param name="toValue"></param>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static DoubleAnimation CreateAnimation(double toValue, double milliseconds = 200)
    {
        return new(toValue, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
        {
            EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
        };
    }

    public static DoubleAnimation CreateAnimation(double formValue, double toValue, double milliseconds = 200)
    {
        return new(formValue, toValue, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
        {
            EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
        };
    }

    internal static Geometry ComposeGeometry(string[] strings, double[] arr)
    {
        var builder = new StringBuilder(strings[0]);
        for (var i = 0; i < arr.Length; i++)
        {
            var s = strings[i + 1];
            var n = arr[i];
            if (!double.IsNaN(n))
            {
                _ = builder.Append(n).Append(s);
            }
        }

        return Geometry.Parse(builder.ToString());
    }

    internal static Geometry InterpolateGeometry(double[] from, double[] to, double progress, string[] strings)
    {
        var accumulated = new double[to.Length];
        for (var i = 0; i < to.Length; i++)
        {
            var fromValue = from[i];
            accumulated[i] = fromValue + ((to[i] - fromValue) * progress);
        }

        return ComposeGeometry(strings, accumulated);
    }

    internal static double[] InterpolateGeometryValue(double[] from, double[] to, double progress)
    {
        var accumulated = new double[to.Length];
        for (var i = 0; i < to.Length; i++)
        {
            var fromValue = from[i];
            accumulated[i] = fromValue + ((to[i] - fromValue) * progress);
        }

        return accumulated;
    }
}