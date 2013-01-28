using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Models
{
    public class DomainInfoRequestModel:RequestModelBase
    {
        [RequestModelAlias("domain_id")]
        public int DomainID { get; set; }
        [RequestModelAlias("domain")]
        public string DomainName { get; set; }
    }
}
