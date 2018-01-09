namespace FluentFtpDemo.Lib.Enumerations
{
    public enum FtpPolicies
    {
        Accept, // accept any certificate
        Refuse, // reject any certificate
        Verify, // verify certificate (suppose that certificate was installed)
        Prompt, // prompt user to accept/install or not certificate in store
    }
}
