using DDnsPod.Monitor.Core;
using DDnsPod.Monitor.Models;
using DDnsPod.Monitor.ViewModels;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Monitor.NinjectModules
{
    class RuntimeNinjectModule:NinjectModule
    {
        public override void Load()
        {
            Bind<MonitorRuntime>().ToSelf().InSingletonScope();
            Bind<TempStorage>().ToSelf().InSingletonScope();
            Bind<DomainsCache>().ToSelf().InSingletonScope();

            Bind<LoginWindowViewModel>().ToSelf();
            Bind<DDNSMonitorWindowViewModel>().ToSelf();
            Bind<RecordManageWindowViewModel>().ToSelf();
            Bind<SettingsWindowViewModel>().ToSelf();
        }
    }
}
