using DDnsSharp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Monitor.Design
{
    public class DesigntimeUserInfo:UserInfo
    {
        public DesigntimeUserInfo()
        {
            this.Model = new UserModel();
            this.Agent = new UserAgent();
            this.Model.Email = "tff.assist@gmail.com";
            this.Model.Name = "SNAKE";
            this.Model.Type = "D_Free";
        }
    }
}
