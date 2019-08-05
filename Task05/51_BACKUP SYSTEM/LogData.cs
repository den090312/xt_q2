using System;
using System.Data;
using System.IO;
using System.Threading;

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

            var first = 0;
            var logRows = logTable.Rows;
            var last = logRows.Count - 1;

            int middle;

            do
            {
                middle = (first + last) / 2;
                var middleDate = DateTime.Parse(logRows[middle]["Date"].ToString());

                if (middleDate == restoreDate)
                {
                    return logRows[middle]["Guid"].ToString();
                }

                if (middleDate > restoreDate)
                {
                    last = middle - 1;
                }

                if (middleDate < restoreDate)
                {
                    first = middle + 1;
                }
            }
            while (first < last);

            return logRows[middle]["Guid"].ToString();
        }

        public static DataTable GetTable()
        {
            var logTable = CreateTable();

            Thread.Sleep(10);
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

        public static DataTable GetDirectories(DataTable dataTable, DirectoryInfo[] directories, string guid)
        {
            Storage.NullCheck(dataTable);
            Storage.NullCheck(directories);
            Storage.NullCheck(guid);

            var rowDir = dataTable.NewRow();

            rowDir["Date"] = DateTime.Now;
            rowDir["Guid"] = guid;

            dataTable.Rows.Add(rowDir);

            return dataTable;
        }
    }
}
