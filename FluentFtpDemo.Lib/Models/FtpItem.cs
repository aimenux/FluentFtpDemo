using System;

namespace FluentFtpDemo.Lib.Models
{
    public class FtpItem
    {
        public long Size { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
