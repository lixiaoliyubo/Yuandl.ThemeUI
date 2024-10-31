// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Controls.Primitives;

namespace Yuandl.ThemeUI.Controls;

public class ButtonGroupItemStyleSelector : StyleSelector
{
    public override Style SelectStyle(object item, DependencyObject container)
    {
        if (container is ButtonGroup buttonGroup && item is ButtonBase buttonBase)
        {
            var count = GetVisibleButtonsCount(buttonGroup);
            if (item is RadioButton)
            {
                return ChangeStyle(typeof(RadioButton), (Style)UiApplication.Current.Resources["DefaultRadioGroupStyle"], count, buttonGroup, buttonBase);
            }
            else if (item is Yuandl.ThemeUI.Controls.Button)
            {
                return ChangeStyle(typeof(Yuandl.ThemeUI.Controls.Button), (Style)UiApplication.Current.Resources["DefaultUiButtonStyle"], count, buttonGroup, buttonBase);
            }
            else if (item is System.Windows.Controls.Button)
            {
                return ChangeStyle(typeof(System.Windows.Controls.Button), (Style)UiApplication.Current.Resources["DefaultButtonStyle"], count, buttonGroup, buttonBase);
            }
            else if (item is ToggleButton)
            {
                return ChangeStyle(typeof(ToggleButton), (Style)UiApplication.Current.Resources["DefaultToggleButtonStyle"], count, buttonGroup, buttonBase);
            }
        }

        return null;
    }

    private static int GetVisibleButtonsCount(ButtonGroup buttonGroup)
    {
        return buttonGroup.Items.OfType<ButtonBase>().Count(button => button.IsVisible);
    }

    private static Style ChangeStyle(Type type, Style baseStyle, int count, ButtonGroup buttonGroup, ButtonBase button)
    {
        Style style = new Style(type);
        style.BasedOn = baseStyle;
        style.Setters.Add(new Setter(Control.BorderBrushProperty, buttonGroup.BorderBrush));
        style.Setters.Add(new Setter(Control.BackgroundProperty, buttonGroup.Background));
        style.Setters.Add(new Setter(Control.ForegroundProperty, buttonGroup.Foreground));
        if (count == 1)
        {
            style.Setters.Add(new Setter(Border.CornerRadiusProperty, buttonGroup.CornerRadius));
            return style;
        }

        CornerRadius radius = new CornerRadius(0);
        Thickness thickness = new Thickness(0);
        var index = buttonGroup.Items.IndexOf(button);
        if (buttonGroup.Orientation == Orientation.Horizontal)
        {
            if (index == 0)
            {
                radius.TopLeft = buttonGroup.CornerRadius.TopLeft;
                radius.BottomLeft = buttonGroup.CornerRadius.BottomLeft;
                thickness.Right = buttonGroup.BorderThickness.Right > 0 ? buttonGroup.BorderThickness.Right : buttonGroup.BorderThickness.Left;
            }
            else if (index == count - 1)
            {
                radius.TopRight = buttonGroup.CornerRadius.TopRight;
                radius.BottomRight = buttonGroup.CornerRadius.BottomRight;
                thickness.Left = buttonGroup.BorderThickness.Right > 0 ? buttonGroup.BorderThickness.Right : buttonGroup.BorderThickness.Left;
            }
            else if (index > 0 && index < (count - 2))
            {
                thickness.Right = buttonGroup.BorderThickness.Right > 0 ? buttonGroup.BorderThickness.Right : buttonGroup.BorderThickness.Left;
            }
        }
        else
        {
            if (index == 0)
            {
                radius.TopLeft = buttonGroup.CornerRadius.TopLeft;
                radius.TopRight = buttonGroup.CornerRadius.TopRight;
                thickness.Bottom = buttonGroup.BorderThickness.Top > 0 ? buttonGroup.BorderThickness.Top : buttonGroup.BorderThickness.Bottom;
            }
            else if (index == count - 1)
            {
                radius.BottomLeft = buttonGroup.CornerRadius.BottomLeft;
                radius.BottomRight = buttonGroup.CornerRadius.BottomRight;
                thickness.Top = buttonGroup.BorderThickness.Top > 0 ? buttonGroup.BorderThickness.Top : buttonGroup.BorderThickness.Bottom;
            }
            else if (index > 0 && index < (count - 2))
            {
                thickness.Bottom = buttonGroup.BorderThickness.Top > 0 ? buttonGroup.BorderThickness.Top : buttonGroup.BorderThickness.Bottom;
            }
        }

        if (type == typeof(Yuandl.ThemeUI.Controls.Button))
        {
            style.Setters.Add(new Setter(Yuandl.ThemeUI.Controls.Button.CornerRadiusProperty, radius));
        }
        else
        {
            style.Setters.Add(new Setter(Border.CornerRadiusProperty, radius));
        }

        style.Setters.Add(new Setter(Border.BorderThicknessProperty, thickness));
        return style;
    }
}