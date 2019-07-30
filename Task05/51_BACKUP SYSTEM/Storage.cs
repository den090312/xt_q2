using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _51_BACKUP_SYSTEM
{
    public abstract class Storage
    {
        private readonly string storagePath = @"D:\Storage";
        private readonly string logFile = @"D:\log.txt";
    }
}
