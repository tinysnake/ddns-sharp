using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for RecordManageWindow.xaml
    /// </summary>
    public partial class RecordManageWindow : Window
    {
        public RecordManageWindow()
        {
            InitializeComponent();
            this.Closed += (o, e) => (DataContext as ViewModelBase).Cleanup();
        }
    }
}
