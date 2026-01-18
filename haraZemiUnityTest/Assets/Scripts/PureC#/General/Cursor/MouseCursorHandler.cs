using System.Runtime.InteropServices;

//マウスカーソル関係の処理(移動、クリック)

public static class MouseCursorHandler
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int x, int y);

    public static void Move(int x, int y)
    {
        SetCursorPos(x, y);
    }

    // ===== カーソル位置取得 =====
    [StructLayout(LayoutKind.Sequential)]
    struct POINT
    {
        public int x;
        public int y;
    }

    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out POINT lpPoint);

    public static bool TryGetPosition(out int x, out int y)
    {
        if (GetCursorPos(out POINT p))
        {
            x = p.x;
            y = p.y;
            return true;
        }

        x = 0;
        y = 0;
        return false;
    }

    // ===== 左クリック =====
    const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    const uint MOUSEEVENTF_LEFTUP = 0x0004;

    [DllImport("user32.dll")]
    static extern void mouse_event(
        uint dwFlags,
        uint dx,
        uint dy,
        uint dwData,
        System.IntPtr dwExtraInfo
    );

    public static void LeftClick()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, System.IntPtr.Zero);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, System.IntPtr.Zero);
    }
}
