// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace Yuandl.ThemeUI.Mvvm.ViewModels;

public partial class HomePageViewModel : ViewModel
{
    [ObservableProperty]
    private int _counter = 0;

    [RelayCommand]
    private void OnCounterIncrement()
    {
        Counter++;
    }
    public override void OnNavigatedTo()
    {
        Debug.WriteLine($"进入页面HomePageViewModel");
    }
    public override void OnNavigatedFrom()
    {
        Debug.WriteLine($"离开页面HomePageViewModel");
    }
}
