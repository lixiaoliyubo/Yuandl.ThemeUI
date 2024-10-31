// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Security;
using System.Windows.Input;

namespace Yuandl.ThemeUI.Controls;

[TemplatePart(Name = nameof(PART_Panel), Type = typeof(UniformGrid))]
public partial class PinBox : System.Windows.Controls.Control
{
    private readonly string PART_Panel = "PART_Panel";
    private UniformGrid? _uniformGrid;
    private List<SecureString>? _passwordList;

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _uniformGrid = GetTemplateChild(nameof(PART_Panel)) as UniformGrid;
        UpdateOrientation(Orientation);
        CreatePinBoxes(PasswordLength);
        UpdatePinBoxes(GetPassword(), PasswordLength);

        ForegroundProperty.OverrideMetadata(typeof(PinBox), new FrameworkPropertyMetadata(Brushes.Transparent, new PropertyChangedCallback(OnForegroundChanged)));
        BackgroundProperty.OverrideMetadata(typeof(PinBox), new FrameworkPropertyMetadata(Brushes.Transparent, new PropertyChangedCallback(OnBackgroundChanged)));
        BorderBrushProperty.OverrideMetadata(typeof(PinBox), new FrameworkPropertyMetadata(Brushes.Transparent, new PropertyChangedCallback(OnBorderBrushChanged)));
        BorderThicknessProperty.OverrideMetadata(typeof(PinBox), new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0), new PropertyChangedCallback(OnBorderThicknessChanged)));
    }

    private void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (_uniformGrid != null)
        {
            foreach (System.Windows.Controls.PasswordBox box in _uniformGrid.Children)
            {
                box.Foreground = Foreground;
            }
        }
    }

    private void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (_uniformGrid != null)
        {
            foreach (System.Windows.Controls.PasswordBox box in _uniformGrid.Children)
            {
                box.Background = Background;
            }
        }
    }

    private void OnBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (_uniformGrid != null)
        {
            foreach (System.Windows.Controls.PasswordBox box in _uniformGrid.Children)
            {
                box.BorderBrush = BorderBrush;
            }
        }
    }

    private void OnBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (_uniformGrid != null)
        {
            foreach (System.Windows.Controls.PasswordBox box in _uniformGrid.Children)
            {
                box.BorderThickness = BorderThickness;
            }
        }
    }

    private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (PinBox)d;
        if (ctl != null && ctl._uniformGrid != null)
        {
            string newValue = ctl.GetPassword();
            ctl.UpdatePinBoxes(newValue, ctl.PasswordLength);
            ctl.PasswordChanged?.Invoke(ctl, new PinBoxPasswordChangedEventArgs((string)e.OldValue, newValue));
        }
    }

    private static void OnPasswordLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (PinBox)d;

        if (ctl != null)
        {
            ctl.CreatePinBoxes((int)e.NewValue);
            ctl.UpdatePinBoxes(ctl.GetPassword(), (int)e.NewValue);
            ctl.UpdateOrientation(ctl.Orientation);
        }
    }

    private static void OnPasswordRevealModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (PinBox)d;
        if (ctl._uniformGrid != null)
        {
            foreach (System.Windows.Controls.PasswordBox box in ctl._uniformGrid.Children)
            {
                // box.RevealButtonEnabled = (bool)e.NewValue;
            }
        }
    }

    private static void OnPasswordCharChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (PinBox)d;
        if (ctl._uniformGrid != null)
        {
            foreach (System.Windows.Controls.PasswordBox box in ctl._uniformGrid.Children)
            {
                box.PasswordChar = ctl.GetPasswordChar((string)e.NewValue);
            }
        }
    }

    private static void OnItemSpacingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (PinBox)d;
        if (ctl != null)
        {
            ctl.UpdateOrientation(ctl.Orientation);
        }
    }

    private static void OnItemWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (PinBox)d;
        if (ctl != null && ctl._uniformGrid != null)
        {
            ctl.CreatePinBoxes(ctl.PasswordLength);
            ctl.UpdatePinBoxes(ctl.GetPassword(), ctl.PasswordLength);
        }
    }

    private static void OnPlaceHolderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (PinBox)d;
        ctl.UpdatePlaceHolderText((string)e.NewValue);
    }

    private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (PinBox)d;
        if (ctl != null)
        {
            ctl.UpdateOrientation((Orientation)e.NewValue);
        }
    }

    private void OnBoxPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (_uniformGrid == null)
        {
            return;
        }

        var currentBox = sender as System.Windows.Controls.PasswordBox;
        var currentBoxIndex = GetCurrentPasswordBoxIndex(currentBox) ?? 0;

        int nextBoxIndex = currentBoxIndex + 1;

        if (string.IsNullOrEmpty(currentBox.Password))
        {
            nextBoxIndex = currentBoxIndex - 1;
        }

        FocusBoxAndSelectAll(nextBoxIndex);

        var password = string.Join(string.Empty, _uniformGrid.Children.OfType<System.Windows.Controls.PasswordBox>().Select(item => item.Password));
        Password = password;
    }

    private void OnBoxKeyDown(object sender, KeyEventArgs e)
    {
        if (_uniformGrid != null)
        {
            var currentBox = sender as System.Windows.Controls.PasswordBox;
            var currentBoxIndex = GetCurrentPasswordBoxIndex(currentBox) ?? 0;

            switch (Orientation)
            {
                case Orientation.Vertical:
                    if (e.Key == Key.Up || e.Key == Key.PageUp)
                    {
                        var nextBoxIndex = currentBoxIndex - 1;
                        FocusBoxAndSelectAll(nextBoxIndex);
                    }

                    if (e.Key == Key.Down || e.Key == Key.PageDown)
                    {
                        var nextBoxIndex = currentBoxIndex + 1;
                        FocusBoxAndSelectAll(nextBoxIndex);
                    }

                    break;
                case Orientation.Horizontal:
                    if (e.Key == Key.Left || e.Key == Key.PageDown)
                    {
                        var nextBoxIndex = currentBoxIndex - 1;
                        FocusBoxAndSelectAll(nextBoxIndex);
                    }

                    if (e.Key == Key.Right || e.Key == Key.PageUp)
                    {
                        var nextBoxIndex = currentBoxIndex + 1;
                        FocusBoxAndSelectAll(nextBoxIndex);
                    }

                    break;
            }

            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var nextBoxIndex = currentBoxIndex - 1;
                currentBox.Password = string.Empty;
                FocusBoxAndSelectAll(nextBoxIndex);
            }
        }
    }

    private void OnBoxGettingFocus(object sender, RoutedEventArgs args)
    {
        var currentBox = sender as System.Windows.Controls.PasswordBox;
        if (currentBox != null)
        {
            currentBox.SelectAll();
            UpdateFocusMode(currentBox);
        }
    }

    private void CreatePinBoxes(int passwordLength)
    {
        if (passwordLength == 0 || passwordLength < 0)
        {
            passwordLength = 4;
        }

        if (_uniformGrid == null)
        {
            return;
        }

        _uniformGrid.Children.Clear();

        for (int i = 1; i <= passwordLength; i++)
        {
            System.Windows.Controls.PasswordBox pinBox = CreatePinBox(i);
            _ = _uniformGrid.Children.Add(pinBox);
        }
    }

    private System.Windows.Controls.PasswordBox CreatePinBox(int index)
    {
        string password = null;

        if (_passwordList != null && index - 1 < _passwordList.Count)
        {
            SecureString secureString = _passwordList[index - 1];
            password = new System.Net.NetworkCredential(string.Empty, secureString).Password;
        }

        // var style = (Style)TryFindResource("PinBoxPasswordBoxStyle");
        var passwordBox = new System.Windows.Controls.PasswordBox()
        {
            MaxLength = 1,
            TabIndex = index,
            Name = $"passwordBoxItem{index}",
            Width = ItemWidth,
            Padding = new Thickness(0, 0, 0, 0),

            // PlaceholderText = "*",
            Margin = new Thickness(ItemSpacing, 0, ItemSpacing, 0),
            PasswordChar = GetPasswordChar(PasswordChar),
            Foreground = this.Foreground,
            Background = this.Background,
            BorderBrush = this.BorderBrush,
            BorderThickness = this.BorderThickness,
            FlowDirection = this.FlowDirection,
            IsEnabled = this.IsEnabled,
            Visibility = this.Visibility,

            // ClearButtonEnabled = false,
            // RevealButtonEnabled = this.PasswordRevealMode,
            // Style= style
        };

        if (!string.IsNullOrEmpty(password))
        {
            passwordBox.Password = password;
        }

        passwordBox.PasswordChanged += OnBoxPasswordChanged;
        passwordBox.PreviewKeyDown += OnBoxKeyDown;
        passwordBox.GotFocus += OnBoxGettingFocus;
        return passwordBox;
    }

    private void UpdatePinBoxes(string value, int passwordLength)
    {
        if (_uniformGrid == null)
        {
            FillPasswordList(value);
            return;
        }

        FillPasswordList(value);

        if (string.IsNullOrEmpty(value) || passwordLength <= 0)
        {
            foreach (System.Windows.Controls.PasswordBox passwordBox in _uniformGrid.Children.OfType<System.Windows.Controls.PasswordBox>())
            {
                passwordBox.Password = string.Empty;
            }
        }
        else
        {
            int count = Math.Min(passwordLength, value.Length);
            var passwordBoxes = _uniformGrid.Children.OfType<System.Windows.Controls.PasswordBox>().Take(count).ToList();

            for (int index = 0; index < count; index++)
            {
                passwordBoxes[index].Password = value[index].ToString();
            }

            IEnumerable<System.Windows.Controls.PasswordBox> passwordBoxesToClear = _uniformGrid.Children
                .OfType<System.Windows.Controls.PasswordBox>()
                .Skip(value.Length)
                .Take(passwordLength - value.Length);

            foreach (System.Windows.Controls.PasswordBox? passwordBox in passwordBoxesToClear)
            {
                passwordBox.Password = string.Empty;
            }
        }

        UpdatePlaceHolderText(PlaceholderText);
    }

    private void UpdatePlaceHolderText(string newValue)
    {
        if (_uniformGrid != null)
        {
            foreach (System.Windows.Controls.PasswordBox box in _uniformGrid.Children)
            {
                // box.PlaceholderText = string.Empty;
            }

            if (!string.IsNullOrEmpty(newValue))
            {
                string chars;

                if (newValue.Length < PasswordLength)
                {
                    chars = newValue.Substring(0, newValue.Length);
                }
                else
                {
                    chars = newValue.Substring(0, PasswordLength);
                }

                List<char> charsList = new List<char>(chars);
                for (int i = 0; i < charsList.Count; i++)
                {
                    _ = _uniformGrid.Children[i] as System.Windows.Controls.PasswordBox;

                    // box.PlaceholderText = charsList[i].ToString();
                }
            }
        }
    }

    public void UpdateFocusMode(System.Windows.Controls.PasswordBox passwordBox)
    {
        switch (FocusMode)
        {
            case PinBoxFocusMode.Normal:
                _ = VisualStateManager.GoToState(passwordBox, "NormalFocused", true);
                break;
            case PinBoxFocusMode.Complete:
                _ = VisualStateManager.GoToState(passwordBox, "CompleteFocused", true);
                break;
            default:
                _ = VisualStateManager.GoToState(passwordBox, "CompleteFocused", true);
                break;
        }
    }

    private void FillPasswordList(string value)
    {
        if (_passwordList == null)
        {
            _passwordList = new List<SecureString>();
        }
        else
        {
            _passwordList.Clear();
        }

        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        foreach (var item in value)
        {
            var secureString = new SecureString();
            secureString.AppendChar(item);
            _passwordList.Add(secureString);
        }
    }

    private void FocusBoxAndSelectAll(int nextBoxIndex)
    {
        if (nextBoxIndex != -1 && nextBoxIndex <= _uniformGrid.Children.Count - 1)
        {
            System.Windows.Controls.PasswordBox nextBox = (System.Windows.Controls.PasswordBox)_uniformGrid.Children[nextBoxIndex];
            _ = nextBox.Focus();
            nextBox.SelectAll();
        }
    }

    private void UpdateOrientation(Orientation orientation)
    {
        if (_uniformGrid != null)
        {
            switch (orientation)
            {
                case Orientation.Vertical:
                    _uniformGrid.Rows = PasswordLength;
                    _uniformGrid.RowSpacing = ItemSpacing;
                    _uniformGrid.Columns = 1;
                    break;
                case Orientation.Horizontal:

                    _uniformGrid.Rows = 1;
                    _uniformGrid.Columns = PasswordLength;
                    _uniformGrid.ColumnSpacing = ItemSpacing;
                    break;
            }
        }
    }
}