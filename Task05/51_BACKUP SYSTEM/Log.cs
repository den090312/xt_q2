using System.IO;

namespace _51_BACKUP_SYSTEM
{
    public static class Log
    {
        public static void StreamWriter(DirectoryInfo storageCatalog, string session)
        {
            var dataTable = DataTableCreator.Create();
            var files = storageCatalog.GetFiles(); 

            dataTable = DataTableCreator.GetDirectories(dataTable, storageCatalog.GetDirectories(), storageCatalog, session);
            dataTable = DataTableCreator.GetFiles(dataTable, files, session);
            FileWriter.Write(dataTable);

            foreach (var file in files)
            {
                if (file.Name != Storage.Log)
                {
                    FileWriter.Write(session, file.Name, File.ReadAllText(file.FullName));
                }
            }
        }
    }
}
