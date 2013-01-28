using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Models
{
    public class UserInfoReturnValue :ReturnValueBase
    {
        [JsonProperty("info")]
        public UserInfo Info { get; set; }
    }
}
