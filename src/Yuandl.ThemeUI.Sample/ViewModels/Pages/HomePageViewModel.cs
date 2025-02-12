// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;
using Yuandl.ThemeUI.Sample.Helper;
using Yuandl.ThemeUI.Sample.Models;

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class HomePageViewModel : ViewModel
{
    [ObservableProperty]
    private ICollection<NavigationCard> _navigationCards = new ObservableCollection<NavigationCard>(
        ControlPages
            .All()
            .Select(x => new NavigationCard()
            {
                Name = x.Name,
                Icon = x.Icon,
                Description = x.Description,
                PageType = x.PageType
            })
            .OrderBy(x => x.Name)
    );
}