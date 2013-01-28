using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Models
{
    public class RequestModelAliasAttribute : Attribute
    {
        public RequestModelAliasAttribute(string alias)
        {
            Alias = alias;
        }

        public string Alias { get; set; }
    }
}
