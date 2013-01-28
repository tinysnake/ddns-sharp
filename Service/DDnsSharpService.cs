using DDnsSharp.Core;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DDnsSharp.Service
{
    partial class DDnsSharpService : ServiceBase
    {
        public DDnsSharpService()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            InitializeComponent();
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            System.IO.Directory.SetCurrentDirectory(path);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.FatalException("服务出现致命异常.", e.ExceptionObject as Exception);
        }

        private Logger logger;

        private Timer timer;

        protected override void OnStart(string[] args)
        {
            logger = LogManager.GetCurrentClassLogger();
            try
            {
                DDnsSharpRuntime.LoadAppConfig();
            }
            catch (IOException)
            {
                logger.Fatal("无法获取到DDnsSharp配置,服务停止.");
                this.Stop();
                return;
            }

            timer = new Timer(30000);
            timer.Elapsed += timer_Elapsed;
            timer.Start();

            OnJob();

        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnJob();
        }

        protected override void OnStop()
        {
        }

        private async void OnJob()
        {
            await DDNS.Start(DDnsSharpRuntime.AppConfig.UpdateList);
            DDnsSharpRuntime.SaveAppConfig();
        }
    }
}
