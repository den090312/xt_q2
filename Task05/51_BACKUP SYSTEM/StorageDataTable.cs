using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _51_BACKUP_SYSTEM
{
    public static class StorageDataTable
    {
        public static void DataTableStream(FileSystemEventArgs storageFile, DirectoryInfo storageCatalog)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("StorageType"));
            dataTable.Columns.Add(new DataColumn("Date"));
            dataTable.Columns.Add(new DataColumn("Time"));
            dataTable.Columns.Add(new DataColumn("Path"));

            var directories = storageCatalog.GetDirectories();

            foreach (var dir in directories)
            {
                DataRow row = dataTable.NewRow();

                row["StorageType"] = "Catalog";
                row["Date"] = storageCatalog.LastWriteTime.Date.ToString("dd.MM.yyyy");
                row["Time"] = storageCatalog.LastWriteTime.ToString("HH:mm");
                row["Path"] = dir.FullName;

                dataTable.Rows.Add(row);
            }

            if (storageFile.Name != "0")
            {
                DataRow row = dataTable.NewRow(); 

                row["StorageType"] = "File";
                var lastWriteTime = File.GetLastWriteTime(storageFile.FullPath).Date;
                row["Date"] = lastWriteTime.ToString("dd.MM.yyyy");
                row["Time"] = lastWriteTime.ToString("HH:mm");
                row["Path"] = storageFile.FullPath;

                dataTable.Rows.Add(row);
            }

            var streamWriter = new StreamWriter(Storage.LogDir, false);

            foreach (DataRow row in dataTable.Rows)
            {
                var array = row.ItemArray;

                int i;

                for (i = 0; i < array.Length - 1; i++)
                {
                    streamWriter.Write($"{array[i].ToString()}|");
                }

                streamWriter.Write(array[i].ToString());
                streamWriter.WriteLine();
            }

            streamWriter.Close();
        }

        //private static byte[] GetBitArray(FileSystemEventArgs file) => File.ReadAllBytes(file.FullPath);
    }
}
