using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Monitor.Utils
{
    public class ConfigurationHelper
    {
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void SetValue(string key, object value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(Environment.GetCommandLineArgs()[0]);
            if (configuration.AppSettings.Settings[key] != null)
            {
                configuration.AppSettings.Settings.Remove(key);
            }
            configuration.AppSettings.Settings.Add(key, value.ToString());
            configuration.Save();
        }
    }
}
