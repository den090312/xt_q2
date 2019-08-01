using System.IO;

namespace _51_BACKUP_SYSTEM
{
    public static class Log
    {
        public static void StreamWriter(DirectoryInfo storageRootCatalog, string session)
        {
            var dataTable = DataTableCreator.Create();
            var files = storageRootCatalog.GetFiles("*.*", SearchOption.AllDirectories);
            var directories = storageRootCatalog.GetDirectories("*.*", SearchOption.AllDirectories);

            dataTable = DataTableCreator.GetDirectories(dataTable, directories, storageRootCatalog, session);
            dataTable = DataTableCreator.GetFiles(dataTable, files, session);

            FileWriter.Write(dataTable);

            foreach (var file in files)
            {
                FileWriter.Write(session, file.Name, File.ReadAllText(file.FullName));
            }
        }
    }
}
