using log4net;
using System;

namespace Fiveways.Logging
{
    public static class ApplicationLog
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Info(string message, params object[] parameters)
        {
            log.Info(string.Format(message, parameters));
        }

        public static void Error(string message, params object[] parameters)
        {
            log.Error(string.Format(message, parameters));
        }

        public static void Debug(string message)
        {
            
        }
    }
}
