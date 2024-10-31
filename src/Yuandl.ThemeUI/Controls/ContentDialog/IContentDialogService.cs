// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Controls;

public interface IContentDialogService
{
    /// <summary>
    /// Sets the <see cref="ContentPresenter"/>
    /// </summary>
    /// <param name="dialogHost"><see cref="ContentPresenter"/> inside of which the dialogue will be placed. The new <see cref="ContentDialog"/> will replace the current <see cref="ContentPresenter.Content"/>.</param>
    void SetDialogHost(ContentPresenter dialogHost);

    /// <summary>
    /// Provides direct access to the <see cref="ContentPresenter"/>
    /// </summary>
    /// <returns>Reference to the currently selected <see cref="ContentPresenter"/> which displays the <see cref="ContentDialog"/>'s.</returns>
    ContentPresenter? GetDialogHost();

    /// <summary>
    /// Asynchronously shows the specified dialog.
    /// </summary>
    /// <param name="dialog">The dialog to be displayed.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the dialog result.</returns>
    Task<ContentDialogResult> ShowAsync(ContentDialog dialog, CancellationToken cancellationToken);
}