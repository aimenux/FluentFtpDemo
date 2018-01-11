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
            FtpService.AddFileListner(@"ftp.log");

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
            var factory = new FtpFactory
            {
                Host = Host,
                User = User,
                Password = Pass,
                Type = FtpTypes.Ftp,
                Mode = FtpModes.Passive
            };
            var service = new FtpService(factory);
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }

        private static void PassiveFtpsModeExample()
        {
            var factory = new FtpFactory
            {
                Host = Host,
                User = User,
                Password = Pass,
                Type = FtpTypes.Ftps,
                Mode = FtpModes.Passive,
                Policy = FtpPolicies.Accept
            };
            var service = new FtpService(factory);
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }

        private static void ActiveFtpModeExample()
        {
            var factory = new FtpFactory
            {
                Host = Host,
                User = User,
                Password = Pass,
                Type = FtpTypes.Ftp,
                Mode = FtpModes.Active,
                ActivePorts = new List<int> { 32490, 32491, 32492 }
            };
            var service = new FtpService(factory);
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }

        private static void ActiveFtpsModeExample()
        {
            var factory = new FtpFactory
            {
                Host = Host,
                User = User,
                Password = Pass,
                Type = FtpTypes.Ftps,
                Mode = FtpModes.Active,
                Policy = FtpPolicies.Accept,
                ActivePorts = new List<int> { 32490, 32491, 32492 }
            };
            var service = new FtpService(factory);
            var items = service.GetListing();
            Console.WriteLine($"Items found : {items.Count}");
        }
    }
}
