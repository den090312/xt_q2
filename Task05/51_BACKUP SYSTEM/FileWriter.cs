using System.Data;
using System.IO;

namespace _51_BACKUP_SYSTEM
{
    public static class FileWriter
    {
        public static void Write(string session, string fileName, string fileContents)
        {
            var fullPath = $"{Storage.Root}\"{session}\"{fileName}";

            var streamWriter = new StreamWriter(fullPath, false);
            streamWriter.Write(fileContents);
            streamWriter.Close();
        }

        public static void Write(DataTable dataTable)
        {
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
