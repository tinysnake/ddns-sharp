using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDnsPod.Core.Models
{
    public class UpdateModel
    {
        public string DomainName { get; set; }
        public string SubDomain { get; set; }
        public string LineName { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public string LastUpdateIP { get; set; }
        public bool Enabled { get; set; }
        [JsonIgnore]
        public int DomainID { get; set; }
        [JsonIgnore]
        public int RecordID { get; set; }
    }
}
