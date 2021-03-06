﻿using System;
using System.Data;
using System.IO;
using System.Threading;

namespace _51_BACKUP_SYSTEM
{
    public abstract class Log
    {
        public static char Separator { get; } = '|';

        public static string DateFormat { get; } = "dd.MM.yyyy H:mm:ss";

        private static DataTable CreateTable()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("Date"));
            dataTable.Columns.Add(new DataColumn("Guid"));

            return dataTable;
        }

        private static void StreamWriter(DataTable dataTable)
        {
            Storage.NullCheck(dataTable);

            var streamWriter = new StreamWriter(Storage.LogFile, true);

            foreach (DataRow rowTable in dataTable.Rows)
            {
                var itemArray = rowTable.ItemArray;

                int i;

                for (i = 0; i < itemArray.Length - 1; i++)
                {
                    streamWriter.Write($"{itemArray[i].ToString()}{Separator}");
                }

                streamWriter.Write(itemArray[i].ToString());
                streamWriter.WriteLine();
            }

            streamWriter.Close();
        }

        public static string GetRestoreGuid(DataTable logTable, DateTime restoreDate)
        {
            Storage.NullCheck(logTable);

            var logRows = logTable.Rows;
            var rowsCount = logRows.Count - 1;

            for (int i = 0; i < rowsCount; i++)
            {
                var tableDate = DateTime.Parse(logRows[i]["Date"].ToString());

                if (tableDate >= restoreDate)
                {
                    return logRows[i]["Guid"].ToString();
                }
            }

            return logRows[rowsCount]["Guid"].ToString();
        }

        public static DataTable GetTable()
        {
            var logTable = CreateTable();

            Thread.Sleep(10);
            var logContest = File.ReadAllLines(Storage.LogFile);

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

        public static void AddRecord(string guid)
        {
            var dataTable = CreateTable();

            dataTable = AddRow(dataTable, guid);

            Thread.Sleep(1000);
            new Thread(() => StreamWriter(dataTable)).Start();
        }

        private static DataTable AddRow(DataTable dataTable, string guid)
        {
            Storage.NullCheck(guid);
            Storage.NullCheck(dataTable);

            var rowDir = dataTable.NewRow();

            rowDir["Date"] = DateTime.Now;
            rowDir["Guid"] = guid;

            dataTable.Rows.Add(rowDir);

            return dataTable;
        }
    }
}
