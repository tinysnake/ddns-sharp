using DDnsSharp.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Services
{
    public class ServiceHelper
    {
        public static string BuildAPIUrl(string serviceName, string methodName)
        {
            return String.Format(Consts.API_FORMAT_STRING, Consts.API_BASE_URL, serviceName, methodName);
        }

        public async static Task<string> AccessAPI(string url,string method="GET", string requestData = null)
        {
            var req = HttpWebRequest.Create(url);
            if (req == null)
                throw new ArgumentException("invalid url");

            req.Method = method;
            if (requestData!=null)
            {
                using (var reqStream = await req.GetRequestStreamAsync())
                {
                    var queryString = requestData;
                    var bytes = Encoding.UTF8.GetBytes(queryString);
                    await reqStream.WriteAsync(bytes, 0, bytes.Length);
                }
            }

            var res = await req.GetResponseAsync();

            using (var ResStream = res.GetResponseStream())
            {
                using (var sr = new StreamReader(ResStream))
                {
                    var data = await sr.ReadToEndAsync();
                    return data;
                }
            }
        }

        public async static Task<T> AccessAPI<T>(string serviceName, string methodName, RequestModelBase requestModel)
            where T : ReturnValueBase
        {
            var url = BuildAPIUrl(serviceName, methodName);
            return await AccessAPI<T>(url, requestModel);
        }

        public async static Task<T> AccessAPI<T>(string url,RequestModelBase requestModel)
            where T:ReturnValueBase
        {
            var req = HttpWebRequest.Create(url);
            if (req == null)
                throw new ArgumentException("invalid url");

            req.Method="POST";
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            req.Headers["UserAgent"] = "snake-ddnspod/0.1.0 (tff.assist@gmail.com)";

            using (var reqStream = await req.GetRequestStreamAsync())
            {
                var queryString = requestModel.ToQueryString();
                var bytes = Encoding.UTF8.GetBytes(queryString);
                await reqStream.WriteAsync(bytes, 0, bytes.Length);
            }

            var res = await req.GetResponseAsync();
            
            using (var ResStream = res.GetResponseStream())
            {
                using (var sr = new StreamReader(ResStream))
                {
                    var data = await sr.ReadToEndAsync();
                    return await JsonConvert.DeserializeObjectAsync<T>(data);
                }
            }
        }
    }
}
