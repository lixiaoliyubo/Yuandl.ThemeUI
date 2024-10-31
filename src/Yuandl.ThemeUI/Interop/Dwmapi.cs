// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Runtime.InteropServices;

namespace Yuandl.ThemeUI.Interop;

public static class Dwmapi
{
    /// <summary>
    /// Flags used by the DwmSetWindowAttribute function to specify the rounded corner preference for a window.
    /// </summary>
    [Flags]
    public enum DWM_WINDOW_CORNER_PREFERENCE
    {
        DEFAULT = 0,
        DONOTROUND = 1,
        ROUND = 2,
        ROUNDSMALL = 3
    }

    /// <summary>
    /// 供 DwmGetWindowAttribute 和 DwmSetWindowAttribute 函数使用的选项。
    /// <para><see href="https://github.com/electron/electron/issues/29937"/></para>
    /// </summary>
    [Flags]
    public enum DWMWINDOWATTRIBUTE
    {
        /// <summary>
        /// 非客户端渲染是否启用/禁用
        /// </summary>
        DWMWA_NCRENDERING_ENABLED = 1,

        /// <summary>
        /// DWMNCRENDERINGPOLICY - 非客户端渲染策略
        /// </summary>
        DWMWA_NCRENDERING_POLICY = 2,

        /// <summary>
        /// 可能启用/强制禁用过渡效果
        /// </summary>
        DWMWA_TRANSITIONS_FORCEDISABLED = 3,

        /// <summary>
        /// 允许在由 DWM 绘制的框架上显示非客户端区域中渲染的内容
        /// </summary>
        DWMWA_ALLOW_NCPAINT = 4,

        /// <summary>
        /// 检索窗口相对空间中标题按钮区域的边界
        /// </summary>
        DWMWA_CAPTION_BUTTON_BOUNDS = 5,

        /// <summary>
        /// 非客户端内容是否为 RTL 镜像
        /// </summary>
        DWMWA_NONCLIENT_RTL_LAYOUT = 6,

        /// <summary>
        /// 强制窗口显示标志性的缩略图或窥视表示（静态位图），即使有窗口的实时或快照表示可用
        /// </summary>
        DWMWA_FORCE_ICONIC_REPRESENTATION = 7,

        /// <summary>
        /// 指定 Flip3D 如何处理窗口
        /// </summary>
        DWMWA_FLIP3D_POLICY = 8,

        /// <summary>
        /// 获取屏幕空间中的扩展框架边界矩形
        /// </summary>
        DWMWA_EXTENDED_FRAME_BOUNDS = 9,

        /// <summary>
        /// 指示在没有更好的缩略图表示时可用的位图
        /// </summary>
        DWMWA_HAS_ICONIC_BITMAP = 10,

        /// <summary>
        /// 不在窗口上调用 Peek
        /// </summary>
        DWMWA_DISALLOW_PEEK = 11,

        /// <summary>
        /// LivePreview 排除信息
        /// </summary>
        DWMWA_EXCLUDED_FROM_PEEK = 12,

        /// <summary>
        /// 隐藏窗口，使其对用户不可见
        /// </summary>
        DWMWA_CLOAK = 13,

        /// <summary>
        /// 如果窗口被隐藏，提供以下值之一解释原因
        /// </summary>
        DWMWA_CLOAKED = 14,

        /// <summary>
        /// 冻结窗口的缩略图图像，使其具有当前的视觉效果。不再对缩略图图像进行进一步的实时更新以匹配窗口的内容
        /// </summary>
        DWMWA_FREEZE_REPRESENTATION = 15,

        /// <summary>
        /// BOOL，仅当桌面组合因其他原因运行时才更新窗口
        /// </summary>
        DWMWA_PASSIVE_UPDATE_MODE = 16,

        /// <summary>
        /// BOOL，允许窗口使用主机背景刷
        /// </summary>
        DWMWA_USE_HOSTBACKDROPBRUSH = 17,

        /// <summary>
        /// 允许窗口根据用户的颜色模式偏好使用强调色或深色
        /// </summary>
        DMWA_USE_IMMERSIVE_DARK_MODE_OLD = 19,

        /// <summary>
        /// 允许窗口根据用户的颜色模式偏好使用强调色或深色
        /// </summary>
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,

        /// <summary>
        /// 控制顶级窗口角的舍入策略。
        /// <para>Windows 11 及以上版本。</para>
        /// </summary>
        DWMWA_WINDOW_CORNER_PREFERENCE = 33,

        /// <summary>
        /// 顶级窗口周围细边框的颜色。
        /// </summary>
        DWMWA_BORDER_COLOR = 34,

        /// <summary>
        /// 标题的颜色。
        /// <para>Windows 11 及以上版本。</para>
        /// </summary>
        DWMWA_CAPTION_COLOR = 35,

        /// <summary>
        /// 标题文字的颜色。
        /// <para>Windows 11 及以上版本。</para>
        /// </summary>
        DWMWA_TEXT_COLOR = 36,

        /// <summary>
        /// 厚框架窗口周围可见边框的宽度。
        /// <para>Windows 11 及以上版本。</para>
        /// </summary>
        DWMWA_VISIBLE_FRAME_BORDER_THICKNESS = 37,

        /// <summary>
        /// 允许输入 0 到 4 的值来决定所施加的背景效果。
        /// </summary>
        DWMWA_SYSTEMBACKDROP_TYPE = 38,

        /// <summary>
        /// 指示窗口是否应使用 Mica 效果。
        /// <para>Windows 11 及以上版本。</para>
        /// </summary>
        DWMWA_MICA_EFFECT = 1029
    }

    /// <summary>
    /// 背景类型。
    /// </summary>
    [Flags]
    public enum DWMSBT : uint
    {
        /// <summary>
        /// 自动选择背景效果。
        /// </summary>
        DWMSBT_AUTO = 0,

        /// <summary>
        /// 关闭背景效果。
        /// </summary>
        DWMSBT_DISABLE = 1,

        /// <summary>
        /// 设置带有生成的壁纸色调的 Mica 效果。
        /// </summary>
        DWMSBT_MAINWINDOW = 2,

        /// <summary>
        /// 设置亚克力效果。
        /// </summary>
        DWMSBT_TRANSIENTWINDOW = 3,

        /// <summary>
        /// 设置模糊的壁纸效果，类似于没有色调的 Mica 效果。
        /// </summary>
        DWMSBT_TABBEDWINDOW = 4
    }

    /// <summary>
    /// 窗口主题非客户端属性
    /// </summary>
    [Flags]
    public enum WTNCA : uint
    {
        /// <summary>
        /// 阻止绘制窗口标题。
        /// </summary>
        NODRAWCAPTION = 0x00000001,

        /// <summary>
        /// 阻止绘制系统图标。
        /// </summary>
        NODRAWICON = 0x00000002,

        /// <summary>
        /// 阻止显示系统图标菜单。
        /// </summary>
        NOSYSMENU = 0x00000004,

        /// <summary>
        /// 即使在从右到左（RTL）布局中，也阻止问号的镜像。
        /// </summary>
        NOMIRRORHELP = 0x00000008,

        /// <summary>
        /// 包含所有有效位的掩码。
        /// </summary>
        VALIDBITS = NODRAWCAPTION | NODRAWICON | NOMIRRORHELP | NOSYSMENU,
    }

    /// <summary>
    /// 表示当前 DWM 颜色强调设置的结构体。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DWMCOLORIZATIONPARAMS
    {
        /// <summary>
        /// 颜色化颜色
        /// </summary>
        public uint ClrColor;

        /// <summary>
        /// 颜色化余晖
        /// </summary>
        public uint ClrAfterGlow;

        /// <summary>
        /// 颜色化颜色平衡
        /// </summary>
        public uint NIntensity;

        /// <summary>
        /// 颜色化余晖平衡
        /// </summary>
        public uint ClrAfterGlowBalance;

        /// <summary>
        /// 颜色化模糊平衡
        /// </summary>
        public uint ClrBlurBalance;

        /// <summary>
        /// 颜色化玻璃反射强度
        /// </summary>
        public uint ClrGlassReflectionIntensity;

        /// <summary>
        /// 颜色化不透明混合
        /// </summary>
        public bool FOpaque;
    }

    /// <summary>
    /// 窗口长标志。
    /// <para><see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlonga"/></para>
    /// </summary>
    [Flags]
    public enum GWL
    {
        /// <summary>
        /// 设置新的扩展窗口样式。
        /// </summary>
        GWL_EXSTYLE = -20,

        /// <summary>
        /// 设置新的应用程序实例句柄。
        /// </summary>
        GWLP_HINSTANCE = -6,

        /// <summary>
        /// 设置新的窗口父句柄。
        /// </summary>
        GWLP_HWNDPARENT = -8,

        /// <summary>
        /// 设置新的子窗口标识符。该窗口不能是顶级窗口。
        /// </summary>
        GWL_ID = -12,

        /// <summary>
        /// 设置新的窗口样式。
        /// </summary>
        GWL_STYLE = -16,

        /// <summary>
        /// 设置与窗口关联的用户数据。
        /// 此数据供创建窗口的应用程序使用。其初始值为零。
        /// </summary>
        GWL_USERDATA = -21,

        /// <summary>
        /// 设置新的窗口过程地址。
        /// 如果窗口不属于调用线程的同一进程，则无法更改此属性。
        /// </summary>
        GWL_WNDPROC = -4,

        /// <summary>
        /// 设置新的专用于应用程序的额外信息，例如句柄或指针。
        /// </summary>
        DWLP_USER = 0x8,

        /// <summary>
        /// 设置在对话框过程中处理的消息的返回值。
        /// </summary>
        DWLP_MSGRESULT = 0x0,

        /// <summary>
        /// 设置新的对话框过程地址。
        /// </summary>
        DWLP_DLGPROC = 0x4
    }

    /// <summary>
    /// 指定要在窗口上设置的视觉样式属性的类型。
    /// </summary>
    public enum WINDOWTHEMEATTRIBUTETYPE : uint
    {
        /// <summary>
        /// 将设置非客户端区域的窗口属性。
        /// </summary>
        WTA_NONCLIENT = 1,
    }

    /// <summary>
    /// 定义了 Windows 消息值（WM_*）的枚举
    /// </summary>
    public enum WM
    {
        /// <summary>
        /// 空消息
        /// </summary>
        NULL = 0x0000,

        /// <summary>
        /// 创建窗口消息
        /// </summary>
        CREATE = 0x0001,

        /// <summary>
        /// 销毁窗口消息
        /// </summary>
        DESTROY = 0x0002,

        /// <summary>
        /// 移动窗口消息
        /// </summary>
        MOVE = 0x0003,

        /// <summary>
        /// 调整窗口大小消息
        /// </summary>
        SIZE = 0x0005,

        /// <summary>
        /// 激活窗口消息
        /// </summary>
        ACTIVATE = 0x0006,

        /// <summary>
        /// 设置焦点到窗口消息
        /// </summary>
        SETFOCUS = 0x0007,

        /// <summary>
        /// 窗口失去焦点消息
        /// </summary>
        KILLFOCUS = 0x0008,

        /// <summary>
        /// 启用窗口消息
        /// </summary>
        ENABLE = 0x000A,

        /// <summary>
        /// 设置窗口重绘标志消息
        /// </summary>
        SETREDRAW = 0x000B,

        /// <summary>
        /// 设置窗口文本消息
        /// </summary>
        SETTEXT = 0x000C,

        /// <summary>
        /// 获取窗口文本消息
        /// </summary>
        GETTEXT = 0x000D,

        /// <summary>
        /// 获取窗口文本长度消息
        /// </summary>
        GETTEXTLENGTH = 0x000E,

        /// <summary>
        /// 窗口重绘消息
        /// </summary>
        PAINT = 0x000F,

        /// <summary>
        /// 关闭窗口消息
        /// </summary>
        CLOSE = 0x0010,

        /// <summary>
        /// 查询会话结束消息
        /// </summary>
        QUERYENDSESSION = 0x0011,

        /// <summary>
        /// 退出应用程序消息
        /// </summary>
        QUIT = 0x0012,

        /// <summary>
        /// 查询窗口是否打开消息
        /// </summary>
        QUERYOPEN = 0x0013,

        /// <summary>
        /// 擦除窗口背景消息
        /// </summary>
        ERASEBKGND = 0x0014,

        /// <summary>
        /// 系统颜色改变消息
        /// </summary>
        SYSCOLORCHANGE = 0x0015,

        /// <summary>
        /// 显示或隐藏窗口消息
        /// </summary>
        SHOWWINDOW = 0x0018,

        /// <summary>
        /// 控件颜色消息
        /// </summary>
        CTLCOLOR = 0x0019,

        /// <summary>
        /// Windows 系统配置改变消息
        /// </summary>
        WININICHANGE = 0x001A,

        /// <summary>
        /// 系统设置改变消息（与 WININICHANGE 相同）
        /// </summary>
        SETTINGCHANGE = WININICHANGE,

        /// <summary>
        /// 激活应用程序消息
        /// </summary>
        ACTIVATEAPP = 0x001C,

        /// <summary>
        /// 设置鼠标光标消息
        /// </summary>
        SETCURSOR = 0x0020,

        /// <summary>
        /// 鼠标激活消息
        /// </summary>
        MOUSEACTIVATE = 0x0021,

        /// <summary>
        /// 子窗口激活消息
        /// </summary>
        CHILDACTIVATE = 0x0022,

        /// <summary>
        /// 队列同步消息
        /// </summary>
        QUEUESYNC = 0x0023,

        /// <summary>
        /// 获取窗口最小最大信息消息
        /// </summary>
        GETMINMAXINFO = 0x0024,

        /// <summary>
        /// 窗口位置正在改变消息
        /// </summary>
        WINDOWPOSCHANGING = 0x0046,

        /// <summary>
        /// 窗口位置已改变消息
        /// </summary>
        WINDOWPOSCHANGED = 0x0047,

        /// <summary>
        /// 上下文菜单消息
        /// </summary>
        CONTEXTMENU = 0x007B,

        /// <summary>
        /// 窗口样式正在改变消息
        /// </summary>
        STYLECHANGING = 0x007C,

        /// <summary>
        /// 窗口样式已改变消息
        /// </summary>
        STYLECHANGED = 0x007D,

        /// <summary>
        /// 显示设置改变消息
        /// </summary>
        DISPLAYCHANGE = 0x007E,

        /// <summary>
        /// 获取窗口图标消息
        /// </summary>
        GETICON = 0x007F,

        /// <summary>
        /// 设置窗口图标消息
        /// </summary>
        SETICON = 0x0080,

        /// <summary>
        /// 非客户区创建消息
        /// </summary>
        NCCREATE = 0x0081,

        /// <summary>
        /// 非客户区销毁消息
        /// </summary>
        NCDESTROY = 0x0082,

        /// <summary>
        /// 非客户区计算大小消息
        /// </summary>
        NCCALCSIZE = 0x0083,

        /// <summary>
        /// 非客户区鼠标点击测试消息
        /// </summary>
        NCHITTEST = 0x0084,

        /// <summary>
        /// 非客户区重绘消息
        /// </summary>
        NCPAINT = 0x0085,

        /// <summary>
        /// 非客户区激活消息
        /// </summary>
        NCACTIVATE = 0x0086,

        /// <summary>
        /// 获取对话框代码消息
        /// </summary>
        GETDLGCODE = 0x0087,

        /// <summary>
        /// 同步绘制消息
        /// </summary>
        SYNCPAINT = 0x0088,

        /// <summary>
        /// 非客户区鼠标移动消息
        /// </summary>
        NCMOUSEMOVE = 0x00A0,

        /// <summary>
        /// 非客户区鼠标左键按下消息
        /// </summary>
        NCLBUTTONDOWN = 0x00A1,

        /// <summary>
        /// 非客户区鼠标左键抬起消息
        /// </summary>
        NCLBUTTONUP = 0x00A2,

        /// <summary>
        /// 非客户区鼠标左键双击消息
        /// </summary>
        NCLBUTTONDBLCLK = 0x00A3,

        /// <summary>
        /// 非客户区鼠标右键按下消息
        /// </summary>
        NCRBUTTONDOWN = 0x00A4,

        /// <summary>
        /// 非客户区鼠标右键抬起消息
        /// </summary>
        NCRBUTTONUP = 0x00A5,

        /// <summary>
        /// 非客户区鼠标右键双击消息
        /// </summary>
        NCRBUTTONDBLCLK = 0x00A6,

        /// <summary>
        /// 非客户区鼠标中键按下消息
        /// </summary>
        NCMBUTTONDOWN = 0x00A7,

        /// <summary>
        /// 非客户区鼠标中键抬起消息
        /// </summary>
        NCMBUTTONUP = 0x00A8,

        /// <summary>
        /// 非客户区鼠标中键双击消息
        /// </summary>
        NCMBUTTONDBLCLK = 0x00A9,

        /// <summary>
        /// 系统键按下消息
        /// </summary>
        SYSKEYDOWN = 0x0104,

        /// <summary>
        /// 系统键抬起消息
        /// </summary>
        SYSKEYUP = 0x0105,

        /// <summary>
        /// 系统字符消息
        /// </summary>
        SYSCHAR = 0x0106,

        /// <summary>
        /// 系统无效字符消息
        /// </summary>
        SYSDEADCHAR = 0x0107,

        /// <summary>
        /// 命令消息
        /// </summary>
        COMMAND = 0x0111,

        /// <summary>
        /// 系统命令消息
        /// </summary>
        SYSCOMMAND = 0x0112,

        /// <summary>
        /// 鼠标移动消息
        /// </summary>
        MOUSEMOVE = 0x0200,

        /// <summary>
        /// 鼠标左键按下消息
        /// </summary>
        LBUTTONDOWN = 0x0201,

        /// <summary>
        /// 鼠标左键抬起消息
        /// </summary>
        LBUTTONUP = 0x0202,

        /// <summary>
        /// 鼠标左键双击消息
        /// </summary>
        LBUTTONDBLCLK = 0x0203,

        /// <summary>
        /// 鼠标右键按下消息
        /// </summary>
        RBUTTONDOWN = 0x0204,

        /// <summary>
        /// 鼠标右键抬起消息
        /// </summary>
        RBUTTONUP = 0x0205,

        /// <summary>
        /// 鼠标右键双击消息
        /// </summary>
        RBUTTONDBLCLK = 0x0206,

        /// <summary>
        /// 鼠标中键按下消息
        /// </summary>
        MBUTTONDOWN = 0x0207,

        /// <summary>
        /// 鼠标中键抬起消息
        /// </summary>
        MBUTTONUP = 0x0208,

        /// <summary>
        /// 鼠标中键双击消息
        /// </summary>
        MBUTTONDBLCLK = 0x0209,

        /// <summary>
        /// 鼠标滚轮滚动消息
        /// </summary>
        MOUSEWHEEL = 0x020A,

        /// <summary>
        /// 扩展鼠标按钮按下消息
        /// </summary>
        XBUTTONDOWN = 0x020B,

        /// <summary>
        /// 扩展鼠标按钮抬起消息
        /// </summary>
        XBUTTONUP = 0x020C,

        /// <summary>
        /// 扩展鼠标按钮双击消息
        /// </summary>
        XBUTTONDBLCLK = 0x020D,

        /// <summary>
        /// 水平鼠标滚轮滚动消息
        /// </summary>
        MOUSEHWHEEL = 0x020E,

        /// <summary>
        /// 父窗口通知消息
        /// </summary>
        PARENTNOTIFY = 0x0210,

        /// <summary>
        /// 捕获状态改变消息
        /// </summary>
        CAPTURECHANGED = 0x0215,

        /// <summary>
        /// 电源广播消息
        /// </summary>
        POWERBROADCAST = 0x0218,

        /// <summary>
        /// 设备改变消息
        /// </summary>
        DEVICECHANGE = 0x0219,

        // 进入大小调整移动状态的消息值
        ENTERSIZEMOVE = 0x0231,

        // 退出大小调整移动状态的消息值
        EXITSIZEMOVE = 0x0232,

        // IME（输入法编辑器）设置上下文的消息值
        IME_SETCONTEXT = 0x0281,

        // IME 通知的消息值
        IME_NOTIFY = 0x0282,

        // IME 控制的消息值
        IME_CONTROL = 0x0283,

        // IME 组合完成的消息值
        IME_COMPOSITIONFULL = 0x0284,

        // IME 选择的消息值
        IME_SELECT = 0x0285,

        // IME 字符的消息值
        IME_CHAR = 0x0286,

        // IME 请求的消息值
        IME_REQUEST = 0x0288,

        // IME 键按下的消息值
        IME_KEYDOWN = 0x0290,

        // IME 键抬起的消息值
        IME_KEYUP = 0x0291,

        // 鼠标离开非客户区的消息值
        NCMOUSELEAVE = 0x02A2,

        // 平板电脑的基础定义消息值
        TABLET_DEFBASE = 0x02C0,

        // 平板电脑添加的消息值
        TABLET_ADDED = TABLET_DEFBASE + 8,

        // 平板电脑删除的消息值
        TABLET_DELETED = TABLET_DEFBASE + 9,

        // 平板电脑轻弹的消息值
        TABLET_FLICK = TABLET_DEFBASE + 11,

        // 平板电脑查询系统手势状态的消息值
        TABLET_QUERYSYSTEMGESTURESTATUS = TABLET_DEFBASE + 12,

        // 剪切操作的消息值
        CUT = 0x0300,

        // 复制操作的消息值
        COPY = 0x0301,

        // 粘贴操作的消息值
        PASTE = 0x0302,

        // 清除操作的消息值
        CLEAR = 0x0303,

        // 撤销操作的消息值
        UNDO = 0x0304,

        // 渲染格式的消息值
        RENDERFORMAT = 0x0305,

        // 渲染所有格式的消息值
        RENDERALLFORMATS = 0x0306,

        // 销毁剪贴板的消息值
        DESTROYCLIPBOARD = 0x0307,

        // 绘制剪贴板的消息值
        DRAWCLIPBOARD = 0x0308,

        // 绘制剪贴板的消息值
        PAINTCLIPBOARD = 0x0309,

        // 垂直滚动剪贴板的消息值
        VSCROLLCLIPBOARD = 0x030A,

        // 调整剪贴板大小的消息值
        SIZECLIPBOARD = 0x030B,

        // 请求剪贴板格式名称的消息值
        ASKCBFORMATNAME = 0x030C,

        // 更改剪贴板链的消息值
        CHANGECBCHAIN = 0x030D,

        // 水平滚动剪贴板的消息值
        HSCROLLCLIPBOARD = 0x030E,

        // 查询新调色板的消息值
        QUERYNEWPALETTE = 0x030F,

        // 调色板正在改变的消息值
        PALETTEISCHANGING = 0x0310,

        // 调色板已改变的消息值
        PALETTECHANGED = 0x0311,

        // 热键的消息值
        HOTKEY = 0x0312,

        // 打印的消息值
        PRINT = 0x0317,

        // 打印客户端的消息值
        PRINTCLIENT = 0x0318,

        // 应用程序命令的消息值
        APPCOMMAND = 0x0319,

        // 主题改变的消息值
        THEMECHANGED = 0x031A,

        // DWM（桌面窗口管理器）合成改变的消息值
        DWMCOMPOSITIONCHANGED = 0x031E,

        // DWM 非客户区渲染改变的消息值
        DWMNCRENDERINGCHANGED = 0x031F,

        // DWM 颜色化颜色改变的消息值
        DWMCOLORIZATIONCOLORCHANGED = 0x0320,

        // DWM 窗口最大化改变的消息值
        DWMWINDOWMAXIMIZEDCHANGE = 0x0321,

        // 获取标题栏信息扩展的消息值
        GETTITLEBARINFOEX = 0x033F,

        // DWM 发送图标缩略图的消息值
        DWMSENDICONICTHUMBNAIL = 0x0323,

        // DWM 发送图标实时预览位图的消息值
        DWMSENDICONICLIVEPREVIEWBITMAP = 0x0326,

        // 用户自定义消息的起始值
        USER = 0x0400,

        /// <summary>
        /// 这是 WinForms 用于 Shell_NotifyIcon 的硬编码消息值。
        /// 复用相对安全。
        /// </summary>
        TRAYMOUSEMESSAGE = 0x800, // WM_USER + 1024

                                  // 应用程序特定消息的起始值
        APP = 0x8000
    }

    /// <summary>
    /// 定义用于设置窗口视觉样式属性的选项。
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct WTA_OPTIONS
    {
        // public static readonly uint Size = (uint)Marshal.SizeOf(typeof(WTA_OPTIONS));
        public const uint Size = 8;

        /// <summary>
        /// 用于修改窗口视觉样式属性的标志组合。
        /// 可以是 WTNCA 常量的组合。
        /// </summary>
        [FieldOffset(0)]
        public WTNCA DwFlags;

        /// <summary>
        /// 一个位掩码，描述 dwFlags 中指定的值应如何应用。
        /// 如果对应于 dwFlags 中的值的位为 0，则将删除该标志。
        /// 如果位为 1，则将添加该标志。
        /// </summary>
        [FieldOffset(4)]
        public WTNCA DwMask;
    }

    /// <summary>
    /// 窗口样式值，WS_*
    /// </summary>
    [Flags]
    public enum WS : long
    {
        /// <summary>
        /// 重叠窗口样式
        /// </summary>
        OVERLAPPED = 0x00000000,

        /// <summary>
        /// 弹出窗口样式
        /// </summary>
        POPUP = 0x80000000,

        /// <summary>
        /// 子窗口样式
        /// </summary>
        CHILD = 0x40000000,

        /// <summary>
        /// 最小化窗口样式
        /// </summary>
        MINIMIZE = 0x20000000,

        /// <summary>
        /// 可见窗口样式
        /// </summary>
        VISIBLE = 0x10000000,

        /// <summary>
        /// 禁用窗口样式
        /// </summary>
        DISABLED = 0x08000000,

        /// <summary>
        /// 剪裁兄弟窗口
        /// </summary>
        CLIPSIBLINGS = 0x04000000,

        /// <summary>
        /// 剪裁子窗口
        /// </summary>
        CLIPCHILDREN = 0x02000000,

        /// <summary>
        /// 最大化窗口样式
        /// </summary>
        MAXIMIZE = 0x01000000,

        /// <summary>
        /// 边框窗口样式
        /// </summary>
        BORDER = 0x00800000,

        /// <summary>
        /// 对话框边框窗口样式
        /// </summary>
        DLGFRAME = 0x00400000,

        /// <summary>
        /// 垂直滚动条窗口样式
        /// </summary>
        VSCROLL = 0x00200000,

        /// <summary>
        /// 水平滚动条窗口样式
        /// </summary>
        HSCROLL = 0x00100000,

        /// <summary>
        /// 系统菜单窗口样式
        /// </summary>
        SYSMENU = 0x00080000,

        /// <summary>
        /// 厚边框窗口样式
        /// </summary>
        THICKFRAME = 0x00040000,

        /// <summary>
        /// 分组窗口样式
        /// </summary>
        GROUP = 0x00020000,

        /// <summary>
        /// 制表位停止窗口样式
        /// </summary>
        TABSTOP = 0x00010000,

        /// <summary>
        /// 最小化框（与 GROUP 相同）
        /// </summary>
        MINIMIZEBOX = GROUP,

        /// <summary>
        /// 最大化框（与 TABSTOP 相同）
        /// </summary>
        MAXIMIZEBOX = TABSTOP,

        /// <summary>
        /// 标题栏（包括边框和对话框边框）
        /// </summary>
        CAPTION = BORDER | DLGFRAME,

        /// <summary>
        /// 平铺窗口（与 OVERLAPPED 相同）
        /// </summary>
        TILED = OVERLAPPED,

        /// <summary>
        /// 图标化（与 MINIMIZE 相同）
        /// </summary>
        ICONIC = MINIMIZE,

        /// <summary>
        /// 大小框（与 THICKFRAME 相同）
        /// </summary>
        SIZEBOX = THICKFRAME,

        /// <summary>
        /// 重叠窗口（包括标题栏、系统菜单、厚边框、最小化框和最大化框）
        /// </summary>
        OVERLAPPEDWINDOW = OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX,

        /// <summary>
        /// 平铺窗口（与 OVERLAPPEDWINDOW 相同）
        /// </summary>
        TILEDWINDOW = OVERLAPPEDWINDOW,

        /// <summary>
        /// 弹出窗口（包括边框和系统菜单）
        /// </summary>
        POPUPWINDOW = POPUP | BORDER | SYSMENU,

        /// <summary>
        /// 子窗口
        /// </summary>
        CHILDWINDOW = CHILD,
    }

    /// <summary>
    /// 定义了 WM_NCHITTEST（非客户区点击测试）和 MOUSEHOOKSTRUCT 鼠标位置代码的枚举
    /// </summary>
    public enum WM_NCHITTEST
    {
        /// <summary>
        /// 点击测试返回错误
        /// </summary>
        HTERROR = unchecked(-2),

        /// <summary>
        /// 点击测试返回透明
        /// </summary>
        HTTRANSPARENT = unchecked(-1),

        /// <summary>
        /// 在屏幕背景或窗口之间的分隔线上
        /// </summary>
        HTNOWHERE = 0,

        /// <summary>
        /// 在客户区
        /// </summary>
        HTCLIENT = 1,

        /// <summary>
        /// 在标题栏
        /// </summary>
        HTCAPTION = 2,

        /// <summary>
        /// 在窗口菜单或子窗口的关闭按钮中
        /// </summary>
        HTSYSMENU = 3,

        /// <summary>
        /// 在尺寸框（与 HTSIZE 相同）
        /// </summary>
        HTGROWBOX = 4,
        HTSIZE = HTGROWBOX,

        /// <summary>
        /// 在菜单中
        /// </summary>
        HTMENU = 5,

        /// <summary>
        /// 在水平滚动条中
        /// </summary>
        HTHSCROLL = 6,

        /// <summary>
        /// 在垂直滚动条中
        /// </summary>
        HTVSCROLL = 7,

        /// <summary>
        /// 在最小化按钮中
        /// </summary>
        HTMINBUTTON = 8,

        /// <summary>
        /// 在最大化按钮中
        /// </summary>
        HTMAXBUTTON = 9,

        // ZOOM = 9,

        /// <summary>
        /// 在可调整大小窗口的左边框（用户可以点击鼠标水平调整窗口大小）
        /// </summary>
        HTLEFT = 10,

        /// <summary>
        /// 在可调整大小窗口的右边框（用户可以点击鼠标水平调整窗口大小）
        /// </summary>
        HTRIGHT = 11,

        /// <summary>
        /// 在窗口的上水平边框
        /// </summary>
        HTTOP = 12,

        // 从 10.0.22000.0\um\WinUser.h
        HTTOPLEFT = 13,
        HTTOPRIGHT = 14,
        HTBOTTOM = 15,
        HTBOTTOMLEFT = 16,
        HTBOTTOMRIGHT = 17,
        HTBORDER = 18,
        HTREDUCE = HTMINBUTTON,
        HTZOOM = HTMAXBUTTON,
        HTSIZEFIRST = HTLEFT,
        HTSIZELAST = HTBOTTOMRIGHT,
        HTOBJECT = 19,
        HTCLOSE = 20,
        HTHELP = 21
    }

    /// <summary>
    /// 定义了系统度量值的枚举。
    /// </summary>
    public enum SM
    {
        /// <summary>
        /// 屏幕宽度（以像素为单位）
        /// </summary>
        CXSCREEN = 0,

        /// <summary>
        /// 屏幕高度（以像素为单位）
        /// </summary>
        CYSCREEN = 1,

        /// <summary>
        /// 垂直滚动条宽度
        /// </summary>
        CXVSCROLL = 2,

        /// <summary>
        /// 水平滚动条高度
        /// </summary>
        CYHSCROLL = 3,

        /// <summary>
        /// 窗口标题栏高度
        /// </summary>
        CYCAPTION = 4,

        /// <summary>
        /// 窗口边框宽度
        /// </summary>
        CXBORDER = 5,

        /// <summary>
        /// 窗口边框高度
        /// </summary>
        CYBORDER = 6,

        /// <summary>
        /// 具有固定框架的窗口边框宽度
        /// </summary>
        CXFIXEDFRAME = 7,

        /// <summary>
        /// 具有固定框架的窗口边框高度
        /// </summary>
        CYFIXEDFRAME = 8,

        /// <summary>
        /// 垂直滚动条滑块高度
        /// </summary>
        CYVTHUMB = 9,

        /// <summary>
        /// 水平滚动条滑块宽度
        /// </summary>
        CXHTHUMB = 10,

        /// <summary>
        /// 图标的宽度
        /// </summary>
        CXICON = 11,

        /// <summary>
        /// 图标的高度
        /// </summary>
        CYICON = 12,

        /// <summary>
        /// 光标的宽度
        /// </summary>
        CXCURSOR = 13,

        /// <summary>
        /// 光标的高度
        /// </summary>
        CYCURSOR = 14,

        /// <summary>
        /// 菜单高度
        /// </summary>
        CYMENU = 15,

        /// <summary>
        /// 全屏窗口宽度
        /// </summary>
        CXFULLSCREEN = 16,

        /// <summary>
        /// 全屏窗口高度
        /// </summary>
        CYFULLSCREEN = 17,

        /// <summary>
        /// 日文字体窗口高度
        /// </summary>
        CYKANJIWINDOW = 18,

        /// <summary>
        /// 鼠标是否存在
        /// </summary>
        MOUSEPRESENT = 19,

        /// <summary>
        /// 垂直滚动条的宽度
        /// </summary>
        CYVSCROLL = 20,

        /// <summary>
        /// 水平滚动条的宽度
        /// </summary>
        CXHSCROLL = 21,

        /// <summary>
        /// 调试状态
        /// </summary>
        DEBUG = 22,

        /// <summary>
        /// 鼠标左右键是否交换
        /// </summary>
        SWAPBUTTON = 23,

        /// <summary>
        /// 窗口最小宽度
        /// </summary>
        CXMIN = 28,

        /// <summary>
        /// 窗口最小高度
        /// </summary>
        CYMIN = 29,

        /// <summary>
        /// 窗口的默认宽度
        /// </summary>
        CXSIZE = 30,

        /// <summary>
        /// 窗口的默认高度
        /// </summary>
        CYSIZE = 31,

        /// <summary>
        /// 窗口框架宽度
        /// </summary>
        CXFRAME = 32,

        /// <summary>
        /// 窗口框架高度
        /// </summary>
        CYFRAME = 33,

        /// <summary>
        /// 窗口最小跟踪宽度
        /// </summary>
        CXMINTRACK = 34,

        /// <summary>
        /// 窗口最小跟踪高度
        /// </summary>
        CYMINTRACK = 35,

        /// <summary>
        /// 鼠标双击的水平间隔
        /// </summary>
        CXDOUBLECLK = 36,

        /// <summary>
        /// 鼠标双击的垂直间隔
        /// </summary>
        CYDOUBLECLK = 37,

        /// <summary>
        /// 图标水平间距
        /// </summary>
        CXICONSPACING = 38,

        /// <summary>
        /// 图标垂直间距
        /// </summary>
        CYICONSPACING = 39,

        /// <summary>
        /// 菜单下拉对齐方式
        /// </summary>
        MENUDROPALIGNMENT = 40,

        /// <summary>
        /// 笔窗口相关
        /// </summary>
        PENWINDOWS = 41,

        /// <summary>
        /// 是否支持双字节字符集（DBCS）
        /// </summary>
        DBCSENABLED = 42,

        /// <summary>
        /// 鼠标按钮数量
        /// </summary>
        CMOUSEBUTTONS = 43,

        /// <summary>
        /// 安全模式
        /// </summary>
        SECURE = 44,

        /// <summary>
        /// 窗口边缘宽度
        /// </summary>
        CXEDGE = 45,

        /// <summary>
        /// 窗口边缘高度
        /// </summary>
        CYEDGE = 46,

        /// <summary>
        /// 窗口最小水平间距
        /// </summary>
        CXMINSPACING = 47,

        /// <summary>
        /// 窗口最小垂直间距
        /// </summary>
        CYMINSPACING = 48,

        /// <summary>
        /// 小图标宽度
        /// </summary>
        CXSMICON = 49,

        /// <summary>
        /// 小图标高度
        /// </summary>
        CYSMICON = 50,

        /// <summary>
        /// 小窗口标题栏高度
        /// </summary>
        CYSMCAPTION = 51,

        /// <summary>
        /// 小窗口默认宽度
        /// </summary>
        CXSMSIZE = 52,

        /// <summary>
        /// 小窗口默认高度
        /// </summary>
        CYSMSIZE = 53,

        /// <summary>
        /// 菜单宽度
        /// </summary>
        CXMENUSIZE = 54,

        /// <summary>
        /// 菜单高度
        /// </summary>
        CYMENUSIZE = 55,

        /// <summary>
        /// 窗口排列相关
        /// </summary>
        ARRANGE = 56,

        /// <summary>
        /// 最小化窗口宽度
        /// </summary>
        CXMINIMIZED = 57,

        /// <summary>
        /// 最小化窗口高度
        /// </summary>
        CYMINIMIZED = 58,

        /// <summary>
        /// 窗口最大跟踪宽度
        /// </summary>
        CXMAXTRACK = 59,

        /// <summary>
        /// 窗口最大跟踪高度
        /// </summary>
        CYMAXTRACK = 60,

        /// <summary>
        /// 最大化窗口宽度
        /// </summary>
        CXMAXIMIZED = 61,

        /// <summary>
        /// 最大化窗口高度
        /// </summary>
        CYMAXIMIZED = 62,

        /// <summary>
        /// 网络相关
        /// </summary>
        NETWORK = 63,

        /// <summary>
        /// 干净启动
        /// </summary>
        CLEANBOOT = 67,

        /// <summary>
        /// 拖动窗口边框宽度
        /// </summary>
        CXDRAG = 68,

        /// <summary>
        /// 拖动窗口边框高度
        /// </summary>
        CYDRAG = 69,

        /// <summary>
        /// 是否显示声音
        /// </summary>
        SHOWSOUNDS = 70,

        /// <summary>
        /// 菜单复选框宽度
        /// </summary>
        CXMENUCHECK = 71,

        /// <summary>
        /// 菜单复选框高度
        /// </summary>
        CYMENUCHECK = 72,

        /// <summary>
        /// 机器运行速度慢
        /// </summary>
        SLOWMACHINE = 73,

        /// <summary>
        /// 中东语言支持启用
        /// </summary>
        MIDEASTENABLED = 74,

        // 指示鼠标滚轮是否存在
        MOUSEWHEELPRESENT = 75,

        // 虚拟屏幕的 x 坐标
        XVIRTUALSCREEN = 76,

        // 虚拟屏幕的 y 坐标
        YVIRTUALSCREEN = 77,

        // 虚拟屏幕的宽度（以像素为单位）
        CXVIRTUALSCREEN = 78,

        // 虚拟屏幕的高度（以像素为单位）
        CYVIRTUALSCREEN = 79,

        // 系统中的监视器数量
        CMONITORS = 80,

        // 指示所有显示器是否具有相同的显示格式
        SAMEDISPLAYFORMAT = 81,

        // 指示输入法编辑器 (IME) 是否启用
        IMMENABLED = 82,

        // 具有焦点的窗口边框宽度（以像素为单位）
        CXFOCUSBORDER = 83,

        // 具有焦点的窗口边框高度（以像素为单位）
        CYFOCUSBORDER = 84,

        // 指示系统是否为 Tablet PC
        TABLETPC = 86,

        // 指示系统是否为 Media Center
        MEDIACENTER = 87,

        // 窗口边框的额外添加宽度（以像素为单位）
        CXPADDEDBORDER = 92,

        // 指示是否处于远程会话
        REMOTESESSION = 0x1000,

        // 指示是否处于远程控制
        REMOTECONTROL = 0x2001,
    }
}