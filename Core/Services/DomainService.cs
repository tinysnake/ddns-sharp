using DDnsSharp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Services
{
    public static class DomainService
    {
        public const string SERVICE_NAME = "Domain";

        public static async Task<DomainListRetrunValue> GetList(string domainType = "all", string groupID = null)
        {
            var requestModel = DDnsSharpRuntime.NewRequestModel <DomainListRequestModel>();
            requestModel.Type = domainType;
            requestModel.GroupID = groupID;
            return await ServiceHelper.AccessAPI<DomainListRetrunValue>(SERVICE_NAME, "List", requestModel);
        }

        public static async Task<DomainInfoReturnValue> GetInfo(int domainID)
        {
            var requestModel = DDnsSharpRuntime.NewRequestModel<DomainInfoRequestModel>();
            requestModel.DomainID = domainID;

            return await ServiceHelper.AccessAPI<DomainInfoReturnValue>(SERVICE_NAME, "Info", requestModel);
        }

        public static async Task<DomainInfoReturnValue> GetInfo(string domainName)
        {
            var requestModel = DDnsSharpRuntime.NewRequestModel<DomainInfoRequestModel>();
            requestModel.DomainName = domainName;
            return await ServiceHelper.AccessAPI<DomainInfoReturnValue>(SERVICE_NAME, "Info", requestModel);
        }

        public static async Task<DomainLogReturnValue> GetLog(int domainID)
        {
            var requestModel = DDnsSharpRuntime.NewRequestModel<DomainInfoRequestModel>();
            requestModel.DomainID = domainID;
            return await ServiceHelper.AccessAPI<DomainLogReturnValue>(SERVICE_NAME, "Log", requestModel);
        }
    }
}
