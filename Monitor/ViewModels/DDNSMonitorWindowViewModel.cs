using DDnsSharp.Core;
using DDnsSharp.Core.Services;
using DDnsSharp.Monitor.Core;
using DDnsSharp.Monitor.Models;
using DDnsSharp.Monitor.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Monitor;
using Ninject;
using NLog;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace DDnsSharp.Monitor.ViewModels
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
            Runtime.SetUpdateList(DDnsSharpRuntime.AppConfig.UpdateList);
            service = ServiceControl.GetService();
            GetServiceStatus();
            serviceStatusCheker = TimerDispatch.Current.AddInterval((m)=>GetServiceStatus(),5000);
            updateListRefresher = TimerDispatch.Current.AddInterval((m) => RefreshUpdateList(), 15000);
            UpdateCurrentIP();
        }

        private async void UpdateCurrentIP()
        {
            CurrentIP = await CommonService.GetCurrentIP();
        }

        private Logger logger = LogManager.GetCurrentClassLogger();
        private ServiceController service;
        private TimerModel serviceStatusCheker;
        private TimerModel updateListRefresher;

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

                    DDnsSharpRuntime.AppConfig.UpdateList = (from w in Runtime.UpdateList
                                                           select w.UnWrap()).ToList();
                    DDnsSharpRuntime.SaveAppConfig();
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

                    DDnsSharpRuntime.AppConfig.UpdateList = (from w in Runtime.UpdateList
                                                           select w.UnWrap()).ToList();
                    DDnsSharpRuntime.SaveAppConfig();
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
                    ?? (_deleteRecordCommand = new RelayCommand<UpdateModelWrapper>(DeleteRecrod));
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
                    var wins = Application.Current.Windows;
                    foreach (Window win in wins)
                    {
                        if (win is DDNSMonitorWindow)
                        {
                            (win as DDNSMonitorWindow).Dispose();
                        }
                        win.Close();
                    }
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
                        Path.Combine(Directory.GetCurrentDirectory(), "ddnssharp.info"));
                }));
            }
        }
        #endregion

        #region ServiceManagementCommand
        private RelayCommand<string> _svcManageCommand;

        /// <summary>
        /// Gets the ServiceManagementCommand.
        /// </summary>
        public RelayCommand<string> ServiceManagementCommand
        {
            get
            {
                return _svcManageCommand
                    ?? (_svcManageCommand = new RelayCommand<string>(OnManageService));
            }
        }
        #endregion

        #region OpenSettingsCommand
        private RelayCommand _openSettingsCommand;

        /// <summary>
        /// Gets the OpenSettingsCommand.
        /// </summary>
        public RelayCommand OpenSettingsCommand
        {
            get
            {
                return _openSettingsCommand
                    ?? (_openSettingsCommand = new RelayCommand(() =>
                {
                    var win = new SettingsWindow();
                    win.ShowDialog();
                }));
            }
        }
        #endregion

        #region OpenAboutWindowCommand
        private RelayCommand _openAboutWinCommand;

        /// <summary>
        /// Gets the OpenAboutWindowCommand.
        /// </summary>
        public RelayCommand OpenAboutWindowCommand
        {
            get
            {
                return _openAboutWinCommand
                    ?? (_openAboutWinCommand = new RelayCommand(() =>
                {
                    var win = new AboutWindow();
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

            DDnsSharpRuntime.AppConfig.UpdateList = (from w in Runtime.UpdateList
                                                   select w.UnWrap()).ToList();
            DDnsSharpRuntime.SaveAppConfig();
        }

        private void DeleteRecrod(UpdateModelWrapper um)
        {
            var mbr = MessageBox.Show("请确认操作.", "注意", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                Runtime.UpdateList.Remove(um);

                DDnsSharpRuntime.AppConfig.UpdateList = (from w in Runtime.UpdateList
                                                       select w.UnWrap()).ToList();
                DDnsSharpRuntime.SaveAppConfig();
            }
        }

        private async void OnForceUpdate()
        {
            var updateModels = from u in Runtime.UpdateList select u.UnWrap();
            try
            {
                await DDNS.Start(updateModels, true);
            }
            catch(WebException)
            {
                MessageBox.Show("无法连接至服务器.");
            }
            Runtime.SetUpdateList(updateModels);
            DDnsSharpRuntime.AppConfig.UpdateList = updateModels.ToList();
            DDnsSharpRuntime.SaveAppConfig();
        }

        private void GetServiceStatus()
        {
            if (service != null)
                service.Refresh();
            ServiceStatus = ServiceControl.GetServiceStatus(service);
        }

        private void RefreshUpdateList()
        {
            DDnsSharpRuntime.LoadAppConfig();
            Runtime.SetUpdateList(DDnsSharpRuntime.AppConfig.UpdateList);
        }

        private void OnManageService(string cmd)
        {
            switch (cmd)
            {
                case "start":
                    if (ServiceStatus == ServiceStatus.Stopped)
                    {
                        try
                        {
                            service.Start();
                            GetServiceStatus();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                    else
                        MessageBox.Show("无法启动服务.");
                    break;
                case "stop":
                    if (ServiceStatus == ServiceStatus.Running)
                    {
                        try
                        {
                            service.Stop();
                            GetServiceStatus();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                    else
                        MessageBox.Show("无法停止服务.");
                    break;
                case "install":
                    if (ServiceStatus == ServiceStatus.NotExist)
                    {
                        InstallService();
                    }
                    else
                        MessageBox.Show("服务已存在.");
                    break;
                case "uninstall":
                    if (ServiceStatus != ServiceStatus.NotExist)
                    {
                        UninstallService();
                    }
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
                        try
                        {
                            if (service != null)
                                service.Dispose();
                            service = ServiceControl.GetService();
                            ServiceStatus = ServiceControl.GetServiceStatus(service);
                            MessageBox.Show("如果左下角的服务状态在1分钟之后还是处于非绿灯状态,请确保您正在使用管理员权限运行本程序,并且程序目录内的文件完整,最后查看程序目录下的DDNSPod.Service.InstallLog和sevice.log日志文件,并向社区求助.");
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
                        }
                        catch (Win32Exception ex)
                        {
                            logger.ErrorException(ex.Message, ex);
                        }
                    });
                    task.Start();
                }
                else
                {
                    MessageBox.Show("服务安装脚本丢失,请确保您的DDNSPod的程序目录完整!");
                }
            }
        }

        private void UninstallService()
        {
            var dialogResult = MessageBox.Show("您确定这么做吗?", "", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "uninstallService.bat");
                if (File.Exists(path))
                {
                    if (service != null)
                    {
                        try
                        {
                            if(service.CanStop)
                            service.Stop();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        ShellExecute(IntPtr.Zero, "Open", path, String.Empty, Directory.GetCurrentDirectory(), 0);
                        var task = new Task(() =>
                        {
                            System.Threading.Thread.Sleep(5000);
                            if (service != null)
                            {
                                service.Dispose();
                                service = ServiceControl.GetService();
                            }
                            ServiceStatus = ServiceControl.GetServiceStatus(service);
                            MessageBox.Show("如果左下角的服务状态在1分钟之后还是处于非红色叉叉状态,请确保您正在使用管理员权限运行本程序,并且程序目录内的文件完整,最后查看程序目录下的DDNSPod.Service.InstallLog和sevice.log日志文件,并向社区求助.");
                        });
                        task.Start();
                    }
                }
                else
                {
                    MessageBox.Show("服务安装脚本丢失,请确保您的DDNSPod的程序目录完整!");
                }
            }
        }

        public override void Cleanup()
        {
            if (service != null)
                service.Dispose();
            TimerDispatch.Current.RemoveTimer(serviceStatusCheker);
            TimerDispatch.Current.RemoveTimer(updateListRefresher);
            base.Cleanup();
        }

        [DllImport("shell32.dll")]
        public extern static int ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);
    }
}
