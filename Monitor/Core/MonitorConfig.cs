using DDnsPod.Monitor.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Monitor.Core
{
    public class MonitorConfig
    {
        public const string HideOnStartupPropertyName = "HideOnStartup";
        static MonitorConfig()
        {

            if (ConfigurationManager.AppSettings[HideOnStartupPropertyName] != null)
                _hideOnStartup = Convert.ToBoolean(ConfigurationManager.AppSettings[HideOnStartupPropertyName]);
        }
        private static bool _hideOnStartup;
        public static bool HideOnStartup
        {
            get 
            {
                return _hideOnStartup;
            }
            set
            {
                if (_hideOnStartup == value)
                    return;
                _hideOnStartup = value;
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(Environment.GetCommandLineArgs()[0]);
                if (configuration.AppSettings.Settings[HideOnStartupPropertyName] != null)
                {
                    configuration.AppSettings.Settings.Remove(HideOnStartupPropertyName);
                }
                configuration.AppSettings.Settings.Add(HideOnStartupPropertyName, value.ToString());
                configuration.Save();
            }
        }
    }
}
