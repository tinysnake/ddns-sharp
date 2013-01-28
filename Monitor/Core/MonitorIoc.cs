using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Monitor.Core
{
    public static class MonitorIoc
    {
        private static IKernel kernel = new StandardKernel();

        public static IKernel Current
        {
            get { return kernel; }
        }
    }
}
