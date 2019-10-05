using InterfacesDAL;
using log4net;
using log4net.Config;
using System;
using System.IO;

namespace DAL
{
    public class LoggerDaoFile : ILoggerDao
    {
        public ILog Log { get; } = LogManager.GetLogger("LOGGER");

        public void InitLogger()
        {
            var configFile = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            XmlConfigurator.Configure(configFile);
        }
    }
}
