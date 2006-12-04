/***************************************************************************
*   Copyright (C) 2005 by Ambertation                                     *
*   quaxi@ambertation.de                                                  *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU General Public License as published by  *
*   the Free Software Foundation; either version 2 of the License, or     *
*   (at your option) any later version.                                   *
*                                                                         *
*   This program is distributed in the hope that it will be useful,       *
*   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
*   GNU General Public License for more details.                          *
*                                                                         *
*   You should have received a copy of the GNU General Public License     *
*   along with this program; if not, write to the                         *
*   Free Software Foundation, Inc.,                                       *
*   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Ambertation.Windows.Forms
{
    // Class to assist with Win32 API calls
    public class APIHelp
    {
        // Required constants from Pinvoke.NET
        public const Int32 WM_ACTIVATE = 0x0006;
        public const Int32 WM_ACTIVATEAPP = 0x001C;
        public const Int32 WM_ACTIVATEAPP_EXT = 0xC0F3;
        public const Int32 WM_AFXFIRST = 0x0360;
        public const Int32 WM_AFXLAST = 0x037F;
        public const Int32 WM_APP = 0x8000;
        public const Int32 WM_ASKCBFORMATNAME = 0x030C;
        public const Int32 WM_CANCELJOURNAL = 0x004B;
        public const Int32 WM_CANCELMODE = 0x001F;
        public const Int32 WM_CAPTURECHANGED = 0x0215;
        public const Int32 WM_CHANGECBCHAIN = 0x030D;
        public const Int32 WM_CHANGEUISTATE = 0x0127;
        public const Int32 WM_CHAR = 0x0102;
        public const Int32 WM_CHARTOITEM = 0x002F;
        public const Int32 WM_CHILDACTIVATE = 0x0022;
        public const Int32 WM_CLEAR = 0x0303;
        public const Int32 WM_CLOSE = 0x0010;
        public const Int32 WM_COMMAND = 0x0111;
        public const Int32 WM_COMPACTING = 0x0041;
        public const Int32 WM_COMPAREITEM = 0x0039;
        public const Int32 WM_CONTEXTMENU = 0x007B;
        public const Int32 WM_COPY = 0x0301;
        public const Int32 WM_COPYDATA = 0x004A;
        public const Int32 WM_CREATE = 0x0001;
        public const Int32 WM_CTLCOLORBTN = 0x0135;
        public const Int32 WM_CTLCOLORDLG = 0x0136;
        public const Int32 WM_CTLCOLOREDIT = 0x0133;
        public const Int32 WM_CTLCOLORLISTBOX = 0x0134;
        public const Int32 WM_CTLCOLORMSGBOX = 0x0132;
        public const Int32 WM_CTLCOLORSCROLLBAR = 0x0137;
        public const Int32 WM_CTLCOLORSTATIC = 0x0138;
        public const Int32 WM_CUT = 0x0300;
        public const Int32 WM_DEADCHAR = 0x0103;
        public const Int32 WM_DELETEITEM = 0x002D;
        public const Int32 WM_DESTROY = 0x0002;
        public const Int32 WM_DESTROYCLIPBOARD = 0x0307;
        public const Int32 WM_DEVICECHANGE = 0x0219;
        public const Int32 WM_DEVMODECHANGE = 0x001B;
        public const Int32 WM_DISPLAYCHANGE = 0x007E;
        public const Int32 WM_DRAWCLIPBOARD = 0x0308;
        public const Int32 WM_DRAWITEM = 0x002B;
        public const Int32 WM_DROPFILES = 0x0233;
        public const Int32 WM_ENABLE = 0x000A;
        public const Int32 WM_ENDSESSION = 0x0016;
        public const Int32 WM_ENTERIDLE = 0x0121;
        public const Int32 WM_ENTERMENULOOP = 0x0211;
        public const Int32 WM_ENTERSIZEMOVE = 0x0231;
        public const Int32 WM_ERASEBKGND = 0x0014;
        public const Int32 WM_EXITMENULOOP = 0x0212;
        public const Int32 WM_EXITSIZEMOVE = 0x0232;
        public const Int32 WM_FONTCHANGE = 0x001D;
        public const Int32 WM_GETDLGCODE = 0x0087;
        public const Int32 WM_GETFONT = 0x0031;
        public const Int32 WM_GETHOTKEY = 0x0033;
        public const Int32 WM_GETICON = 0x007F;
        public const Int32 WM_GETMINMAXINFO = 0x0024;
        public const Int32 WM_GETOBJECT = 0x003D;
        public const Int32 WM_GETTEXT = 0x000D;
        public const Int32 WM_GETTEXTLENGTH = 0x000E;
        public const Int32 WM_HANDHELDFIRST = 0x0358;
        public const Int32 WM_HANDHELDLAST = 0x035F;
        public const Int32 WM_HELP = 0x0053;
        public const Int32 WM_HOTKEY = 0x0312;
        public const Int32 WM_HSCROLL = 0x0114;
        public const Int32 WM_HSCROLLCLIPBOARD = 0x030E;
        public const Int32 WM_ICONERASEBKGND = 0x0027;
        public const Int32 WM_IME_CHAR = 0x0286;
        public const Int32 WM_IME_COMPOSITION = 0x010F;
        public const Int32 WM_IME_COMPOSITIONFULL = 0x0284;
        public const Int32 WM_IME_CONTROL = 0x0283;
        public const Int32 WM_IME_ENDCOMPOSITION = 0x010E;
        public const Int32 WM_IME_KEYDOWN = 0x0290;
        public const Int32 WM_IME_KEYLAST = 0x010F;
        public const Int32 WM_IME_KEYUP = 0x0291;
        public const Int32 WM_IME_NOTIFY = 0x0282;
        public const Int32 WM_IME_REQUEST = 0x0288;
        public const Int32 WM_IME_SELECT = 0x0285;
        public const Int32 WM_IME_SETCONTEXT = 0x0281;
        public const Int32 WM_IME_STARTCOMPOSITION = 0x010D;
        public const Int32 WM_INITDIALOG = 0x0110;
        public const Int32 WM_INITMENU = 0x0116;
        public const Int32 WM_INITMENUPOPUP = 0x0117;
        public const Int32 WM_INPUTLANGCHANGE = 0x0051;
        public const Int32 WM_INPUTLANGCHANGEREQUEST = 0x0050;
        public const Int32 WM_KEYDOWN = 0x0100;
        public const Int32 WM_KEYFIRST = 0x0100;
        public const Int32 WM_KEYLAST = 0x0108;
        public const Int32 WM_KEYUP = 0x0101;
        public const Int32 WM_KILLFOCUS = 0x0008;
        public const Int32 WM_LBUTTONDBLCLK = 0x0203;
        public const Int32 WM_LBUTTONDOWN = 0x0201;
        public const Int32 WM_LBUTTONUP = 0x0202;
        public const Int32 WM_MBUTTONDBLCLK = 0x0209;
        public const Int32 WM_MBUTTONDOWN = 0x0207;
        public const Int32 WM_MBUTTONUP = 0x0208;
        public const Int32 WM_MDIACTIVATE = 0x0222;
        public const Int32 WM_MDICASCADE = 0x0227;
        public const Int32 WM_MDICREATE = 0x0220;
        public const Int32 WM_MDIDESTROY = 0x0221;
        public const Int32 WM_MDIGETACTIVE = 0x0229;
        public const Int32 WM_MDIICONARRANGE = 0x0228;
        public const Int32 WM_MDIMAXIMIZE = 0x0225;
        public const Int32 WM_MDINEXT = 0x0224;
        public const Int32 WM_MDIREFRESHMENU = 0x0234;
        public const Int32 WM_MDIRESTORE = 0x0223;
        public const Int32 WM_MDISETMENU = 0x0230;
        public const Int32 WM_MDITILE = 0x0226;
        public const Int32 WM_MEASUREITEM = 0x002C;
        public const Int32 WM_MENUCHAR = 0x0120;
        public const Int32 WM_MENUCOMMAND = 0x0126;
        public const Int32 WM_MENUDRAG = 0x0123;
        public const Int32 WM_MENUGETOBJECT = 0x0124;
        public const Int32 WM_MENURBUTTONUP = 0x0122;
        public const Int32 WM_MENUSELECT = 0x011F;
        public const Int32 WM_MOUSEACTIVATE = 0x0021;
        public const Int32 WM_MOUSEFIRST = 0x0200;
        public const Int32 WM_MOUSEHOVER = 0x02A1;
        public const Int32 WM_MOUSELAST = 0x020A;
        public const Int32 WM_MOUSELEAVE = 0x02A3;
        public const Int32 WM_MOUSEMOVE = 0x0200;
        public const Int32 WM_MOUSEWHEEL = 0x020A;
        public const Int32 WM_MOVE = 0x0003;
        public const Int32 WM_MOVING = 0x0216;
        public const Int32 WM_NCACTIVATE = 0x0086;
        public const Int32 WM_NCCALCSIZE = 0x0083;
        public const Int32 WM_NCCREATE = 0x0081;
        public const Int32 WM_NCDESTROY = 0x0082;
        public const Int32 WM_NCHITTEST = 0x0084;
        public const Int32 WM_NCLBUTTONDBLCLK = 0x00A3;
        public const Int32 WM_NCLBUTTONDOWN = 0x00A1;
        public const Int32 WM_NCLBUTTONUP = 0x00A2;
        public const Int32 WM_NCMBUTTONDBLCLK = 0x00A9;
        public const Int32 WM_NCMBUTTONDOWN = 0x00A7;
        public const Int32 WM_NCMBUTTONUP = 0x00A8;
        public const Int32 WM_NCMOUSEMOVE = 0x00A0;
        public const Int32 WM_NCPAINT = 0x0085;
        public const Int32 WM_NCRBUTTONDBLCLK = 0x00A6;
        public const Int32 WM_NCRBUTTONDOWN = 0x00A4;
        public const Int32 WM_NCRBUTTONUP = 0x00A5;
        public const Int32 WM_NEXTDLGCTL = 0x0028;
        public const Int32 WM_NEXTMENU = 0x0213;
        public const Int32 WM_NOTIFY = 0x004E;
        public const Int32 WM_NOTIFYFORMAT = 0x0055;
        public const Int32 WM_NULL = 0x0000;
        public const Int32 WM_PAINT = 0x000F;
        public const Int32 WM_PAINTCLIPBOARD = 0x0309;
        public const Int32 WM_PAINTICON = 0x0026;
        public const Int32 WM_PALETTECHANGED = 0x0311;
        public const Int32 WM_PALETTEISCHANGING = 0x0310;
        public const Int32 WM_PARENTNOTIFY = 0x0210;
        public const Int32 WM_PASTE = 0x0302;
        public const Int32 WM_PENWINFIRST = 0x0380;
        public const Int32 WM_PENWINLAST = 0x038F;
        public const Int32 WM_POWER = 0x0048;
        public const Int32 WM_POWERBROADCAST = 0x0218;
        public const Int32 WM_PRINT = 0x0317;
        public const Int32 WM_PRINTCLIENT = 0x0318;
        public const Int32 WM_QUERYDRAGICON = 0x0037;
        public const Int32 WM_QUERYENDSESSION = 0x0011;
        public const Int32 WM_QUERYNEWPALETTE = 0x030F;
        public const Int32 WM_QUERYOPEN = 0x0013;
        public const Int32 WM_QUEUESYNC = 0x0023;
        public const Int32 WM_QUIT = 0x0012;
        public const Int32 WM_RBUTTONDBLCLK = 0x0206;
        public const Int32 WM_RBUTTONDOWN = 0x0204;
        public const Int32 WM_RBUTTONUP = 0x0205;
        public const Int32 WM_RENDERALLFORMATS = 0x0306;
        public const Int32 WM_RENDERFORMAT = 0x0305;
        public const Int32 WM_SETCURSOR = 0x0020;
        public const Int32 WM_SETFOCUS = 0x0007;
        public const Int32 WM_SETFONT = 0x0030;
        public const Int32 WM_SETHOTKEY = 0x0032;
        public const Int32 WM_SETICON = 0x0080;
        public const Int32 WM_SETREDRAW = 0x000B;
        public const Int32 WM_SETTEXT = 0x000C;
        public const Int32 WM_SETTINGCHANGE = 0x001A;
        public const Int32 WM_SHOWWINDOW = 0x0018;
        public const Int32 WM_SIZE = 0x0005;
        public const Int32 WM_SIZECLIPBOARD = 0x030B;
        public const Int32 WM_SIZING = 0x0214;
        public const Int32 WM_SPOOLERSTATUS = 0x002A;
        public const Int32 WM_STYLECHANGED = 0x007D;
        public const Int32 WM_STYLECHANGING = 0x007C;
        public const Int32 WM_SYNCPAINT = 0x0088;
        public const Int32 WM_SYSCHAR = 0x0106;
        public const Int32 WM_SYSCOLORCHANGE = 0x0015;
        public const Int32 WM_SYSCOMMAND = 0x0112;
        public const Int32 WM_SYSDEADCHAR = 0x0107;
        public const Int32 WM_SYSKEYDOWN = 0x0104;
        public const Int32 WM_SYSKEYUP = 0x0105;
        public const Int32 WM_TCARD = 0x0052;
        public const Int32 WM_TIMECHANGE = 0x001E;
        public const Int32 WM_TIMER = 0x0113;
        public const Int32 WM_UNDO = 0x0304;
        public const Int32 WM_UNINITMENUPOPUP = 0x0125;
        public const Int32 WM_USER = 0x0400;
        public const Int32 WM_USERCHANGED = 0x0054;
        public const Int32 WM_VKEYTOITEM = 0x002E;
        public const Int32 WM_VSCROLL = 0x0115;
        public const Int32 WM_VSCROLLCLIPBOARD = 0x030A;
        public const Int32 WM_WINDOWPOSCHANGED = 0x0047;
        public const Int32 WM_WINDOWPOSCHANGING = 0x0046;
        public const Int32 WM_WININICHANGE = 0x001A;

        public const Int32 WS_EX_LAYERED = 0x80000;
        public const Int32 HTCAPTION = 0x02;
        public const Int32 WM_NCMOUSEHOVER = 0x2A0;
        public const Int32 WM_NCMOUSELEAVE = 0x2A2;
        public const Int32 ULW_ALPHA = 0x02;
        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;
        public const Int32 WS_EX_TOOLWINDOW = 0x00000080;
        public const Int32 WS_EX_APPWINDOW = 0x00040000;

        public const Int32 WS_BORDER = ~8388608;
        public const Int32 WS_EX_CLIENTEDGE = ~512;

        public const Int32 RDW_FRAME = 0x0400;
        public const Int32 RDW_UPDATENOW = 0x0100;
        public const Int32 RDW_INVALIDATE = 0x0001;

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left_, int top_, int right_, int bottom_)
            {
                Left = left_;
                Top = top_;
                Right = right_;
                Bottom = bottom_;
            }

            public int Height { get { return Bottom - Top; } }
            public int Width { get { return Right - Left; } }
            public Size Size { get { return new Size(Width, Height); } }

            public Point Location { get { return new Point(Left, Top); } }

            // Handy method for converting to a System.Drawing.Rectangle
            public Rectangle ToRectangle()
            { return Rectangle.FromLTRB(Left, Top, Right, Bottom); }

            public static RECT FromRectangle(Rectangle rectangle)
            {
                return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }

            public override int GetHashCode()
            {
                return Left ^ ((Top << 13) | (Top >> 0x13))
                  ^ ((Width << 0x1a) | (Width >> 6))
                  ^ ((Height << 7) | (Height >> 0x19));
            }

            #region Operator overloads

            public static implicit operator Rectangle(RECT rect)
            {
                return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }

            public static implicit operator RECT(Rectangle rect)
            {
                return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }

            #endregion
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rgrc0, rgrc1, rgrc2;
            public IntPtr lppos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x, y;
            public int cx, cy;
            public int flags;
        }

        [DllImport("User32.dll")]
        public extern static IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("User32.dll")]
        public extern static int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        public enum Bool
        {
            False = 0,
            True = 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public Int32 x;
            public Int32 y;

            public Point(Int32 x, Int32 y) { this.x = x; this.y = y; }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            public Int32 cx;
            public Int32 cy;

            public Size(Int32 cx, Int32 cy) { this.cx = cx; this.cy = cy; }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;
        }

        #region Windows Version
        

       

        public enum WindowsVersion
        {
            Unknown, Windows95, Windows98, Windows98SE, WindowsME, WindowsCE, WindowsNT35, WindowsNT4, Windows2000, WindowsServer2003, WindowsXP, Vista,
        }
        public static WindowsVersion GetVersionEx()
        {
            

            OperatingSystem osInfo = Environment.OSVersion;
            switch (osInfo.Platform)
            {
                case PlatformID.Win32Windows:
                    {
                        switch (osInfo.Version.Minor)
                        {
                            case 0:
                                return WindowsVersion.Windows95;

                            case 10:
                                {
                                    if (osInfo.Version.Revision.ToString() == "2222A")
                                        return WindowsVersion.Windows98SE;

                                    return WindowsVersion.Windows98;
                                }

                            case 90:
                                return WindowsVersion.WindowsME;

                        }
                        break;
                    }

                case PlatformID.Win32NT:
                    {
                        switch (osInfo.Version.Major)
                        {
                            case 5:
                                {
                                    if (osInfo.Version.Minor == 0)
                                        return WindowsVersion.Windows2000;
                                    else if (osInfo.Version.Minor == 1)
                                        return WindowsVersion.WindowsXP;

                                    else if (osInfo.Version.Minor == 2)
                                        return WindowsVersion.WindowsServer2003;

                                    break;
                                }
                            case 3:
                                {
                                    return WindowsVersion.WindowsNT35;
                                }

                            case 4:
                                {
                                    return WindowsVersion.WindowsNT4;
                                }
                            case 6:

                                return WindowsVersion.Vista;

                            

                            
                        }
                        break;
                    }
            }
            return WindowsVersion.Unknown;
        }

        public static bool CanUseLayerdWindows
        {
            get
            {
                
                WindowsVersion ver = GetVersionEx();
                //Console.WriteLine(ver);

                if (ver == WindowsVersion.Vista) return true;
                if (ver == WindowsVersion.WindowsXP) return true;
                if (ver == WindowsVersion.Windows2000) return true;
                if (ver == WindowsVersion.WindowsServer2003) return true;
                return false;
            }
        }        
        #endregion

        

        [DllImport("user32.dll")]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);


        public static Bool CallUpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags){
            if (CanUseLayerdWindows)
                return UpdateLayeredWindow(hwnd, hdcDst, ref pptDst, ref psize, hdcSrc, ref pprSrc, crKey, ref pblend, dwFlags);

            return Bool.True;
        }

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        protected static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);        

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr handle, UInt32 message, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImportAttribute("user32.dll")]
        public static extern int ReleaseCapture(IntPtr hwnd);

        [DllImportAttribute("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, [In] ref RECT lprcUpdate, IntPtr hrgnUpdate, uint flags);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(VirtualKeyStates nVirtKey);

        #region keystate
        public enum VirtualKeyStates : int
        {
            VK_LBUTTON = 0x01,
            VK_RBUTTON = 0x02,
            VK_CANCEL = 0x03,
            VK_MBUTTON = 0x04,
            //
            VK_XBUTTON1 = 0x05,
            VK_XBUTTON2 = 0x06,
            //
            VK_BACK = 0x08,
            VK_TAB = 0x09,
            //
            VK_CLEAR = 0x0C,
            VK_RETURN = 0x0D,
            //
            VK_SHIFT = 0x10,
            VK_CONTROL = 0x11,
            VK_MENU = 0x12,
            VK_PAUSE = 0x13,
            VK_CAPITAL = 0x14,
            //
            VK_KANA = 0x15,
            VK_HANGEUL = 0x15,  /* old name - should be here for compatibility */
            VK_HANGUL = 0x15,
            VK_JUNJA = 0x17,
            VK_FINAL = 0x18,
            VK_HANJA = 0x19,
            VK_KANJI = 0x19,
            //
            VK_ESCAPE = 0x1B,
            //
            VK_CONVERT = 0x1C,
            VK_NONCONVERT = 0x1D,
            VK_ACCEPT = 0x1E,
            VK_MODECHANGE = 0x1F,
            //
            VK_SPACE = 0x20,
            VK_PRIOR = 0x21,
            VK_NEXT = 0x22,
            VK_END = 0x23,
            VK_HOME = 0x24,
            VK_LEFT = 0x25,
            VK_UP = 0x26,
            VK_RIGHT = 0x27,
            VK_DOWN = 0x28,
            VK_SELECT = 0x29,
            VK_PRINT = 0x2A,
            VK_EXECUTE = 0x2B,
            VK_SNAPSHOT = 0x2C,
            VK_INSERT = 0x2D,
            VK_DELETE = 0x2E,
            VK_HELP = 0x2F,
            //
            VK_LWIN = 0x5B,
            VK_RWIN = 0x5C,
            VK_APPS = 0x5D,
            //
            VK_SLEEP = 0x5F,
            //
            VK_NUMPAD0 = 0x60,
            VK_NUMPAD1 = 0x61,
            VK_NUMPAD2 = 0x62,
            VK_NUMPAD3 = 0x63,
            VK_NUMPAD4 = 0x64,
            VK_NUMPAD5 = 0x65,
            VK_NUMPAD6 = 0x66,
            VK_NUMPAD7 = 0x67,
            VK_NUMPAD8 = 0x68,
            VK_NUMPAD9 = 0x69,
            VK_MULTIPLY = 0x6A,
            VK_ADD = 0x6B,
            VK_SEPARATOR = 0x6C,
            VK_SUBTRACT = 0x6D,
            VK_DECIMAL = 0x6E,
            VK_DIVIDE = 0x6F,
            VK_F1 = 0x70,
            VK_F2 = 0x71,
            VK_F3 = 0x72,
            VK_F4 = 0x73,
            VK_F5 = 0x74,
            VK_F6 = 0x75,
            VK_F7 = 0x76,
            VK_F8 = 0x77,
            VK_F9 = 0x78,
            VK_F10 = 0x79,
            VK_F11 = 0x7A,
            VK_F12 = 0x7B,
            VK_F13 = 0x7C,
            VK_F14 = 0x7D,
            VK_F15 = 0x7E,
            VK_F16 = 0x7F,
            VK_F17 = 0x80,
            VK_F18 = 0x81,
            VK_F19 = 0x82,
            VK_F20 = 0x83,
            VK_F21 = 0x84,
            VK_F22 = 0x85,
            VK_F23 = 0x86,
            VK_F24 = 0x87,
            //
            VK_NUMLOCK = 0x90,
            VK_SCROLL = 0x91,
            //
            VK_OEM_NEC_EQUAL = 0x92,   // '=' key on numpad
            //
            VK_OEM_FJ_JISHO = 0x92,   // 'Dictionary' key
            VK_OEM_FJ_MASSHOU = 0x93,   // 'Unregister word' key
            VK_OEM_FJ_TOUROKU = 0x94,   // 'Register word' key
            VK_OEM_FJ_LOYA = 0x95,   // 'Left OYAYUBI' key
            VK_OEM_FJ_ROYA = 0x96,   // 'Right OYAYUBI' key
            //
            VK_LSHIFT = 0xA0,
            VK_RSHIFT = 0xA1,
            VK_LCONTROL = 0xA2,
            VK_RCONTROL = 0xA3,
            VK_LMENU = 0xA4,
            VK_RMENU = 0xA5,
            //
            VK_BROWSER_BACK = 0xA6,
            VK_BROWSER_FORWARD = 0xA7,
            VK_BROWSER_REFRESH = 0xA8,
            VK_BROWSER_STOP = 0xA9,
            VK_BROWSER_SEARCH = 0xAA,
            VK_BROWSER_FAVORITES = 0xAB,
            VK_BROWSER_HOME = 0xAC,
            //
            VK_VOLUME_MUTE = 0xAD,
            VK_VOLUME_DOWN = 0xAE,
            VK_VOLUME_UP = 0xAF,
            VK_MEDIA_NEXT_TRACK = 0xB0,
            VK_MEDIA_PREV_TRACK = 0xB1,
            VK_MEDIA_STOP = 0xB2,
            VK_MEDIA_PLAY_PAUSE = 0xB3,
            VK_LAUNCH_MAIL = 0xB4,
            VK_LAUNCH_MEDIA_SELECT = 0xB5,
            VK_LAUNCH_APP1 = 0xB6,
            VK_LAUNCH_APP2 = 0xB7,
            //
            VK_OEM_1 = 0xBA,   // ';:' for US
            VK_OEM_PLUS = 0xBB,   // '+' any country
            VK_OEM_COMMA = 0xBC,   // ',' any country
            VK_OEM_MINUS = 0xBD,   // '-' any country
            VK_OEM_PERIOD = 0xBE,   // '.' any country
            VK_OEM_2 = 0xBF,   // '/?' for US
            VK_OEM_3 = 0xC0,   // '`~' for US
            //
            VK_OEM_4 = 0xDB,  //  '[{' for US
            VK_OEM_5 = 0xDC,  //  '\|' for US
            VK_OEM_6 = 0xDD,  //  ']}' for US
            VK_OEM_7 = 0xDE,  //  ''"' for US
            VK_OEM_8 = 0xDF,
            //
            VK_OEM_AX = 0xE1,  //  'AX' key on Japanese AX kbd
            VK_OEM_102 = 0xE2,  //  "<>" or "\|" on RT 102-key kbd.
            VK_ICO_HELP = 0xE3,  //  Help key on ICO
            VK_ICO_00 = 0xE4,  //  00 key on ICO
            //
            VK_PROCESSKEY = 0xE5,
            //
            VK_ICO_CLEAR = 0xE6,
            //
            VK_PACKET = 0xE7,
            //
            VK_OEM_RESET = 0xE9,
            VK_OEM_JUMP = 0xEA,
            VK_OEM_PA1 = 0xEB,
            VK_OEM_PA2 = 0xEC,
            VK_OEM_PA3 = 0xED,
            VK_OEM_WSCTRL = 0xEE,
            VK_OEM_CUSEL = 0xEF,
            VK_OEM_ATTN = 0xF0,
            VK_OEM_FINISH = 0xF1,
            VK_OEM_COPY = 0xF2,
            VK_OEM_AUTO = 0xF3,
            VK_OEM_ENLW = 0xF4,
            VK_OEM_BACKTAB = 0xF5,
            //
            VK_ATTN = 0xF6,
            VK_CRSEL = 0xF7,
            VK_EXSEL = 0xF8,
            VK_EREOF = 0xF9,
            VK_PLAY = 0xFA,
            VK_ZOOM = 0xFB,
            VK_NONAME = 0xFC,
            VK_PA1 = 0xFD,
            VK_OEM_CLEAR = 0xFE
        }
        #endregion

        #region SetWindowPos
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        public const UInt32 SWP_NOSIZE = 0x0001;
        public const UInt32 SWP_NOMOVE = 0x0002;
        public const UInt32 SWP_NOZORDER = 0x0004;
        public const UInt32 SWP_NOREDRAW = 0x0008;
        public const UInt32 SWP_NOACTIVATE = 0x0010;
        public const UInt32 SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        public const UInt32 SWP_SHOWWINDOW = 0x0040;
        public const UInt32 SWP_HIDEWINDOW = 0x0080;
        public const UInt32 SWP_NOCOPYBITS = 0x0100;
        public const UInt32 SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        public const UInt32 SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */
        #endregion
    }
}
