using System;
using System.Collections.Generic;
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
    public class FtpFactory : IFtpFactory
    {
        #region properties

        public int Retry { get; }
        public int Timeout { get; }
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public FtpTypes Type { get; set; }
        public FtpModes Mode { get; set; }
        public FtpPolicies Policy { get; set; }
        public ICollection<int> ActivePorts { get; set; }

        #endregion

        #region constructors

        public FtpFactory() : this(FtpConstants.DefaultRetry, FtpConstants.DefaultTimeout)
        {
        }

        public FtpFactory(int retry, int timeout)
        {
            Type = FtpConstants.DefaultType;
            Mode = FtpConstants.DefaultMode;
            Policy = FtpConstants.DefaultPolicy;
            Retry = retry > 0 ? retry : FtpConstants.DefaultRetry;
            Timeout = timeout > 0 ? timeout : FtpConstants.DefaultTimeout;
        }

        #endregion

        #region methods

        public FtpClient BuildClient()
        {
            try
            {
                var client = new FtpClient(Host)
                {
                    Credentials = new NetworkCredential(User, Password),
                    DataConnectionType = GetConnectionType(Mode),
                    ActivePorts = ActivePorts,
                    ConnectTimeout = Timeout,
                    RetryAttempts = Retry
                };

                if (Type == FtpTypes.Ftp) return client;
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

        private void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e)
        {
            switch (Policy)
            {
                case FtpPolicies.Accept:
                    e.Accept = true;
                    break;
                case FtpPolicies.Refuse:
                    e.Accept = false;
                    break;
                case FtpPolicies.Verify:
                    e.Accept = Verify(e);
                    break;
                case FtpPolicies.Prompt:
                    e.Accept = Verify(e, true);
                    break;
                default:
                    var msg = $"Unexpected strategy [{Policy}]";
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

        private static FtpDataConnectionType GetConnectionType(FtpModes type)
        {
            switch (type)
            {
                case FtpModes.Active:
                    return FtpDataConnectionType.AutoActive;
                case FtpModes.Passive:
                    return FtpDataConnectionType.AutoPassive;
                default:
                    var msg = $"Unexpected connection type [{type}]";
                    throw new ArgumentOutOfRangeException(msg);
            }
        }

        #endregion
    }
}
