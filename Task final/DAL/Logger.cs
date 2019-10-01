using log4net;
using log4net.Config;
using System;
using System.IO;

namespace DAL
{
    public class Logger
    {
        public static ILog Log { get; } = LogManager.GetLogger("LOGGER");

        public static void InitLogger()
        {
            var configFile = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            XmlConfigurator.Configure(configFile);
        }
    }
}
