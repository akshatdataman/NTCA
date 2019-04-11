using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Security.Principal;

namespace WiseThink
{
    public class LogHandler
    {
        public static void LogDebug(string message, Exception error, Type type)
        {
            ILog logger = LogManager.GetLogger(type);
            if (error.InnerException != null)
            {
                error = error.InnerException;
            }
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message, error);
            }
        }
        public static void LogDebug(string message, IPrincipal user, Uri url, Exception error, Type type)
        {
            SetOptionalParametersOnLogger(user, url);
            LogDebug(message, error, type);
        }
        public static void LogFatal(string message, Exception error, Type type)
        {
            ILog logger = LogManager.GetLogger(type);
            if (error.InnerException != null)
            {
                error = error.InnerException;
            }
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message, error);
                
            }
        }
        public static void LogFatal(string message, IPrincipal user, Uri url, Exception error, Type type)
        {
            SetOptionalParametersOnLogger(user, url);
            LogFatal(message, error, type);
        }
        public static void LogError(string message, Exception error, Type type)
        {
            ILog logger = LogManager.GetLogger(type);
            if (error.InnerException != null)
            {
                error = error.InnerException;
            }
            if (logger.IsErrorEnabled)
            {
                logger.Error(message, error);
            }
        }
        
        public static void LogError(string message, IPrincipal user, Uri url, Exception error, Type type)
        {
            SetOptionalParametersOnLogger(user, url);

            LogError(message, error,type);
        }
        public static void LogInfo(string message, Type type)
        {
            ILog logger = LogManager.GetLogger(type);
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }
        public static void LogWarning(string message, Exception error, Type type)
        {
            ILog logger = LogManager.GetLogger(type);
            if (error.InnerException != null)
            {
                error = error.InnerException;
            }
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message, error);
            }
        }
        public static void LogWarning(string message, IPrincipal user, Uri url, Exception error, Type type)
        {
            SetOptionalParametersOnLogger(user, url);

            LogWarning(message, error, type);
        }
        private static void SetOptionalParametersOnLogger(IPrincipal user, Uri url)
        {
            if (user != null && user.Identity.IsAuthenticated)
            {
                MDC.Set("user", user.Identity.Name);
            }
            MDC.Set("url", url.ToString());
        }
    }
}
