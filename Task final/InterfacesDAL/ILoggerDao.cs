using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDAL
{
    public interface ILoggerDao
    {
        ILog Log { get; }

        void InitLogger();
    }
}
