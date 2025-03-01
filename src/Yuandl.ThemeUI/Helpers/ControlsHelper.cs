// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Media;
using System.Windows.Documents;
using System.Windows.Media.Animation;

namespace Yuandl.ThemeUI.Helpers;

public static class ControlsHelper
{
    public static AdornerLayer GetAdornerLayer(Visual visual)
    {
        var decorator = visual as AdornerDecorator;
        if (decorator != null)
        {
            return decorator.AdornerLayer;
        }

        var presenter = visual as ScrollContentPresenter;
        if (presenter != null)
        {
            return presenter.AdornerLayer;
        }

        var visualContent = (visual as Window)?.Content as Visual;
        return AdornerLayer.GetAdornerLayer(visualContent ?? visual);
    }

    public static Window? GetDefaultWindow()
    {
        Window? window = null;
        if (Application.Current.Windows.Count > 0)
        {
            window = Application.Current.Windows.OfType<Window>().FirstOrDefault(o => o.IsActive);
            if (window == null)
            {
                window = Enumerable.FirstOrDefault(Application.Current.Windows.OfType<Window>());
            }
        }

        return window;
    }

    public static T FindVisualChild<T>(this DependencyObject parent)
        where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);
            if (child != null && child is T)
            {
                return (T)child;
            }
            else
            {
                T childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
        }

        return null;
    }

    public static T FindAncestor<T>(this DependencyObject element)
        where T : DependencyObject
    {
        while (element != null)
        {
            if (element is T)
            {
                return (T)element;
            }

            element = VisualTreeHelper.GetParent(element);
        }

        return null;
    }

    public static void FindChildControls<T>(this DependencyObject parent, List<T> combos)
    {
        int childCount = VisualTreeHelper.GetChildrenCount(parent);

        for (int i = 0; i < childCount; i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);

            // 检查子控件是否为 ComboBox 控件
            if (child is T comboBox)
            {
                combos.Add(comboBox);

                // 进行处理
            }
            else
            {
                // 递归查找子控件的子控件
                FindChildControls(child, combos);
            }
        }
    }

    public static T FindParentByName<T>(DependencyObject child, string name)
        where T : DependencyObject
    {
        // Start with the immediate parent of the child
        DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        while (parentObject != null)
        {
            // Check if the parent is of the desired type
            if (parentObject is T parent && (parent as FrameworkElement)?.Name == name)
            {
                return parent;
            }

            // Move up the tree
            parentObject = VisualTreeHelper.GetParent(parentObject);
        }

        // Return null if no parent with the specified name is found
        return null;
    }

}