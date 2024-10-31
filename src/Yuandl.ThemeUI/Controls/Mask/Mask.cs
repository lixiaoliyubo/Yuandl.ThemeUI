// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Yuandl.ThemeUI.Extensions;
using Yuandl.ThemeUI.Helpers;

namespace Yuandl.ThemeUI.Controls;

public class Mask : Control
{
    public static object GetChild(UIElement element)
    {
        if (element == null)
        {
            throw new ArgumentNullException("element");
        }

        return (object)element.GetValue(ChildProperty);
    }

    public static void SetChild(UIElement element, object child)
    {
        if (element == null)
        {
            throw new ArgumentNullException("element");
        }

        element.SetValue(ChildProperty, child);
    }

    public object Child
    {
        get { return (object)GetValue(ChildProperty); }
        set { SetValue(ChildProperty, value); }
    }

    public static readonly DependencyProperty ChildProperty =
        DependencyProperty.Register(nameof(Child), typeof(object), typeof(Mask), new PropertyMetadata(null));

    public static readonly DependencyProperty IsShowProperty =
        DependencyProperty.RegisterAttached("IsShow", typeof(bool), typeof(Mask), new PropertyMetadata(false, OnIsShowChanged));

    private static void OnIsShowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is bool isShow && d is FrameworkElement parent)
        {
            if (isShow)
            {
                parent.IsVisibleChanged += Parent_IsVisibleChanged;
                if (!parent.IsLoaded)
                {
                    parent.Loaded += Parent_Loaded;
                }
                else
                {
                    CreateMask(parent);
                }
            }
            else
            {
                parent.Loaded -= Parent_Loaded;
                parent.IsVisibleChanged -= Parent_IsVisibleChanged;
                CreateMask(parent, true);
            }
        }
    }

    private static void Parent_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is bool isVisible && sender is FrameworkElement parent)
        {
            var isShow = GetIsShow(parent);
            if (isVisible && isShow && !parent.IsLoaded)
            {
                CreateMask(parent);
            }
        }
    }

    private static void Parent_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is UIElement element)
        {
            CreateMask(element);
        }
    }

    private static void CreateMask(UIElement uIElement, bool isRemove = false)
    {
        if (isRemove && uIElement != null)
        {
            uIElement.HideAdorner<AdornerContainer>();
            return;
        }

        MaskControl maskControl;
        var value = GetChild(uIElement);
        if (value != null)
        {
            maskControl = new MaskControl(uIElement) { Content = value };
        }
        else
        {
            maskControl = new MaskControl(uIElement);
        }

        uIElement.ShowAdorner(new AdornerContainer(uIElement)
        {
            Child = maskControl
        });
    }

    public static bool GetIsShow(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsShowProperty);
    }

    public static void SetIsShow(DependencyObject obj, bool value)
    {
        obj.SetValue(IsShowProperty, value);
    }
}