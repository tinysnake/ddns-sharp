using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Core.Models
{
    public class RecordListReturnValue : ReturnValueBase
    {
        [JsonProperty("records")]
        public IEnumerable<RecordModel> Records { get; set; }
    }
}
