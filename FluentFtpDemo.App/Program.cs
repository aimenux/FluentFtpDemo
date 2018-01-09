using System;
using System.Collections.Generic;
using FluentFtpDemo.Lib;
using FluentFtpDemo.Lib.Enumerations;

namespace FluentFtpDemo.App
{
    internal class Program
    {
        private static void Main()
        {
            FtpBuilder.AddFileListner(@"ftp.log");
            PassiveFtpModeExample();
            PassiveFtpsModeExample();
            ActiveFtpModeExample();
            ActiveFtpsModeExample();
        }

        private static void PassiveFtpModeExample()
        {
            const string host = @"ftp_ip_adress";
            const string user = @"ftp_user";
            const string pass = @"ftp_pass";
            var builder = new FtpBuilder
            {
                Host = host,
                User = user,
                Password = pass,
                Type = FtpTypes.Passive
            };
            var service = new FtpService(builder);
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }

        private static void PassiveFtpsModeExample()
        {
            const string host = @"ftp_ip_adress";
            const string user = @"ftp_user";
            const string pass = @"ftp_pass";
            var builder = new FtpBuilder
            {
                Host = host,
                User = user,
                Password = pass,
                Type = FtpTypes.Passive,
                Policy = FtpPolicies.Accept
            };
            var service = new FtpService(builder) {FtpsEnabled = true};
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }

        private static void ActiveFtpModeExample()
        {
            const string host = @"ftp_ip_adress";
            const string user = @"ftp_user";
            const string pass = @"ftp_pass";
            var builder = new FtpBuilder
            {
                Host = host,
                User = user,
                Password = pass,
                Type = FtpTypes.Active,
                ActivePorts = new List<int> { 32490, 32491, 32492 }
            };
            var service = new FtpService(builder);
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }

        private static void ActiveFtpsModeExample()
        {
            const string host = @"ftp_ip_adress";
            const string user = @"ftp_user";
            const string pass = @"ftp_pass";
            var builder = new FtpBuilder
            {
                Host = host,
                User = user,
                Password = pass,
                Type = FtpTypes.Active,
                Policy = FtpPolicies.Accept,
                ActivePorts = new List<int> { 32490, 32491, 32492 }
            };
            var service = new FtpService(builder);
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }
    }
}
