using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Models
{
    public class RequestModelBase
    {
        public RequestModelBase()
        {
            Format = "json";
            Language = "cn";
            ErrorOnEmpty = "no";
        }

        [RequestModelAlias("login_email")]
        public string LoginEmail { get; set; }

        [RequestModelAlias("login_password")]
        public string LoginPassword { get; set; }

        [RequestModelAlias("format")]
        public string Format { get; set; }

        [RequestModelAlias("lang")]
        public string Language { get; set; }

        [RequestModelAlias("error_on_empty")]
        public string ErrorOnEmpty { get; set; }

        public string ToQueryString()
        {
            var type = this.GetType();
            var props = type.GetProperties();
            var kvps = new List<string>();
            foreach (var p in props)
            {
                if (p.CanRead)
                {
                    object value = p.GetValue(this);
                    if(value==null)
                        continue;
                    var attrs = p.GetCustomAttributes(typeof(RequestModelAliasAttribute), false);
                    var name = p.Name;
                    if (attrs.Length > 0)
                    {
                        var attr = attrs[0] as RequestModelAliasAttribute;
                        name = attr.Alias;
                    }
                    kvps.Add(String.Format("{0}={1}", name, value));
                }

            }
            return String.Join("&", kvps);
        }
    }
}
