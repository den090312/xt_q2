using System.IO;

namespace _51_BACKUP_SYSTEM
{
    public static class Backup
    {
        public static void Create(DirectoryInfo storageRootCatalog, string guid)
        {
            var dataTable = DataTableCreator.Create();
            var files = storageRootCatalog.GetFiles("*.*", SearchOption.AllDirectories);
            var directories = storageRootCatalog.GetDirectories("*.*", SearchOption.AllDirectories);

            dataTable = DataTableCreator.GetDirectories(dataTable, directories, storageRootCatalog, guid);
            dataTable = DataTableCreator.GetFiles(dataTable, files, guid);

            FileWriter.Write(dataTable);

            foreach (var dir in directories)
            {
                FileWriter.Write(guid, dir.FullName);
            }

            foreach (var file in files)
            {
                FileWriter.Write(guid, file.FullName, File.ReadAllText(file.FullName));
            }
        }
    }
}
