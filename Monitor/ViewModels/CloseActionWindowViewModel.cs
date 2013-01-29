using DDnsSharp.Monitor.Core;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using DDnsSharp.Monitor.Views;
using DDnsSharp.Monitor.Utils;

namespace DDnsSharp.Monitor.ViewModels
{
    public class CloseActionWindowViewModel:ViewModelBase
    {
        public CloseActionWindowViewModel()
        {
            mconfig = MonitorIoc.Current.Get<MonitorConfig>();
        }

        private MonitorConfig mconfig;

        #region INPC
        /// <summary>
        /// The <see cref="IsHideOnWindowClose" /> property's name.
        /// </summary>

        /// <summary>
        /// Sets and gets the HideOnWindowClosedPropertyName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool HideOnWindowClosed
        {
            get
            {
                return mconfig.HideOnWindowsClosed;
            }

            set
            {
                if (mconfig.HideOnWindowsClosed == value)
                {
                    return;
                }

                RaisePropertyChanging(MonitorConfig.HideOnWindowClosedPropertyName);
                mconfig.HideOnWindowsClosed = value;
                RaisePropertyChanged(MonitorConfig.HideOnWindowClosedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ExitOnWindowClosed" /> property's name.
        /// </summary>
        public const string ExitOnWindowClosedPropertyName = "ExitOnWindowClosed";

        /// <summary>
        /// Sets and gets the ExitOnWindowClosed property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ExitOnWindowClosed
        {
            get
            {
                return !mconfig.HideOnWindowsClosed;
            }

            set
            {
                if (mconfig.HideOnWindowsClosed == !value)
                {
                    return;
                }

                RaisePropertyChanging(ExitOnWindowClosedPropertyName);
                mconfig.HideOnWindowsClosed = !value;
                RaisePropertyChanged(ExitOnWindowClosedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsNeverShowThisWin" /> property's name.
        /// </summary>

        /// <summary>
        /// Sets and gets the NeverShowCloseActionWindow property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool NeverShowCloseActionWindow
        {
            get
            {
                return mconfig.NeverShowCloseActionWindow;
            }

            set
            {
                if (mconfig.NeverShowCloseActionWindow == value)
                {
                    return;
                }

                RaisePropertyChanging(MonitorConfig.NeverShowCloseActionWindowPropertyName);
                mconfig.NeverShowCloseActionWindow = value;
                RaisePropertyChanged(MonitorConfig.NeverShowCloseActionWindowPropertyName);
            }
        }
        #endregion
    }
}
