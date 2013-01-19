using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDnsPod.Core.Models
{
    public class DomainInfoModel
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("punycode")]
        public string PunyCode { get; set; }
        [JsonProperty("grade")]
        public string Grade { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("ext_status")]
        public string ExtStatus { get; set; }
        [JsonProperty("records")]
        public int Records { get; set; }
        [JsonProperty("group_id")]
        public string GroupID { get; set; }
        [JsonProperty("remark")]
        public string Remark { get; set; }
        [JsonProperty("is_mark")]
        public string IsMark { get; set; }
        [JsonProperty("is_vip")]
        public string IsVip { get; set; }
        [JsonProperty("updated_on")]
        public DateTime LastUpdate { get; set; }
        [JsonProperty("searchengine_push")]
        public string SearchEnginePush { get; set; }
        [JsonProperty("beian")]
        public string Beian { get; set; }
        [JsonProperty("user_id")]
        public string UserID { get; set; }
        [JsonProperty("ttl")]
        public int TTL { get; set; }
        [JsonProperty("created_on")]
        public DateTime DateCreated { get; set; }
    }
}
