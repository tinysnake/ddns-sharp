using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Models
{
    public class RecordModel
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("line")]
        public string LineName { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("ttl")]
        public int TTL { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("mx")]
        public int MX { get; set; }
        [JsonProperty("enabled")]
        public int Enabled { get; set; }
        [JsonProperty("monitor_status")]
        public string MonitorStatus { get; set; }
        [JsonProperty("remark")]
        public string Remark { get; set; }
        [JsonProperty("updated_on")]
        public DateTime DateUpdated { get; set; }
    }
}
