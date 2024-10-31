// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Threading;
using Yuandl.ThemeUI.Enums;
using Yuandl.ThemeUI.Helpers;

namespace Yuandl.ThemeUI.Controls;

[TemplatePart(Name = nameof(pART_ContentRoot), Type = typeof(Border))]
public partial class Notification : InfoBar
{
    private readonly string pART_ContentRoot = "ContentRoot";
    private Border _BorderRoot;

    private DispatcherTimer timer;
    private Func<object, RoutedEventArgs, bool>? confirmButtonClicked;
    private Func<object, RoutedEventArgs, bool>? closeButtonClicked;
    private static readonly Dictionary<string, Panel> PanelDic = new Dictionary<string, Panel>();

    public static NotificationWindow? NotificationWindow { get; private set; }

    public static Panel? NotificationPanel { get; set; }

    public Notification()
    {
        ButtonClicked += OnButtonClicked;
    }

    private void OnButtonClicked(InfoBar sender, InfoBarButtonClickEventArgs args)
    {
        if (sender.Parent is Panel panel)
        {
            RemoveNotificationFromPanel(panel);
            if (args.Button == "Closed")
            {
                if (closeButtonClicked != null)
                {
                    bool result = closeButtonClicked.Invoke(sender, args);
                    if (result)
                    {
                        IsOpen = false;
                    }
                }
            }
            else if (args.Button == "Confirm")
            {
                if (confirmButtonClicked != null)
                {
                    bool result = confirmButtonClicked.Invoke(sender, args);
                    if (result)
                    {
                        IsOpen = false;
                    }
                }
            }
        }
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _BorderRoot = (Border)GetTemplateChild(pART_ContentRoot);
        if (_BorderRoot != null)
        {
            var transform = new TranslateTransform
            {
                X = FlowDirection == FlowDirection.LeftToRight ? MaxWidth : -MaxWidth
            };
            _BorderRoot.RenderTransform = transform;
            transform.BeginAnimation(TranslateTransform.XProperty, AnimationHelper.CreateAnimation(0));
        }
    }

    public static void Register(string token, Panel panel)
    {
        if (string.IsNullOrEmpty(token) || panel == null)
        {
            return;
        }

        PanelDic[token] = panel;
    }

    public static void Unregister(Panel panel)
    {
        if (panel == null)
        {
            return;
        }

        KeyValuePair<string, Panel> first = PanelDic.FirstOrDefault(item => ReferenceEquals(panel, item.Value));
        if (!string.IsNullOrEmpty(first.Key))
        {
            _ = PanelDic.Remove(first.Key);
        }
    }

    private static void SetDefaultPanelTransition(Panel panel)
    {
        _ = GetNotificationEnterTransition(panel);
        if (panel.RenderTransform is TransformGroup transformGroup)
        {
            foreach (Transform? transform in transformGroup.Children)
            {
                if (transform is TranslateTransform translateTransform)
                {
                    translateTransform.BeginAnimation(TranslateTransform.XProperty, AnimationHelper.CreateAnimation(0));
                }
                else if (transform is RotateTransform rotateTransform)
                {
                    rotateTransform.BeginAnimation(TranslateTransform.XProperty, AnimationHelper.CreateAnimation(0));
                }
                else if (transform is ScaleTransform scaleTransform)
                {
                    scaleTransform.BeginAnimation(TranslateTransform.XProperty, AnimationHelper.CreateAnimation(0));
                }
            }
        }
    }

    private static bool HasToken(DependencyObject element) => GetToken(element) != null;

    private void RemoveNotificationFromPanel(Panel panel)
    {
        if (panel == null)
        {
            return;
        }

        var transform = new TranslateTransform();

        _BorderRoot.RenderTransform = transform;

        System.Windows.Media.Animation.DoubleAnimation animation = AnimationHelper.CreateAnimation(FlowDirection == FlowDirection.LeftToRight ? ActualWidth : -ActualWidth);
        animation.Completed += (s, e) =>
        {
            if (Parent is Panel panel)
            {
                panel.Children.Remove(this);
            }
        };
        transform.BeginAnimation(TranslateTransform.XProperty, animation);
    }

    private static void InitNotification(NotificationInfo growlInfo, bool isForceSeverity = false, ControlAppearance forceSeverity = ControlAppearance.Info)
    {
        var ctl = new Notification();
        ctl.Title = growlInfo.Title;
        ctl.Message = growlInfo.Message;
        ctl.IsIconVisible = growlInfo.IsIconVisible;
        ctl.Icon = growlInfo.IconSource;
        ctl.IsClosable = growlInfo.IsClosable;
        ctl.Appearance = isForceSeverity ? forceSeverity : growlInfo.Appearance;
        ctl.DateTime = growlInfo.DateTime;
        if (string.IsNullOrEmpty(ctl.DateTime))
        {
            ctl.DateTime = DateTimeOffset.Now.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        ctl.ConfirmButtonText = growlInfo.ConfirmButtonText;
        ctl.CloseButtonText = growlInfo.CloseButtonText;
        ctl.ShowConfirmButton = growlInfo.ShowConfirmButton ? Visibility.Visible : Visibility.Collapsed;
        ctl.ShowCloseButton = growlInfo.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed;
        ctl.ShowDateTime = growlInfo.ShowDateTime && !string.IsNullOrEmpty(ctl.DateTime) ? Visibility.Visible : Visibility.Collapsed;

        ctl.closeButtonClicked = growlInfo.CloseButtonClicked;
        ctl.confirmButtonClicked = growlInfo.ConfirmButtonClicked;

        if (!string.IsNullOrEmpty(growlInfo.Token) && PanelDic.TryGetValue(growlInfo.Token, out Panel? panel))
        {
            panel.Children.Insert(0, ctl);
            if (!growlInfo.StaysOpen)
            {
                ctl.SetupTimer(growlInfo.WaitTime, panel);
            }
        }
        else if (NotificationPanel != null)
        {
            NotificationPanel.Children.Insert(0, ctl);
            if (!growlInfo.StaysOpen)
            {
                ctl.SetupTimer(growlInfo.WaitTime, NotificationPanel);
            }
        }
    }

    private static void InitNotificationGlobal(NotificationInfo growlInfo, bool isForceSeverity = false, ControlAppearance forceSeverity = ControlAppearance.Info)
    {
        var ctl = new Notification();
        ctl.Title = growlInfo.Title;
        ctl.Message = growlInfo.Message;
        ctl.IsIconVisible = growlInfo.IsIconVisible;
        ctl.Icon = growlInfo.IconSource;
        ctl.IsClosable = growlInfo.IsClosable;
        ctl.Appearance = isForceSeverity ? forceSeverity : growlInfo.Appearance;
        ctl.DateTime = growlInfo.DateTime;
        if (string.IsNullOrEmpty(ctl.DateTime))
        {
            ctl.DateTime = DateTimeOffset.Now.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        ctl.ConfirmButtonText = growlInfo.ConfirmButtonText;
        ctl.CloseButtonText = growlInfo.CloseButtonText;
        ctl.ShowConfirmButton = growlInfo.ShowConfirmButton ? Visibility.Visible : Visibility.Collapsed;
        ctl.ShowCloseButton = growlInfo.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed;
        ctl.ShowDateTime = growlInfo.ShowDateTime && !string.IsNullOrEmpty(ctl.DateTime) ? Visibility.Visible : Visibility.Collapsed;

        ctl.closeButtonClicked = growlInfo.CloseButtonClicked;
        ctl.confirmButtonClicked = growlInfo.ConfirmButtonClicked;

        if (NotificationWindow == null || NotificationWindow.WindowClosed)
        {
            NotificationWindow = new NotificationWindow();
            NotificationWindow.Show();
            NotificationWindow.Init();
        }

        NotificationWindow?.Show();

        if (NotificationWindow?.NotificationPanel != null)
        {
            NotificationWindow.NotificationPanel.Children.Insert(0, ctl);
            if (!growlInfo.StaysOpen)
            {
                ctl.SetupTimer(growlInfo.WaitTime, NotificationWindow.NotificationPanel);
            }
        }
    }

    private void SetupTimer(TimeSpan timeSpan, Panel panel)
    {
        timer = new DispatcherTimer();
        timer.Interval = timeSpan;
        timer.Tick += (sender, e) =>
        {
            RemoveNotificationFromPanel(panel);
            timer.Stop(); // Stop the timer after it's ticked
        };
        timer.Start();
    }

    private static void Clear(Panel? panel) => panel?.Children.Clear();

    public static void Clear(string token = "")
    {
        if (!string.IsNullOrEmpty(token))
        {
            if (PanelDic.TryGetValue(token, out Panel? panel))
            {
                Clear(panel);
            }
        }
        else
        {
            Clear(NotificationPanel);
        }
    }

    public static void ClearGlobal()
    {
        if (NotificationWindow == null)
        {
            return;
        }

        Clear(NotificationWindow.NotificationPanel);
        NotificationWindow.Close();
        NotificationWindow = null;
    }

    public string DateTime
    {
        get { return (string)GetValue(DateTimeProperty); }
        set { SetValue(DateTimeProperty, value); }
    }

    public static readonly DependencyProperty DateTimeProperty =
        DependencyProperty.Register("DateTime", typeof(string), typeof(Notification), new PropertyMetadata(null));

    public Visibility ShowDateTime
    {
        get { return (Visibility)GetValue(ShowDateTimeProperty); }
        set { SetValue(ShowDateTimeProperty, value); }
    }

    public static readonly DependencyProperty ShowDateTimeProperty =
        DependencyProperty.Register("ShowDateTime", typeof(Visibility), typeof(Notification), new PropertyMetadata(Visibility.Collapsed));

    public Visibility ShowConfirmButton
    {
        get { return (Visibility)GetValue(ShowConfirmButtonProperty); }
        set { SetValue(ShowConfirmButtonProperty, value); }
    }

    public static readonly DependencyProperty ShowConfirmButtonProperty =
        DependencyProperty.Register("ShowConfirmButton", typeof(Visibility), typeof(Notification), new PropertyMetadata(Visibility.Collapsed));

    public Visibility ShowCloseButton
    {
        get { return (Visibility)GetValue(ShowCloseButtonProperty); }
        set { SetValue(ShowCloseButtonProperty, value); }
    }

    public static readonly DependencyProperty ShowCloseButtonProperty =
        DependencyProperty.Register("ShowCloseButton", typeof(Visibility), typeof(Notification), new PropertyMetadata(Visibility.Collapsed));

    public string ConfirmButtonText
    {
        get { return (string)GetValue(ConfirmButtonTextProperty); }
        set { SetValue(ConfirmButtonTextProperty, value); }
    }

    public static readonly DependencyProperty ConfirmButtonTextProperty =
        DependencyProperty.Register("ConfirmButtonText", typeof(string), typeof(Notification), new PropertyMetadata(null));

    public string CloseButtonText
    {
        get { return (string)GetValue(CloseButtonTextProperty); }
        set { SetValue(CloseButtonTextProperty, value); }
    }

    public static readonly DependencyProperty CloseButtonTextProperty =
        DependencyProperty.Register("CloseButtonText", typeof(string), typeof(Notification), new PropertyMetadata(null));

    public Thickness RootGridMargin
    {
        get { return (Thickness)GetValue(RootGridMarginProperty); }
        set { SetValue(RootGridMarginProperty, value); }
    }

    public static readonly DependencyProperty RootGridMarginProperty =
        DependencyProperty.Register("RootGridMargin", typeof(Thickness), typeof(Notification), new PropertyMetadata(new Thickness(10, 10, 10, 10)));

    public static string GetToken(DependencyObject obj)
    {
        return (string)obj.GetValue(TokenProperty);
    }

    public static void SetToken(DependencyObject obj, string value)
    {
        obj.SetValue(TokenProperty, value);
    }

    public static readonly DependencyProperty TokenProperty =
        DependencyProperty.RegisterAttached("Token", typeof(string), typeof(Notification), new PropertyMetadata(null, OnTokenChanged));

    private static void OnTokenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Panel panel)
        {
            if (e.NewValue != null)
            {
                Unregister(panel);
            }
            else
            {
                Register(e.NewValue.ToString(), panel);
            }
        }
    }

    public static void SetNotificationParent(DependencyObject element, bool value) => element.SetValue(NotificationParentProperty, value);

    public static bool GetNotificationParent(DependencyObject element) => (bool)element.GetValue(NotificationParentProperty);

    public static readonly DependencyProperty NotificationParentProperty = DependencyProperty.RegisterAttached(
        "NotificationParent", typeof(bool), typeof(Notification), new PropertyMetadata(false, (o, args) =>
        {
            if ((bool)args.NewValue && o is Panel panel)
            {
                panel.Loaded += Panel_Loaded;
            }
        }));

    private static void Panel_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is Panel panel)
        {
            // Panel is now fully loaded, you can perform initialization here
            if (GetNotificationParent(panel))
            {
                if (!HasToken(panel))
                {
                    // If Token is not set, handle things as before
                    NotificationPanel = panel;
                }
                else
                {
                    // If Token is set, use the dictionary
                    var token = GetToken(panel);
                    PanelDic[token] = panel;
                }

                SetDefaultPanelTransition(panel);
            }

            // Remove the event handler to avoid multiple subscriptions
            panel.Loaded -= Panel_Loaded;
        }
    }

    public static void SetNotificationEnterTransition(DependencyObject element, NotificationTransition value) => element.SetValue(NotificationEnterTransitionProperty, value);

    public static NotificationTransition GetNotificationEnterTransition(DependencyObject element) => (NotificationTransition)element.GetValue(NotificationEnterTransitionProperty);

    public static readonly DependencyProperty NotificationEnterTransitionProperty = DependencyProperty.RegisterAttached(
        "NotificationEnterTransition", typeof(NotificationTransition), typeof(Notification), new PropertyMetadata(NotificationTransition.TranslateTransform, (o, args) =>
        {
        }));

    public static void SetNotificationExitTransition(DependencyObject element, NotificationTransition value) => element.SetValue(NotificationExitTransitionProperty, value);

    public static NotificationTransition GetNotificationExitTransition(DependencyObject element) => (NotificationTransition)element.GetValue(NotificationExitTransitionProperty);

    public static readonly DependencyProperty NotificationExitTransitionProperty = DependencyProperty.RegisterAttached(
        "NotificationExitTransition", typeof(NotificationTransition), typeof(Notification), new PropertyMetadata(NotificationTransition.TranslateTransform));

}