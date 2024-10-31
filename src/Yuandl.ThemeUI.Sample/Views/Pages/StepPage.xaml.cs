// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Sample.ViewModels.Pages;

namespace Yuandl.ThemeUI.Sample.Views.Pages;

/// <summary>
/// StepPage.xaml 的交互逻辑
/// </summary>
[GalleryPage("This is an example of a step control.", SymbolRegular.ArrowStepInRight24)]
public partial class StepPage : INavigableView<StepPageViewModel>
{
    public StepPageViewModel ViewModel { get; }

    public StepPage(StepPageViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}