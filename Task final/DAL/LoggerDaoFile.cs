using InterfacesDAL;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository.Hierarchy;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL
{
    public class LoggerDaoFile : ILoggerDao
    {
        private static readonly string name;

        private readonly FileInfo config = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

        public ILog Log { get; } = LogManager.GetLogger(name);

        static LoggerDaoFile() => name = "LOGGER";

        public void StartLogger() => XmlConfigurator.Configure(config);

        public string GetLastError()
        {
            var logPath = GetLogPath();

            var fs = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var encoding = Encoding.GetEncoding(1251);

            var lastError = string.Empty;

            using (var sr = new StreamReader(fs, encoding))
            {
                while (!sr.EndOfStream)
                {
                    lastError = sr.ReadLine();
                }
            }

            return lastError;
        }

        private string GetLogPath()
        {
            var repository = Log.Logger.Repository;
            var appenders = ((Hierarchy)repository).Root.Appenders.OfType<RollingFileAppender>();

            var logPath = string.Empty;

            foreach (var appender in appenders)
            {
                logPath = appender.File;
            }

            return logPath;
        }
    }
}