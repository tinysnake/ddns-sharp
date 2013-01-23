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
using System.Collections.ObjectModel;
using DDnsPod.Monitor.Models;

namespace DDnsPod.Monitor.ViewModels
{
    public class DDNSMonitorWindowViewModel:ViewModelBase
    {
        public const string RECORD_FETCH_KEY = "DDNSMonitorWindowViewModel.RECORD_FETCH_KEY";

        public DDNSMonitorWindowViewModel()
        {
            Initialize();
            MessengerInstance.Register<UpdateModelWrapper>(this, OnRecordManaged);
        }

        private void Initialize()
        {
            Runtime = MonitorIoc.Current.Get<MonitorRuntime>();
            Runtime.SetUpdateList(DDNSPodRuntime.AppConfig.UpdateList);
            service = ServiceControl.GetService();
            ServiceStatus = ServiceControl.GetServiceStatus(service);
            UpdateCurrentIP();
        }

        private async void UpdateCurrentIP()
        {
            CurrentIP = await CommonService.GetCurrentIP();
        }

        private ServiceController service;

        public MonitorRuntime Runtime { get; private set; }

        [Inject]
        public TempStorage TempStorage { get; set; }

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
        private RelayCommand<UpdateModelWrapper> _enableRecordCommand;

        /// <summary>
        /// Gets the EnableRecordCommand.
        /// </summary>
        public RelayCommand<UpdateModelWrapper> EnableRecordCommand
        {
            get
            {
                return _enableRecordCommand
                    ?? (_enableRecordCommand = new RelayCommand<UpdateModelWrapper>(
                (um) =>
                {
                    um.Enabled = true;

                    DDNSPodRuntime.AppConfig.UpdateList = (from w in Runtime.UpdateList
                                                           select w.UnWrap()).ToList();
                    DDNSPodRuntime.SaveAppConfig();
                }));
            }
        }
        #endregion

        #region DisableRecordCommand
        private RelayCommand<UpdateModelWrapper> _disableRecordCommand;

        /// <summary>
        /// Gets the EnableRecordCommand.
        /// </summary>
        public RelayCommand<UpdateModelWrapper> DisableRecordCommand
        {
            get
            {
                return _disableRecordCommand
                    ?? (_disableRecordCommand = new RelayCommand<UpdateModelWrapper>(
                (um) =>
                {
                    um.Enabled = false;

                    DDNSPodRuntime.AppConfig.UpdateList = (from w in Runtime.UpdateList
                                                           select w.UnWrap()).ToList();
                    DDNSPodRuntime.SaveAppConfig();
                }));
            }
        }
        #endregion

        #region EditRecordCommand
        private RelayCommand<UpdateModelWrapper> _editRecordCommand;

        /// <summary>
        /// Gets the EditRecordCommand.
        /// </summary>
        public RelayCommand<UpdateModelWrapper> EditRecordCommand
        {
            get
            {
                return _editRecordCommand
                    ?? (_editRecordCommand = new RelayCommand<UpdateModelWrapper>(
                (um) =>
                {
                    TempStorage.Set(RECORD_FETCH_KEY, um);
                    var win = new RecordManageWindow();
                    win.ShowDialog();
                }));
            }
        }
        #endregion

        #region DeleteRecordCommand
        private RelayCommand<UpdateModelWrapper> _deleteRecordCommand;

        /// <summary>
        /// Gets the DeleteRecordCommand.
        /// </summary>
        public RelayCommand<UpdateModelWrapper> DeleteRecordCommand
        {
            get
            {
                return _deleteRecordCommand
                    ?? (_deleteRecordCommand = new RelayCommand<UpdateModelWrapper>(
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
                    var win = new RecordManageWindow();
                    win.ShowDialog();
                }));
            }
        }
        #endregion

        private void OnRecordManaged(UpdateModelWrapper obj)
        {
            if (Runtime.UpdateList.Count(m => m == obj) <= 0)
            {
                Runtime.UpdateList.Add(obj);
            }

            DDNSPodRuntime.AppConfig.UpdateList = (from w in Runtime.UpdateList
                                                   select w.UnWrap()).ToList();
            DDNSPodRuntime.SaveAppConfig();
        }

        private void DeleteRecrod(UpdateModelWrapper um)
        {
            var mbr = MessageBox.Show("请确认操作.","注意", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                Runtime.UpdateList.Remove(um);

                DDNSPodRuntime.AppConfig.UpdateList = (from w in Runtime.UpdateList
                                                       select w.UnWrap()).ToList();
                DDNSPodRuntime.SaveAppConfig();
            }
        }
    }
}
