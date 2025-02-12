// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class HyperlinkButtonPageViewModel : ViewModel
{
    [ObservableProperty]
    private bool _isHyperlinkEnabled = true;

    [RelayCommand]
    private void OnHyperlinkCheckboxChecked(object sender)
    {
        if (sender is not CheckBox checkbox)
        {
            return;
        }

        IsHyperlinkEnabled = !(checkbox?.IsChecked ?? false);
    }
}