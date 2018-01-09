﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using FluentFtpDemo.Lib.Constants;
using FluentFtpDemo.Lib.Enumerations;
using FluentFTP;
using FluentFtpDemo.Lib.Helpers;

namespace FluentFtpDemo.Lib
{
    public class FtpBuilder : IFtpBuilder
    {
        #region properties

        public int Retry { get; }
        public int Timeout { get; }
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public FtpTypes Type { get; set; }
        public FtpStrategies Strategy { get; set; }
        public ICollection<int> ActivePorts { get; set; }

        #endregion

        #region constructors

        public FtpBuilder() : this(FtpConstants.DefaultRetry, FtpConstants.DefaultTimeout)
        {
        }

        public FtpBuilder(int retry, int timeout)
        {
            Type = FtpConstants.DefaultType;
            Strategy = FtpConstants.DefaultStrategy;
            Retry = retry > 0 ? retry : FtpConstants.DefaultRetry;
            Timeout = timeout > 0 ? timeout : FtpConstants.DefaultTimeout;
        }

        #endregion

        #region methods

        public FtpClient BuildClient(bool secure = false)
        {
            try
            {
                var client = new FtpClient(Host)
                {
                    Credentials = new NetworkCredential(User, Password),
                    DataConnectionType = GetConnectionType(Type),
                    ActivePorts = ActivePorts,
                    ConnectTimeout = Timeout,
                    RetryAttempts = Retry
                };
                if (!secure) return client;
                client.SslProtocols = SslProtocols.Tls;
                client.EncryptionMode = FtpEncryptionMode.Explicit;
                client.ValidateCertificate += OnValidateCertificate;
                return client;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return null;
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

        private void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e)
        {
            switch (Strategy)
            {
                case FtpStrategies.Accept:
                    e.Accept = true;
                    break;
                case FtpStrategies.Refuse:
                    e.Accept = false;
                    break;
                case FtpStrategies.Verify:
                    e.Accept = Verify(e);
                    break;
                case FtpStrategies.Prompt:
                    e.Accept = Verify(e, true);
                    break;
                default:
                    var msg = $"Unexpected strategy [{Strategy}]";
                    throw new ArgumentOutOfRangeException(msg);
            }
        }

        private static bool Verify(FtpSslValidationEventArgs e, bool display = false)
        {
            if (e.PolicyErrors == SslPolicyErrors.None) return true;
            var certificate = new X509Certificate2(e.Certificate);
            if (certificate.Verify()) return true;
            if (!display) return false;
            X509Certificate2UI.DisplayCertificate(certificate);
            return certificate.Verify();
        }

        private static FtpDataConnectionType GetConnectionType(FtpTypes type)
        {
            switch (type)
            {
                case FtpTypes.Active:
                    return FtpDataConnectionType.AutoActive;
                case FtpTypes.Passive:
                    return FtpDataConnectionType.AutoPassive;
                default:
                    var msg = $"Unexpected connection type [{type}]";
                    throw new ArgumentOutOfRangeException(msg);
            }
        }

        #endregion
    }
}