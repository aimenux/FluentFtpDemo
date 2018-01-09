using System;

namespace FluentFtpDemo.Lib.Helpers
{
    public static class Logger
    {
        public static void Log(Exception ex)
        {
            Log(ex.Message);
        }

        public static void Log(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
