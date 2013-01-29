using DDnsSharp.Core;
using GalaSoft.MvvmLight;
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
using System.Windows.Shapes;

namespace DDnsSharp.Monitor.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.Closed += (o, e) => (DataContext as ViewModelBase).Cleanup();
        }

        private void PasswordBox_PasswordChanged_1(object sender, RoutedEventArgs e)
        {
            DDnsSharpRuntime.AppConfig.Password = pwdbox_pwd.Password;
        }
    }
}
