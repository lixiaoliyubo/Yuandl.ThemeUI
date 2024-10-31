// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Controls;
public class ContentDialogService : IContentDialogService
{
    private ContentPresenter? _dialogHost;

    public void SetDialogHost(ContentPresenter contentPresenter)
    {
        _dialogHost = contentPresenter;
    }

    public ContentPresenter? GetDialogHost()
    {
        return _dialogHost;
    }

    public Task<ContentDialogResult> ShowAsync(ContentDialog dialog, CancellationToken cancellationToken)
    {
        if (_dialogHost == null)
        {
            throw new InvalidOperationException("The DialogHost was never set.");
        }

        if (dialog.DialogHost != null && _dialogHost != dialog.DialogHost)
        {
            throw new InvalidOperationException(
                "The DialogHost is not the same as the one that was previously set."
            );
        }

        dialog.DialogHost = _dialogHost;

        return dialog.ShowAsync(cancellationToken);
    }
}