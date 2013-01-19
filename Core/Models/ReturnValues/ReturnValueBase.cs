using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Core.Models
{
    public class ReturnValueBase
    {
        [JsonProperty("status")]
        public CallbackStatus Status{get;set;}
    }

    public class CallbackStatus
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("created_at")]
        public DateTime ResponseDateTime { get; set; }
    }
}
