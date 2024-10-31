// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Globalization;

namespace Yuandl.ThemeUI.Sample.Models;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        return string.IsNullOrWhiteSpace((value ?? string.Empty).ToString())
            ? new ValidationResult(false, "Field is required.")
            : ValidationResult.ValidResult;
    }
}