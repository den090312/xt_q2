using System;
using System.Data;
using System.IO;

namespace _51_BACKUP_SYSTEM
{
    public class LogData
    {
        public static char Separator { get; } = '|';

        public static string DateFormat { get; } = "dd.MM.yyyy HH:mm:ss";

        public static DataTable CreateTable()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("Date"));
            dataTable.Columns.Add(new DataColumn("Guid"));
            dataTable.Columns.Add(new DataColumn("Name"));
            dataTable.Columns.Add(new DataColumn("Hash"));

            return dataTable;
        }

        public static string GetRestoreGuid(DataRowCollection logRows, DateTime restoreDate, int first, int last)
        {
            int middle;

            do
            {
                middle = (first + last) / 2;
                var middleDate = DateTime.Parse(logRows[middle]["Date"].ToString());

                if (middleDate > restoreDate)
                {
                    last = middle - 1;
                }
                else
                {
                    first = middle + 1;
                }
            }
            while (first != last);

            return logRows[middle]["Guid"].ToString();
        }

        public static DataTable GetTable()
        {
            var logTable = CreateTable();
            var logContest = File.ReadAllLines(Storage.Log);

            foreach (var logRow in logContest)
            {
                var collsArray = logRow.Split(Separator);

                var rowDir = logTable.NewRow();

                rowDir["Date"] = collsArray[0];
                rowDir["Guid"] = collsArray[1];
                rowDir["Name"] = collsArray[2];
                rowDir["Hash"] = collsArray[3];

                logTable.Rows.Add(rowDir);
            }

            return logTable;
        }

        public static DataTable GetDirectories(DataTable dataTable, DirectoryInfo[] directories, DirectoryInfo storageCatalog, string guid)
        {
            foreach (var dir in directories)
            {
                var rowDir = dataTable.NewRow();

                rowDir["Date"] = DateTime.Now; 
                rowDir["Guid"] = guid;
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
                if (file.Extension == Storage.Extension)
                {
                    DataRow rowFile = dataTable.NewRow();

                    rowFile["Date"] = DateTime.Now;
                    rowFile["Guid"] = guid;
                    rowFile["Name"] = file.Name;
                    rowFile["Hash"] = File.ReadAllText(file.FullName).GetHashCode();

                    dataTable.Rows.Add(rowFile);
                }
            }

            return dataTable;
        }
    }
}
