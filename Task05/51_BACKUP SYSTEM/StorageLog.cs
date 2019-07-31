﻿using System.Data;
using System.IO;

namespace _51_BACKUP_SYSTEM
{
    public static class StorageLog
    {
        public static void LogStreamWriter(FileSystemEventArgs storageFile, DirectoryInfo storageCatalog)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("Date"));
            dataTable.Columns.Add(new DataColumn("Time"));
            dataTable.Columns.Add(new DataColumn("Path"));
            //dataTable.Columns.Add(new DataColumn("Hash"));

            var directories = storageCatalog.GetDirectories();

            foreach (var dir in directories)
            {
                DataRow rowDir = dataTable.NewRow();

                rowDir["Date"] = storageCatalog.LastWriteTime.Date.ToString("dd.MM.yyyy");
                rowDir["Time"] = storageCatalog.LastWriteTime.ToString("HH:mm");
                rowDir["Path"] = dir.FullName;
                //row["Hash"] = string.Empty;

                dataTable.Rows.Add(rowDir);
            }

            DataRow rowFile = dataTable.NewRow();

            var fileFullPath = storageFile.FullPath;
            var lastWriteTime = File.GetLastWriteTime(fileFullPath).Date;

            rowFile["Date"] = lastWriteTime.ToString("dd.MM.yyyy");
            rowFile["Time"] = lastWriteTime.ToString("HH:mm");
            rowFile["Path"] = fileFullPath;
            //row["Hash"] = File.ReadAllText(fileFullPath).GetHashCode();

            dataTable.Rows.Add(rowFile);

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
