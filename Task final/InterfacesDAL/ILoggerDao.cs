using log4net;

namespace InterfacesDAL
{
    public interface ILoggerDao
    {
        ILog Log { get; }

        void StartLogger();
    }
}
