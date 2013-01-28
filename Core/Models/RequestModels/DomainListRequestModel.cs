using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Models
{
    public class DomainListRequestModel:RequestModelBase
    {
        [RequestModelAlias("type")]
        public string Type { get; set; }
        [RequestModelAlias("group_id")]
        public string GroupID { get; set; }
    }
}
