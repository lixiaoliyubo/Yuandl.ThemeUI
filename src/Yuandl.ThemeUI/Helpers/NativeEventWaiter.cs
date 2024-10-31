// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

using System.Windows.Threading;

namespace Yuandl.ThemeUI.Helpers;

public static class NativeEventWaiter
{
    public static void WaitForEventLoop(string eventName, Action callback, Dispatcher dispatcher, CancellationToken cancel)
    {
        new Thread(() =>
        {
            var eventHandle = new EventWaitHandle(false, EventResetMode.AutoReset, eventName);
            while (true)
            {
                if (WaitHandle.WaitAny([cancel.WaitHandle, eventHandle]) == 1)
                {
                    _ = dispatcher.BeginInvoke(callback);
                }
                else
                {
                    return;
                }
            }
        }).Start();
    }
}