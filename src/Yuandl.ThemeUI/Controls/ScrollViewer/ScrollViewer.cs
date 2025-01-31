// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Yuandl.ThemeUI.Controls;

public class ScrollViewer : System.Windows.Controls.ScrollViewer
{
    private double _totalVerticalOffset;
    private double _totalHorizontalOffset;
    private bool _isRunning;

    public static readonly DependencyProperty CanMouseWheelProperty = DependencyProperty.Register(
        nameof(CanMouseWheel), typeof(bool), typeof(ScrollViewer), new PropertyMetadata(true));

    public bool CanMouseWheel
    {
        get => (bool)GetValue(CanMouseWheelProperty);
        set => SetValue(CanMouseWheelProperty, value);
    }

    public static readonly DependencyProperty OrientationProperty = DependencyProperty.RegisterAttached(
        "Orientation", typeof(Orientation), typeof(ScrollViewer), new PropertyMetadata(Orientation.Vertical));

    public static void SetOrientation(DependencyObject element, Orientation value)
        => element.SetValue(OrientationProperty, value);

    public static Orientation GetOrientation(DependencyObject element)
        => (Orientation)element.GetValue(OrientationProperty);

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        if (!CanMouseWheel) return;

        if (!IsInertiaEnabled)
        {
            if (GetOrientation(this) == Orientation.Vertical)
            {
                base.OnMouseWheel(e);
            }
            else
            {
                _totalHorizontalOffset = HorizontalOffset;
                CurrentHorizontalOffset = HorizontalOffset;
                _totalHorizontalOffset = Math.Min(Math.Max(0, _totalHorizontalOffset - e.Delta), ScrollableWidth);
                CurrentHorizontalOffset = _totalHorizontalOffset;
            }

            return;
        }

        e.Handled = true;

        if (GetOrientation(this) == Orientation.Vertical)
        {
            if (!_isRunning)
            {
                _totalVerticalOffset = VerticalOffset;
                CurrentVerticalOffset = VerticalOffset;
            }

            _totalVerticalOffset = Math.Min(Math.Max(0, _totalVerticalOffset - e.Delta), ScrollableHeight);
            ScrollToVerticalOffsetWithAnimation(_totalVerticalOffset);
        }
        else
        {
            if (!_isRunning)
            {
                _totalHorizontalOffset = HorizontalOffset;
                CurrentHorizontalOffset = HorizontalOffset;
            }

            _totalHorizontalOffset = Math.Min(Math.Max(0, _totalHorizontalOffset - e.Delta), ScrollableWidth);
            ScrollToHorizontalOffsetWithAnimation(_totalHorizontalOffset);
        }
    }

    internal void ScrollToTopInternal(double milliseconds = 500)
    {
        if (!_isRunning)
        {
            _totalVerticalOffset = VerticalOffset;
            CurrentVerticalOffset = VerticalOffset;
        }

        ScrollToVerticalOffsetWithAnimation(0, milliseconds);
    }

    public void ScrollToVerticalOffsetWithAnimation(double offset, double milliseconds = 500)
    {
        var animation = new DoubleAnimation(offset, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
            FillBehavior = FillBehavior.Stop
        };

        animation.Completed += (s, e1) =>
        {
            CurrentVerticalOffset = offset;
            _isRunning = false;
        };
        _isRunning = true;

        BeginAnimation(CurrentVerticalOffsetProperty, animation);
    }

    public void ScrollToHorizontalOffsetWithAnimation(double offset, double milliseconds = 500)
    {
        var animation = new DoubleAnimation(offset, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
            FillBehavior = FillBehavior.Stop
        };

        animation.Completed += (s, e1) =>
        {
            CurrentHorizontalOffset = offset;
            _isRunning = false;
        };
        _isRunning = true;

        BeginAnimation(CurrentHorizontalOffsetProperty, animation);
    }

    protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters) =>
        IsPenetrating ? null : base.HitTestCore(hitTestParameters);

    public static readonly DependencyProperty IsInertiaEnabledProperty = DependencyProperty.RegisterAttached(
        nameof(IsInertiaEnabled), typeof(bool), typeof(ScrollViewer), new PropertyMetadata(false));

    public static void SetIsInertiaEnabled(DependencyObject element, bool value)
        => element.SetValue(IsInertiaEnabledProperty, value);

    public static bool GetIsInertiaEnabled(DependencyObject element)
        => (bool)element.GetValue(IsInertiaEnabledProperty);

    public bool IsInertiaEnabled
    {
        get => (bool)GetValue(IsInertiaEnabledProperty);
        set => SetValue(IsInertiaEnabledProperty, value);
    }

    public static readonly DependencyProperty IsPenetratingProperty = DependencyProperty.RegisterAttached(
        nameof(IsPenetrating), typeof(bool), typeof(ScrollViewer), new PropertyMetadata(false));

    public bool IsPenetrating
    {
        get => (bool)GetValue(IsPenetratingProperty);
        set => SetValue(IsPenetratingProperty, value);
    }

    public static void SetIsPenetrating(DependencyObject element, bool value)
        => element.SetValue(IsPenetratingProperty, value);

    public static bool GetIsPenetrating(DependencyObject element)
        => (bool)element.GetValue(IsPenetratingProperty);

    internal static readonly DependencyProperty CurrentVerticalOffsetProperty = DependencyProperty.Register(
        nameof(CurrentVerticalOffset),
        typeof(double),
        typeof(ScrollViewer),
        new PropertyMetadata(.0, OnCurrentVerticalOffsetChanged));

    private static void OnCurrentVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ScrollViewer ctl && e.NewValue is double v)
        {
            ctl.ScrollToVerticalOffset(v);
        }
    }

    internal double CurrentVerticalOffset
    {
        get => (double)GetValue(CurrentVerticalOffsetProperty);
        set => SetValue(CurrentVerticalOffsetProperty, value);
    }

    internal static readonly DependencyProperty CurrentHorizontalOffsetProperty = DependencyProperty.Register(
        nameof(CurrentHorizontalOffset),
        typeof(double),
        typeof(ScrollViewer),
        new PropertyMetadata(.0, OnCurrentHorizontalOffsetChanged));

    private static void OnCurrentHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ScrollViewer ctl && e.NewValue is double v)
        {
            ctl.ScrollToHorizontalOffset(v);
        }
    }

    internal double CurrentHorizontalOffset
    {
        get => (double)GetValue(CurrentHorizontalOffsetProperty);
        set => SetValue(CurrentHorizontalOffsetProperty, value);
    }
}
