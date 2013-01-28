using DDnsSharp.Monitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Monitor.Design
{
    public class DesigntimeUpdateModelWrapper:UpdateModelWrapper
    {
        public DesigntimeUpdateModelWrapper()
            :base(new DesigntimeUpdateModel())
        {

        }
    }
}
