﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Controls;

public class SnackbarPresenter : ContentPresenter
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "WpfAnalyzers.DependencyProperty",
        "WPF0012:CLR property type should match registered type",
        Justification = "seems harmless"
    )]
    public new Snackbar? Content
    {
        get => (Snackbar?)GetValue(ContentProperty);
        protected set => SetValue(ContentProperty, value);
    }

    public SnackbarPresenter()
    {
        Unloaded += static (sender, _) =>
        {
            var self = (SnackbarPresenter)sender;
            self.OnUnloaded();
        };
    }

    protected Queue<Snackbar> Queue { get; } = new();

    protected CancellationTokenSource CancellationTokenSource { get; set; } = new();

    protected virtual void OnUnloaded()
    {
        CancellationTokenSource.Cancel();
        CancellationTokenSource.Dispose();
    }

    protected void ResetCancellationTokenSource()
    {
        CancellationTokenSource.Dispose();
        CancellationTokenSource = new CancellationTokenSource();
    }

    public virtual void AddToQue(Snackbar snackbar)
    {
        Queue.Enqueue(snackbar);

        if (Content is null)
        {
            _ = ShowQueuedSnackbars(); // TODO: Fix detached process
        }
    }

    public virtual async Task ImmediatelyDisplay(Snackbar snackbar)
    {
        await HideCurrent();
        await ShowSnackbar(snackbar);

        await ShowQueuedSnackbars();
    }

    public virtual async Task HideCurrent()
    {
        if (Content is null)
        {
            return;
        }

        CancellationTokenSource.Cancel();
        await HidSnackbar(Content);
        ResetCancellationTokenSource();
    }

    private async Task ShowQueuedSnackbars()
    {
        while (Queue.Count > 0 && !CancellationTokenSource.IsCancellationRequested)
        {
            Snackbar snackbar = Queue.Dequeue();

            await ShowSnackbar(snackbar);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "WpfAnalyzers.DependencyProperty",
        "WPF0041:Set mutable dependency properties using SetCurrentValue",
        Justification = "SetCurrentValue(ContentProperty, ...) will not work"
    )]
    private async Task ShowSnackbar(Snackbar snackbar)
    {
        Content = snackbar;

        snackbar.SetCurrentValue(Snackbar.IsShownProperty, true);

        try
        {
            await Task.Delay(snackbar.Timeout, CancellationTokenSource.Token);
        }
        catch
        {
            return;
        }

        await HidSnackbar(snackbar);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "WpfAnalyzers.DependencyProperty",
        "WPF0041:Set mutable dependency properties using SetCurrentValue",
        Justification = "SetCurrentValue(ContentProperty, ...) will not work"
    )]
    private async Task HidSnackbar(Snackbar snackbar)
    {
        snackbar.SetCurrentValue(Snackbar.IsShownProperty, false);

        await Task.Delay(300);

        Content = null;
    }
}