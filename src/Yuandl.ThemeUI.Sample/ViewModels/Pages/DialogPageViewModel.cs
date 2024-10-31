// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using Yuandl.ThemeUI.Sample.Controls;

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class DialogPageViewModel : ObservableObject
{
    private readonly IContentDialogService _contentDialogService;

    public DialogPageViewModel(IContentDialogService contentDialogService)
    {
        _contentDialogService = contentDialogService;
    }

    [ObservableProperty]
    private string _dialogResultText = string.Empty;

    [RelayCommand]
    private async Task OnShowDialog(object content)
    {
        ContentDialogResult result = await _contentDialogService.ShowAsync(
            new ContentDialog()
            {
                Title = "Save your work?",
                Content = content,
                PrimaryButtonText = "Save",
                SecondaryButtonText = "Don't Save",
                CloseButtonText = "Cancel",
            }, CancellationToken.None
        );

        DialogResultText = result switch
        {
            ContentDialogResult.Primary => "User saved their work",
            ContentDialogResult.Secondary => "User did not save their work",
            _ => "User cancelled the dialog"
        };
    }

    [RelayCommand]
    private async Task OnShowSignInContentDialog()
    {
        var termsOfUseContentDialog = new TermsOfUseContentDialog(_contentDialogService.GetDialogHost());

        _ = await termsOfUseContentDialog.ShowAsync();
    }
}