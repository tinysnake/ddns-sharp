using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Monitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static EventWaitHandle s_event;

        public App()
        {

            bool created;
            s_event = new EventWaitHandle(false,
                EventResetMode.ManualReset, "DDnsSharp#startup", out created);
            if (!created)
            {
                MessageBox.Show("DDnsSharp正在运行,请关注系统托盘图标.");
                Environment.Exit(0);
            }

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
