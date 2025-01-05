// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;
using System.IO;
using Yuandl.ThemeUI.Sample.Models;

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class DataGridPageViewModel : ViewModel
{
    [ObservableProperty]
    public IEnumerable<DataGridSelectionUnit> _selectionUnits = new[] { DataGridSelectionUnit.FullRow, DataGridSelectionUnit.Cell, DataGridSelectionUnit.CellOrRowHeader };

    [ObservableProperty]
    private IEnumerable<string> foods = new[] { "Burger", "Fries", "Shake", "Lettuce" };

    [ObservableProperty]
    private IList<string> _files = new List<string>();

    [ObservableProperty]
    private ObservableCollection<SelectableViewModel> _items1 = new ObservableCollection<SelectableViewModel>();

    [ObservableProperty]
    private ObservableCollection<SelectableViewModel> _items2 = new ObservableCollection<SelectableViewModel>();

    [ObservableProperty]
    private ObservableCollection<SelectableViewModel> _items3 = new ObservableCollection<SelectableViewModel>();

    public DataGridPageViewModel()
    {
        Items1 = CreateData();
        Items2 = CreateData();
        Items3 = CreateData();
        foreach (SelectableViewModel model in Items3)
        {
            model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(SelectableViewModel.IsSelected))
                {
                    OnPropertyChanged(nameof(IsAllItems1Selected));
                }
            };
        }

        Files = new List<string>();

        for (int i = 0; i < 1000; i++)
        {
            Files.Add(Path.GetRandomFileName());
        }
    }

    public bool? IsAllItems1Selected
    {
        get
        {
            var selected = Items3.Select(item => item.IsSelected).Distinct().ToList();
            return selected.Count == 1 ? selected.Single() : (bool?)null;
        }

        set
        {
            if (value.HasValue)
            {
                SelectAll(value.Value, Items3);
                OnPropertyChanged();
            }
        }
    }

    private static void SelectAll(bool select, IEnumerable<SelectableViewModel> models)
    {
        foreach (SelectableViewModel model in models)
        {
            model.IsSelected = select;
        }
    }

    private static ObservableCollection<SelectableViewModel> CreateData()
    {
        return new ObservableCollection<SelectableViewModel>
        {
            new SelectableViewModel { Code = 'C', Name = "Predator", Description = "If it bleeds, we can kill it" },
            new SelectableViewModel { Code = 'A', Name = "Superman", Description = "Up, up, and away!" },
            new SelectableViewModel { Code = 'D', Name = "Spiderman", Description = "With great power comes great responsibility" },
            new SelectableViewModel { Code = 'E', Name = "Neo", Description = "The Matrix has you" },
            new SelectableViewModel { Code = 'G', Name = "Trinity", Description = "Why so serious?" },
            new SelectableViewModel { Code = 'H', Name = "Batman", Description = "Why so serious?" },
            new SelectableViewModel { Code = 'B', Name = "Alien", Description = "In space, no one can hear you scream" },
            new SelectableViewModel { Code = 'F', Name = "Terminator", Description = "I'll be back" },
            new SelectableViewModel { Code = 'I', Name = "Morpheus", Description = "The Matrix has you" },
            new SelectableViewModel { Code = 'J', Name = "Terminator", Description = "If it bleeds, we can kill it" },
            new SelectableViewModel { Code = 'K', Name = "Predator", Description = "I'll be back" },
            new SelectableViewModel { Code = 'L', Name = "Trinity", Description = "With great power comes great responsibility" },
            new SelectableViewModel { Code = 'M', Name = "Spiderman", Description = "Up, up, and away!" },
            new SelectableViewModel { Code = 'N', Name = "Neo", Description = "I'll be back" },
            new SelectableViewModel { Code = 'O', Name = "Predator", Description = "With great power comes great responsibility" },
            new SelectableViewModel { Code = 'P', Name = "Batman", Description = "If it bleeds, we can kill it" },
            new SelectableViewModel { Code = 'Q', Name = "Superman", Description = "Why so serious?" },
            new SelectableViewModel { Code = 'R', Name = "Alien", Description = "The Matrix has you" },
            new SelectableViewModel { Code = 'S', Name = "Morpheus", Description = "In space, no one can hear you scream" },
            new SelectableViewModel { Code = 'T', Name = "Trinity", Description = "Up, up, and away!" },
            new SelectableViewModel { Code = 'U', Name = "Superman", Description = "If it bleeds, we can kill it" },
            new SelectableViewModel { Code = 'V', Name = "Batman", Description = "I'll be back" },
            new SelectableViewModel { Code = 'W', Name = "Terminator", Description = "In space, no one can hear you scream" },
            new SelectableViewModel { Code = 'X', Name = "Morpheus", Description = "Why so serious?" },
            new SelectableViewModel { Code = 'Y', Name = "Neo", Description = "Up, up, and away!" },
            new SelectableViewModel { Code = 'Z', Name = "Predator", Description = "If it bleeds, we can kill it" },
        };
    }
}