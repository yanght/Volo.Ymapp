using KH10086.Service;
using KH10086.Service.Common;
using log4net;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace KH10086.DataSyncTask.Jobs
{
    public class UtourDataSyncJob : IJob
    {
        ILog Logger = LogManager.GetLogger(typeof(UtourDataSyncJob));
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Logger.Info("***********开始执行数据同步***********");

                string FTPAddress = ConfigurationManager.AppSettings["FTPAddress"];


                string FTPUsername = ConfigurationManager.AppSettings["FTPUsername"];
                string FTPPwd = ConfigurationManager.AppSettings["FTPPwd"];
                // string LocalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["LocalPath"]);
                string classId = ConfigurationManager.AppSettings["classId"];
                //string FtpPath = ConfigurationManager.AppSettings["FtpPath"];

                //FTPClient client = new FTPClient("116.255.135.162", "/hydatabasehxwl", FTPUsername, FTPPwd, 212);
                //client.Get("#zyzmyxhuayuweblygswzs.mdb", "c:\\ftp", "#zyzmyxhuayuweblygswzs.mdb");

                //FTPClientHelper client = new FTPClientHelper(FTPAddress, FTPUsername, FTPPwd);
                //client.FTPDownFile("c:\\ftp\\#zyzmyxhuayuweblygswzs.mdb", "/hydatabasehxwl/#zyzmyxhuayuweblygswzs.mdb");
                FTPClient client = new FTPClient("116.255.135.162", "/KH10086DataSyncTask", "kh10086", "Fq9pmKw4XT", 212);

                client.DownloadFile("#zyzmyxhuayuweblygswzs.mdb", "C:\\ftp", "#zyzmyxhuayuweblygswzs.mdb");
                
                //client.DownloadBrokenFile("/KH10086DataSyncTask/#zyzmyxhuayuweblygswzs.mdb", "C:\\ftp", "#zyzmyxhuayuweblygswzs.mdb", 0);


                //UTourService uTourService = new UTourService();
                //bool result = uTourService.DataSync(int.Parse(classId));
                //if (result)
                //{
                //    Logger.Info("***********结束执行数据同步***********");
                //}
                //else
                //{
                //    Logger.Error($"数据同步执行失败");
                //}

            }
            catch (Exception ex)
            {
                Logger.Error($"数据同步执行失败,失败原因:{ex.ToString()}");
            }
            Logger.Info("***********结束执行数据同步***********");
        }
    }
}
