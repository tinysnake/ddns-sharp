using DDnsSharp.Monitor.Core;
using DDnsSharp.Core;
using DDnsSharp.Core.Models;
using DDnsSharp.Core.Services;
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
using DDnsSharp.Monitor.Models;
using Ninject;
using System.Net;

namespace DDnsSharp.Monitor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ViewModelLocator.Setup();
            DDnsSharpRuntime.LoadAppConfig();
            var config = DDnsSharpRuntime.AppConfig;
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
            try
            {
                var userInfo = await CommonService.GetUserInfo();
                if (userInfo.Status.Code == 1)
                {
                    var runtime = MonitorIoc.Current.Get<MonitorRuntime>();
                    runtime.UserInfo = userInfo.Info;
                    var win = new DDNSMonitorWindow();
                    if(!MonitorConfig.HideOnStartup)
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
            catch (WebException)
            {
                MessageBox.Show("无法连接至服务器..");
                ShowLoginWindow();
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
