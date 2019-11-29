using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace KH10086.Service.Common
{
    /// <summary>
    /// FTP文件上传下载
    /// </summary>
    public class FTPClientHelper
    {
        /// <summary>
        /// ftp服务器地址
        /// </summary>
        string FTPAddress { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        string FTPUsername { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        string FTPPwd { get; set; }
        /// <summary>
        /// log4net日志组件
        /// </summary>
        ILog Logger { get; set; }

        /// <summary>
        /// 初始化FTP服务信息
        /// </summary>
        /// <param name="ftpAddress"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public FTPClientHelper(string ftpAddress, string userName, string password)
        {
            Logger = LogManager.GetLogger(typeof(FTPClientHelper));
            FTPAddress = ftpAddress;
            FTPUsername = userName;
            FTPPwd = password;
        }

        //static string FTPAddress = "ftp://116.255.135.162:212"; //ftp服务器地址
        //static string FTPUsername = "kh10086";   //用户名
        //static string FTPPwd = "Fq9pmKw4XT";     //密码

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="localPath">本地文件路径,如("C:\\ftp\\#zyzmyxhuayuweblygswzs.mdb")</param>
        /// <param name="ftpPath">远程文件路径,如(/hydatabasehxwl/#zyzmyxhuayuweblygswzs.mdb)</param>
        public bool FTPUpFile(string localPath, string ftpPath)
        {
            FileInfo f = new FileInfo(localPath);
            string FileName = f.Name;
            string ftpRemotePath = ftpPath;
            string FTPPath = FTPAddress + ftpRemotePath + FileName; //上传到ftp路径,如ftp://***.***.***.**:21/home/test/test.txt
            //实现文件传输协议 (FTP) 客户端
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPPath.Replace("#", Uri.HexEscape('#'))));
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(FTPUsername, FTPPwd); //设置通信凭据
            reqFtp.KeepAlive = false; //请求完成后关闭ftp连接
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            reqFtp.ContentLength = f.Length;
            int buffLength = 1024;
            byte[] buff = new byte[buffLength];
            int contentLen;
            //读本地文件数据并上传
            FileStream fs = f.OpenRead();
            try
            {
                Stream strm = reqFtp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("文件上传失败，远程路径{0},本地路径{1}\r\n", ftpPath, localPath) + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="localPath">本地文件路径,如("C:\\ftp\\#zyzmyxhuayuweblygswzs.mdb")</param>
        /// <param name="ftpPath">远程文件路径,如(/hydatabasehxwl/#zyzmyxhuayuweblygswzs.mdb)</param>
        public bool FTPDownFile(string localPath, string ftpPath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(localPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(localPath));
            }
            string FtpFilePath = ftpPath;//远程路径
            if (File.Exists(localPath))
            {
                File.Delete(localPath);
            }
            string FTPPath = FTPAddress + FtpFilePath.Replace("#", Uri.HexEscape('#'));
            //建立ftp连接
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPPath));
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(FTPUsername, FTPPwd);
            FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            long cl = response.ContentLength;
            int buffersize = 1024;
            int readCount;
            byte[] buffer = new byte[buffersize];
            readCount = ftpStream.Read(buffer, 0, buffersize);
            //创建并写入文件
            FileStream OutputStream = new FileStream(localPath, FileMode.Create);
            while (readCount > 0)
            {
                OutputStream.Write(buffer, 0, buffersize);
                readCount = ftpStream.Read(buffer, 0, buffersize);
            }
            ftpStream.Close();
            OutputStream.Close();
            response.Close();
            if (File.Exists(localPath))
            {
                Logger.Info(string.Format("文件下载完毕,远程路径{0},本地路径{1}", ftpPath, localPath));
                return true;
            }
            return false;
        }
    }
}
