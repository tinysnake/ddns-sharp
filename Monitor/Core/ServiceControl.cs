using DDnsPod.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Monitor.Core
{
    public enum ServiceStatus
    {
        Running,
        Stopped,
        NotExist,
        UnKnown
    }

    public class ServiceControl
    {
        public static ServiceController GetService()
        {
            ServiceController[] services = ServiceController.GetServices();
            var sc = services.FirstOrDefault(_ => _.ServiceName == "DDNSPodService");
            return sc;
        }

        public static ServiceStatus GetServiceStatus(ServiceController sc)
        {
            if (sc != null)
            {
                var status = ServiceStatus.UnKnown;
                try
                {
                    switch (sc.Status)
                    {
                        case ServiceControllerStatus.Running:
                            status = ServiceStatus.Running;
                            break;
                        case ServiceControllerStatus.Stopped:
                            status = ServiceStatus.Stopped;
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                }
                return status;
            }
            else
                return ServiceStatus.NotExist;
        }

        public static void InstallServiceStatus()
        {
        }
    }
}
