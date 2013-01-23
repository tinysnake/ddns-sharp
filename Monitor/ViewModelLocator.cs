/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DDnsPod.Monitor"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using DDnsPod.Monitor.Core;
using DDnsPod.Monitor.NinjectModules;
using DDnsPod.Monitor.ViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace DDnsPod.Monitor
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            
        }

        public static void Setup()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                MonitorIoc.Current.Load<DesigntimeNinjectModule>();
            }
            else
            {
                MonitorIoc.Current.Load<RuntimeNinjectModule>();
            }
        }

        /// <summary>
        /// Gets the LoginWindowViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public LoginWindowViewModel LoginWindowViewModel
        {
            get { return MonitorIoc.Current.Get<LoginWindowViewModel>(); }
        }

        /// <summary>
        /// Gets the DDNSMonitorWindowViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DDNSMonitorWindowViewModel DDNSMonitorWindowViewModel
        {
            get { return MonitorIoc.Current.Get<DDNSMonitorWindowViewModel>(); }
        }

        /// <summary>
        /// Gets the RecordManageWindowViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public RecordManageWindowViewModel RecordManageWindowViewModel
        {
            get { return MonitorIoc.Current.Get<RecordManageWindowViewModel>(); }
        }
    }
}