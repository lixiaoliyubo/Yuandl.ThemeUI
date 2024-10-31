// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Sample.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{

    [ObservableProperty]
    private string pageTitle = "源动力 ThemeUI";

    public MainWindowViewModel()
    {
    }
}