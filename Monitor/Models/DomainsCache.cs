using DDnsPod.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Monitor.Core
{
    public class DomainsCache
    {
        public DomainsCache()
        {
            DomainInfos = new List<DomainInfoModel>();
            DomainRecordSet = new Dictionary<DomainInfoModel, List<RecordModel>>();
        }
        public List<DomainInfoModel> DomainInfos { get; set; }
        public Dictionary<DomainInfoModel, List<RecordModel>> DomainRecordSet { get; set; }
    }
}
