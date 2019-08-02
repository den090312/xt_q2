using System.Data;
using System.IO;

namespace _51_BACKUP_SYSTEM
{
    public static class StorageLog
    {
        public static DataTable CreateDataTable()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("Guid"));
            dataTable.Columns.Add(new DataColumn("Date"));
            dataTable.Columns.Add(new DataColumn("Time"));
            dataTable.Columns.Add(new DataColumn("Name"));
            dataTable.Columns.Add(new DataColumn("Hash"));

            return dataTable;
        }

        public static DataTable GetDirectories(DataTable dataTable, DirectoryInfo[] directories, DirectoryInfo storageCatalog, string guid)
        {
            foreach (var dir in directories)
            {
                DataRow rowDir = dataTable.NewRow();

                rowDir["Guid"] = guid;
                rowDir["Date"] = storageCatalog.LastWriteTime.Date.ToString("dd.MM.yyyy");
                rowDir["Time"] = storageCatalog.LastWriteTime.ToString("HH:mm:ss");
                rowDir["Name"] = dir.Name;
                rowDir["Hash"] = string.Empty;

                dataTable.Rows.Add(rowDir);
            }

            return dataTable;
        }

        public static DataTable GetFiles(DataTable dataTable, FileInfo[] files, string guid)
        {
            foreach (var file in files)
            {
                DataRow rowFile = dataTable.NewRow();

                var fileName      = file.Name;
                var fileFullName  = file.FullName;
                var lastWriteDate = File.GetLastWriteTime(fileFullName);
                var fileContents  = File.ReadAllText(fileFullName);

                rowFile["Guid"] = guid;
                rowFile["Date"] = lastWriteDate.ToString("dd.MM.yyyy");
                rowFile["Time"] = lastWriteDate.ToString("HH:mm:ss");
                rowFile["Name"] = fileName;
                rowFile["Hash"] = fileContents.GetHashCode();

                dataTable.Rows.Add(rowFile);
            }

            return dataTable;
        }
    }
}
