using InterfacesDAL;
using log4net;
using log4net.Config;
using System;
using System.IO;

namespace DAL
{
    public class LoggerDaoFile : ILoggerDao
    {
        private static readonly string name;

        private readonly FileInfo config = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

        public ILog Log { get; } = LogManager.GetLogger(name);

        public void StartLogger() => XmlConfigurator.Configure(config);

        static LoggerDaoFile() => name = "LOGGER";
    }
}