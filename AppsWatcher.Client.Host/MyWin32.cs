using System;
using System.Runtime.InteropServices;

namespace AppsWatcher.Client.Host
{
    internal class MyWin32
    {
        #region Windows API Functions Declarations

        //This Function is used to get Active Window Title...
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hwnd, string lpString, int cch);

        //This Function is used to get Handle for Active Window...
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetForegroundWindow();

        //This Function is used to get Active process ID...
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out Int32 lpdwProcessId);

        #endregion

        public static Int32 GetWindowProcessID(IntPtr hwnd)
        {
            //This Function is used to get Active process ID...
            Int32 pid;
            GetWindowThreadProcessId(hwnd, out pid);
            return pid;
        }
    }
}
