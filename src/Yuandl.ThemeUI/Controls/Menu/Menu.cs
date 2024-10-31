// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

/// <summary>
///
/// </summary>
public class Menu : System.Windows.Controls.Menu
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Menu"/> class.
    /// </summary>
    public Menu()
    {
        ItemContainerStyleProperty.OverrideMetadata(typeof(Menu), new FrameworkPropertyMetadata(ItemContainerStyle));
    }

    public new Style ItemContainerStyle
    {
        get
        {
            var menuItem = new MenuItem();
            menuItem.Padding = new Thickness(8, 6, 8, 6);
            var brushKey = new ComponentResourceKey(typeof(MenuItem), "ThemeUIMenuItemStyle");
            var style = (Style)menuItem.TryFindResource(brushKey);
            return style;
        }
        set => SetValue(ItemContainerStyleProperty, value);
    }

    protected override void OnInitialized(EventArgs e)
    {
        Items.CurrentChanged += ItemsOnCollectionChanged;

        base.OnInitialized(e);
    }

    private void ItemsOnCollectionChanged(object? sender, EventArgs e)
    {
        foreach (ItemCollection singleColumn in Items)
        {
        }
    }
}