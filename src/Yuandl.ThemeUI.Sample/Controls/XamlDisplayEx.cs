// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Sample.Controls;

public static class XamlDisplayEx
{
    public static readonly DependencyProperty ButtonDockProperty = DependencyProperty.RegisterAttached(
        "ButtonDock", typeof(Dock), typeof(XamlDisplayEx), new PropertyMetadata(default(Dock)));

    public static void SetButtonDock(DependencyObject element, Dock value)
    {
        element.SetValue(ButtonDockProperty, value);
    }

    public static Dock GetButtonDock(DependencyObject element)
    {
        return (Dock)element.GetValue(ButtonDockProperty);
    }
}