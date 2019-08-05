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

            return dataTable;
        }

        public static string GetRestoreGuid(DataTable logTable, DateTime restoreDate)
        {
            Storage.NullCheck(logTable);

            logTable = Sort(logTable, "Date");

            var first = 0;
            var logRows = logTable.Rows;
            var last = logRows.Count;

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

                logTable.Rows.Add(rowDir);
            }

            return logTable;
        }

        public static DataTable Sort(DataTable dataTable, string columnName)
        {
            var defaultView = dataTable.DefaultView;
            defaultView.Sort = $"{columnName} ASC";

            return defaultView.ToTable();
        }

        public static DataTable GetDirectories(DataTable dataTable, DirectoryInfo[] directories, string guid)
        {
            Storage.NullCheck(dataTable);
            Storage.NullCheck(directories);
            Storage.NullCheck(guid);

            foreach (var dir in directories)
            {
                var rowDir = dataTable.NewRow();

                rowDir["Date"] = dir.LastWriteTime; 
                rowDir["Guid"] = guid;

                dataTable.Rows.Add(rowDir);
            }

            return dataTable;
        }

        public static DataTable GetFiles(DataTable dataTable, FileInfo[] files, string guid)
        {
            Storage.NullCheck(dataTable);
            Storage.NullCheck(files);
            Storage.NullCheck(guid);

            foreach (var file in files)
            {
                if (file.Extension == Storage.Extension)
                {
                    DataRow rowFile = dataTable.NewRow();

                    rowFile["Date"] = file.LastWriteTime;
                    rowFile["Guid"] = guid;

                    dataTable.Rows.Add(rowFile);
                }
            }

            return dataTable;
        }
    }
}
