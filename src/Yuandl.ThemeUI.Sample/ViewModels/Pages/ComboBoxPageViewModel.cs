// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class ComboBoxPageViewModel : ViewModel
{
    [ObservableProperty]
    private ObservableCollection<string> _comboBoxFontFamilies =
    [
        "Arial",
        "Comic Sans MS",
        "Segoe UI",
        "Times New Roman"
    ];

    [ObservableProperty]
    private ObservableCollection<int> _comboBoxFontSizes =
    [
        8,
        9,
        10,
        11,
        12,
        14,
        16,
        18,
        20,
        24,
        28,
        36,
        48,
        72
    ];
}