// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Extensions;

public static class StringExtension
{
    public static T Value<T>(this string input)
    {
        try
        {
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
        }
        catch
        {
            return default;
        }
    }

    public static object Value(this string input, Type type)
    {
        try
        {
            return TypeDescriptor.GetConverter(type).ConvertFromString(input);
        }
        catch
        {
            return null;
        }
    }
}