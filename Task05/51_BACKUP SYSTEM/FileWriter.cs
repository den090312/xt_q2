using System.Data;
using System.IO;
using System.Threading;

namespace _51_BACKUP_SYSTEM
{
    public static class FileWriter
    {
        public static void Write(string guid, string filePath, string fileContents)
        {
            Storage.NullCheck(guid);
            Storage.NullCheck(filePath);
            Storage.NullCheck(fileContents);

            Directory.CreateDirectory($"{Storage.Backup}\\{guid}");

            filePath = filePath.Replace(Storage.Root + "\\", string.Empty);

            var path = Path.Combine(Storage.Backup, guid, filePath);

            if (File.Exists(path))
            {
                var streamWriter = new StreamWriter(path, false);

                streamWriter.Write(fileContents);
                streamWriter.Close();
            }
        }

        public static void Write(string guid, string filePath)
        {
            Storage.NullCheck(guid);
            Storage.NullCheck(filePath);

            filePath = filePath.Replace(Storage.Root + "\\", string.Empty);

            var path = Path.Combine(Storage.Backup, guid, filePath);

            Directory.CreateDirectory(path);
        }

        public static void Write(DataTable dataTable)
        {
            Storage.NullCheck(dataTable);

            var streamWriter = new StreamWriter(Storage.Log, true);

            foreach (DataRow rowTable in dataTable.Rows)
            {
                var itemArray = rowTable.ItemArray;

                int i;

                for (i = 0; i < itemArray.Length - 1; i++)
                {
                    streamWriter.Write($"{itemArray[i].ToString()}{LogData.Separator}");
                }

                streamWriter.Write(itemArray[i].ToString());
                streamWriter.WriteLine();
            }

            streamWriter.Close();
        }
    }
}
