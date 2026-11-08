using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AhoyBrowser
{
    /// <summary>
    /// When Ahoy is started while it is already running, bring the running instance (or the dialog
    /// it is waiting on, e.g. the first-run or password dialog) to the front instead of leaving the
    /// user with nothing visible.
    /// </summary>
    internal static class SingleInstance
    {
        private const int SW_RESTORE = 9;

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc callback, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>Returns true if a running instance was found and a window of it was activated.</summary>
        internal static bool ActivateRunningInstance()
        {
            try
            {
                Process current = Process.GetCurrentProcess();
                var otherIds = new HashSet<uint>();
                foreach (Process other in Process.GetProcessesByName(current.ProcessName))
                {
                    using (other)
                    {
                        if (other.Id != current.Id)
                        {
                            otherIds.Add((uint)other.Id);
                        }
                    }
                }
                if (otherIds.Count == 0)
                {
                    return false;
                }

                // Top-level windows come in z-order. The first visible *enabled* one is the window
                // the user can act on (a modal dialog disables the windows behind it). Dialogs without
                // a taskbar button are included, unlike Process.MainWindowHandle.
                IntPtr target = IntPtr.Zero;
                IntPtr fallback = IntPtr.Zero;
                EnumWindows((hWnd, lParam) =>
                {
                    GetWindowThreadProcessId(hWnd, out uint processId);
                    if (otherIds.Contains(processId) && IsWindowVisible(hWnd))
                    {
                        if (IsWindowEnabled(hWnd))
                        {
                            target = hWnd;
                            return false; // stop
                        }
                        if (fallback == IntPtr.Zero)
                        {
                            fallback = hWnd;
                        }
                    }
                    return true;
                }, IntPtr.Zero);

                if (target == IntPtr.Zero)
                {
                    target = fallback;
                }
                if (target == IntPtr.Zero)
                {
                    return false;
                }

                if (IsIconic(target))
                {
                    ShowWindow(target, SW_RESTORE);
                }
                SetForegroundWindow(target);
                return true;
            }
            catch
            {
                return false; // Fall back to the message
            }
        }
    }
}
