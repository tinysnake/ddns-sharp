using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Monitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            System.IO.Directory.SetCurrentDirectory(path);
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private Logger logger = LogManager.GetCurrentClassLogger();

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            logger.FatalException(e.Exception.Message, e.Exception);
        }
    }
}
