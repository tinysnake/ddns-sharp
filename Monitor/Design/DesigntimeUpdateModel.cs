using DDnsSharp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Monitor.Design
{
    public class DesigntimeUpdateModel:UpdateModel
    {
        public DesigntimeUpdateModel()
        {
            var random = new Random();
            this.DomainName = new[] { "google.com", "tiny-snake.com", "github.com", "amazon.com" }[random.Next(0,3)];
            this.SubDomain = new[] { "www","test","@","pay","drive"}[random.Next(0,4)];
            this.LastUpdateIP = "255.255.255.255";
            this.Enabled = random.NextDouble() > .5f ? true : false;
            this.LineName = "默认";
        }
    }
}
