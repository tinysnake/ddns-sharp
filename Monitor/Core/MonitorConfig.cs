using DDnsSharp.Monitor.Utils;
using DDnsSharp.Monitor.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Monitor.Core
{
    public class MonitorConfig
    {

        public MonitorConfig()
        {

            _hideOnStartup = Convert.ToBoolean(ConfigurationHelper.GetValue(HideOnStartupPropertyName));
            _hideOnWindowClosed = Convert.ToBoolean(ConfigurationHelper.GetValue(HideOnWindowClosedPropertyName));
            _neverShowCloseActionWindow = Convert.ToBoolean(ConfigurationHelper.GetValue(NeverShowCloseActionWindowPropertyName));
        }

        public const string HideOnStartupPropertyName = "HideOnStartup";

        private bool _hideOnStartup;

        public bool HideOnStartup
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
                ConfigurationHelper.SetValue(HideOnStartupPropertyName, value);
            }
        }

        public const string HideOnWindowClosedPropertyName = "HideOnWindowClosed";

        private bool _hideOnWindowClosed;

        public bool HideOnWindowsClosed
        {
            get
            {
                return _hideOnWindowClosed;
            }
            set
            {
                if (_hideOnWindowClosed == value)
                    return;
                _hideOnWindowClosed = value;
                ConfigurationHelper.SetValue(HideOnWindowClosedPropertyName, value);
            }
        }

        public const string NeverShowCloseActionWindowPropertyName = "NeverShowCloseActionWindow";

        private bool _neverShowCloseActionWindow;

        public bool NeverShowCloseActionWindow
        {
            get { return _neverShowCloseActionWindow; }
            set
            {
                if (_neverShowCloseActionWindow == value)
                    return;
                _neverShowCloseActionWindow = value;
                ConfigurationHelper.SetValue(NeverShowCloseActionWindowPropertyName, value);
            }
        }

    }
}
