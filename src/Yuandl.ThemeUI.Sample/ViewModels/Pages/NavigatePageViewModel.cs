// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class NavigatePageViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public NavigatePageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void NavigateForward(Type type)
    {
        _ = _navigationService.Navigate(type);
    }

    [RelayCommand]
    private void NavigateBack()
    {
        _ = _navigationService.GoBack();
    }
}