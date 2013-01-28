using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Models
{
    class UpdateDDNSRequestModel:RequestModelBase
    {
        [RequestModelAlias("domain_id")]
        public int DomainID { get; set; }
        [RequestModelAlias("record_id")]
        public int RecordID { get; set; }
        [RequestModelAlias("sub_domain")]
        public string SubDomain { get; set; }
        [RequestModelAlias("record_line")]
        public string LineName { get; set; }
    }
}
