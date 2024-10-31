// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

public partial class HomePage : INavigableView<HomePageViewModel>
{
    public HomePageViewModel ViewModel { get; }

    public HomePage(HomePageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;

        InitializeComponent();
    }
}