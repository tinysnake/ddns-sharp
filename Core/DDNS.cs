using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDnsSharp.Core.Services;
using DDnsSharp.Core.Models;
using NLog;
using System.Net;

namespace DDnsSharp.Core
{
    public static class DDNS
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public static async Task<string> Start(IEnumerable<UpdateModel> updateModels, bool forceUpdate = false)
        {
            if (updateModels == null)
                throw new ArgumentNullException();


            string currentIP = CommonService.EMPTY_IP;
            try
            {
                currentIP = await CommonService.GetCurrentIP();
            }
            catch (WebException)
            {
                logger.Info("无法连接至服务器.");
                return currentIP;
            }

            if (currentIP == CommonService.EMPTY_IP)
            {
                logger.Info("无法获取正确IP.");
                return currentIP;
            }

            foreach (var um in updateModels)
            {
                if (!um.Enabled)
                    continue;

                if (!forceUpdate&&currentIP == um.LastUpdateIP)
                    continue;

                if (String.IsNullOrEmpty(um.DomainName) || String.IsNullOrEmpty(um.SubDomain))
                    continue;

                if (String.IsNullOrEmpty(um.LineName))
                    um.LineName = "默认";

                var recordFullName = um.SubDomain + "." + um.DomainName;

                int domainID = um.DomainID;
                if (domainID <= 0)
                {
                    try
                    {
                        domainID = await GetDomainID(um.DomainName);
                        um.DomainID = domainID;
                    }
                    catch (APIException apiex)
                    {
                        logger.ErrorException(recordFullName, apiex);
                        continue;
                    }
                    catch (WebException)
                    {
                        logger.Info("无法连接至服务器");
                        return currentIP;
                    }
                }

                int recordID = um.RecordID;
                if (recordID <= 0)
                {
                    try
                    {
                        recordID = await GetRecordID(domainID, um);
                        um.RecordID = recordID;
                    }
                    catch (APIException apiex)
                    {
                        logger.ErrorException(recordFullName, apiex);
                        continue;
                    }
                    catch (WebException)
                    {
                        logger.Info("无法连接至服务器");
                        return currentIP;
                    }
                }

                try
                {
                    var updateResult = await RecordService.UpdateDDNS(domainID, recordID, um.SubDomain, um.LineName);
                    if (updateResult.Status.Code != 1)
                        um.LastUpdateIP = updateResult.Status.Message;
                    else
                    {
                        currentIP = updateResult.Info.Value;
                        um.LastUpdateIP = currentIP;
                    }
                }
                catch (APIException apiex)
                {
                    logger.ErrorException(recordFullName, apiex);
                    continue;
                }
                catch (WebException)
                {
                    logger.Info("无法连接至服务器");
                    return currentIP;
                }
                um.LastUpdatedTime = DateTime.UtcNow;
                logger.Info(recordFullName + " 记录更新更新成功, 新的IP地址为: " + currentIP);
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
