using InterfacesBLL;
using InterfacesDAL;
using log4net;
using System;

namespace BLL
{
    public class LoggerLogic : ILoggerLogic
    {
        private readonly ILoggerDao loggerDao;

        public LoggerLogic(ILoggerDao iLoggerDao)
        {
            NullCheck(iLoggerDao);

            loggerDao = iLoggerDao;
        }

        public ILog Log => loggerDao.Log;

        public void InitLogger() => loggerDao.StartLogger();

        private void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}