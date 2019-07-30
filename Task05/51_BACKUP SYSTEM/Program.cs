using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _51_BACKUP_SYSTEM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var watcher = new Watcher();
            watcher.Run();
        }
    }
}
