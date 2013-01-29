using DDnsSharp.Monitor.Core;
using DDnsSharp.Monitor.Utils;
using DDnsSharp.Monitor.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace DDnsSharp.Monitor.Components
{
    public class DDnsSharpTray
    {
        static DDnsSharpTray()
        {
            string iconPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
            iconDict = new Dictionary<ServiceStatus, Icon>();
            iconDict.Add(ServiceStatus.NotExist, new System.Drawing.Icon(Path.Combine(iconPath, "stop.ico")));
            iconDict.Add(ServiceStatus.UnKnown, new System.Drawing.Icon(Path.Combine(iconPath, "warning.ico")));
            iconDict.Add(ServiceStatus.Stopped, new System.Drawing.Icon(Path.Combine(iconPath, "red_light.ico")));
            iconDict.Add(ServiceStatus.Running, new System.Drawing.Icon(Path.Combine(iconPath, "green_light.ico")));
        }

        private static Dictionary<ServiceStatus, Icon> iconDict;
        private static NotifyIcon instance;

        public static void Init()
        {
            if (instance == null)
            {
                instance = new NotifyIcon();
                instance.DoubleClick += (o, e) => OnTrayDoubleClick();
            }
        }

        public static NotifyIcon Current
        {
            get { return instance; }
        }

        public static void SetStatus(ServiceStatus ss)
        {
            if (instance != null)
            {
                instance.Icon = iconDict[ss];
            }
        }

        public static void Dispose()
        {
            if (instance != null)
            {
                instance.Dispose();
            }
        }

        private static void OnTrayDoubleClick()
        {
            if (System.Windows.Application.Current.Windows.Count == 1 && System.Windows.Application.Current.Windows[0] is KeepAliveWindow)
            {
                DDnsSharpHelpers.ShowFromTray();
            }
            else
            {
                DDnsSharpHelpers.HideOnTray();
            }
        }
    }
}
