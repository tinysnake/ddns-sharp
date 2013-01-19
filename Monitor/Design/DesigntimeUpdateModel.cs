using DDnsPod.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Monitor.Design
{
    public class DesigntimeUpdateModel:UpdateModel
    {
        public DesigntimeUpdateModel()
        {
            this.DomainName = "google.com";
            this.SubDomain = "disk";
            this.LineName = "默认";
        }
    }
}
