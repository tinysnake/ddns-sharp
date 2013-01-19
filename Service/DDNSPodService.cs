using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
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
        }

        private Logger logger;

        private Timer timer;

        protected override void OnStart(string[] args)
        {
            logger = LogManager.GetCurrentClassLogger();
            timer = new Timer(3000);
            timer.Elapsed += timer_Elapsed;
            timer.Start();

            logger.Info("DDNSPodService Stopped");
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
