// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Windows.Documents;

namespace Yuandl.ThemeUI.Extensions;

public class AdornerContainer : Adorner
{
    private UIElement child;

    public AdornerContainer(UIElement adornedElement)
        : base(adornedElement)
    {
    }

    // Adorner 直接继承自 FrameworkElement，
    // 没有Content和Child属性，
    // 自己添加一个，方便向其中添加我们的控件
    public UIElement Child
    {
        get => child;
        set
        {
            if (value == null)
            {
                RemoveVisualChild(child);
            }
            else
            {
                AddVisualChild(value);
            }

            child = value;
        }
    }

    // 重写VisualChildrenCount 表示此控件只有一个子控件
    protected override int VisualChildrenCount => 1;

    // 控件计算大小的时候，我们在装饰层上添加的控件也计算一下大小
    protected override Size ArrangeOverride(Size finalSize)
    {
        child?.Arrange(new Rect(finalSize));
        return finalSize;
    }

    // 重写GetVisualChild,返回我们添加的控件
    protected override Visual GetVisualChild(int index)
    {
        if (index == 0 && child != null)
        {
            return child;
        }

        return base.GetVisualChild(index);
    }
}