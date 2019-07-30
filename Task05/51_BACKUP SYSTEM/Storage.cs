using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _51_BACKUP_SYSTEM
{
    public abstract class Storage
    {
        private static readonly string root = @"D:\Storage";
        private static readonly string logDir = @"D:\log.xml";

        public virtual string FullName { get; protected set; }
    }
}
