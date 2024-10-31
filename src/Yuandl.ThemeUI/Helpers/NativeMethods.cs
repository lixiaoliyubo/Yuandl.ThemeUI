// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Runtime.InteropServices;
using System.Text;
using static Yuandl.ThemeUI.Interop.Dwmapi;

namespace Yuandl.ThemeUI.Helpers;

// https://learn.microsoft.com/visualstudio/code-quality/ca1060?view=vs-2019
// will have to rename
public static class NativeMethods
{
    /// <summary>
    /// 确定指定的窗口句柄是否标识一个现有的窗口。
    /// </summary>
    /// <param name="hWnd">要测试的窗口的句柄。</param>
    /// <returns>如果窗口句柄标识一个现有的窗口，返回值为非零值。</returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindow([In] nint hWnd);

    /// <summary>
    /// 设置桌面窗口管理器（DWM）中窗口的非客户端渲染属性的值。
    /// </summary>
    /// <param name="hWnd">要设置属性值的窗口的句柄。</param>
    /// <param name="dwAttribute">描述要设置哪个值的标志，指定为 DWMWINDOWATTRIBUTE 枚举的值。</param>
    /// <param name="pvAttribute">指向包含要设置的属性值的对象的指针。</param>
    /// <param name="cbAttribute">通过 <c>pvAttribute</c> 参数设置的属性值的大小（以字节为单位）。</param>
    /// <returns>如果函数成功，返回 <c>S_OK</c>。否则，返回 <c>HRESULT</c> 错误代码。</returns>
    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute([In] nint hWnd, [In] DWMWINDOWATTRIBUTE dwAttribute, [In] ref int pvAttribute, [In] int cbAttribute);

    /// <summary>
    /// 检索应用于窗口的指定桌面窗口管理器（DWM）属性的当前值。有关编程指南和代码示例，请参阅控制非客户端区域渲染。
    /// </summary>
    /// <param name="hWnd">要从中检索属性值的窗口的句柄。</param>
    /// <param name="dwAttributeToGet">描述要检索哪个值的标志，指定为 <see cref="DWMWINDOWATTRIBUTE"/> 枚举的值。</param>
    /// <param name="pvAttributeValue">指向一个值的指针，当此函数成功返回时，该值接收属性的当前值。检索的值的类型取决于 dwAttribute 参数的值。</param>
    /// <param name="cbAttribute">通过 pvAttribute 参数接收的属性值的大小（以字节为单位）。</param>
    /// <returns>如果函数成功，则返回 S_OK。否则，返回 HRESULT 错误代码。</returns>
    [DllImport("dwmapi.dll")]
    public static extern int DwmGetWindowAttribute([In] nint hWnd, [In] DWMWINDOWATTRIBUTE dwAttributeToGet, [In] ref int pvAttributeValue, [In] int cbAttribute);

    /// <summary>
    /// 设置窗口的主题属性。
    /// </summary>
    /// <param name="hWnd">要设置主题属性的窗口句柄。</param>
    /// <param name="eAttribute">要设置的窗口主题属性类型。</param>
    /// <param name="pvAttribute">包含主题属性选项的引用。</param>
    /// <param name="cbAttribute">属性选项结构的大小。</param>
    [DllImport("uxtheme.dll", PreserveSig = false)]
    public static extern void SetWindowThemeAttribute([In] nint hWnd, [In] WINDOWTHEMEATTRIBUTETYPE eAttribute, [In] ref WTA_OPTIONS pvAttribute, [In] uint cbAttribute);

    /// <summary>
    /// 测试当前应用程序的视觉样式是否处于活动状态。
    /// </summary>
    /// <returns>如果视觉样式已启用，则返回 <see langword="true"/>，并且应用了视觉样式的窗口应调用 OpenThemeData 以开始使用主题绘制服务。</returns>
    [DllImport("uxtheme.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsThemeActive();

    /// <summary>
    /// 检索当前视觉样式的名称，并可选地检索颜色方案名称和大小名称。
    /// </summary>
    /// <param name="pszThemeFileName">指向接收主题路径和文件名的字符串的指针。</param>
    /// <param name="dwMaxNameChars">类型为 int 的值，包含主题文件名允许的最大字符数。</param>
    /// <param name="pszColorBuff">指向接收颜色方案名称的字符串的指针。此参数可以设置为 NULL。</param>
    /// <param name="cchMaxColorChars">类型为 int 的值，包含颜色方案名称允许的最大字符数。</param>
    /// <param name="pszSizeBuff">指向接收大小名称的字符串的指针。此参数可以设置为 NULL。</param>
    /// <param name="cchMaxSizeChars">类型为 int 的值，包含大小名称允许的最大字符数。</param>
    /// <returns>HRESULT</returns>
    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    public static extern int GetCurrentThemeName(
        [Out] StringBuilder pszThemeFileName,
        [In] int dwMaxNameChars,
        [Out] StringBuilder pszColorBuff,
        [In] int cchMaxColorChars,
        [Out] StringBuilder pszSizeBuff,
        [In] int cchMaxSizeChars
    );

    /// <summary>
    /// 获取指示桌面窗口管理器（DWM）合成是否已启用。
    /// </summary>
    /// <param name="pfEnabled">一个指针，当此函数成功返回时，接收一个值，如果 DWM 合成已启用则为 TRUE，否则为 FALSE。</param>
    /// <returns>如果此函数成功，则返回 S_OK。否则，返回一个 HRESULT 错误代码。</returns>
    [DllImport("dwmapi.dll", BestFitMapping = false)]
    public static extern int DwmIsCompositionEnabled([Out] out int pfEnabled);

    /// <summary>
    /// 读取桌面窗口管理器（DWM）的颜色信息。
    /// </summary>
    /// <param name="dwParameters">指向将保存颜色信息的引用值的指针。</param>
    [DllImport("dwmapi.dll", EntryPoint = "#127", PreserveSig = false, CharSet = CharSet.Unicode)]
    public static extern void DwmGetColorizationParameters([Out] out DWMCOLORIZATIONPARAMS dwParameters);

    /// <summary>
    /// 更改指定窗口的属性。该函数还在额外的窗口内存中指定偏移量处设置值。
    /// </summary>
    /// <param name="hWnd">窗口的句柄，间接表示窗口所属的类。</param>
    /// <param name="nIndex">要设置值的基于零的偏移量。</param>
    /// <param name="dwNewLong">新的值。</param>
    /// <returns>如果函数成功，返回值是指定偏移量的先前值。</returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern nint SetWindowLongPtr([In] nint hWnd, [In] int nIndex, [In] nint dwNewLong);

    /// <summary>
    /// 检索指定的系统度量值或系统配置设置。
    /// 请注意，通过 GetSystemMetrics 检索的所有维度均以像素为单位。
    /// </summary>
    /// <param name="nIndex">要检索的系统度量值或配置设置。此参数可以是 <see cref="SM"/> 枚举值之一。
    /// 请注意，所有 SM_CX* 值均为宽度，所有 SM_CY* 值均为高度。还要注意，所有设计为返回布尔数据的设置将任何非零值表示为 <see langword="true"/>，将零值表示为 <see langword="false"/>。</param>
    /// <returns>如果函数成功，返回值为请求的系统度量值或配置设置。</returns>
    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics([In] SM nIndex);

    /// <summary>
    /// 返回指定窗口的每英寸点数（dpi）值。
    /// </summary>
    /// <param name="hWnd">要获取信息的窗口的句柄。</param>
    /// <returns>窗口的 DPI 值，其取决于窗口的 DPI 感知性。</returns>
    [DllImport("user32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
    public static extern uint GetDpiForWindow([In] IntPtr hWnd);

    /// <summary>
    /// 检索指定窗口的信息。该函数还在额外的窗口内存中指定偏移量处检索 32 位（DWORD）值。
    /// <para>如果您正在检索指针或句柄，此函数已被 <see cref="GetWindowLong"/> 函数取代。</para>
    /// </summary>
    /// <param name="hWnd">窗口的句柄，间接表示窗口所属的类。</param>
    /// <param name="nIndex">要检索值的基于零的偏移量。</param>
    /// <returns>如果函数成功，返回值是所请求的值。</returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern long GetWindowLong([In] nint hWnd, [In] GWL nIndex);

    /// <summary>
    /// 更改指定窗口的属性。该函数还在额外的窗口内存中指定偏移量处设置 32 位（长整型）值。
    /// <para>注意：此函数已被 <see cref="SetWindowLongPtr"/> 函数取代。若要编写与 32 位和 64 位 Windows 版本都兼容的代码，请使用 SetWindowLongPtr 函数。</para>
    /// </summary>
    /// <param name="hWnd">窗口的句柄，间接表示窗口所属的类。</param>
    /// <param name="nIndex">要设置值的基于零的偏移量。有效值的范围是从 0 到额外窗口内存的字节数减去一个整数的大小。</param>
    /// <param name="dwNewLong">新的值。</param>
    /// <returns>如果函数成功，返回值是指定 32 位整数的先前值。</returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern long SetWindowLong([In] nint hWnd, [In] int nIndex, [In] long dwNewLong);

}