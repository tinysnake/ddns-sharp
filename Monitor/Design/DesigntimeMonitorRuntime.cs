using DDnsPod.Monitor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Monitor.Design
{
    public class DesigntimeMonitorRuntime:MonitorRuntime
    {
        public DesigntimeMonitorRuntime()
        {
            UserInfo = new DesigntimeUserInfo();
            var list = new[]
            {
                new UpdateModelWrapper(new DesigntimeUpdateModel()),
                new UpdateModelWrapper(new DesigntimeUpdateModel()),
                new UpdateModelWrapper(new DesigntimeUpdateModel()),
                new UpdateModelWrapper(new DesigntimeUpdateModel()),
                new UpdateModelWrapper(new DesigntimeUpdateModel()),
                new UpdateModelWrapper(new DesigntimeUpdateModel())
            };
            UpdateList = new ObservableCollection<UpdateModelWrapper>(list);
        }
    }
}
