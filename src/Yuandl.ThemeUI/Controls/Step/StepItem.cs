// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Controls;

/// <summary>
///
/// </summary>
public class StepItem : ContentControl
{
    public static readonly DependencyProperty IndexProperty = DependencyProperty.Register(
        nameof(Index), typeof(int), typeof(StepItem), new PropertyMetadata(-1));

    public int Index
    {
        get => (int)GetValue(IndexProperty);
        internal set => SetValue(IndexProperty, value);
    }

    public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
        nameof(Status), typeof(Status), typeof(StepItem), new PropertyMetadata(Status.Waiting));

    public Status Status
    {
        get => (Status)GetValue(StatusProperty);
        internal set => SetValue(StatusProperty, value);
    }
}