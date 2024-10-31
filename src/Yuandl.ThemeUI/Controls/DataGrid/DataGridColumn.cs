// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Data;

namespace Yuandl.ThemeUI.Controls;

public class DataGridComboBoxColumn : System.Windows.Controls.DataGridComboBoxColumn
{
    static DataGridComboBoxColumn()
    {
        ElementStyleProperty.OverrideMetadata(typeof(DataGridComboBoxColumn), new FrameworkPropertyMetadata(DefaultElementStyle));
        EditingElementStyleProperty.OverrideMetadata(typeof(DataGridComboBoxColumn), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
    }

    public static readonly DependencyProperty BorderThicknessProperty
        = DependencyProperty.RegisterAttached("BorderThickness", typeof(Thickness), typeof(DataGridComboBoxColumn),
            new FrameworkPropertyMetadata(new Thickness(1, 1, 1, 1), FrameworkPropertyMetadataOptions.Inherits));

    public Thickness BorderThickness
    {
        get => (Thickness)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }

    public BindingBase? ItemsSourceBinding { get; set; }

    public bool? IsEditable { get; set; }

    protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
    {
        FrameworkElement comboBox = base.GenerateElement(cell, cell);
        if (ItemsSourceBinding != null)
        {
            _ = comboBox.SetBinding(ItemsControl.ItemsSourceProperty, ItemsSourceBinding);
        }

        ApplyStyle(false, false, comboBox);

        return comboBox;
    }

    protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
    {
        var comboBox = (ComboBox)base.GenerateElement(cell, cell);
        if (IsEditable is bool isEditable)
        {
            comboBox.IsEditable = isEditable;
        }

        if (ItemsSourceBinding is { } binding)
        {
            _ = comboBox.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        ApplyStyle(true, false, comboBox);

        return comboBox;
    }

    private void ApplyStyle(bool isEditing, bool defaultToElementStyle, FrameworkElement element)
    {
        Style? style = PickStyle(isEditing, defaultToElementStyle);
        if (style != null)
        {
            element.Style = style;
        }
    }

    private Style? PickStyle(bool isEditing, bool defaultToElementStyle)
    {
        Style? style = isEditing ? EditingElementStyle : ElementStyle;
        if (isEditing && defaultToElementStyle && (style == null))
        {
            style = ElementStyle;
        }

        return style;
    }

    protected override void CancelCellEdit(FrameworkElement? editingElement, object? uneditedValue)
    {
        if (editingElement is ComboBox comboBox)
        {
            comboBox.SetCurrentValue(ComboBox.IsDropDownOpenProperty, false);
        }

        base.CancelCellEdit(editingElement, uneditedValue);
    }

    protected override bool CommitCellEdit(FrameworkElement? editingElement)
    {
        if (editingElement is ComboBox comboBox)
        {
            comboBox.SetCurrentValue(ComboBox.IsDropDownOpenProperty, false);
        }

        return base.CommitCellEdit(editingElement);
    }

    public static new Style DefaultElementStyle
    {
        get
        {
            var comboBox = new ComboBox();

            var brushKey = new ComponentResourceKey(typeof(ComboBox), "DataGridComboBoxColumnStyle");
            var style = (Style)comboBox.TryFindResource(brushKey);

            return style;
        }
    }

    public static new Style DefaultEditingElementStyle
    {
        get
        {
            var comboBox = new ComboBox();

            var brushKey = new ComponentResourceKey(typeof(ComboBox), "DataGridComboBoxColumnEditingStyle");
            var style = (Style)comboBox.TryFindResource(brushKey);

            return style;
        }
    }
}

public class DataGridTextColumn : System.Windows.Controls.DataGridTextColumn
{
    protected override object? PrepareCellForEdit(FrameworkElement? editingElement, RoutedEventArgs editingEventArgs)
    {
        if (editingElement is TextBox textBox)
        {
            textBox.MaxLength = MaxLength;
            textBox.SelectionStart = textBox.Text.Length;
        }

        _ = editingElement?.Focus();

        return null;
    }

    /// <summary>
    /// Gets or sets set the maximum length for the text field.
    /// </summary>
    /// <remarks>Not a dependency property, as is only applied once.</remarks>
    public int MaxLength { get; set; }
}

public class DataGridTemplateColumn : System.Windows.Controls.DataGridTemplateColumn
{
}

public class DataGridCheckBoxColumn : System.Windows.Controls.DataGridCheckBoxColumn
{
}

public class DataGridHyperlinkColumn : System.Windows.Controls.DataGridHyperlinkColumn
{
}