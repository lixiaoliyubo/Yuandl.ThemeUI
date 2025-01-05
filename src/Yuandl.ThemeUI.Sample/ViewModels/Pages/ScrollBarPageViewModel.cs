// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Collections.ObjectModel;

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class ScrollBarPageViewModel : ViewModel
{
    [ObservableProperty]
    private string pageTitle = "网页展示";

    [ObservableProperty]
    private string _scrollValue = string.Empty;

    [ObservableProperty]
    private ObservableCollection<string> _scrollBarItems;

    // </summary>
    public ScrollBarPageViewModel()
    {
        ScrollBarItems = GenerateData();
        ScrollValue = @"Windows Presentation Foundation (WPF) is a UI framework for building Windows desktop applications.

WPF supports a broad set of application development features, including an application model, resources, controls, graphics, layout, data binding and documents. WPF uses the Extensible Application Markup Language (XAML) to provide a declarative model for application programming.

WPF's rendering is vector-based, which enables applications to look great on high DPI monitors, as they can be infinitely scaled. WPF also includes a flexible hosting model, which makes it straightforward to host a video in a button, for example.

Visual Studio's designer, as well as Visual Studio Blend, make it easy to build WPF applications, with drag-and-drop and/or direct editing of XAML markup.

As of .NET 6.0, WPF supports ARM64.

See the WPF Roadmap to learn about project priorities, status and ship dates.

WinForms is another UI framework for building Windows desktop applications that is supported on .NET (7.0.x/6.0.x). WPF and WinForms applications only run on Windows. They are part of the Microsoft.NET.Sdk.WindowsDesktop SDK. You are recommended to use the most recent version of Visual Studio to develop WPF and WinForms applications for .NET.

To build the WPF repo and contribute features and fixes for .NET 8.0, Visual Studio 2022 Preview is required.";
    }

    private ObservableCollection<string> GenerateData()
    {
        ObservableCollection<string> collection = new ObservableCollection<string>();
        string[] cities = { "北京", "上海", "广州", "深圳", "成都", "杭州", "武汉", "重庆", "南京", "天津", "西安", "长沙", "青岛", "沈阳", "大连", "厦门", "福州", "济南", "郑州", "合肥", "石家庄", "昆明", "贵阳", "南宁", "海口", "南昌", "太原", "呼和浩特", "哈尔滨", "长春", "乌鲁木齐", "兰州", "银川", "西宁", "唐山", "保定", "秦皇岛", "邯郸", "邢台", "沧州", "廊坊", "衡水", "张家口", "承德", "大同", "阳泉", "长治", "晋城", "朔州", "晋中", "运城", "忻州", "临汾", "吕梁", "包头", "乌海", "赤峰", "通辽", "鄂尔多斯", "呼伦贝尔", "巴彦淖尔", "乌兰察布", "营口", "盘锦", "铁岭", "朝阳", "阜新", "辽阳", "本溪", "丹东", "锦州", "葫芦岛", "吉林", "四平", "辽源", "通化", "白山", "松原", "白城", "齐齐哈尔", "鸡西", "鹤岗", "双鸭山", "大庆", "伊春", "佳木斯", "七台河", "牡丹江", "黑河", "绥化" };

        for (int i = 0; i < 100; i++)
        {
            Random random = new Random();
            int randomIndex = random.Next(cities.Length);
            collection.Add(cities[randomIndex]);
        }

        return collection;
    }
}