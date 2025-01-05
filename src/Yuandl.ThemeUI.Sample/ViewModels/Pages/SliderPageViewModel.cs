// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class SliderPageViewModel : ViewModel
{
    [ObservableProperty]
    private int _simpleSliderValue = 0;

    [ObservableProperty]
    private int _rangeSliderValue = 500;

    [ObservableProperty]
    private int _marksSliderValue = 0;

    [ObservableProperty]
    private int _verticalSliderValue = 0;

    [ObservableProperty]
    private int _colorSliderValue = 0;
}