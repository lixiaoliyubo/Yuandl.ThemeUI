// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class ButtonPageViewModel : ViewModel
{
    [ObservableProperty]
    private string pageTitle = "网页展示";

    [ObservableProperty]
    private ThemeResource[] enumValues = (ThemeResource[])Enum.GetValues(typeof(ThemeResource));

    // </summary>
    public ButtonPageViewModel()
    {
    }

    [ObservableProperty]
    private int counter = 0;

    [RelayCommand]
    private void OnCounterIncrement()
    {
        Counter++;
    }
}