using DDnsPod.Core.Models;
using DDnsPod.Core.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Core
{
    public class DDNSPodRuntime
    {
        static DDNSPodRuntime()
        {
            AppConfig = new AppConfig();
        }

        private static AppConfigService cfgSvc;
        public static AppConfig AppConfig { get; private set; }

        public static void LoadAppConfig()
        {
            if(cfgSvc==null)
                cfgSvc = new AppConfigService();
            AppConfig = cfgSvc.Read();
        }

        public static void SaveAppConfig()
        {
            if (cfgSvc == null)
                cfgSvc = new AppConfigService();
            if (AppConfig != null)
                cfgSvc.Save(AppConfig);
            else
                throw new ArgumentNullException("AppConfig");
        }

        public static T NewRequestModel<T>()
            where T : RequestModelBase
        {
            var c = DDNSPodRuntime.AppConfig;
            var instance = Activator.CreateInstance<T>();
            instance.LoginEmail = c.Email;
            instance.LoginPassword = c.Password;
            return instance;
        }
    }
}
