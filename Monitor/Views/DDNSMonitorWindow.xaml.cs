using DDnsPod.Monitor.Core;
using DDnsPod.Monitor.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace DDnsPod.Monitor.Views
{
    /// <summary>
    /// Interaction logic for DDNSMonitor.xaml
    /// </summary>
    public partial class DDNSMonitorWindow : Window
    {
        public DDNSMonitorWindow()
        {
            InitializeComponent();

            vm = DataContext as DDNSMonitorWindowViewModel;
            vm.PropertyChanged += vm_PropertyChanged;

            string iconPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
            iconDict = new Dictionary<ServiceStatus, Icon>();
            iconDict.Add(ServiceStatus.NotExist, new System.Drawing.Icon(Path.Combine(iconPath, "stop.ico")));
            iconDict.Add(ServiceStatus.UnKnown, new System.Drawing.Icon(Path.Combine(iconPath, "warning.ico")));
            iconDict.Add(ServiceStatus.Stopped, new System.Drawing.Icon(Path.Combine(iconPath, "red_light.ico")));
            iconDict.Add(ServiceStatus.Running, new System.Drawing.Icon(Path.Combine(iconPath, "green_light.ico")));

            notifyIcon = new NotifyIcon();
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            RefreshIconState();
            notifyIcon.Visible = true;

            this.Closing += (o, e) =>
            {
                this.Hide();
                e.Cancel = true;
            };
        }

        void notifyIcon_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                var wins = System.Windows.Application.Current.Windows;
                foreach (Window win in wins)
                {
                    if (win != this)
                        win.Close();
                }
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }

        void vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == DDNSMonitorWindowViewModel.ServiceStatusPropertyName)
            {
                RefreshIconState();
            }
        }

        private void RefreshIconState()
        {
            notifyIcon.Icon = iconDict[vm.ServiceStatus];
            string serviceStr;
            switch (vm.ServiceStatus)
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
            notifyIcon.Text = "DDnsPod: " + serviceStr + "\r\n双击打开或隐藏界面.";
        }

        private DDNSMonitorWindowViewModel vm;
        private Dictionary<ServiceStatus, Icon> iconDict;
        private NotifyIcon notifyIcon;

        private void btn_hide_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void Dispose()
        {
            notifyIcon.Dispose();
        }
    }
}
