using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Models
{
    public class AppConfig
    {
        public AppConfig()
        {
            UpdateList = new List<UpdateModel>();
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<UpdateModel> UpdateList { get; set; }
    }
}
