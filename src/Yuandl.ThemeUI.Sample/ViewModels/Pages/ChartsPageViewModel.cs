// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Collections.ObjectModel;

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class ChartsPageViewModel : ViewModel
{
    [ObservableProperty]
    private ObservableCollection<KeyValuePair<string, double>> _barDatas = new();

    [ObservableProperty]
    private ObservableCollection<KeyValuePair<string, double>> _lineDatas = new();

    [ObservableProperty]
    private ObservableCollection<KeyValuePair<string, double>> _radarDatas = new();

    public ChartsPageViewModel()
    {
        BarDatas = new ObservableCollection<KeyValuePair<string, double>>
        {
            new KeyValuePair<string, double>("2014年收入", 45000.00),  // 2016年财政收入
            new KeyValuePair<string, double>("2015年收入", 30000.00),  // 2016年财政收入
            new KeyValuePair<string, double>("2016年收入", 450000.00),  // 2016年财政收入
            new KeyValuePair<string, double>("2017年收入", 400.00),  // 2017年财政收入
            new KeyValuePair<string, double>("2018年收入", 90000.00),  // 2018年财政收入
            new KeyValuePair<string, double>("2019年收入", 510000.00),  // 2019年财政收入
            new KeyValuePair<string, double>("2020年收入", 100000.00),  // 2020年财政收入
            new KeyValuePair<string, double>("2021年收入", 520000.00),  // 2021年财政收入
            new KeyValuePair<string, double>("2022年收入", 40000.00),  // 2022年财政收入
            new KeyValuePair<string, double>("2023年收入", 80000.00) // 2023年财政收入
        };

        LineDatas = new ObservableCollection<KeyValuePair<string, double>>
        {
            new KeyValuePair<string, double>("One", 100.75),   // Day 1
            new KeyValuePair<string, double>("Two", 1550.60),   // Day 2
            new KeyValuePair<string, double>("Three", 180.85), // Day 3
            new KeyValuePair<string, double>("Four", 1750.40),  // Day 4
            new KeyValuePair<string, double>("Five", 825.30),  // Day 5
            new KeyValuePair<string, double>("Six", 1912.50),   // Day 6
            new KeyValuePair<string, double>("Seven", 2010.90), // Day 7
            new KeyValuePair<string, double>("Eight", 2105.25), // Day 8
            new KeyValuePair<string, double>("Nine", 200.55),  // Day 9
            new KeyValuePair<string, double>("Ten", 2310.40),   // Day 10
        };

        RadarDatas = new ObservableCollection<KeyValuePair<string, double>>
        {
            new KeyValuePair<string, double>("1月访问量", 120000.00),  // 2023年1月访问量
            new KeyValuePair<string, double>("2月访问量", 115000.00),  // 2023年2月访问量
            new KeyValuePair<string, double>("3月访问量", 130000.00),  // 2023年3月访问量
            new KeyValuePair<string, double>("4月访问量", 40000.00),  // 2023年4月访问量
            new KeyValuePair<string, double>("5月访问量", 125000.00),  // 2023年5月访问量
            new KeyValuePair<string, double>("6月访问量", 135000.00),  // 2023年6月访问量
            new KeyValuePair<string, double>("7月访问量", 150000.00),  // 2023年7月访问量
            new KeyValuePair<string, double>("8月访问量", 45000.00) // 2023年8月访问量
        };
    }

    [ObservableProperty]
    private bool _isAnchorEnabled = true;

    [RelayCommand]
    private void OnBarButtonClick(object sender)
    {
        var datas = new ObservableCollection<KeyValuePair<string, double>>();
        foreach (KeyValuePair<string, double> item in BarDatas)
        {
            datas.Add(new KeyValuePair<string, double>(item.Key, new Random().Next(100, 9999)));
        }

        BarDatas = datas;
    }

    [RelayCommand]
    private void OnLineButtonClick(object sender)
    {
        var datas = new ObservableCollection<KeyValuePair<string, double>>();
        foreach (KeyValuePair<string, double> item in LineDatas)
        {
            datas.Add(new KeyValuePair<string, double>(item.Key, new Random().Next(100, 9999)));
        }

        LineDatas = datas;
    }

    [RelayCommand]
    private void OnRadarButtonClick(object sender)
    {
        var datas = new ObservableCollection<KeyValuePair<string, double>>();
        foreach (KeyValuePair<string, double> item in RadarDatas)
        {
            datas.Add(new KeyValuePair<string, double>(item.Key, new Random().Next(100, 9999)));
        }

        RadarDatas = datas;
    }
}

public class Player
{
    public string 姓名 { get; set; }

    public int 击杀 { get; set; }

    public int 生存 { get; set; }

    public int 助攻 { get; set; }

    public int 物理 { get; set; }

    public int 魔法 { get; set; }

    public int 防御 { get; set; }

    public int 金钱 { get; set; }
}