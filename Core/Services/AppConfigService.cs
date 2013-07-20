using DDnsSharp.Core.Models;
using DDnsSharp.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Threading;

namespace DDnsSharp.Core.Services
{
    public class AppConfigService
    {
        private const string MUTEX_NAME = "AppConfigService";
        private const string FILE_NAME = "config.txt";
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AppConfigService()
        {
            try
            {
                syncMutex = Mutex.OpenExisting(MUTEX_NAME);
            }
            catch(WaitHandleCannotBeOpenedException)
            {
                syncMutex = new Mutex(false,MUTEX_NAME);
            }
        }

        private Mutex syncMutex;

        public AppConfig Read()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(dir,FILE_NAME);
            AppConfig result = null;
            try
            {
                syncMutex.WaitOne();
                FileStream fs;
                if (File.Exists(fullPath))
                {
                    fs = File.Open(fullPath, FileMode.Open);
                }
                else
                {
                    fs = File.Create(fullPath);
                }
                using (var sr = new StreamReader(fs))
                {
                    result = JsonConvert.DeserializeObject<AppConfig>(sr.ReadToEnd());
                    if (result == null)
                        result = new AppConfig();
                    else
                        result.Password = Encryption.Decrypt(result.Password);
                }
                fs.Close();
            }
            catch (IOException ex)
            {
                logger.ErrorException("AppConfigService.IOException", ex);
            }
            finally
            {
                if (result == null)
                    result = new AppConfig();
                syncMutex.ReleaseMutex();
            }
            return result;
        }

        public void Save(AppConfig conf)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(dir, FILE_NAME);
            try
            {
                syncMutex.WaitOne();
                FileStream fs;
                if (File.Exists(fullPath))
                {
                    fs = File.Open(fullPath, FileMode.Truncate);
                }
                else
                {
                    fs = File.Create(fullPath);
                }
                using (var sw = new StreamWriter(fs))
                {
                    var nc = new AppConfig();
                    nc.Email = conf.Email;
                    nc.Password = Encryption.Encrypt(conf.Password);
                    nc.UpdateList = conf.UpdateList;
                    var confStr = JsonConvert.SerializeObject(nc);
                    var bytes = Encoding.UTF8.GetBytes(confStr);
                    sw.Write(confStr);
                }
                fs.Close();
            }
            catch (IOException ex)
            {
                logger.ErrorException("AppConfigService.IOException", ex);
            }
            finally
            {
                syncMutex.ReleaseMutex();
            }
        }
    }
}
