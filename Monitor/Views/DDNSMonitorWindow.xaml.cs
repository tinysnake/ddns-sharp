using DDnsSharp.Monitor.Core;
using DDnsSharp.Monitor.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using Ninject;
using DDnsSharp.Monitor.Components;
using DDnsSharp.Monitor.Utils;

namespace DDnsSharp.Monitor.Views
{
    /// <summary>
    /// Interaction logic for DDNSMonitor.xaml
    /// </summary>
    public partial class DDNSMonitorWindow : Window
    {
        public DDNSMonitorWindow()
        {
            InitializeComponent();

            mconfig = MonitorIoc.Current.Get<MonitorConfig>();
            vm = DataContext as DDNSMonitorWindowViewModel;

            DDnsSharpTray.Init();
            var notifyIcon = DDnsSharpTray.Current;
            RefreshIconState();
            notifyIcon.Visible = true;
            vm.PropertyChanged += vm_PropertyChanged;

            this.StateChanged += DDNSMonitorWindow_StateChanged;
            this.Closing += DDNSMonitorWindow_Closing;
            this.Closed += (o, e) => vm.Cleanup();
        }

        private MonitorConfig mconfig;
        private DDNSMonitorWindowViewModel vm;
        /// <summary>
        /// 如果为false则是用户单击关闭按钮,就弹出窗口提示用户关闭行为,否则直接关闭本窗口
        /// </summary>
        private bool isClosingBySystem;

        public void Close(bool force)
        {
            if (force)
                isClosingBySystem = true;
            this.Close();
        }

        private void DDNSMonitorWindow_StateChanged(object sender, System.EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                btn_hide_Click(this, null);
            }
        }

        private void DDNSMonitorWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosingBySystem)
            {
                var result = MessageBox.Show("您确定要关闭DDnsSharp吗?","提示",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                    e.Cancel = true;
                else
                    DDnsSharpHelpers.ExitApp();
            }
        }

        private void vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == DDNSMonitorWindowViewModel.ServiceStatusPropertyName)
            {
                RefreshIconState();
            }
        }

        private void RefreshIconState()
        {
            DDnsSharpTray.SetStatus(vm.ServiceStatus);
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
            DDnsSharpTray.Current.Text = "DDnsSharp: " + serviceStr + "\r\n双击打开或隐藏界面.";
        }

        private void btn_hide_Click(object sender, RoutedEventArgs e)
        {
            DDnsSharpHelpers.HideOnTray();
        }
    }
}
