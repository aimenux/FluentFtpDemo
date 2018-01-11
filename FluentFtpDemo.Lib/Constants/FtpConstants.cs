using System.Diagnostics;
using FluentFtpDemo.Lib.Enumerations;

namespace FluentFtpDemo.Lib.Constants
{
    public static class FtpConstants
    {
        public const int DefaultRetry = 3;
        public const int DefaultTimeout = 10;
        public const FtpTypes DefaultType = FtpTypes.Ftp;
        public const FtpModes DefaultMode = FtpModes.Passive;
        public const SourceLevels DefaultLevel = SourceLevels.All;
        public const FtpPolicies DefaultPolicy = FtpPolicies.Verify;
    }
}
