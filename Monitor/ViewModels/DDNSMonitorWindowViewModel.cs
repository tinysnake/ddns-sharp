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
using System.IO;
using System.Timers;
using System.Runtime.InteropServices;

namespace DDnsPod.Monitor.ViewModels
{
    public class DDNSMonitorWindowViewModel : ViewModelBase
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
            GetServiceStatus();
            serviceStatusCheker = new Timer(5000);
            serviceStatusCheker.Elapsed += (o, e) => GetServiceStatus();
            serviceStatusCheker.Start();
            UpdateCurrentIP();
        }

        private async void UpdateCurrentIP()
        {
            CurrentIP = await CommonService.GetCurrentIP();
        }

        private ServiceController service;
        private Timer serviceStatusCheker;

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

        #region ExitCommand
        private RelayCommand _exitCommand;

        /// <summary>
        /// Gets the ExitCommand.
        /// </summary>
        public RelayCommand ExitCommand
        {
            get
            {
                return _exitCommand
                    ?? (_exitCommand = new RelayCommand(() =>
                {
                    Environment.Exit(0);
                }));
            }
        }
        #endregion

        #region ForceUpdateCommand
        private RelayCommand _forceUpdateCommand;

        /// <summary>
        /// Gets the ForceUpdateCommand.
        /// </summary>
        public RelayCommand ForceUpdateCommand
        {
            get
            {
                return _forceUpdateCommand
                    ?? (_forceUpdateCommand = new RelayCommand(OnForceUpdate));
            }
        }
        #endregion

        #region CheckoutLogCommand
        private RelayCommand _chkoutLogCommand;

        /// <summary>
        /// Gets the CheckoutLogCommand.
        /// </summary>
        public RelayCommand CheckoutLogCommand
        {
            get
            {
                return _chkoutLogCommand
                    ?? (_chkoutLogCommand = new RelayCommand(() =>
                {
                    System.Diagnostics.Process.Start("notepad",
                        Path.Combine(Directory.GetCurrentDirectory(), "ddnspod.info"));
                }));
            }
        }
        #endregion

        #region ServiceManagementCommand
        private RelayCommand _svcManageCommand;

        /// <summary>
        /// Gets the ServiceManagementCommand.
        /// </summary>
        public RelayCommand ServiceManagementCommand
        {
            get
            {
                return _svcManageCommand
                    ?? (_svcManageCommand = new RelayCommand(OnManageService));
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
            var mbr = MessageBox.Show("请确认操作.", "注意", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                Runtime.UpdateList.Remove(um);

                DDNSPodRuntime.AppConfig.UpdateList = (from w in Runtime.UpdateList
                                                       select w.UnWrap()).ToList();
                DDNSPodRuntime.SaveAppConfig();
            }
        }

        private async void OnForceUpdate()
        {
            var updateModels = from u in Runtime.UpdateList select u.UnWrap();
            await DDNS.Start(updateModels, true);
            Runtime.UpdateList = new ObservableCollection<UpdateModelWrapper>(from u in updateModels
                                                                              select new UpdateModelWrapper(u));
            DDNSPodRuntime.AppConfig.UpdateList = updateModels.ToList();
            DDNSPodRuntime.SaveAppConfig();
        }

        private void GetServiceStatus()
        {
            if (service == null)
                service = ServiceControl.GetService();
            service.Refresh();
            ServiceStatus = ServiceControl.GetServiceStatus(service);
        }

        private async void OnManageService()
        {

            switch (ServiceStatus)
            {
                case ServiceStatus.Running:
                    MessageBox.Show("服务正在运行..");
                    break;
                case ServiceStatus.Stopped:
                    try
                    {
                        service.Start();
                        GetServiceStatus();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    break;
                case ServiceStatus.NotExist:
                    InstallService();
                    break;
                case ServiceStatus.UnKnown:
                    MessageBox.Show("无法检测服务状态,请稍候再试.");
                    break;
                default:
                    break;
            }
        }

        private void InstallService()
        {
            var dialogResult = MessageBox.Show("尚未安装DDNS服务,该服务将会在开机的时候自动运行,并且每30秒自动更新您的最新IP您的指定记录.在安装之前请确保您正在使用管理员权限运行本程序.是否继续?", "", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "installService.bat");
                if (File.Exists(path))
                {
                    ShellExecute(IntPtr.Zero, "Open", path, String.Empty, Directory.GetCurrentDirectory(), 0);
                    var task = new Task(() =>
                    {
                        System.Threading.Thread.Sleep(5000);
                        ServiceStatus = ServiceControl.GetServiceStatus(service);
                        MessageBox.Show("如果右上角服务状态处于非绿灯状态,请确保您正在使用管理员权限运行本程序,并且程序目录内的文件完整,最后查看程序目录下的DDNSPod.Service.InstallLog和sevice.log日志文件,向社区求助.");
                        if (service != null)
                        {
                            try
                            {
                                service.Start();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }
                        }
                    });
                }
                else
                {
                    MessageBox.Show("服务安装脚本丢失,请确保您的DDNSPod的程序目录完整!");
                }
            }
        }

        public override void Cleanup()
        {
            serviceStatusCheker.Stop();
            base.Cleanup();
        }

        [DllImport("shell32.dll")]
        public extern static int ShellExecute(IntPtr hwnd,
        string lpOperation,
        string lpFile,
        string lpParameters,
        string lpDirectory,
        int nShowCmd
        );
    }
}
