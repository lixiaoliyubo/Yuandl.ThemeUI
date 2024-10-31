// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.

namespace Yuandl.ThemeUI.Sample.ViewModels.Pages;

public partial class NotificationPageViewModel : ObservableObject
{
    [RelayCommand]
    private void OnButton(object sender)
    {
        var number = new Random().Next(1, 11);
        switch (number)
        {
            case 1:
                Notification.Info("Hello");
                break;
            case 2:
                Notification.Info("Hello", "Info");
                break;
            case 3:
                Notification.Info2("Hello");
                break;
            case 4:
                Notification.Info2("Hello", "Info2");
                break;
            case 5:
                Notification.Warning("Hello");
                break;
            case 6:
                Notification.Warning("Hello", "Warning");
                break;
            case 7:
                Notification.Error("Hello");
                break;
            case 8:
                Notification.Error("Hello", "Error");
                break;
            case 9:
                Notification.Success("Hello");
                break;
            case 10:
                Notification.Success("Hello", "Success");
                break;
            case 11:
                Notification.Ask("Hello", (s, e) =>
                {
                    Notification.Info("Clicked");
                    return true;
                });
                break;
        }
    }

    [RelayCommand]
    private void OnButtonInfo(object sender)
    {
        var number = new Random().Next(1, 5);
        switch (number)
        {
            case 1:
                Notification.Info(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Info with NotificationInfo",
                });
                break;
            case 2:
                Notification.Info(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Info with NotificationInfo",
                    UseBlueColorForInfo = true,
                });
                break;
            case 3:
                Notification.Warning(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Warning with NotificationInfo"
                });
                break;
            case 4:
                Notification.Error(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Error with NotificationInfo"
                });
                break;
            case 5:
                Notification.Success(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Success with NotificationInfo"
                });
                break;
        }
    }

    [RelayCommand]
    private void OnButtonToken(object sender)
    {
        var number = new Random().Next(1, 11);
        switch (number)
        {
            case 1:
                Notification.InfoWithToken("Hello", "Test");
                break;
            case 2:
                Notification.InfoWithToken("Hello", "Info", "Test");
                break;
            case 3:
                Notification.Info2WithToken("Hello", "Test");
                break;
            case 4:
                Notification.Info2WithToken("Hello", "Info2", "Test");
                break;
            case 5:
                Notification.WarningWithToken("Hello", "Test");
                break;
            case 6:
                Notification.WarningWithToken("Hello", "Warning", "Test");
                break;
            case 7:
                Notification.ErrorWithToken("Hello", "Test");
                break;
            case 8:
                Notification.ErrorWithToken("Hello", "Error", "Test");
                break;
            case 9:
                Notification.SuccessWithToken("Hello", "Test");
                break;
            case 10:
                Notification.SuccessWithToken("Hello", "Success", "Test");
                break;
            case 11:
                Notification.AskWithToken("Hello", "Test", (s, e) =>
                {
                    Notification.Info("Clicked");
                    return true;
                });
                break;
        }
    }

    [RelayCommand]
    private void OnButtonInfoToken(object sender)
    {
        var number = new Random().Next(1, 5);
        switch (number)
        {
            case 1:
                Notification.Info(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Info with NotificationInfo",
                    Token = "Test"
                });
                break;
            case 2:
                Notification.Info(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Info with NotificationInfo",
                    UseBlueColorForInfo = true,
                    Token = "Test"
                });
                break;
            case 3:
                Notification.Warning(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Warning with NotificationInfo",
                    Token = "Test"
                });
                break;
            case 4:
                Notification.Error(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Error with NotificationInfo",
                    Token = "Test"
                });
                break;
            case 5:
                Notification.Success(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Success with NotificationInfo",
                    Token = "Test"
                });
                break;
        }
    }

    [RelayCommand]
    private void OnButtonClearAll(object sender)
    {
        Notification.Clear();
        Notification.Clear("Test");
    }

    [RelayCommand]
    private void OnButtonGlobal(object sender)
    {
        var number = new Random().Next(1, 12);
        switch (number)
        {
            case 1:
                Notification.InfoGlobal("Hello");
                break;
            case 2:
                Notification.InfoGlobal("Hello", "Info");
                break;
            case 3:
                Notification.Info2Global("Hello");
                break;
            case 4:
                Notification.Info2Global("Hello", "Info2");
                break;
            case 5:
                Notification.WarningGlobal("Hello");
                break;
            case 6:
                Notification.WarningGlobal("Hello", "Warning");
                break;
            case 7:
                Notification.ErrorGlobal("Hello");
                break;
            case 8:
                Notification.ErrorGlobal("Hello", "Error");
                break;
            case 9:
                Notification.SuccessGlobal("Hello");
                break;
            case 10:
                Notification.SuccessGlobal("Hello", "Success");
                break;
            case 11:
                Notification.AskGlobal("Hello", (s, e) =>
                {
                    Notification.Info("Clicked");
                    return true;
                });
                break;
        }
    }

    [RelayCommand]
    private void OnButtonInfoGlobal(object sender)
    {
        var number = new Random().Next(1, 5);
        switch (number)
        {
            case 1:
                Notification.InfoGlobal(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Info with NotificationInfo"
                });
                break;
            case 2:
                Notification.InfoGlobal(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Info with NotificationInfo",
                    UseBlueColorForInfo = true
                });
                break;
            case 3:
                Notification.WarningGlobal(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Warning with NotificationInfo"
                });
                break;
            case 4:
                Notification.ErrorGlobal(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Error with NotificationInfo"
                });
                break;
            case 5:
                Notification.SuccessGlobal(new NotificationInfo
                {
                    ShowDateTime = true,
                    StaysOpen = true,
                    IsClosable = false,
                    Title = "Hello",
                    Message = "Success with NotificationInfo"
                });
                break;
        }
    }

    [RelayCommand]
    private void OnButtonClearGlobalAll(object sender)
    {
        Notification.ClearGlobal();
    }
}