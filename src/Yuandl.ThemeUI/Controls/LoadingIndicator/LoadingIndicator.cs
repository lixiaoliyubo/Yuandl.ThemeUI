// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Reflection;
using Yuandl.ThemeUI.Helpers;

namespace Yuandl.ThemeUI.Controls;

[TemplatePart(Name = TemplateBorderName, Type = typeof(Border))]
public class LoadingIndicator : Control
{
    internal const string TemplateBorderName = "PART_Border";

    public static readonly DependencyProperty PenSizeProperty =
        DependencyProperty.Register(nameof(PenSize), typeof(int), typeof(LoadingIndicator), new PropertyMetadata(5, OnPenSizeChanged));

    public static readonly DependencyProperty SpeedRatioProperty =
        DependencyProperty.Register(nameof(SpeedRatio), typeof(double), typeof(LoadingIndicator), new PropertyMetadata(1d, OnSpeedRatioChanged));

    public static readonly DependencyProperty IsActiveProperty =
        DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(LoadingIndicator), new PropertyMetadata(true, OnIsActiveChanged));

    public static readonly DependencyProperty ModeProperty =
        DependencyProperty.Register(nameof(Mode), typeof(LoadingIndicatorMode), typeof(LoadingIndicator), new PropertyMetadata(default(LoadingIndicatorMode), OnModeChanged));

    private static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (LoadingIndicator)d;
        if (ctl != null)
        {
            ctl.SetLoadingIndicatorMode((LoadingIndicatorMode)e.NewValue);
        }
    }

    private static void OnPenSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (LoadingIndicator)d;
        if (ctl != null)
        {
            switch (ctl.Mode)
            {
                case LoadingIndicatorMode.Wave:
                    ctl.Width = (int)e.NewValue * 10;
                    ctl.Height = (int)e.NewValue * 4;
                    break;
                case LoadingIndicatorMode.Arcs:
                    break;
                case LoadingIndicatorMode.ArcsRing:
                    ctl.Width = (int)e.NewValue;
                    ctl.Height = (int)e.NewValue;
                    break;
                case LoadingIndicatorMode.DoubleBounce:
                    ctl.Width = (int)e.NewValue;
                    ctl.Height = (int)e.NewValue;
                    break;
                case LoadingIndicatorMode.FlipPlane:
                    ctl.Width = (int)e.NewValue;
                    ctl.Height = (int)e.NewValue;
                    break;
                case LoadingIndicatorMode.Pulse:
                    ctl.Width = (int)e.NewValue;
                    ctl.Height = (int)e.NewValue;
                    break;
                case LoadingIndicatorMode.Ring:
                    ctl.Width = (int)e.NewValue * 8;
                    ctl.Height = (int)e.NewValue * 8;
                    break;
                case LoadingIndicatorMode.ThreeDots:
                    ctl.MinWidth = (int)e.NewValue * 3.2;
                    ctl.MinHeight = (int)e.NewValue * 1.5;
                    break;
                default:
                    break;
            }
        }
    }

    private void SetLoadingIndicatorMode(LoadingIndicatorMode loadingIndicatorMode)
    {
        var styleName = GetLoadingIndicatorModeDescription(loadingIndicatorMode);
        Style = Application.Current.Resources[styleName] as Style;
    }

    protected Border pART_Border;

    public LoadingIndicatorMode Mode
    {
        get => (LoadingIndicatorMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    public int PenSize
    {
        get => (int)GetValue(PenSizeProperty);
        set => SetValue(PenSizeProperty, value);
    }

    public double SpeedRatio
    {
        get => (double)GetValue(SpeedRatioProperty);
        set => SetValue(SpeedRatioProperty, value);
    }

    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    private static void OnSpeedRatioChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        var li = (LoadingIndicator)o;

        if (li.pART_Border == null || li.IsActive == false)
        {
            return;
        }

        SetStoryBoardSpeedRatio(li.pART_Border, (double)e.NewValue);
    }

    private static void OnIsActiveChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        var li = (LoadingIndicator)o;

        if (li.pART_Border == null)
        {
            return;
        }

        if ((bool)e.NewValue == false)
        {
            _ = VisualStateManager.GoToState(li, IndicatorVisualStateNames.InactiveState.Name,
                false);
            li.pART_Border.SetValue(VisibilityProperty, Visibility.Collapsed);
        }
        else
        {
            _ = VisualStateManager.GoToState(li, IndicatorVisualStateNames.ActiveState.Name, false);

            li.pART_Border.SetValue(VisibilityProperty, Visibility.Visible);

            SetStoryBoardSpeedRatio(li.pART_Border, li.SpeedRatio);
        }
    }

    private static void SetStoryBoardSpeedRatio(FrameworkElement element, double speedRatio)
    {
        IEnumerable<VisualState> activeStates = element.GetActiveVisualStates();
        foreach (VisualState activeState in activeStates)
        {
            activeState.Storyboard.SpeedRatio = speedRatio;
        }
    }

    public LoadingIndicator()
    {
        SetLoadingIndicatorMode(Mode);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        pART_Border = (Border)GetTemplateChild(TemplateBorderName);

        if (pART_Border == null)
        {
            return;
        }

        _ = VisualStateManager.GoToState(
            this,
            IsActive
                ? IndicatorVisualStateNames.ActiveState.Name
                : IndicatorVisualStateNames.InactiveState.Name, false);

        SetStoryBoardSpeedRatio(pART_Border, SpeedRatio);

        pART_Border.SetValue(VisibilityProperty, IsActive ? Visibility.Visible : Visibility.Collapsed);
    }

    public string? GetLoadingIndicatorModeDescription(LoadingIndicatorMode value)
    {
        return
            value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description;
    }
}