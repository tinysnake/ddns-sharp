using DDnsSharp.Monitor.Components;
using DDnsSharp.Monitor.Core;
using DDnsSharp.Monitor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DDnsSharp.Monitor.Views
{
    /// <summary>
    /// Interaction logic for KeepLiveWindow.xaml
    /// </summary>
    public partial class KeepAliveWindow : Window
    {
        public KeepAliveWindow()
        {
            InitializeComponent();
            service = ServiceControl.GetService();
            refreshServiceStatusTimer = TimerDispatch.Current.AddInterval((m) => GetServiceStatus(), 10000);
            this.Closed += (o, e) => Dispose();
            this.Hide();
        }

        private TimerModel refreshServiceStatusTimer;
        private ServiceController service;
        private ServiceStatus serviceStatus;

        private void GetServiceStatus()
        {
            if (service != null)
                service.Refresh();
            var ss = ServiceControl.GetServiceStatus(service);
            if (ss != serviceStatus)
            {
                serviceStatus = ss;
                DDnsSharpTray.SetStatus(serviceStatus);
                string serviceStr;
                switch (serviceStatus)
                {
                    case ServiceStatus.Running:
                        serviceStr = "服务正在运行.";
                        break;
                    case ServiceStatus.Stopped:
                        serviceStr = "服务已停止.";
                        break;
                    case ServiceStatus.NotExist:
                        serviceStr = "服务尚未安装.";
                        break;
                    default:
                        serviceStr = "服务状态未知.";
                        break;
                }
                DDnsSharpTray.Current.Text = "DDnsSharp: " + serviceStr + "\r\n双击打开或隐藏界面.";
            }
        }

        public void Dispose()
        {
            TimerDispatch.Current.RemoveTimer(refreshServiceStatusTimer);
        }
    }
}
