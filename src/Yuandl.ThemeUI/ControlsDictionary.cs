// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Markup;

namespace Yuandl.ThemeUI.Markup;

[Localizability(LocalizationCategory.Ignore)]
[Ambient]
[UsableDuringInitialization(true)]
public class ControlsDictionary : ResourceDictionary
{
    private const string DictionaryUri = "pack://application:,,,/Yuandl.ThemeUI;component/Resources/Yuandl.xaml";

    /// <summary>
    /// Initializes a new instance of the <see cref="ControlsDictionary"/> class.
    /// Default constructor defining <see cref="ResourceDictionary.Source"/> of the <c>WPF UI</c> ui dictionary.
    /// </summary>
    public ControlsDictionary()
    {
        Source = new Uri(DictionaryUri, UriKind.Absolute);
    }
}