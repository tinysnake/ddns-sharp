using DDnsPod.Monitor.Models;
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
    /// Interaction logic for UpdateModelControl.xaml
    /// </summary>
    public partial class UpdateModelControl : UserControl
    {
        public UpdateModelControl()
        {
            InitializeComponent();
        }

        public UpdateModelWrapper Model
        {
            get { return (UpdateModelWrapper)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Model.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(UpdateModelWrapper), typeof(UpdateModelControl));

        public ICommand EnableCommand
        {
            get { return (ICommand)GetValue(EnableCommandProperty); }
            set { SetValue(EnableCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnableCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableCommandProperty =
            DependencyProperty.Register("EnableCommand", typeof(ICommand), typeof(UpdateModelControl));

        public ICommand DisableCommand
        {
            get { return (ICommand)GetValue(DisableCommandProperty); }
            set { SetValue(DisableCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisableCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisableCommandProperty =
            DependencyProperty.Register("DisableCommand", typeof(ICommand), typeof(UpdateModelControl));

        public ICommand EditCommand
        {
            get { return (ICommand)GetValue(EditCommandProperty); }
            set { SetValue(EditCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register("EditCommand", typeof(ICommand), typeof(UpdateModelControl));

        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeleteCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(UpdateModelControl));
    }
}
