namespace GTA5OnlineTools.Features.Core;

public static class WinAPI
{
    public const int PROCESS_VM_READ = 0x0010;
    public const int PROCESS_VM_WRITE = 0x0020;
    public const int PROCESS_VM_OPERATION = 0x0008;
    public const int PAGE_READWRITE = 0x0004;

    public const int KEY_PRESSED = 0x8000;

    public const int WM_SETTEXT = 0x0C;
    public const int WM_LBUTTONDOWN = 0x0201;
    public const int WM_LBUTTONUP = 0x0202;

    public const int WM_STYLECHANGING = 0x007C;
    public const int GWL_EXSTYLE = -20;
    public const int WS_EX_TRANSPARENT = 0x20;
    public const int WS_EX_LAYERED = 0x00080000;

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern bool CloseHandle(IntPtr handle);

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(IntPtr hProcess, long lpBaseAddress, [In, Out] byte[] lpBuffer, int nsize, out IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    public static extern bool WriteProcessMemory(IntPtr hProcess, long lpBaseAddress, [In, Out] byte[] lpBuffer, int nsize, out IntPtr lpNumberOfBytesWritten);

    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out W32RECT lpRect);

    [DllImport("user32.dll")]
    public static extern bool GetClientRect(IntPtr hWnd, out W32RECT lpRect);

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    public static extern int SetForegroundWindow(IntPtr hwnd);

    [DllImport("user32.dll")]
    public static extern int GetKeyState(int nVirtKey);

    [DllImport("user32.dll")]
    public static extern int GetAsyncKeyState(int vKey);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

    [DllImport("user32.dll")]
    public static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int nMaxCount);

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);

    [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
    public static extern void Keybd_Event(WinVK bVk, uint bScan, uint dwFlags, uint dwExtraInfo);

    [DllImport("user32.dll")]
    public static extern uint MapVirtualKey(WinVK uCode, uint uMapType);

    [DllImport("User32")]
    public extern static void SetCursorPos(int x, int y);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool GetCursorPos(out POINT pt);

    [DllImport("kernel32.dll")]
    public static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    public static extern bool FreeConsole();

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr hwnd, int index);

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong);
}

public struct POINT
{
    public int X;
    public int Y;
    public POINT(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct W32RECT
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}
