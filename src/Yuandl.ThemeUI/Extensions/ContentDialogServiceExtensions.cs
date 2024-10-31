// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Controls;

namespace Yuandl.ThemeUI.Extensions;

public static class ContentDialogServiceExtensions
{
    /// <summary>
    /// Shows the simple alert-like dialog.
    /// </summary>
    /// <returns>Result of the life cycle of the <see cref="ContentDialog"/>.</returns>
    public static Task<ContentDialogResult> ShowAlertAsync(
        this IContentDialogService dialogService,
        string title,
        string message,
        string closeButtonText,
        CancellationToken cancellationToken = default
    )
    {
        var dialog = new ContentDialog();

        dialog.SetCurrentValue(ContentDialog.TitleProperty, title);
        dialog.SetCurrentValue(ContentControl.ContentProperty, message);
        dialog.SetCurrentValue(ContentDialog.CloseButtonTextProperty, closeButtonText);

        return dialogService.ShowAsync(dialog, cancellationToken);
    }
}