using System.Collections.Generic;
using FluentFtpDemo.Lib.Enumerations;
using FluentFTP;

namespace FluentFtpDemo.Lib
{
    public interface IFtpBuilder
    {
        int Retry { get; }
        int Timeout { get; }
        string Host { get; set; }
        string User { get; set; }
        string Password { get; set; }
        FtpTypes Type { get; set; }
        FtpPolicies Policy { get; set; }
        ICollection<int> ActivePorts { get; set; }
        FtpClient BuildClient(bool secure = false);
    }
}
