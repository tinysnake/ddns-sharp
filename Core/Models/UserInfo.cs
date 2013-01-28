using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDnsSharp.Core.Models
{
    public class UserInfo
    {
        [JsonProperty("user")]
        public UserModel Model { get; set; }
        [JsonProperty("agent")]
        public UserAgent Agent { get; set; }
    }

    public class UserModel
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("email_verified")]
        public string EmailVerified { get; set; }
        [JsonProperty("telephone_verified")]
        public string TelVerified { get; set; }
        [JsonProperty("agent_pending")]
        public bool AgentPending { get; set; }
        [JsonProperty("real_name")]
        public string RealName { get; set; }
        [JsonProperty("user_type")]
        public string Type { get; set; }
        [JsonProperty("telephone")]
        public string Tel { get; set; }
        [JsonProperty("im")]
        public string IMNumber { get; set; }
        [JsonProperty("nick")]
        public string Name { get; set; }
        [JsonProperty("balance")]
        public int Balance { get; set; }
        [JsonProperty("smsbalance")]
        public int SMSBalance { get; set; }
    }
    public class UserAgent
    {
        [JsonProperty("user_id")]
        public string UserID { get; set; }
        [JsonProperty("discount")]
        public int Discount { get; set; }
        [JsonProperty("points")]
        public int Points { get; set; }
        [JsonProperty("users")]
        public int Users { get; set; }
    }
}
