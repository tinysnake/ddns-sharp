using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Models
{
    public class CreateRecordRequestModel:RequestModelBase
    {
        [RequestModelAlias("domain_id")]
        public int DomainID { get; set; }
        [RequestModelAlias("sub_domain")]
        public string SubDomain { get; set; }
        [RequestModelAlias("record_type")]
        public string RecordType { get; set; }
        [RequestModelAlias("record_line")]
        public string LineName { get; set; }
        [RequestModelAlias("value")]
        public string Value { get; set; }
        [RequestModelAlias("mx")]
        public int MX { get; set; }
        [RequestModelAlias("ttl")]
        public int TTL { get; set; }
    }
}
