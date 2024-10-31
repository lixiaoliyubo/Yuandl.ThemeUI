// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Windows.Controls.Primitives;
using Yuandl.ThemeUI.Helpers;

namespace Yuandl.ThemeUI.Controls;

[TemplatePart(Name = ProgressBarTemplateName, Type = typeof(ProgressBar))]
public class Step : ItemsControl
{
    private const string ProgressBarTemplateName = "PART_ProgressBar";
    private ProgressBar _progressBar;

    public Orientation Orientation
    {
        get { return (Orientation)GetValue(OrientationProperty); }
        set { SetValue(OrientationProperty, value); }
    }

    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(Step), new PropertyMetadata(Orientation.Horizontal));

    public static readonly RoutedEvent StepChangedEvent =
        EventManager.RegisterRoutedEvent(nameof(StepChanged), RoutingStrategy.Bubble,
            typeof(TypedEventHandler<Step, RoutedEventArgs>), typeof(Step));

    [Category("Behavior")]
    public event TypedEventHandler<Step, RoutedEventArgs> StepChanged
    {
        add => AddHandler(StepChangedEvent, value);
        remove => RemoveHandler(StepChangedEvent, value);
    }

    public int StepIndex
    {
        get => (int)GetValue(StepIndexProperty);
        set => SetValue(StepIndexProperty, value);
    }

    public static readonly DependencyProperty StepIndexProperty = DependencyProperty.Register(
       nameof(StepIndex), typeof(int), typeof(Step), new PropertyMetadata(0, OnStepIndexChanged));

    private static void OnStepIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var step = (Step)d;
        var stepIndex = (int)e.NewValue;
        step.UpdateStepItemState(stepIndex);
    }

    private void UpdateStepItemState(int stepIndex)
    {
        var count = Items.Count;
        if (count <= 0)
        {
            return;
        }

        if (stepIndex >= count)
        {
            StepIndex--;
            return;
        }

        if (stepIndex < 0)
        {
            StepIndex++;
            return;
        }

        for (var i = 0; i < stepIndex; i++)
        {
            if (ItemContainerGenerator.ContainerFromIndex(i) is StepItem stepItem)
            {
                stepItem.Status = Status.Complete;
            }
        }

        if (ItemContainerGenerator.ContainerFromIndex(stepIndex) is StepItem itemInProgress)
        {
            itemInProgress.Status = Status.InProgress;
        }

        for (var i = stepIndex + 1; i < Items.Count; i++)
        {
            if (ItemContainerGenerator.ContainerFromIndex(i) is StepItem stepItem)
            {
                stepItem.Status = Status.Waiting;
            }
        }

        _progressBar?.BeginAnimation(RangeBase.ValueProperty, AnimationHelper.CreateAnimation(stepIndex));

        RaiseEvent(new StepChangedEventArgs(StepChangedEvent, this) { Value = stepIndex });
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _progressBar = (ProgressBar)GetTemplateChild(ProgressBarTemplateName);
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
        var count = Items.Count;
        if (_progressBar == null || count <= 0)
        {
            return;
        }

        _progressBar.Maximum = count - 1;

        _progressBar.Value = StepIndex;

        if (Orientation == Orientation.Horizontal)
        {
            _progressBar.Width = (ActualWidth / count) * (count - 1);
        }
        else
        {
            _progressBar.Height = (ActualHeight / count) * (count - 1);
        }
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is StepItem;
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        return new StepItem();
    }

    public Step()
    {
        ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
    }

    public void Next()
    {
        StepIndex++;
    }

    public void Previous()
    {
        StepIndex--;
    }

    private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
    {
        if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
        {
            var count = Items.Count;

            if (count <= 0)
            {
                return;
            }

            UpdateStepItemState(StepIndex);
        }
    }
}

/// <summary>
/// 状态值
/// </summary>
public enum Status
{
    /// <summary>
    /// 等待中
    /// </summary>
    Waiting,

    /// <summary>
    /// 正在进行中
    /// </summary>
    InProgress,

    /// <summary>
    /// 完成
    /// </summary>
    Complete
}