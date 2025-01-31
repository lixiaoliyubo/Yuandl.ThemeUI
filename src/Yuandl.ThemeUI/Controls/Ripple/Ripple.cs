// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Yuandl.ThemeUI.Controls;

[TemplateVisualState(GroupName = "CommonStates", Name = TemplateStateNormal)]
[TemplateVisualState(GroupName = "CommonStates", Name = TemplateStateMousePressed)]
[TemplateVisualState(GroupName = "CommonStates", Name = TemplateStateMouseOut)]
public class Ripple : ContentControl
{
    public const string TemplateStateNormal = "Normal";
    public const string TemplateStateMousePressed = "MousePressed";
    public const string TemplateStateMouseOut = "MouseOut";

    private static readonly HashSet<Ripple> PressedInstances = new HashSet<Ripple>();

    static Ripple()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Ripple), new FrameworkPropertyMetadata(typeof(Ripple)));

        EventManager.RegisterClassHandler(typeof(ContentControl), Mouse.PreviewMouseUpEvent, new MouseButtonEventHandler(MouseButtonEventHandler), true);
        EventManager.RegisterClassHandler(typeof(ContentControl), Mouse.MouseMoveEvent, new MouseEventHandler(MouseMoveEventHandler), true);
        EventManager.RegisterClassHandler(typeof(Popup), Mouse.PreviewMouseUpEvent, new MouseButtonEventHandler(MouseButtonEventHandler), true);
        EventManager.RegisterClassHandler(typeof(Popup), Mouse.MouseMoveEvent, new MouseEventHandler(MouseMoveEventHandler), true);
    }

    public Ripple()
    {
        SizeChanged += OnSizeChanged;
    }

    private static readonly DependencyPropertyKey RippleSizePropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(RippleSize), typeof(double), typeof(Ripple), new PropertyMetadata(default(double)));

    public static readonly DependencyProperty RippleSizeProperty =
        RippleSizePropertyKey.DependencyProperty;

    public double RippleSize
    {
        get => (double)GetValue(RippleSizeProperty);
        private set => SetValue(RippleSizePropertyKey, value);
    }

    private static readonly DependencyPropertyKey RippleXPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(RippleX), typeof(double), typeof(Ripple), new PropertyMetadata(default(double)));

    public static readonly DependencyProperty RippleXProperty = RippleXPropertyKey.DependencyProperty;

    public double RippleX
    {
        get => (double)GetValue(RippleXProperty);
        private set => SetValue(RippleXPropertyKey, value);
    }

    private static readonly DependencyPropertyKey RippleYPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(RippleY), typeof(double), typeof(Ripple), new PropertyMetadata(default(double)));

    public static readonly DependencyProperty RippleYProperty = RippleYPropertyKey.DependencyProperty;

    public double RippleY
    {
        get => (double)GetValue(RippleYProperty);
        private set => SetValue(RippleYPropertyKey, value);
    }

    /// <summary>
    ///   The DependencyProperty for the RecognizesAccessKey property.
    ///   Default Value: false
    /// </summary>
    public static readonly DependencyProperty RecognizesAccessKeyProperty =
        DependencyProperty.Register(nameof(RecognizesAccessKey), typeof(bool), typeof(Ripple), new PropertyMetadata(default(bool)));

    /// <summary> Gets or sets a value indicating whether
    ///   Determine if Ripple should use AccessText in its style
    /// </summary>
    public bool RecognizesAccessKey
    {
        get => (bool)GetValue(RecognizesAccessKeyProperty);
        set => SetValue(RecognizesAccessKeyProperty, value);
    }

    /// <summary>
    /// Set to <c>true</c> to cause the ripple to originate from the centre of the
    /// content.  Otherwise the effect will originate from the mouse down position.
    /// </summary>
    public static readonly DependencyProperty IsCenteredProperty = DependencyProperty.Register(
        nameof(IsCentered), typeof(bool), typeof(Ripple), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

    public bool IsCentered
    {
        get => (bool)GetValue(IsCenteredProperty);
        set => SetValue(IsCenteredProperty, value);
    }

    /// <summary>
    /// Set to <c>True</c> to disable ripple effect
    /// </summary>
    public static readonly DependencyProperty IsDisabledProperty = DependencyProperty.RegisterAttached(
        nameof(IsDisabled), typeof(bool), typeof(Ripple), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

    public bool IsDisabled
    {
        get => (bool)GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    public static readonly DependencyProperty SizeMultiplier = DependencyProperty.Register(
        nameof(RippleSizeMultiplier), typeof(double), typeof(Ripple), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.Inherits));

    public double RippleSizeMultiplier
    {
        get => (double)GetValue(SizeMultiplier);
        set => SetValue(SizeMultiplier, value);
    }

    public static readonly DependencyProperty FeedbackProperty = DependencyProperty.Register(
        nameof(Feedback), typeof(Brush), typeof(Ripple), new FrameworkPropertyMetadata(default(Brush),  FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender));

    public Brush? Feedback
    {
        get => (Brush?)GetValue(FeedbackProperty);
        set => SetValue(FeedbackProperty, value);
    }

    public static readonly DependencyProperty OnTopProperty = DependencyProperty.Register(
        nameof(OnTop), typeof(bool), typeof(Ripple), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender));

    public bool OnTop
    {
        get => (bool)GetValue(OnTopProperty);
        set => SetValue(OnTopProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _ = VisualStateManager.GoToState(this, TemplateStateNormal, false);
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
    {
        var innerContent = Content as FrameworkElement;

        double width, height;

        if (IsCentered && innerContent != null)
        {
            width = innerContent.ActualWidth;
            height = innerContent.ActualHeight;
        }
        else
        {
            width = sizeChangedEventArgs.NewSize.Width;
            height = sizeChangedEventArgs.NewSize.Height;
        }

        var radius = Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height, 2));

        RippleSize = 2 * radius * RippleSizeMultiplier;
    }

    private static void MouseButtonEventHandler(object sender, MouseButtonEventArgs e)
    {
        foreach (Ripple ripple in PressedInstances)
        {
            // adjust the transition scale time according to the current animated scale
            var scaleTrans = ripple.Template.FindName("ScaleTransform", ripple) as ScaleTransform;
            if (scaleTrans != null)
            {
                double currentScale = scaleTrans.ScaleX;
                var newTime = TimeSpan.FromMilliseconds(300 * (1.0 - currentScale));

                // change the scale animation according to the current scale
                var scaleXKeyFrame = ripple.Template.FindName("MousePressedToNormalScaleXKeyFrame", ripple) as EasingDoubleKeyFrame;
                if (scaleXKeyFrame != null)
                {
                    scaleXKeyFrame.KeyTime = KeyTime.FromTimeSpan(newTime);
                }

                var scaleYKeyFrame = ripple.Template.FindName("MousePressedToNormalScaleYKeyFrame", ripple) as EasingDoubleKeyFrame;
                if (scaleYKeyFrame != null)
                {
                    scaleYKeyFrame.KeyTime = KeyTime.FromTimeSpan(newTime);
                }
            }

            _ = VisualStateManager.GoToState(ripple, TemplateStateNormal, true);
        }

        PressedInstances.Clear();
    }

    private static void MouseMoveEventHandler(object sender, MouseEventArgs e)
    {
#if NET6_0_OR_GREATER
        DispatcherExtensions.Invoke(Dispatcher.CurrentDispatcher, () =>
                {
                    foreach (Ripple? ripple in PressedInstances.ToList())
                    {
                        Point relativePosition = Mouse.GetPosition(ripple);
                        if (relativePosition.X < 0 || relativePosition.Y < 0 || relativePosition.X >= ripple.ActualWidth || relativePosition.Y >= ripple.ActualHeight)
                        {
                            VisualStateManager.GoToState(ripple, TemplateStateMouseOut, true);
                            PressedInstances.Remove(ripple);
                        }
                    }
                });
#else
        if (Dispatcher.CurrentDispatcher.CheckAccess())
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                foreach (var ripple in PressedInstances.ToList())
                {
                    var relativePosition = Mouse.GetPosition(ripple);
                    if (relativePosition.X < 0 || relativePosition.Y < 0 || relativePosition.X >= ripple.ActualWidth || relativePosition.Y >= ripple.ActualHeight)
                    {
                        VisualStateManager.GoToState(ripple, TemplateStateMouseOut, true);
                        PressedInstances.Remove(ripple);
                    }
                }
            });
        }
#endif
    }

    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (IsCentered)
        {
            var innerContent = Content as FrameworkElement;

            if (innerContent != null)
            {
                Point position = innerContent.TransformToAncestor(this)
                    .Transform(new Point(0, 0));

                if (FlowDirection == FlowDirection.RightToLeft)
                {
                    RippleX = position.X - (innerContent.ActualWidth / 2) - (RippleSize / 2);
                }
                else
                {
                    RippleX = position.X + (innerContent.ActualWidth / 2) - (RippleSize / 2);
                }

                RippleY = position.Y + (innerContent.ActualHeight / 2) - (RippleSize / 2);
            }
            else
            {
                RippleX = (ActualWidth / 2) - (RippleSize / 2);
                RippleY = (ActualHeight / 2) - (RippleSize / 2);
            }
        }
        else
        {
            Point point = e.GetPosition(this);
            RippleX = point.X - (RippleSize / 2);
            RippleY = point.Y - (RippleSize / 2);
        }

        if (!IsDisabled)
        {
            _ = VisualStateManager.GoToState(this, TemplateStateNormal, false);
            _ = VisualStateManager.GoToState(this, TemplateStateMousePressed, true);
            _ = PressedInstances.Add(this);
        }

        if (!IsDisabled)
        {
            _ = VisualStateManager.GoToState(this, TemplateStateNormal, false);
            _ = VisualStateManager.GoToState(this, TemplateStateMousePressed, true);
            _ = PressedInstances.Add(this);
        }

        base.OnPreviewMouseLeftButtonDown(e);
    }
}