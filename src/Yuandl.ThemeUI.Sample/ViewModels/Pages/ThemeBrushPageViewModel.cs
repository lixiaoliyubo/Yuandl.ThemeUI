// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;
using Yuandl.ThemeUI.Sample.Models;

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class ThemeBrushPageViewModel : ViewModel
{
    [ObservableProperty]
    private ObservableCollection<ThemeColorEntity> _items = new ObservableCollection<ThemeColorEntity>();

    public ThemeBrushPageViewModel()
    {
    }
}