using DDnsSharp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Services
{
    public static class RecordService
    {
        public const string SERVICE_NAME = "Record";

        public static async Task<RecordChangedReturnValue> UpdateDDNS(int domainID, int recordID, string subDomain, string line)
        {
            var model = DDnsSharpRuntime.NewRequestModel<UpdateDDNSRequestModel>();
            model.DomainID = domainID;
            model.RecordID = recordID;
            model.SubDomain = subDomain;
            model.LineName = line;
            return await ServiceHelper.AccessAPI<RecordChangedReturnValue>(SERVICE_NAME, "Ddns", model);
        }

        public static async Task<RecordListReturnValue> GetList(int domainID, string subDomain = null)
        {
            var m = DDnsSharpRuntime.NewRequestModel<RecordListRequestModel>();
            m.DomainID = domainID;
            m.SubDomain = subDomain;
            return await ServiceHelper.AccessAPI<RecordListReturnValue>(SERVICE_NAME, "List", m);
        }

        public static async Task<RecordChangedReturnValue> CreateRecord(int domainID, string subDomain, string recordType,
            string line, string value, int mx, int ttl)
        {
            if (ttl < 1 || ttl > 604800)
                ttl = 600;
            var m = DDnsSharpRuntime.NewRequestModel<CreateRecordRequestModel>();
            m.DomainID = domainID;
            m.SubDomain = subDomain;
            m.RecordType = recordType;
            m.LineName = line;
            m.Value = value;
            m.MX = mx;
            m.TTL = ttl;
            return await ServiceHelper.AccessAPI<RecordChangedReturnValue>(SERVICE_NAME, "Create", m);
        }
    }
}
