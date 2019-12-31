using FluentFTP;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH10086.Service.Common
{
    public class FtpFileMetadata
    {
        public long FileLength { get; set; }
        public string MD5Hash { get; set; }
        public DateTime LastModifyTime { get; set; }
    }

    public class FtpHelper
    {
        private FtpClient _client = null;
        private string _host = "116.255.135.162";
        private int _port = 212;
        private string _username = "kh10086";
        private string _password = "Fq9pmKw4XT";
        private string _workingDirectory = "";
        public string WorkingDirectory
        {
            get
            {
                return _workingDirectory;
            }
        }
        public FtpHelper(string host, int port, string username, string password)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
        }

        public Stream GetStream(string remotePath)
        {
            Open();
            return _client.OpenRead(remotePath);
        }

        public void Get(string localPath, string remotePath)
        {
            Open();
            _client.DownloadFile(localPath, remotePath, FtpLocalExists.Overwrite);
        }

        public void Upload(Stream s, string remotePath)
        {
            Open();
            _client.Upload(s, remotePath, FtpExists.Overwrite, true);
        }

        public void Upload(string localFile, string remotePath)
        {
            Open();
            using (FileStream fileStream = new FileStream(localFile, FileMode.Open))
            {
                _client.Upload(fileStream, remotePath, FtpExists.Overwrite, true);
            }
        }

        public int UploadFiles(IEnumerable<string> localFiles, string remoteDir)
        {
            Open();
            List<FileInfo> files = new List<FileInfo>();
            foreach (var lf in localFiles)
            {
                files.Add(new FileInfo(lf));
            }
            int count = _client.UploadFiles(files, remoteDir, FtpExists.Overwrite, true, FtpVerify.Retry);
            return count;
        }

        public void MkDir(string dirName)
        {
            Open();
            _client.CreateDirectory(dirName);
        }

        public bool FileExists(string remotePath)
        {
            Open();
            return _client.FileExists(remotePath);
        }
        public bool DirExists(string remoteDir)
        {
            Open();
            return _client.DirectoryExists(remoteDir);
        }

        public FtpListItem[] List(string remoteDir)
        {
            Open();
            var f = _client.GetListing();
            FtpListItem[] listItems = _client.GetListing(remoteDir);
            return listItems;
        }

        public FtpFileMetadata Metadata(string remotePath)
        {
            Open();
            long size = _client.GetFileSize(remotePath);
            DateTime lastModifyTime = _client.GetModifiedTime(remotePath);

            return new FtpFileMetadata()
            {
                FileLength = size,
                LastModifyTime = lastModifyTime
            };
        }

        public bool TestConnection()
        {
            return _client.IsConnected;
        }

        public void SetWorkingDirectory(string remoteBaseDir)
        {
            Open();
            if (!DirExists(remoteBaseDir))
                MkDir(remoteBaseDir);
            _client.SetWorkingDirectory(remoteBaseDir);
            _workingDirectory = remoteBaseDir;
        }
        private void Open()
        {
            if (_client == null)
            {
                _client = new FtpClient(_host, new System.Net.NetworkCredential(_username, _password));
                _client.Port = _port;
                _client.RetryAttempts = 100;
                if (!string.IsNullOrWhiteSpace(_workingDirectory))
                {
                    _client.SetWorkingDirectory(_workingDirectory);
                }
            }
        }
    }
}
