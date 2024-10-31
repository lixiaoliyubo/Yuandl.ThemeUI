// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Markup;

namespace Yuandl.ThemeUI.Helpers;

public class IndicatorVisualStateNames : MarkupExtension
{
    private static IndicatorVisualStateNames _activeState;
    private static IndicatorVisualStateNames _inactiveState;

    public static IndicatorVisualStateNames ActiveState =>
        _activeState ?? (_activeState = new IndicatorVisualStateNames("Active"));

    public static IndicatorVisualStateNames InactiveState =>
        _inactiveState ?? (_inactiveState = new IndicatorVisualStateNames("Inactive"));

    private IndicatorVisualStateNames(string name)
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