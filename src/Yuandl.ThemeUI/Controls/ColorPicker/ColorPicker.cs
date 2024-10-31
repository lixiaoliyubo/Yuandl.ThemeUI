// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Extensions;
using Yuandl.ThemeUI.Helpers;
using Yuandl.ThemeUI.Input;

namespace Yuandl.ThemeUI.Controls;

[TemplatePart(Name = TextBoxTemplateName, Type = typeof(TextBox))]
[TemplatePart(Name = HueSliderPartName, Type = typeof(Slider))]
[TemplatePart(Name = NumberBoxTemplateName1, Type = typeof(TextBox))]
[TemplatePart(Name = NumberBoxTemplateName2, Type = typeof(TextBox))]
[TemplatePart(Name = NumberBoxTemplateName3, Type = typeof(TextBox))]
[TemplatePart(Name = HslBoxTemplateName1, Type = typeof(TextBox))]
[TemplatePart(Name = HslBoxTemplateName2, Type = typeof(TextBox))]
[TemplatePart(Name = HslBoxTemplateName3, Type = typeof(TextBox))]
[TemplatePart(Name = SaturationBrightnessPickerPartName, Type = typeof(Canvas))]
[TemplatePart(Name = SaturationBrightnessPickerThumbPartName, Type = typeof(Thumb))]
public class ColorPicker : System.Windows.Controls.Control
{
    private const string HueSliderPartName = "PART_HueSlider";
    private const string TextBoxTemplateName = "PART_TextBox";
    private const string NumberBoxTemplateName1 = "PART_NumberBox1";
    private const string NumberBoxTemplateName2 = "PART_NumberBox2";
    private const string NumberBoxTemplateName3 = "PART_NumberBox3";
    private const string HslBoxTemplateName1 = "PART_HSLBox1";
    private const string HslBoxTemplateName2 = "PART_HSLBox2";
    private const string HslBoxTemplateName3 = "PART_HSLBox3";
    private const string SaturationBrightnessPickerPartName = "PART_Canvas";
    private const string SaturationBrightnessPickerThumbPartName = "PART_Thumb";

    static ColorPicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
    }

    public ColorPicker()
    {
        SetValue(TemplateButtonCommandProperty, new RelayCommand<object>(OnTemplateButtonClick));
    }

    public static readonly RoutedEvent ColorEvent = EventManager.RegisterRoutedEvent(nameof(Color), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));

    public event RoutedPropertyChangedEventHandler<Color> ColorChanged
    {
        add => AddHandler(ColorEvent, value);
        remove => RemoveHandler(ColorEvent, value);
    }

    public static readonly DependencyProperty ColorTypeProperty = DependencyProperty.Register("ColorType", typeof(ColorTypeEnum), typeof(ColorPicker), new PropertyMetadata(ColorTypeEnum.RGB));

    public ColorTypeEnum ColorType
    {
        get => (ColorTypeEnum)GetValue(ColorTypeProperty);
        set => SetValue(ColorTypeProperty, value);
    }

    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(ColorPicker), new FrameworkPropertyMetadata(default(Color), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColorPropertyChangedCallback));

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    internal static readonly DependencyProperty HsbProperty = DependencyProperty.Register(nameof(Hsb), typeof(Hsb), typeof(ColorPicker), new FrameworkPropertyMetadata(default(Hsb), FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, HsbPropertyChangedCallback));

    internal Hsb Hsb
    {
        get => (Hsb)GetValue(HsbProperty);
        set => SetValue(HsbProperty, value);
    }

    /// <summary>Identifies the <see cref="TemplateButtonCommand"/> dependency property.</summary>
    public static readonly DependencyProperty TemplateButtonCommandProperty = DependencyProperty.Register(
        nameof(TemplateButtonCommand),
        typeof(IRelayCommand),
        typeof(ColorPicker),
        new PropertyMetadata(null)
    );

    /// <summary>
    /// Gets the <see cref="RelayCommand{T}"/> triggered after clicking
    /// </summary>
    public IRelayCommand TemplateButtonCommand => (IRelayCommand)GetValue(TemplateButtonCommandProperty);

    private bool _inCallback;

    private static void HsbPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var colorPicker = (ColorPicker)d;
        if (colorPicker._inCallback)
        {
            return;
        }

        colorPicker._inCallback = true;

        var color = default(Color);
        if (e.NewValue is Hsb hsb)
        {
            color = hsb.ToColor();
        }

        colorPicker.SetCurrentValue(ColorProperty, color);

        colorPicker._inCallback = false;
    }

    private static void ColorPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var colorPicker = (ColorPicker)d;

        colorPicker.SetCurrentValue(HsbProperty, ((Color)e.NewValue).ToHsb());
        var args = new RoutedPropertyChangedEventArgs<Color>((Color)e.OldValue, (Color)e.NewValue) { RoutedEvent = ColorEvent };
        colorPicker.RaiseEvent(args);
    }

    protected override Size ArrangeOverride(Size arrangeBounds)
    {
        Size result = base.ArrangeOverride(arrangeBounds);
        SetThumbLeft();
        SetThumbTop();
        return result;
    }

    private TextBox? _textBox;
    private TextBox? _numberBox1;
    private TextBox? _numberBox2;
    private TextBox? _numberBox3;
    private TextBox? _hslBox1;
    private TextBox? _hslBox2;
    private TextBox? _hslBox3;
    private Thumb? _saturationBrightnessThumb;
    private Canvas? _saturationBrightnessCanvas;
    private Slider? _hueSlider;
    private ColorTypeEnum[] colorTypeEnums;
    private int currentGridStateIndex;

    public override void OnApplyTemplate()
    {
        currentGridStateIndex = 0;
        colorTypeEnums = (ColorTypeEnum[])Enum.GetValues(typeof(ColorTypeEnum));

        if (_saturationBrightnessCanvas != null)
        {
            _saturationBrightnessCanvas.MouseDown -= SaturationBrightnessCanvasMouseDown;
            _saturationBrightnessCanvas.MouseMove -= SaturationBrightnessCanvasMouseMove;
            _saturationBrightnessCanvas.MouseUp -= SaturationBrightnessCanvasMouseUp;
        }

        _saturationBrightnessCanvas = GetTemplateChild(SaturationBrightnessPickerPartName) as Canvas;
        if (_saturationBrightnessCanvas != null)
        {
            _saturationBrightnessCanvas.MouseDown += SaturationBrightnessCanvasMouseDown;
            _saturationBrightnessCanvas.MouseMove += SaturationBrightnessCanvasMouseMove;
            _saturationBrightnessCanvas.MouseUp += SaturationBrightnessCanvasMouseUp;
        }

        if (_saturationBrightnessThumb != null)
        {
            _saturationBrightnessThumb.DragDelta -= SaturationBrightnessThumbDragDelta;
        }

        _saturationBrightnessThumb = GetTemplateChild(SaturationBrightnessPickerThumbPartName) as Thumb;
        if (_saturationBrightnessThumb != null)
        {
            _saturationBrightnessThumb.DragDelta += SaturationBrightnessThumbDragDelta;
        }

        if (_hueSlider != null)
        {
            _hueSlider.ValueChanged -= HueSliderOnValueChanged;
        }

        _hueSlider = GetTemplateChild(HueSliderPartName) as Slider;
        if (_hueSlider != null)
        {
            _hueSlider.ValueChanged += HueSliderOnValueChanged;
        }

        if (_textBox != null)
        {
            _textBox.LostFocus -= TextBoxOnTextChanged;
        }

        _textBox = GetTemplateChild(TextBoxTemplateName) as TextBox;
        if (_textBox != null)
        {
            _textBox.LostFocus += TextBoxOnTextChanged;
        }

        if (_numberBox1 != null)
        {
            _numberBox1.LostFocus -= NumberBoxOnValueChanged;
        }

        _numberBox1 = GetTemplateChild(NumberBoxTemplateName1) as TextBox;
        if (_numberBox1 != null)
        {
            _numberBox1.LostFocus += NumberBoxOnValueChanged;
        }

        if (_numberBox2 != null)
        {
            _numberBox2.LostFocus -= NumberBoxOnValueChanged;
        }

        _numberBox2 = GetTemplateChild(NumberBoxTemplateName2) as TextBox;
        if (_numberBox2 != null)
        {
            _numberBox2.LostFocus += NumberBoxOnValueChanged;
        }

        if (_numberBox3 != null)
        {
            _numberBox3.LostFocus -= NumberBoxOnValueChanged;
        }

        _numberBox3 = GetTemplateChild(NumberBoxTemplateName3) as TextBox;
        if (_numberBox3 != null)
        {
            _numberBox3.LostFocus += NumberBoxOnValueChanged;
        }

        if (_hslBox1 != null)
        {
            _hslBox1.LostFocus -= HslOnValueChanged;
        }

        _hslBox1 = GetTemplateChild(HslBoxTemplateName1) as TextBox;
        if (_hslBox1 != null)
        {
            _hslBox1.LostFocus += HslOnValueChanged;
        }

        if (_hslBox2 != null)
        {
            _hslBox2.LostFocus -= HslOnValueChanged;
        }

        _hslBox2 = GetTemplateChild(HslBoxTemplateName2) as TextBox;
        if (_hslBox2 != null)
        {
            _hslBox2.LostFocus += HslOnValueChanged;
        }

        if (_hslBox3 != null)
        {
            _hslBox3.LostFocus -= HslOnValueChanged;
        }

        _hslBox3 = GetTemplateChild(HslBoxTemplateName3) as TextBox;
        if (_hslBox3 != null)
        {
            _hslBox3.LostFocus += HslOnValueChanged;
        }

        base.OnApplyTemplate();
    }

    private void NumberBoxOnValueChanged(object sender, RoutedEventArgs e)
    {
        if (_inCallback)
        {
            return;
        }

        TextBox? numberBox = sender as TextBox;
        if (_numberBox1 != null && _numberBox2 != null && _numberBox3 != null)
        {
            double R, G, B;
            if (double.TryParse(_numberBox1.Text, out R) && double.TryParse(_numberBox2.Text, out G) && double.TryParse(_numberBox3.Text, out B))
            {
                if (R >= 0 && R <= 255 && G >= 0 && G <= 255 && B >= 0 && B <= 255)
                {
                    Hsb = HsbExtensions.RGBToHsb(R, G, B);
                }
            }
        }
    }

    private void HslOnValueChanged(object sender, RoutedEventArgs e)
    {
        if (_inCallback)
        {
            return;
        }

        TextBox? numberBox = sender as TextBox;
        if (numberBox != null)
        {
            double value;
            if (double.TryParse(numberBox.Text, out value))
            {
                if (Hsb is Hsb hsb)
                {
                    switch (numberBox.Name)
                    {
                        case HslBoxTemplateName1:
                            Hsb = new Hsb(value, hsb.Saturation, hsb.Brightness);
                            break;
                        case HslBoxTemplateName2:
                            Hsb = new Hsb(hsb.Hue, value, hsb.Brightness);
                            break;
                        case HslBoxTemplateName3:
                            Hsb = new Hsb(hsb.Hue, hsb.Saturation, value);
                            break;
                    }
                }
            }
        }
    }

    private void TextBoxOnTextChanged(object sender, RoutedEventArgs e)
    {
        if (_inCallback)
        {
            return;
        }

        TextBox? textBox = sender as TextBox;
        if (textBox != null)
        {
            if (!ColorHelper.IsColorHex(textBox.Text) || !textBox.Text.StartsWith("#"))
            {
                return;
            }

            try
            {
                var color = (Color)ColorConverter.ConvertFromString(ColorHelper.FormatHexColor(textBox.Text));
                (double Hue, double Saturation, double Value) hsv = ColorHelper.ConvertToHSVColor(color);
                if (Hsb is Hsb hsb)
                {
                    Hsb = new Hsb(hsv.Hue, hsv.Saturation, hsv.Value);
                }
            }
            catch (Exception)
            {
            }
        }
    }

    private void OnTemplateButtonClick(object? sender)
    {
        currentGridStateIndex = (currentGridStateIndex + 1) % colorTypeEnums.Length;
        SetCurrentValue(ColorTypeProperty, colorTypeEnums[currentGridStateIndex]);
    }

    private void SaturationBrightnessCanvasMouseDown(object sender, MouseButtonEventArgs e)
    {
        _ = _saturationBrightnessThumb?.CaptureMouse();
    }

    private void SaturationBrightnessCanvasMouseMove(object sender, MouseEventArgs e)
    {
        if (Mouse.Captured is null || Mouse.Captured != _saturationBrightnessThumb)
        {
            return;
        }

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            Point position = e.GetPosition(_saturationBrightnessCanvas);
            ApplyThumbPosition(position.X, position.Y);
        }
    }

    private void SaturationBrightnessCanvasMouseUp(object sender, MouseButtonEventArgs e)
    {
        _saturationBrightnessThumb?.ReleaseMouseCapture();
    }

    private void SaturationBrightnessThumbDragDelta(object sender, DragDeltaEventArgs e)
    {
        Point position = Mouse.GetPosition(_saturationBrightnessCanvas);
        ApplyThumbPosition(position.X, position.Y);
    }

    private void ApplyThumbPosition(double left, double top)
    {
        if (left < 0)
        {
            left = 0;
        }

        if (top < 0)
        {
            top = 0;
        }

        if (left > _saturationBrightnessCanvas?.ActualWidth)
        {
            left = _saturationBrightnessCanvas.ActualWidth;
        }

        if (top > _saturationBrightnessCanvas?.ActualHeight)
        {
            top = _saturationBrightnessCanvas.ActualHeight;
        }

        double saturation = (1 / (_saturationBrightnessCanvas?.ActualWidth / left)) ?? 0;
        double brightness = (1 - (top / _saturationBrightnessCanvas?.ActualHeight)) ?? 0;

        SetCurrentValue(HsbProperty, new Hsb(Hsb.Hue, saturation, brightness));
    }

    private void HueSliderOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (Hsb is Hsb hsb)
        {
            Hsb = new Hsb(e.NewValue, hsb.Saturation, hsb.Brightness);
        }
    }

    private void SetThumbLeft()
    {
        if (_saturationBrightnessCanvas is null)
        {
            return;
        }

        var left = _saturationBrightnessCanvas.ActualWidth / (1 / Hsb.Saturation);
        Canvas.SetLeft(_saturationBrightnessThumb, left);
    }

    private void SetThumbTop()
    {
        if (_saturationBrightnessCanvas is null)
        {
            return;
        }

        var top = (1 - Hsb.Brightness) * _saturationBrightnessCanvas.ActualHeight;
        Canvas.SetTop(_saturationBrightnessThumb, top);
    }
}