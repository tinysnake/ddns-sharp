using DDnsPod.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Monitor.Design
{
    public class DesigntimeAppConfig : AppConfig
    {
        public DesigntimeAppConfig()
        {
            this.Email = "tff.assist@gmail.com";
            this.UpdateList = new[]{
                new UpdateModel()
                {
                    DomainName="google.com",
                    SubDomain="@",
                    Enabled=true,
                    LastUpdatedTime=DateTime.Now,
                    LastUpdateIP="255.255.255.255",
                    LineName="默认"
                },
                new UpdateModel()
                {
                    DomainName="tiny-snake.com",
                    SubDomain="www",
                    Enabled=false,
                    LastUpdatedTime=DateTime.Now,
                    LastUpdateIP="255.255.255.255",
                    LineName="电信"
                },
                new UpdateModel()
                {
                    DomainName="github.com",
                    SubDomain="gist",
                    Enabled=true,
                    LastUpdatedTime=DateTime.Now,
                    LastUpdateIP="255.255.255.255",
                    LineName="国外"
                }
            }.ToList();
        }
    }
}
