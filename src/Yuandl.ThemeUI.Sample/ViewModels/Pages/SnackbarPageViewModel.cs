// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class SnackbarPageViewModel : ObservableObject
{
    private readonly ISnackbarService _snackbarService;

    public SnackbarPageViewModel(ISnackbarService snackbarService)
    {
        _snackbarService = snackbarService;
    }

    private ControlAppearance _snackbarAppearance = ControlAppearance.Primary;

    [ObservableProperty]
    private int _snackbarTimeout = 2;

    private int _snackbarAppearanceComboBoxSelectedIndex = 1;

    public int SnackbarAppearanceComboBoxSelectedIndex
    {
        get => _snackbarAppearanceComboBoxSelectedIndex;
        set
        {
            _ = SetProperty(ref _snackbarAppearanceComboBoxSelectedIndex, value);
            UpdateSnackbarAppearance(value);
        }
    }

    [RelayCommand]
    private void OnOpenSnackbar(object sender)
    {
        _snackbarService.Show(
            "Don't Blame Yourself.",
            "No Witcher's Ever Died In His Bed.",
            _snackbarAppearance,
            new SymbolIcon(SymbolRegular.Fluent24),
            TimeSpan.FromSeconds(SnackbarTimeout)
        );
    }

    private void UpdateSnackbarAppearance(int appearanceIndex)
    {
        _snackbarAppearance = appearanceIndex switch
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