using DDnsSharp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Services
{
    public static class CommonService
    {
        public const string EMPTY_IP = "0.0.0.0";
        public static async Task<ReturnValueBase> GetAPIVersion()
        {
            var requestModel = DDnsSharpRuntime.NewRequestModel<RequestModelBase>();
            var url = ServiceHelper.BuildAPIUrl("Info", "Version");
            return await ServiceHelper.AccessAPI<ReturnValueBase>(url, requestModel);
        }

        public static async Task<UserInfoReturnValue> GetUserInfo()
        {
            var requestModel = DDnsSharpRuntime.NewRequestModel<RequestModelBase>();
            var url = ServiceHelper.BuildAPIUrl("User", "Detail");
            return await ServiceHelper.AccessAPI<UserInfoReturnValue>(url, requestModel);
        }

        private static string[] ipurls = new[] { "http://iframe.ip138.com/ic.asp", "http://checkip.dyndns.org/" };
        private static int urlIndex = 0;

        public static async Task<string> GetCurrentIP()
        {
            var ip = EMPTY_IP;
            var url = ipurls[urlIndex];
            urlIndex++;
            if (urlIndex > ipurls.Length - 1)
                urlIndex = 0;

            try
            {
                var html = await ServiceHelper.AccessAPI(url);
                var match = Regex.Match(html, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}");
                if (match.Success)
                    ip = match.Value;
            }
            catch (WebException)
            { }

            return ip;
        }
    }
}
