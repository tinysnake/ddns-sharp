using DDnsSharp.Monitor.Core;
using DDnsSharp.Monitor.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows;

namespace DDnsSharp.Monitor.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SettingsWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the SettingsWindowViewModel class.
        /// </summary>
        public SettingsWindowViewModel()
        {
            rk = Registry.CurrentUser.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        }

        private RegistryKey rk;

        #region INPC
        /// <summary>
        /// The <see cref="IsRunOnStartup" /> property's name.
        /// </summary>
        public const string IsRunOnStartupPropertyName = "IsRunOnStartup";

        /// <summary>
        /// Sets and gets the IsRunOnStartup property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsRunOnStartup
        {
            get
            {
                return rk.GetValue("DDnsSharp") != null ? true : false;
            }

            set
            {
                if (IsRunOnStartup == value)
                {
                    return;
                }

                RaisePropertyChanging(IsRunOnStartupPropertyName);
                if (value)
                {
                    rk.SetValue("DDnsSharp", Assembly.GetExecutingAssembly().Location);
                }
                else
                {
                    rk.DeleteValue("DDnsSharp", false);
                }
                RaisePropertyChanged(IsRunOnStartupPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the HideOnStartup property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool HideOnStartup
        {
            get
            {
                return MonitorConfig.HideOnStartup;
            }

            set
            {
                if (HideOnStartup == value)
                {
                    return;
                }
                RaisePropertyChanging(MonitorConfig.HideOnStartupPropertyName);
                MonitorConfig.HideOnStartup = value;
                RaisePropertyChanged(MonitorConfig.HideOnStartupPropertyName);
            }
        }
        #endregion

        public override void Cleanup()
        {
            rk.Dispose();
            base.Cleanup();
        }
    }
}