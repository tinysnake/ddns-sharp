using DDnsPod.Monitor.Core;
using DDnsPod.Monitor.Design;
using DDnsPod.Monitor.Views;
using DDnsPod.Core.Models;
using DDnsPod.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using DDnsPod.Core;
using Monitor;
using System.Windows;

namespace DDnsPod.Monitor.ViewModels
{
    public class DDNSMonitorWindowViewModel:ViewModelBase
    {
        public const string RECORD_FETCH_KEY = "DDNSMonitorWindowViewModel.RECORD_FETCH_KEY";

        public DDNSMonitorWindowViewModel()
        {
            if (IsInDesignMode)
            {
                AppConfig = new DesigntimeAppConfig();
            }
            else
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            AppConfig = DDNSPodRuntime.AppConfig;
            service = ServiceControl.GetService();
            ServiceStatus = ServiceControl.GetServiceStatus(service);
            UpdateCurrentIP();
        }

        private async void UpdateCurrentIP()
        {
            CurrentIP = await CommonService.GetCurrentIP();
        }

        private ServiceController service;

        [Inject]
        public UserInfo UserInfo { get; set; }
        [Inject]
        public TempStorage TempStorage { get; set; }

        public AppConfig AppConfig { get; private set; }

        #region INPC
        /// <summary>
        /// The <see cref="CurrentIP" /> property's name.
        /// </summary>
        public const string CurrentIPPropertyName = "CurrentIP";

        private string _currentIP = "255.255.255.255";

        /// <summary>
        /// Sets and gets the CurrentIP property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CurrentIP
        {
            get
            {
                return _currentIP;
            }

            set
            {
                if (_currentIP == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentIPPropertyName);
                _currentIP = value;
                RaisePropertyChanged(CurrentIPPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ServiceStatus" /> property's name.
        /// </summary>
        public const string ServiceStatusPropertyName = "ServiceStatus";

        private ServiceStatus _serviceStatus = ServiceStatus.UnKnown;

        /// <summary>
        /// Sets and gets the ServiceStatus property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ServiceStatus ServiceStatus
        {
            get
            {
                return _serviceStatus;
            }

            set
            {
                if (_serviceStatus == value)
                {
                    return;
                }

                RaisePropertyChanging(ServiceStatusPropertyName);
                _serviceStatus = value;
                RaisePropertyChanged(ServiceStatusPropertyName);
            }
        }
        #endregion

        #region LogoutCommand
        private RelayCommand _logoutCommand;

        /// <summary>
        /// Gets the LogoutCommand.
        /// </summary>
        public RelayCommand LogoutCommand
        {
            get
            {
                return _logoutCommand
                    ?? (_logoutCommand = new RelayCommand(() =>
                {

                    var loginWin = new LoginWindow();
                    loginWin.Show();
                    foreach (Window win in App.Current.Windows)
                    {
                        if (win != loginWin)
                            win.Close();
                    }
                }));
            }
        }
        #endregion

        #region EnableRecordCommand
        private RelayCommand<UpdateModel> _enableRecordCommand;

        /// <summary>
        /// Gets the EnableRecordCommand.
        /// </summary>
        public RelayCommand<UpdateModel> EnableRecordCommand
        {
            get
            {
                return _enableRecordCommand
                    ?? (_enableRecordCommand = new RelayCommand<UpdateModel>(
                (um) =>
                {
                    um.Enabled = true;
                }));
            }
        }
        #endregion

        #region DisableRecordCommand
        private RelayCommand<UpdateModel> _disableRecordCommand;

        /// <summary>
        /// Gets the EnableRecordCommand.
        /// </summary>
        public RelayCommand<UpdateModel> DisableRecordCommand
        {
            get
            {
                return _disableRecordCommand
                    ?? (_disableRecordCommand = new RelayCommand<UpdateModel>(
                (um) =>
                {
                    um.Enabled = false;
                }));
            }
        }
        #endregion

        #region EditRecordCommand
        private RelayCommand<UpdateModel> _editRecordCommand;

        /// <summary>
        /// Gets the EditRecordCommand.
        /// </summary>
        public RelayCommand<UpdateModel> EditRecordCommand
        {
            get
            {
                return _editRecordCommand
                    ?? (_editRecordCommand = new RelayCommand<UpdateModel>(
                (um) =>
                {
                    
                }));
            }
        }
        #endregion

        #region DeleteRecordCommand
        private RelayCommand<UpdateModel> _deleteRecordCommand;

        /// <summary>
        /// Gets the DeleteRecordCommand.
        /// </summary>
        public RelayCommand<UpdateModel> DeleteRecordCommand
        {
            get
            {
                return _deleteRecordCommand
                    ?? (_deleteRecordCommand = new RelayCommand<UpdateModel>(
                (um) =>
                {
                    um.Enabled = true;
                }));
            }
        }
        #endregion

        #region AddRecordCommand
        private RelayCommand _addRecordCommand;

        /// <summary>
        /// Gets the AddRecordCommand.
        /// </summary>
        public RelayCommand AddRecordCommand
        {
            get
            {
                return _addRecordCommand
                    ?? (_addRecordCommand = new RelayCommand(() =>
                {
                    TempStorage.Set(RECORD_FETCH_KEY, new UpdateModel());
                    var win = new RecordManageWindow();
                    win.ShowDialog();
                }));
            }
        }
        #endregion
    }
}
