// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Controls;

public partial class PinBox
{
    public event EventHandler<PinBoxPasswordChangedEventArgs>? PasswordChanged;

    public string Password
    {
        get { return GetPassword((string)GetValue(PasswordProperty)); }
        set { SetValue(PasswordProperty, value); }
    }

    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register(nameof(Password), typeof(string), typeof(PinBox), new PropertyMetadata(default(string), OnPasswordChanged));

    public int PasswordLength
    {
        get { return (int)GetValue(PasswordLengthProperty); }
        set { SetValue(PasswordLengthProperty, value); }
    }

    public static readonly DependencyProperty PasswordLengthProperty =
        DependencyProperty.Register(nameof(PasswordLength), typeof(int), typeof(PinBox), new PropertyMetadata(4, OnPasswordLengthChanged));

    public bool PasswordRevealMode
    {
        get { return (bool)GetValue(PasswordRevealModeProperty); }
        set { SetValue(PasswordRevealModeProperty, value); }
    }

    public static readonly DependencyProperty PasswordRevealModeProperty =
        DependencyProperty.Register(nameof(PasswordRevealMode), typeof(bool), typeof(PinBox), new PropertyMetadata(true, OnPasswordRevealModeChanged));

    public string PasswordChar
    {
        get { return (string)GetValue(PasswordCharProperty); }
        set { SetValue(PasswordCharProperty, value); }
    }

    public static readonly DependencyProperty PasswordCharProperty =
        DependencyProperty.Register(nameof(PasswordChar), typeof(string), typeof(PinBox), new PropertyMetadata("●", OnPasswordCharChanged));

    public double ItemSpacing
    {
        get => (double)GetValue(ItemSpacingProperty);
        set => SetValue(ItemSpacingProperty, value);
    }

    public static readonly DependencyProperty ItemSpacingProperty = DependencyProperty.Register(
        nameof(ItemSpacing), typeof(double), typeof(PinBox), new PropertyMetadata(12.0, OnItemSpacingChanged));

    public double ItemWidth
    {
        get => (double)GetValue(ItemWidthProperty);
        set => SetValue(ItemWidthProperty, value);
    }

    public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(
        nameof(ItemWidth), typeof(double), typeof(PinBox), new PropertyMetadata(double.NaN, OnItemWidthChanged));

    public object Header
    {
        get { return (object)GetValue(HeaderProperty); }
        set { SetValue(HeaderProperty, value); }
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(object), typeof(PinBox), new PropertyMetadata(default(object)));

    public object Description
    {
        get { return (object)GetValue(DescriptionProperty); }
        set { SetValue(DescriptionProperty, value); }
    }

    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(object), typeof(PinBox), new PropertyMetadata(default(object)));

    public string PlaceholderText
    {
        get { return (string)GetValue(PlaceholderTextProperty); }
        set { SetValue(PlaceholderTextProperty, value); }
    }

    public static readonly DependencyProperty PlaceholderTextProperty =
        DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(PinBox), new PropertyMetadata(default(string), OnPlaceHolderChanged));

    public PinBoxFocusMode FocusMode
    {
        get { return (PinBoxFocusMode)GetValue(FocusModeProperty); }
        set { SetValue(FocusModeProperty, value); }
    }

    public static readonly DependencyProperty FocusModeProperty =
        DependencyProperty.Register(nameof(FocusMode), typeof(PinBoxFocusMode), typeof(PinBox), new PropertyMetadata(PinBoxFocusMode.Complete));

    public Orientation Orientation
    {
        get { return (Orientation)GetValue(OrientationProperty); }
        set { SetValue(OrientationProperty, value); }
    }

    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(PinBox), new PropertyMetadata(Orientation.Horizontal, OnOrientationChanged));
}