using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Core.Models
{
    public class RecordListRequestModel:RequestModelBase
    {
        [RequestModelAlias("domain_id")]
        public int DomainID { get; set; }
        [RequestModelAlias("offset")]
        public int Offset { get; set; }
        [RequestModelAlias("length")]
        public int Length { get; set; }
        [RequestModelAlias("sub_domain")]
        public string SubDomain { get; set; }
    }
}
