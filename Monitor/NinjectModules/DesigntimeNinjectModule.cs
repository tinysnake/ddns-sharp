using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using DDnsSharp.Monitor.ViewModels;
using DDnsSharp.Core.Models;
using DDnsSharp.Monitor.Design;
using DDnsSharp.Monitor.Models;

namespace DDnsSharp.Monitor.NinjectModules
{
    public class DesigntimeNinjectModule:NinjectModule
    {
        public override void Load()
        {
            Bind<LoginWindowViewModel>().ToSelf();
            Bind<DDNSMonitorWindowViewModel>().ToSelf();
            Bind<RecordManageWindowViewModel>().ToSelf();
            Bind<MonitorRuntime>().To<DesigntimeMonitorRuntime>().InSingletonScope();
        }
    }
}
