using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace DDnsPod.Monitor.Views
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            string versionStr = "当前版本: {0}, 服务当前版本: {1}.";
            string curVer = "(无法获取)";
            string svcVer = "(无法获取)";

            Regex regex = new Regex(@"\d+\.\d+\.\d+\.\d+");
            var curAsmMatch = regex.Match(Assembly.GetExecutingAssembly().FullName);
            if (curAsmMatch.Success)
                curVer = curAsmMatch.Value;

            var serviceFullName = String.Empty;
            try
            {
                serviceFullName = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "DDnsPod.Service.exe")).FullName;
            }
            catch { }
            var svcAsmMatch = regex.Match(serviceFullName);
            if (svcAsmMatch.Success)
                svcVer = svcAsmMatch.Value;

            lb_version.Content = String.Format(versionStr, curVer, svcVer);
        }

        private void lb_author_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("http://weibo.com/tangfeifan");
        }

        private void lb_git_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://gitcafe.com/snake/DDnsPod");
        }

        private void lb_donate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://me.alipay.com/tangfeifan");
        }

        private void lb_feedback_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://gitcafe.com/snake/DDnsPod/tickets");
        }
    }
}
