// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class InfoBadgePageViewModel : ObservableObject
{
    [ObservableProperty]
    private ControlAppearance _infoBadgeSeverity = ControlAppearance.Primary;

    private int _infoBadgeSeverityComboBoxSelectedIndex = 0;

    public int InfoBadgeSeverityComboBoxSelectedIndex
    {
        get => _infoBadgeSeverityComboBoxSelectedIndex;
        set
        {
            _ = SetProperty(ref _infoBadgeSeverityComboBoxSelectedIndex, value);
            InfoBadgeSeverity = ConvertIndexToInfoBadgeSeverity(value);
        }
    }

    private static ControlAppearance ConvertIndexToInfoBadgeSeverity(int value)
    {
        return value switch
        {
            1 => ControlAppearance.Primary,
            2 => ControlAppearance.Warning,
            3 => ControlAppearance.Info,
            4 => ControlAppearance.Error,
            5 => ControlAppearance.Success,
            6 => ControlAppearance.Transparent,
            _ => ControlAppearance.Custom
        };
    }
}