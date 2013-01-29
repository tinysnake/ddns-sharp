using DDnsSharp.Monitor.Components;
using DDnsSharp.Monitor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DDnsSharp.Monitor.Utils
{
    public static class DDnsSharpHelpers
    {
        public static void ExitApp()
        {
            DDnsSharpTray.Dispose();
            Environment.Exit(0);
        }

        public static void HideOnTray()
        {
            var keepAliveWin = new KeepAliveWindow();
            CloseAllWindowsBut(new[] { keepAliveWin });
        }

        public static void ShowFromTray()
        {
            var montior = new DDNSMonitorWindow();
            montior.Show();
            CloseAllWindowsBut(new []{montior});
        }

        public static void CloseAllWindows()
        {
            var wins = Application.Current.Windows;
            foreach (Window win in wins)
            {
                if (win is DDNSMonitorWindow)
                    (win as DDNSMonitorWindow).Close(true);
                else
                    win.Close();
            }
        }

        public static void CloseAllWindowsBut(IEnumerable<Window> windows)
        {
            var wins = Application.Current.Windows;
            foreach (Window win in wins)
            {
                if (!windows.Contains(win))
                {
                    if (win is DDNSMonitorWindow)
                        (win as DDNSMonitorWindow).Close(true);
                    else
                        win.Close();
                }
            }
        }

        public static void CloseAllWindowsExceptMainWindow()
        {
            var wins = Application.Current.Windows;
            foreach (Window win in wins)
            {
                if (win is DDNSMonitorWindow)
                    continue;
                else
                    win.Close();
            }
        }
    }
}
