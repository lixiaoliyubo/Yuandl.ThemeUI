// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class InfoBarPageViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isShortInfoBarOpened = true;

    [ObservableProperty]
    private bool _isLongInfoBarOpened = true;

    [ObservableProperty]
    private ControlAppearance _shortInfoBarSeverity = ControlAppearance.Info;

    [ObservableProperty]
    private ControlAppearance _longInfoBarSeverity = ControlAppearance.Info;

    private int _shortInfoBarSeverityComboBoxSelectedIndex = 0;

    public int ShortInfoBarSeverityComboBoxSelectedIndex
    {
        get => _shortInfoBarSeverityComboBoxSelectedIndex;
        set
        {
            _ = SetProperty(ref _shortInfoBarSeverityComboBoxSelectedIndex, value);

            ShortInfoBarSeverity = ConvertIndexToInfoBarSeverity(value);
        }
    }

    private int _longInfoBarSeverityComboBoxSelectedIndex = 0;

    public int LongInfoBarSeverityComboBoxSelectedIndex
    {
        get => _longInfoBarSeverityComboBoxSelectedIndex;
        set
        {
            _ = SetProperty(ref _longInfoBarSeverityComboBoxSelectedIndex, value);

            LongInfoBarSeverity = ConvertIndexToInfoBarSeverity(value);
        }
    }

    private static ControlAppearance ConvertIndexToInfoBarSeverity(int value)
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