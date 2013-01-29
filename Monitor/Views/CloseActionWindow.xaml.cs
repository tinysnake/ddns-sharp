using DDnsSharp.Monitor.ViewModels;
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
    /// Interaction logic for CloseActionWindow.xaml
    /// </summary>
    public partial class CloseActionWindow : Window
    {
        public CloseActionWindow()
        {
            InitializeComponent();
            vm = DataContext as CloseActionWindowViewModel;
            this.Closing += (o, e) =>
            {
                if (OnCancel != null) OnCancel();
            };
            this.Closed += (o, e) => (DataContext as ViewModelBase).Cleanup();
        }

        private CloseActionWindowViewModel vm;

        public Action OnExit { get; set; }

        public Action OnHide { get; set; }

        public Action OnCancel { get; set; }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (vm.HideOnWindowClosed)
            {
                if (OnHide != null) OnHide();
            }
            else
            {
                if (OnExit != null) OnExit();
            }
            this.Close();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            if (OnCancel != null) OnCancel();
            this.Close();
        }
    }
}
