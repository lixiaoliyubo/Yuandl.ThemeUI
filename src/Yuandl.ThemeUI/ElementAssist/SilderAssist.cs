// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.ElementAssist;

public static class SilderAssist
{
    /// <summary>
    /// Control whether to use gradient colors.
    /// </summary>
    public static readonly DependencyProperty IsGradientProperty
        = DependencyProperty.RegisterAttached("IsGradient", typeof(bool), typeof(SilderAssist), new PropertyMetadata(false));

    public static bool GetIsGradient(DependencyObject element) => (bool)element.GetValue(IsGradientProperty);

    public static void SetIsGradient(DependencyObject element, bool value) => element.SetValue(IsGradientProperty, value);

}