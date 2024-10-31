// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Controls;

public class PinBoxPasswordChangedEventArgs : EventArgs
{
    public string OldValue { get; }

    public string NewValue { get; }

    public PinBoxPasswordChangedEventArgs(string oldValue, string newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }
}