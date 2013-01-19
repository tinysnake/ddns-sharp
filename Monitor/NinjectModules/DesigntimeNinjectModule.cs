using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using DDnsPod.Monitor.ViewModels;
using DDnsPod.Core.Models;
using DDnsPod.Monitor.Design;

namespace DDnsPod.Monitor.NinjectModules
{
    public class DesigntimeNinjectModule:NinjectModule
    {
        public override void Load()
        {
            Bind<LoginWindowViewModel>().ToSelf();
            Bind<DDNSMonitorWindowViewModel>().ToSelf();
            Bind<RecordManageWindowViewModel>().ToSelf();
            Bind<UserInfo>().To<DesigntimeUserInfo>();
        }
    }
}
