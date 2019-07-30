using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _51_BACKUP_SYSTEM
{
    public abstract class Storage
    {
        private static readonly string rootDirectory = @"D:\Storage";
        private static readonly string logDirectory = @"D:\log.txt";

        public virtual string FullName { get; protected set; }
    }
}
