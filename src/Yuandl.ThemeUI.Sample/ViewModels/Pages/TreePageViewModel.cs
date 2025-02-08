// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;
using Yuandl.ThemeUI.Sample.Models;

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class TreePageViewModel : ViewModel
{
    [ObservableProperty]
    private List<TreeItem> _itemsSource = new();

    public TreePageViewModel()
    {
        var items = new List<TreeItem>
        {
            new TreeItem
            {
                Name = "Root 1",
                Children = new List<TreeItem>
                {
                    new TreeItem { Name = "Child 1.1" },
                    new TreeItem { Name = "Child 1.2" }
                }
            },
            new TreeItem
            {
                Name = "Root 2",
                Children = new List<TreeItem>
                {
                    new TreeItem { Name = "Child 2.1" },
                    new TreeItem { Name = "Child 2.2" }
                }
            }
        };

        ItemsSource = items;
    }
}
