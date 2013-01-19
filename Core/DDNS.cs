using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDnsPod.Core.Services;
using DDnsPod.Core.Models;

namespace DDnsPod.Core
{
    public static class DDNS
    {
        public static async Task<string> Start(IEnumerable<UpdateModel> updateModels, bool forceUpdate = false)
        {
            if (updateModels == null)
                throw new ArgumentNullException();

            string currentIP = await CommonService.GetCurrentIP();

            foreach (var um in updateModels)
            {
                if (!um.Enabled)
                    continue;

                if (!forceUpdate)
                {
                    if (currentIP == um.LastUpdateIP)
                        continue;
                }
                if (String.IsNullOrEmpty(um.DomainName) || String.IsNullOrEmpty(um.SubDomain))
                    throw new ArgumentNullException();
                if (String.IsNullOrEmpty(um.LineName))
                    um.LineName = "默认";


                int domainID = um.DomainID;
                if (domainID <= 0)
                    domainID = await GetDomainID(um.DomainName);

                int recordID = um.RecordID;
                if (recordID <= 0)
                    recordID = await GetRecordID(domainID, um);

                var updateResult = await RecordService.UpdateDDNS(domainID, recordID, um.SubDomain, um.LineName);
                if (updateResult.Status.Code != 1)
                    throw new APIException(updateResult.Status);
            }

            return currentIP;
        }

        private static async Task<int> GetDomainID(string domainName)
        {
            var domain = await DomainService.GetInfo(domainName);

            if (domain.Status.Code != 1)
                throw new APIException(domain.Status);

            return domain.Info.ID;
        }

        private static async Task<int> GetRecordID(int domainID, UpdateModel um)
        {
            int recordID;
            var records = await RecordService.GetList(domainID, um.SubDomain);

            if (records.Status.Code != 1)
                throw new APIException(records.Status);


            var record = records.Records.FirstOrDefault(_ => _.Type == "A" && _.Enabled == 1);
            if (record == null)
            {
                var newRecord = await RecordService.CreateRecord(domainID, um.SubDomain, "A", um.LineName, "0.0.0.0", 0, 300);

                if (newRecord.Status.Code != 1)
                    throw new APIException(newRecord.Status);

                recordID = newRecord.Info.RecordID;
            }
            else
                recordID = record.ID;

            return recordID;
        }
    }
}
