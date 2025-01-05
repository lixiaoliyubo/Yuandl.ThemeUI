// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.ComponentModel;

namespace Yuandl.ThemeUI.Sample.Models;

public class SelectableViewModel : ViewModel
{
    private bool _isSelected;
    private string? _name;
    private string? _description;
    private char _code;
    private double _numeric;
    private string? _food;
    private string? _files;
    private VehicleType _vehicleType;

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    [Browsable(true)]
    [DisplayName("Code")]
    [ReadOnly(true)]
    public char Code
    {
        get => _code;
        set => SetProperty(ref _code, value);
    }

    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string? Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public double Numeric
    {
        get => _numeric;
        set => SetProperty(ref _numeric, value);
    }

    public string? Food
    {
        get => _food;
        set => SetProperty(ref _food, value);
    }

    public string? Files
    {
        get => _files;
        set => SetProperty(ref _files, value);
    }

    public VehicleType VehicleType
    {
        get => _vehicleType;
        set => SetProperty(ref _vehicleType, value);
    }
}

public enum VehicleType
{
    Car,
    Bus,
    Motorcycle,
    Van,
    Scooter,
    Truck
}