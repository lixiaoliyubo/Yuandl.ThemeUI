// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Controls;

public partial class PinBox
{
    private string GetPassword()
    {
        return GetPassword(Password);
    }

    private string GetPassword(string value)
    {
        if (_uniformGrid == null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        var newPassword = value.Length > PasswordLength ? value.Substring(0, PasswordLength) : value;
        return newPassword;
    }

    private char GetPasswordChar(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
        {
            return '*';
        }
        else
        {
            return newValue[0];
        }
    }

    public int? GetCurrentPasswordBoxIndex(System.Windows.Controls.PasswordBox? passwordBox)
    {
        return _uniformGrid?.Children.IndexOf(passwordBox);
    }

    public List<System.Windows.Controls.PasswordBox>? GetPinBoxList()
    {
        return _uniformGrid?.Children.Cast<System.Windows.Controls.PasswordBox>().ToList();
    }
}