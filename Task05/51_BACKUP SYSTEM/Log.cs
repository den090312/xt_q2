using System.Data;
using System.IO;

namespace _51_BACKUP_SYSTEM
{
    public static class Log
    {
        public static void StreamWriter(DirectoryInfo storageCatalog, string session)
        {
            var dataTable = DataTableCreator.Create();

            dataTable = DataTableCreator.GetDirectories(dataTable, storageCatalog.GetDirectories(), storageCatalog, session);
            dataTable = DataTableCreator.GetFiles(dataTable, storageCatalog.GetFiles(), storageCatalog, session);
        }
    }
}
