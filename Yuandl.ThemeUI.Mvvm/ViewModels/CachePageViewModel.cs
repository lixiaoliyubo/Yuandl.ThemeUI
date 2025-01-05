// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Media;

namespace Yuandl.ThemeUI.Mvvm.ViewModels;

public partial class CachePageViewModel : ViewModel
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private string _content = "";

    [RelayCommand]
    private void OnUpdate()
    {
        InitializeViewModel();
    }
    public override void OnNavigatedTo()
    {
        Debug.WriteLine($"进入页面CachePageView：{Content}");
        if (!_isInitialized)
        {
            InitializeViewModel();
        }
    }
    public override void OnNavigatedFrom()
    {
        Debug.WriteLine($"离开页面CachePageView：{Content}");
    }
    private void InitializeViewModel()
    {
        var random = new Random();
        Content = $"缓存页面：{random.Next(1111111,9999999)}";
        _isInitialized = true;
    }
}
