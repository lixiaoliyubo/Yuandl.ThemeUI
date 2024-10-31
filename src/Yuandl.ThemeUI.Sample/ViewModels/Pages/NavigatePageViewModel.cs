// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class NavigatePageViewModel : ObservableObject
{
    private readonly IWindow _window;

    public NavigatePageViewModel(IWindow window)
    {
        _window = window;
    }

    [RelayCommand]
    private void NavigateForward(Type type)
    {
        _window.Navigate(type);
    }

    [RelayCommand]
    private void NavigateBack()
    {
        _window.GoBack();
    }
}