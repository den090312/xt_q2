using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _51_BACKUP_SYSTEM
{
    public static class StorageXmlWriter
    {
        public static void ChangeXmlLog(FileSystemEventArgs file)
        {
            //if (file.ChangeType == WatcherChangeTypes.Changed)
            //{

            //}

            XmlWriter writer = XmlWriter.Create(Storage.LogDir);
        }
    }
}
