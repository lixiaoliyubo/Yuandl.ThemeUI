// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Text.RegularExpressions;
using Yuandl.ThemeUI.Extensions;

namespace Yuandl.ThemeUI.Helpers;

/// <summary>
/// Helper class to easier work with colors
/// </summary>
public static class ColorHelper
{
    public static bool IsLightColor(this Color color)
    {
        static double Rgb_srgb(double d)
        {
            d /= 255.0;
            return (d > 0.03928)
                ? Math.Pow((d + 0.055) / 1.055, 2.4)
                : d / 12.92;
        }

        double r = Rgb_srgb(color.R);
        double g = Rgb_srgb(color.G);
        double b = Rgb_srgb(color.B);

        double luminance = (0.2126 * r) + (0.7152 * g) + (0.0722 * b);
        return luminance > 0.179;
    }

    /// <summary>
    /// Convert a given <see cref="Color"/> to a HSB color (hue, saturation, brightness)
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to convert</param>
    /// <returns>The hue [0°..360°], saturation [0..1] and brightness [0..1] of the converted color</returns>
    public static (double Hue, double Saturation, double Brightness) ConvertToHSBColor(Color color)
    {
        // HSB and HSV represents the same color space
        return ConvertToHSVColor(color);
    }

    /// <summary>
    /// Convert a given <see cref="Color"/> to a HSV color (hue, saturation, value)
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to convert</param>
    /// <returns>The hue [0°..360°], saturation [0..1] and value [0..1] of the converted color</returns>
    public static (double Hue, double Saturation, double Value) ConvertToHSVColor(Color color)
    {
        var min = Math.Min(Math.Min(color.R, color.G), color.B) / 255d;
        var max = Math.Max(Math.Max(color.R, color.G), color.B) / 255d;

        return (color.GetHue(), max == 0d ? 0d : (max - min) / max, max);
    }

    /// <summary>
    /// Convert a given <see cref="Color"/> to a HSI color (hue, saturation, intensity)
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to convert</param>
    /// <returns>The hue [0°..360°], saturation [0..1] and intensity [0..1] of the converted color</returns>
    public static (double Hue, double Saturation, double Intensity) ConvertToHSIColor(Color color)
    {
        // special case for black
        if (color.R == 0 && color.G == 0 && color.B == 0)
        {
            return (0d, 0d, 0d);
        }

        var red = color.R / 255d;
        var green = color.G / 255d;
        var blue = color.B / 255d;

        var intensity = (red + green + blue) / 3d;

        var min = Math.Min(Math.Min(color.R, color.G), color.B) / 255d;

        return (color.GetHue(), 1d - (min / intensity), intensity);
    }

    /// <summary>
    /// Convert a given <see cref="Color"/> to a float color styling(0.1f, 0.1f, 0.1f)
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to convert</param>
    /// <returns>The int / 255d for each value to get value between 0 and 1</returns>
    public static (double Red, double Green, double Blue) ConvertToDouble(Color color)
        => (color.R / 255d, color.G / 255d, color.B / 255d);

    public static Color[] GetSpectrum()
    {
        var rgbs = new Color[360];

        for (int h = 0; h < 360; h++)
        {
            rgbs[h] = RGBFromHSV(h, 1f, 1f);
        }

        return rgbs;
    }

    public static Color[] HueSpectrum(double saturation, double value)
    {
        var rgbs = new Color[7];

        for (int h = 0; h < 7; h++)
        {
            rgbs[h] = RGBFromHSV(h * 60, saturation, value);
        }

        return rgbs;
    }

    public static bool AreColorsEqual(Color color1, Color color2)
    {
        return color1.A == color2.A && color1.R == color2.R && color1.G == color2.G && color1.B == color2.B;
    }

    public static Color RGBFromHSV(double h, double s, double v)
    {
        if (h > 360 || h < 0 || s > 1 || s < 0 || v > 1 || v < 0)
        {
            return Color.FromRgb(0, 0, 0);
        }

        double c = v * s;
        double x = c * (1 - Math.Abs(((h / 60) % 2) - 1));
        double m = v - c;

        double r = 0, g = 0, b = 0;

        if (h < 60)
        {
            r = c;
            g = x;
        }
        else if (h < 120)
        {
            r = x;
            g = c;
        }
        else if (h < 180)
        {
            g = c;
            b = x;
        }
        else if (h < 240)
        {
            g = x;
            b = c;
        }
        else if (h < 300)
        {
            r = x;
            b = c;
        }
        else if (h <= 360)
        {
            r = c;
            b = x;
        }

        return Color.FromRgb((byte)((r + m) * 255), (byte)((g + m) * 255), (byte)((b + m) * 255));
    }

    public static string ColorToHex(Color color, string oldValue = "")
    {
        string newHexString = BitConverter.ToString(new byte[] { color.R, color.G, color.B }).Replace("-", string.Empty);
        newHexString = newHexString.ToLowerInvariant();

        // Return only with hashtag if user typed it before
        bool addHashtag = oldValue.StartsWith("#");
        return addHashtag ? "#" + newHexString : newHexString;
    }

    public static Color GetRandomColor()
    {
        Random random = new Random();
        byte r = (byte)random.Next(0, 256);
        byte g = (byte)random.Next(0, 256);
        byte b = (byte)random.Next(0, 256);
        byte a = (byte)random.Next(0, 256);
        return Color.FromArgb(a, r, g, b);
    }

    public static string FormatHexColor(string hex)
    {
        // Remove any leading # and convert to lowercase
        hex = hex.TrimStart('#').ToLower();

        // Determine length and pad if necessary
        switch (hex.Length)
        {
            case 3: // #RGB format
                hex = $"{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}ff";
                break;
            case 4: // #RGBA format
                hex = $"{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}{hex[3]}{hex[3]}";
                break;
            case 6: // #RRGGBB format
                hex += "ff";
                break;
            case 7: // #RRGGBB with single digit alpha
                hex += $"{hex[6]}{hex[6]}";
                break;
            case 8: // #RRGGBBAA format (already standard)
                break;
            default:
                throw new ArgumentException("Invalid hex color format. Must be 3, 4, 6, 7, or 8 characters long.");
        }

        return $"#{hex}";
    }

    /// <summary>
    /// Formats hex
    /// </summary>
    /// <param name="hexCodeText">The string we read from the hex text box.</param>
    /// <returns>Formatted string with hashtag and six characters of hex code.</returns>
    public static string FormatHexColorString(string hexCodeText)
    {
        if (hexCodeText.Length == 3 || hexCodeText.Length == 4)
        {
            // Hex with or without hashtag and three characters
            return Regex.Replace(hexCodeText, "^#?([0-9a-fA-F])([0-9a-fA-F])([0-9a-fA-F])$", "#$1$1$2$2$3$3");
        }
        else
        {
            // Hex with or without hashtag and six characters
            return hexCodeText.StartsWith("#") ? hexCodeText : "#" + hexCodeText;
        }
    }

    public static bool IsColorHex(string hexCodeText)
    {
        // 正则表达式匹配三位、四位、五位或六位十六进制颜色值
        return Regex.IsMatch(hexCodeText, "^#([0-9A-Fa-f]{6,8})$"); // 不是有效的颜色代码
    }
}