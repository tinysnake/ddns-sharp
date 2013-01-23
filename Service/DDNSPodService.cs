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

namespace DDnsPod.Service
{
    partial class DDNSPodService : ServiceBase
    {
        public DDNSPodService()
        {
            InitializeComponent();
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            System.IO.Directory.SetCurrentDirectory(path);
        }

        private Logger logger;

        private Timer timer;

        protected override void OnStart(string[] args)
        {
            logger = LogManager.GetCurrentClassLogger();
            timer = new Timer(3000);
            timer.Elapsed += timer_Elapsed;
            timer.Start();

            logger.Info("DDNSPodService Started");
            logger.Info("Assembly.GetEntryAssembly().Location: " + System.Reflection.Assembly.GetEntryAssembly().Location);
            logger.Info("Environment.CurrentDirectory: "+Environment.CurrentDirectory);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            logger.Info("DDNSPodService Ticking");
        }

        protected override void OnStop()
        {
            logger.Info("DDNSPodService Stopped");
        }
    }
}
