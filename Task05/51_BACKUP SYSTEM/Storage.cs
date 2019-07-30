using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _51_BACKUP_SYSTEM
{
    public abstract class Storage
    {
        public static string Root { get; } = @"D:\Storage";

        public static string LogDir { get; } = @"D:\log.xml";

        public static string LogName { get; } = "log.xml";

        public virtual string FullName { get; protected set; }
    }
}
