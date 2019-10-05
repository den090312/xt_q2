using Entities;
using InterfacesDAL;
using log4net;
using log4net.Config;

namespace DAL
{
    public class LoggerDaoFile : ILoggerDao
    {
        public ILog Log { get; } = LogManager.GetLogger(Logger.Name);

        public void StartLogger() => XmlConfigurator.Configure(Logger.ConfigFile);
    }
}