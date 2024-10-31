// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Markup;

namespace Yuandl.ThemeUI.Helpers;

internal class IndicatorVisualStateGroupNames : MarkupExtension
{
    private static IndicatorVisualStateGroupNames _internalActiveStates;
    private static IndicatorVisualStateGroupNames _sizeStates;

    public static IndicatorVisualStateGroupNames ActiveStates =>
        _internalActiveStates ?? (_internalActiveStates = new IndicatorVisualStateGroupNames("ActiveStates"));

    public static IndicatorVisualStateGroupNames SizeStates =>
        _sizeStates ?? (_sizeStates = new IndicatorVisualStateGroupNames("SizeStates"));

    private IndicatorVisualStateGroupNames(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        Name = name;
    }

    public string Name { get; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Name;
    }
}