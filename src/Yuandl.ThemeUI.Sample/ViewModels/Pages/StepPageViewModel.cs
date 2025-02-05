// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class StepPageViewModel : ViewModel
{
    [ObservableProperty]
    private ObservableCollection<string> _steps = new();

    [ObservableProperty]
    private int _progress = 1;

    public StepPageViewModel()
    {
        Steps = new ObservableCollection<string>();
        Steps.Add("Step 1");
        Steps.Add("Step 2");
        Steps.Add("Step 3");
        Steps.Add("Step 4");
    }

    [RelayCommand]
    private void OnPrevious(object sender)
    {
        var grid = sender as Grid;
        if (grid == null)
        {
            return;
        }

        foreach (Step step in grid.Children.OfType<Step>())
        {
            step.Previous();
        }
    }

    [RelayCommand]
    private void OnNext(object sender)
    {
        var grid = sender as Grid;
        if (grid == null)
        {
            return;
        }

        foreach (Step step in grid.Children.OfType<Step>())
        {
            step.Next();
        }
    }
}