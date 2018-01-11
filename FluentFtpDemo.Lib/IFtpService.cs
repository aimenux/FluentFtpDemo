using System;
using System.Collections.Generic;
using FluentFtpDemo.Lib.Models;

namespace FluentFtpDemo.Lib
{
    public interface IFtpService
    {
        bool CheckConnectionStatus();
        bool FileExists(string remote);
        bool DeleteFile(string remote);
        long GetFileSize(string remote);
        bool DirectorExists(string remote);
        bool CreateDirectory(string remote);
        bool DeleteDirectory(string remote);
        DateTime? GetModifiedTime(string remote);
        bool UploadFile(string local, string remote);
        bool DownloadFile(string local, string remote);
        bool SetModifiedTime(string remote, DateTime date);
        ICollection<FtpItem> GetListing(string remote = null);
        int UploadFiles(string[] localFiles, string remoteDir);
        int DownloadFiles(string localDir, string[] remoteFiles);
    }
}
