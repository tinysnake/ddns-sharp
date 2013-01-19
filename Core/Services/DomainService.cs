using DDnsPod.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Core.Services
{
    public static class DomainService
    {
        public const string SERVICE_NAME = "Domain";

        public static async Task<DomainListRetrunValue> GetList(string domainType = "all", string groupID = null)
        {
            var requestModel = DDNSPodRuntime.NewRequestModel <DomainListRequestModel>();
            requestModel.Type = domainType;
            requestModel.GroupID = groupID;
            return await ServiceHelper.AccessAPI<DomainListRetrunValue>(SERVICE_NAME, "List", requestModel);
        }

        public static async Task<DomainInfoReturnValue> GetInfo(int domainID)
        {
            var requestModel = DDNSPodRuntime.NewRequestModel<DomainInfoRequestModel>();
            requestModel.DomainID = domainID;

            return await ServiceHelper.AccessAPI<DomainInfoReturnValue>(SERVICE_NAME, "Info", requestModel);
        }

        public static async Task<DomainInfoReturnValue> GetInfo(string domainName)
        {
            var requestModel = DDNSPodRuntime.NewRequestModel<DomainInfoRequestModel>();
            requestModel.DomainName = domainName;
            return await ServiceHelper.AccessAPI<DomainInfoReturnValue>(SERVICE_NAME, "Info", requestModel);
        }

        public static async Task<DomainLogReturnValue> GetLog(int domainID)
        {
            var requestModel = DDNSPodRuntime.NewRequestModel<DomainInfoRequestModel>();
            requestModel.DomainID = domainID;
            return await ServiceHelper.AccessAPI<DomainLogReturnValue>(SERVICE_NAME, "Log", requestModel);
        }
    }
}
