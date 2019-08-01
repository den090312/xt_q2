using System.Data;
using System.IO;

namespace _51_BACKUP_SYSTEM
{
    public static class Log
    {
        public static void StreamWriter(DirectoryInfo storageCatalog, string session)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("Session"));
            dataTable.Columns.Add(new DataColumn("Date"));
            dataTable.Columns.Add(new DataColumn("Time"));
            dataTable.Columns.Add(new DataColumn("Path"));
            dataTable.Columns.Add(new DataColumn("Hash"));

            var directories = storageCatalog.GetDirectories();

            foreach (var dir in directories)
            {
                DataRow rowDir = dataTable.NewRow();

                rowDir["Session"] = session;
                rowDir["Date"] = storageCatalog.LastWriteTime.Date.ToString("dd.MM.yyyy");
                rowDir["Time"] = storageCatalog.LastWriteTime.ToString("HH:mm:ss");
                rowDir["Path"] = dir.FullName;
                rowDir["Hash"] = string.Empty;

                dataTable.Rows.Add(rowDir);
            }

            var files = storageCatalog.GetFiles();

            foreach (var file in files)
            {
                DataRow rowFile = dataTable.NewRow();

                var fileFullName = file.FullName;
                var lastWriteDate = File.GetLastWriteTime(fileFullName);

                rowFile["Session"] = session;
                rowFile["Date"] = lastWriteDate.ToString("dd.MM.yyyy");
                rowFile["Time"] = lastWriteDate.ToString("HH:mm:ss");
                rowFile["Path"] = fileFullName;
                rowFile["Hash"] = File.ReadAllText(fileFullName).GetHashCode();

                dataTable.Rows.Add(rowFile);
            }

            var streamWriter = new StreamWriter(Storage.LogDir, true);

            foreach (DataRow rowTable in dataTable.Rows)
            {
                var itemArray = rowTable.ItemArray;

                int i;

                for (i = 0; i < itemArray.Length - 1; i++)
                {
                    streamWriter.Write($"{itemArray[i].ToString()}|");
                }

                streamWriter.Write(itemArray[i].ToString());
                streamWriter.WriteLine();
            }

            streamWriter.Close();
        }
    }
}
