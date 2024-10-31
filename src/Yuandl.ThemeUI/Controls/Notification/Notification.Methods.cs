// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Enums;

namespace Yuandl.ThemeUI.Controls;

public partial class Notification : InfoBar
{
    public static void Info(string title) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Info
    });

    public static void InfoGlobal(string title) => InitNotificationGlobal(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Info
    });

    public static void InfoWithToken(string title, string token) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Info,
        Token = token
    });

    public static void Info(string title, string message) => InitNotification(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Info
    });

    public static void InfoGlobal(string title, string message) => InitNotificationGlobal(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Info
    });

    public static void InfoWithToken(string title, string message, string token) => InitNotification(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Info,
        Token = token
    });

    public static void Info(NotificationInfo growlInfo) => InitNotification(growlInfo, true, ControlAppearance.Info);

    public static void Info1(NotificationInfo growlInfo) => InitNotification(growlInfo);

    public static void InfoGlobal(NotificationInfo growlInfo) => InitNotificationGlobal(growlInfo, true, ControlAppearance.Info);

    public static void Info2(string title) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Info,
        UseBlueColorForInfo = true
    });

    public static void Info2Global(string title) => InitNotificationGlobal(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Info,
        UseBlueColorForInfo = true
    });

    public static void Info2WithToken(string title, string token) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Info,
        UseBlueColorForInfo = true,
        Token = token
    });

    public static void Info2(string title, string message) => InitNotification(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Info,
        UseBlueColorForInfo = true
    });

    public static void Info2Global(string title, string message) => InitNotificationGlobal(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Info,
        UseBlueColorForInfo = true
    });

    public static void Info2WithToken(string title, string message, string token) => InitNotification(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Info,
        UseBlueColorForInfo = true,
        Token = token
    });

    public static void Success(string title) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Success
    });

    public static void SuccessGlobal(string title) => InitNotificationGlobal(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Success
    });

    public static void SuccessWithToken(string title, string token) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Success,
        Token = token
    });

    public static void Success(string title, string message) => InitNotification(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Success
    });

    public static void SuccessGlobal(string title, string message) => InitNotificationGlobal(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Success
    });

    public static void SuccessWithToken(string title, string message, string token) => InitNotification(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Success,
        Token = token
    });

    public static void Success(NotificationInfo growlInfo) => InitNotification(growlInfo, true, ControlAppearance.Success);

    public static void SuccessGlobal(NotificationInfo growlInfo) => InitNotificationGlobal(growlInfo, true, ControlAppearance.Success);

    public static void Warning(string title) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Warning,
        StaysOpen = true
    });

    public static void WarningGlobal(string title) => InitNotificationGlobal(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Warning,
        StaysOpen = true
    });

    public static void WarningWithToken(string title, string token) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Warning,
        Token = token,
        StaysOpen = true
    });

    public static void Warning(string title, string message) => InitNotification(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Warning,
        StaysOpen = true
    });

    public static void WarningGlobal(string title, string message) => InitNotificationGlobal(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Warning,
        StaysOpen = true
    });

    public static void WarningWithToken(string title, string message, string token) => InitNotification(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Warning,
        Token = token,
        StaysOpen = true
    });

    public static void Warning(NotificationInfo growlInfo) => InitNotification(growlInfo, true, ControlAppearance.Warning);

    public static void WarningGlobal(NotificationInfo growlInfo) => InitNotificationGlobal(growlInfo, true, ControlAppearance.Warning);

    public static void Error(string title) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Error,
        StaysOpen = true
    });

    public static void ErrorGlobal(string title) => InitNotificationGlobal(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Error,
        StaysOpen = true
    });

    public static void ErrorWithToken(string title, string token) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Error,
        Token = token,
        StaysOpen = true
    });

    public static void Error(string title, string message) => InitNotification(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Error,
        StaysOpen = true
    });

    public static void ErrorGlobal(string title, string message) => InitNotificationGlobal(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Error,
        StaysOpen = true
    });

    public static void ErrorWithToken(string title, string message, string token) => InitNotification(new NotificationInfo
    {
        Title = title,
        Message = message,
        Appearance = ControlAppearance.Error,
        Token = token,
        StaysOpen = true
    });

    public static void Error(NotificationInfo growlInfo) => InitNotification(growlInfo, true, ControlAppearance.Error);

    public static void ErrorGlobal(NotificationInfo growlInfo) => InitNotificationGlobal(growlInfo, true, ControlAppearance.Error);

    public static void Ask(string title, Func<object, RoutedEventArgs, bool> actionBeforeClose) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Info,
        StaysOpen = true,
        CloseButtonClicked = actionBeforeClose,
        ShowConfirmButton = true,
        ShowCloseButton = true
    });

    public static void AskGlobal(string title, Func<object, RoutedEventArgs, bool> actionBeforeClose) => InitNotificationGlobal(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Info,
        StaysOpen = true,
        ConfirmButtonClicked = actionBeforeClose,
        ShowConfirmButton = true,
        ShowCloseButton = true
    });

    public static void AskWithToken(string title, string token, Func<object, RoutedEventArgs, bool> actionBeforeClose) => InitNotification(new NotificationInfo
    {
        Title = title,
        Appearance = ControlAppearance.Info,
        StaysOpen = true,
        ConfirmButtonClicked = actionBeforeClose,
        ShowConfirmButton = true,
        ShowCloseButton = true,
        Token = token,
    });
}