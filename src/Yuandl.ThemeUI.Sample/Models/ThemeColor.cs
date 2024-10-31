// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Sample.Models;

public partial class ThemeColorEntity : ObservableObject
{
    public string? Name { get; set; }

    [ObservableProperty]
    public Brush? _colorBrush;

    public int ColorType { get; set; }

    [RelayCommand]
    private void OnTemplateButton(object param)
    {
    }
}