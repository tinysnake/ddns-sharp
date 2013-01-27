using DDnsPod.Monitor.Core;
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

namespace DDnsPod.Monitor.Components
{
    /// <summary>
    /// Interaction logic for ServiceStatusControl.xaml
    /// </summary>
    public partial class ServiceStatusControl : UserControl
    {
        public ServiceStatusControl()
        {
            InitializeComponent();
            Status = ServiceStatus.UnKnown;
        }

        public ServiceStatus Status
        {
            get { return (ServiceStatus)GetValue(StatusProperty); }
            set { 
                SetValue(StatusProperty, value);
                /*
                var status = Status;
                switch (status)
                {
                    case ServiceStatus.Running:
                        img_running.Visibility = Visibility.Visible;
                        img_stopped.Visibility = Visibility.Collapsed;
                        img_unknown.Visibility = Visibility.Collapsed;
                        img_notexist.Visibility = Visibility.Collapsed;
                        break;
                    case ServiceStatus.Stopped:
                        img_running.Visibility = Visibility.Collapsed;
                        img_stopped.Visibility = Visibility.Visible;
                        img_unknown.Visibility = Visibility.Collapsed;
                        img_notexist.Visibility = Visibility.Collapsed;
                        break;
                    case ServiceStatus.NotExist:
                        img_running.Visibility = Visibility.Collapsed;
                        img_stopped.Visibility = Visibility.Collapsed;
                        img_unknown.Visibility = Visibility.Collapsed;
                        img_notexist.Visibility = Visibility.Visible;
                        break;
                    case ServiceStatus.UnKnown:
                        img_running.Visibility = Visibility.Collapsed;
                        img_stopped.Visibility = Visibility.Collapsed;
                        img_unknown.Visibility = Visibility.Visible;
                        img_notexist.Visibility = Visibility.Collapsed;
                        break;
                    default:
                        break;
                }
                 */
            }
        }

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(ServiceStatus),
            typeof(ServiceStatusControl), new PropertyMetadata(ServiceStatus.UnKnown));

    }
}
