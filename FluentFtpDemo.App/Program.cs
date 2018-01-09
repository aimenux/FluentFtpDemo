using System;
using System.Collections.Generic;
using FluentFtpDemo.Lib;
using FluentFtpDemo.Lib.Enumerations;

namespace FluentFtpDemo.App
{
    internal class Program
    {
        private const string Host = @"ftp_ip_adress";
        private const string User = @"ftp_user";
        private const string Pass = @"ftp_pass";

        private static void Main()
        {
            // Add log file (static applied to all instances)
            FtpBuilder.AddFileListner(@"ftp.log");

            // Example of ftp passive mode
            PassiveFtpModeExample();

            // Example of ftps passive mode
            PassiveFtpsModeExample();

            // Example of ftp active mode
            ActiveFtpModeExample();

            // Example of ftps active mode
            ActiveFtpsModeExample();
        }

        private static void PassiveFtpModeExample()
        {
            var builder = new FtpBuilder
            {
                Host = Host,
                User = User,
                Password = Pass,
                Type = FtpTypes.Passive
            };
            var service = new FtpService(builder);
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }

        private static void PassiveFtpsModeExample()
        {
            var builder = new FtpBuilder
            {
                Host = Host,
                User = User,
                Password = Pass,
                Type = FtpTypes.Passive,
                Policy = FtpPolicies.Accept
            };
            var service = new FtpService(builder) {FtpsEnabled = true};
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }

        private static void ActiveFtpModeExample()
        {
            var builder = new FtpBuilder
            {
                Host = Host,
                User = User,
                Password = Pass,
                Type = FtpTypes.Active,
                ActivePorts = new List<int> { 32490, 32491, 32492 }
            };
            var service = new FtpService(builder);
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }

        private static void ActiveFtpsModeExample()
        {
            var builder = new FtpBuilder
            {
                Host = Host,
                User = User,
                Password = Pass,
                Type = FtpTypes.Active,
                Policy = FtpPolicies.Accept,
                ActivePorts = new List<int> { 32490, 32491, 32492 }
            };
            var service = new FtpService(builder) {FtpsEnabled = true};
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }
    }
}
