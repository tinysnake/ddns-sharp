using DDnsSharp.Core.Models;
using DDnsSharp.Core.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDnsSharp.Core
{
    public class DDnsSharpRuntime
    {
        static DDnsSharpRuntime()
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
            {
                cfgSvc.Save(AppConfig);
                if (ConfigSaved != null)
                    ConfigSaved(AppConfig, EventArgs.Empty);
            }
            else
                throw new ArgumentNullException("AppConfig");
        }

        public static event EventHandler ConfigSaved;

        public static T NewRequestModel<T>()
            where T : RequestModelBase
        {
            var c = DDnsSharpRuntime.AppConfig;
            var instance = Activator.CreateInstance<T>();
            instance.LoginEmail = c.Email;
            instance.LoginPassword = c.Password;
            return instance;
        }
    }
}
