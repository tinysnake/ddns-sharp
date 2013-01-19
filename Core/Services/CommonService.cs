using DDnsPod.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DDnsPod.Core.Services
{
    public static class CommonService
    {
        public static async Task<ReturnValueBase> GetAPIVersion()
        {
            var requestModel = DDNSPodRuntime.NewRequestModel<RequestModelBase>();
            var url = ServiceHelper.BuildAPIUrl("Info", "Version");
            return await ServiceHelper.AccessAPI<ReturnValueBase>(url, requestModel);
        }

        public static async Task<UserInfoReturnValue> GetUserInfo()
        {
            var requestModel = DDNSPodRuntime.NewRequestModel<RequestModelBase>();
            var url = ServiceHelper.BuildAPIUrl("User", "Detail");
            return await ServiceHelper.AccessAPI<UserInfoReturnValue>(url, requestModel);
        }

        public static async Task<string> GetCurrentIP()
        {
            var url = "http://checkip.dyndns.org/";
            var html = await ServiceHelper.AccessAPI(url);
            var match = Regex.Match(html, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}");
            if (match.Success)
                return match.Value;

            return "0.0.0.0";
        }
    }
}
