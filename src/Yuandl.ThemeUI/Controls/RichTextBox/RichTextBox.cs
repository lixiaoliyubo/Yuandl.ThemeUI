// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Documents;

namespace Yuandl.ThemeUI.Controls;

/// <summary>
/// Extends the <see cref="System.Windows.Controls.RichTextBox"/> control with additional properties.
/// </summary>
public class RichTextBox : System.Windows.Controls.RichTextBox
{
    /// <summary>Identifies the <see cref="IsTextSelectionEnabled"/> dependency property.</summary>
    public static readonly DependencyProperty IsTextSelectionEnabledProperty = DependencyProperty.Register(
        nameof(IsTextSelectionEnabled),
        typeof(bool),
        typeof(RichTextBox),
        new PropertyMetadata(false)
    );

    /// <summary>
    /// Gets or sets a value indicating whether the user can select text in the control.
    /// </summary>
    public bool IsTextSelectionEnabled
    {
        get => (bool)GetValue(IsTextSelectionEnabledProperty);
        set => SetValue(IsTextSelectionEnabledProperty, value);
    }

    /// <summary>Identifies the <see cref="PlaceholderText"/> dependency property.</summary>
    public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(
        nameof(PlaceholderText),
        typeof(string),
        typeof(RichTextBox),
        new PropertyMetadata(string.Empty)
    );

    /// <summary>Identifies the <see cref="PlaceholderVisible"/> dependency property.</summary>
    public static readonly DependencyProperty PlaceholderVisibleProperty = DependencyProperty.Register(
        nameof(PlaceholderVisible),
        typeof(Visibility),
        typeof(RichTextBox),
        new PropertyMetadata(Visibility.Collapsed)
    );

    /// <summary>
    /// Gets or sets numbers pattern.
    /// </summary>
    public string PlaceholderText
    {
        get => (string)GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display the placeholder text.
    /// </summary>
    public Visibility PlaceholderVisible
    {
        get => (Visibility)GetValue(PlaceholderVisibleProperty);
        set => SetValue(PlaceholderVisibleProperty, value);
    }

    public RichTextBox()
    {
        // 订阅 TextChanged 事件
        // this.TextChanged += OnTextChanged;
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        TextPointer start = Document.ContentStart;
        TextPointer end = Document.ContentEnd;

        // 检查内容是否为空
        PlaceholderVisible = (start.GetOffsetToPosition(end) == 0 || string.IsNullOrWhiteSpace(new TextRange(start, end).Text)) ? Visibility.Visible : Visibility.Collapsed;
    }
}