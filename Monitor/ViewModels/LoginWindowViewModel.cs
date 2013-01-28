using DDnsSharp.Monitor.Core;
using DDnsSharp.Monitor.Views;
using DDnsSharp.Core;
using DDnsSharp.Core.Models;
using DDnsSharp.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Timers;
using System.Windows;
using Monitor;
using DDnsSharp.Monitor.Models;
using Ninject;
using System.Net;

namespace DDnsSharp.Monitor.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LoginWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the LoginWindowViewModel class.
        /// </summary>
        public LoginWindowViewModel()
        {
            _runtime = MonitorIoc.Current.Get<MonitorRuntime>();
            _loginFailedCount = 0;
            _ableToLogin = true;
        }

        private int _loginFailedCount;
        private bool _ableToLogin;
        private Timer _loginTimer;
        private MonitorRuntime _runtime;

        #region INPC
        /// <summary>
        /// The <see cref="LoginEmail" /> property's name.
        /// </summary>
        public const string LoginEmailPropertyName = "LoginEmail";

        /// <summary>
        /// Sets and gets the LoginEmail property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LoginEmail
        {
            get
            {
                return DDnsSharpRuntime.AppConfig.Email;
            }

            set
            {
                if (DDnsSharpRuntime.AppConfig.Email == value)
                {
                    return;
                }

                RaisePropertyChanging(LoginEmailPropertyName);
                DDnsSharpRuntime.AppConfig.Email = value;
                RaisePropertyChanged(LoginEmailPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Password" /> property's name.
        /// </summary>
        public const string PasswordPropertyName = "Password";

        /// <summary>
        /// Sets and gets the Password property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Password
        {
            get
            {
                return DDnsSharpRuntime.AppConfig.Password;
            }

            set
            {
                if (DDnsSharpRuntime.AppConfig.Password == value)
                {
                    return;
                }

                RaisePropertyChanging(PasswordPropertyName);
                DDnsSharpRuntime.AppConfig.Password = value;
                RaisePropertyChanged(PasswordPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ErrorMessage" /> property's name.
        /// </summary>
        public const string ErrorMessagePropertyName = "ErrorMessage";

        private string _errorMsg = String.Empty;

        /// <summary>
        /// Sets and gets the ErrorMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return _errorMsg;
            }

            set
            {
                if (_errorMsg == value)
                {
                    return;
                }

                RaisePropertyChanging(ErrorMessagePropertyName);
                _errorMsg = value;
                ShowErrorMessage = !String.IsNullOrEmpty(_errorMsg);
                RaisePropertyChanged(ErrorMessagePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ShowErrorMessage" /> property's name.
        /// </summary>
        public const string ShowErrorMessagePropertyName = "ShowErrorMessage";

        private bool _showErrorMessage = false;

        /// <summary>
        /// Sets and gets the ShowErrorMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ShowErrorMessage
        {
            get
            {
                return _showErrorMessage;
            }

            set
            {
                if (_showErrorMessage == value)
                {
                    return;
                }

                RaisePropertyChanging(ShowErrorMessagePropertyName);
                _showErrorMessage = value;
                RaisePropertyChanged(ShowErrorMessagePropertyName);
            }
        }
        #endregion

        #region LoginCommand
        private RelayCommand _loginCommand;

        /// <summary>
        /// Gets the LoginCommand.
        /// </summary>
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand
                    ?? (_loginCommand = new RelayCommand(UserLogin, () => _ableToLogin));
            }
        }

        private async void UserLogin()
        {
            UserInfoReturnValue userInfo;
            try
            {
                userInfo = await CommonService.GetUserInfo();
            }
            catch(WebException)
            {
                MessageBox.Show("无法连接至服务器.");
                return;
            }
            if (userInfo.Status.Code == 1)
            {
                _runtime.UserInfo = userInfo.Info;
                DDnsSharpRuntime.SaveAppConfig();

                var mwin = new DDNSMonitorWindow();
                mwin.Show();
                foreach (Window win in App.Current.Windows)
                {
                    if (win != mwin)
                        win.Close();
                }
            }
            else
            {
                _loginFailedCount++;
                if (_loginFailedCount == 10)
                {
                    ErrorMessage = "登陆次数超过已达10次,请5分钟后再试.";
                    if (_loginTimer == null)
                        _loginTimer = new Timer();
                    _loginTimer.Interval = 300000;
                    _loginTimer.Start();
                    _loginTimer.Elapsed += (o, e) =>
                    {
                        _ableToLogin = true;
                        LoginCommand.RaiseCanExecuteChanged();
                        _loginTimer.Stop();
                        _loginTimer.Dispose();
                        _loginTimer = null;
                    };
                    _ableToLogin = false;
                    LoginCommand.RaiseCanExecuteChanged();
                }
                else
                {
                    ErrorMessage = userInfo.Status.Message;
                }
            }
        }
        #endregion

        #region SignUpCommand
        private RelayCommand _signUpCommand;

        /// <summary>
        /// Gets the SignUpCommand.
        /// </summary>
        public RelayCommand SignUpCommand
        {
            get
            {
                return _signUpCommand
                    ?? (_signUpCommand = new RelayCommand(() =>
                {
                    System.Diagnostics.Process.Start("https://www.dnspod.cn/SignUp");
                }));
            }
        }
        #endregion

        #region ExitAppCommand
        private RelayCommand _exitAppCommand;

        /// <summary>
        /// Gets the ExitAppCommand.
        /// </summary>
        public RelayCommand ExitAppCommand
        {
            get
            {
                return _exitAppCommand
                    ?? (_exitAppCommand = new RelayCommand(() =>
                {
                    Environment.Exit(0);
                }));
            }
        }
        #endregion
    }
}