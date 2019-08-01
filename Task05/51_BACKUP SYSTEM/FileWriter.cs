using System.Data;
using System.IO;

namespace _51_BACKUP_SYSTEM
{
    public static class FileWriter
    {
        public static void Write(string session, string filePath, string fileContents)
        {
            var directory = $"{Storage.BackUp}\\{session}";

            Directory.CreateDirectory(directory);

            filePath = filePath.Replace(Storage.Root + "\\", string.Empty);

            var path = Path.Combine(Storage.BackUp, session, filePath);

            var streamWriter = new StreamWriter(path, false);
            streamWriter.Write(fileContents);
            streamWriter.Close();
        }

        public static void Write(string session, string filePath)
        {
            filePath = filePath.Replace(Storage.Root + "\\", string.Empty);

            var path = Path.Combine(Storage.BackUp, session, filePath);

            Directory.CreateDirectory(path);
        }

        public static void Write(DataTable dataTable)
        {
            var streamWriter = new StreamWriter(Storage.Log, true);

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
