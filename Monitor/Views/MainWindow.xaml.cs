using DDnsPod.Monitor.Core;
using DDnsPod.Core;
using DDnsPod.Core.Models;
using DDnsPod.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DDnsPod.Monitor.Models;
using Ninject;

namespace DDnsPod.Monitor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ViewModelLocator.Setup();
            DDNSPodRuntime.LoadAppConfig();
            var config = DDNSPodRuntime.AppConfig;
            if (String.IsNullOrWhiteSpace(config.Email) ||
                String.IsNullOrWhiteSpace(config.Password))
            {
                ShowLoginWindow();
            }
            else
            {
                InitializeComponent();
                Login();
            }
        }

        private async void Login()
        {
            var userInfo = await CommonService.GetUserInfo();
            if (userInfo.Status.Code == 1)
            {
                var runtime = MonitorIoc.Current.Get<MonitorRuntime>();
                runtime.UserInfo = userInfo.Info;
                var win = new DDNSMonitorWindow();
                win.Show();
                this.Close();
            }
            else if (userInfo.Status.Code == -1)
            {
                ShowLoginWindow();
            }
            else
            {
                MessageBox.Show(userInfo.Status.Message);
            }
        }

        private void ShowLoginWindow()
        {
            var win = new LoginWindow();
            win.Show();
            this.Close();
        }
    }
}
