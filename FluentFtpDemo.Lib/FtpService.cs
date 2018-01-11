using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentFtpDemo.Lib.Constants;
using FluentFtpDemo.Lib.Helpers;
using FluentFtpDemo.Lib.Models;
using FluentFTP;

namespace FluentFtpDemo.Lib
{
    public class FtpService : IFtpService
    {
        #region fields

        private readonly IFtpFactory _ftpFactory;

        #endregion

        #region constructors

        public FtpService(IFtpFactory factory)
        {
            _ftpFactory = factory;
        }

        #endregion

        #region methods

        public bool CheckConnectionStatus()
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    client.Disconnect();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        public string GetWorkingDirectory()
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    var res = client.GetWorkingDirectory();
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return null;
            }
        }

        public bool FileExists(string remote)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    var res = client.FileExists(remote);
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        public bool DeleteFile(string remote)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    client.DeleteFile(remote);
                    var res = !client.FileExists(remote);
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        public long GetFileSize(string remote)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    var res = client.GetFileSize(remote);
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return -1;
            }
        }

        public bool DirectorExists(string remote)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    var res = client.DirectoryExists(remote);
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        public bool CreateDirectory(string remote)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    client.CreateDirectory(remote);
                    var res = client.DirectoryExists(remote);
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        public bool DeleteDirectory(string remote)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    client.DeleteDirectory(remote);
                    var res = !client.DirectoryExists(remote);
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        public bool SetWorkingDirectory(string remote)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    client.SetWorkingDirectory(remote);
                    client.Disconnect();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        public DateTime? GetModifiedTime(string remote)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    var res = client.GetModifiedTime(remote);
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return null;
            }
        }

        public bool UploadFile(string local, string remote)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    var res = client.UploadFile(local, remote);
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        public int UploadFiles(string[] localFiles, string remoteDir)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    var res = client.UploadFiles(localFiles, remoteDir);
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return -1;
            }
        }

        public bool DownloadFile(string local, string remote)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    var res = client.DownloadFile(local, remote);
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        public bool SetModifiedTime(string remote, DateTime date)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    client.SetModifiedTime(remote, date);
                    client.Disconnect();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        public int DownloadFiles(string localDir, string[] remoteFiles)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    var res = client.DownloadFiles(localDir, remoteFiles);
                    client.Disconnect();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return -1;
            }
        }

        public bool RenameFile(string oldRemote, string newRemote)
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    client.Rename(oldRemote, newRemote);
                    client.Disconnect();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        public ICollection<FtpItem> GetListing(string remote = "/")
        {
            try
            {
                using (var client = _ftpFactory.BuildClient())
                {
                    client.Connect();
                    var items = client.GetListing(remote);
                    client.Disconnect();
                    return ToFtpItems(items);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return new List<FtpItem>();
            }
        }

        public static bool AddFileListner(string file, SourceLevels level = FtpConstants.DefaultLevel)
        {
            if (string.IsNullOrWhiteSpace(file)) return false;
            try
            {
                using (var listner = new TextWriterTraceListener(file))
                {
                    listner.Filter = new EventTypeFilter(level);
                    FtpTrace.AddListener(listner);
                    FtpTrace.LogUserName = true;
                    FtpTrace.LogPassword = false;
                    FtpTrace.LogIP = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        private static ICollection<FtpItem> ToFtpItems(ICollection<FtpListItem> items)
        {
            if (items == null) return new List<FtpItem>();
            return items.Select(x => new FtpItem
            {
                Name = x.FullName,
                Type = Enum.GetName(typeof(FtpFileSystemObjectType), x.Type),
                Size = x.Size,
                Created = x.Created,
                Modified = x.Modified
            }).ToList();
        }

        #endregion
    }
}
