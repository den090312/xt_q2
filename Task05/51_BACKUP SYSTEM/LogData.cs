﻿using System;
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

        public static string GetRestoreGuid(DataTable logTable, DateTime date) => BinarySearch(logTable.Rows, date, 1, logTable.Rows.Count);

        private static string BinarySearch(DataRowCollection logRows, DateTime date, int first, int logRowsCount)
        {
            int middle = logRows.Count / 2;
            var middleRow = logRows[middle].ToString();

            if (middleRow["Date"] == date.ToString())
            {
                return middleRow["Guid"];
            }

            if (DateTime.Parse(middleRow["Date"]) > date)
            {
                return BinarySearch(logRows, date, first, middle - 1);
            }
            else
            {
                return BinarySearch(logRows, date, middle + 1, logRowsCount);
            }
        }

        public static DataTable GetTable()
        {
            var logContest = File.ReadAllLines(Storage.Log);
            var logTable = CreateTable();

            foreach (var logRow in logContest)
            {
                var collsArray = logRow.Split(Separator);

                var rowDir = logTable.NewRow();

                rowDir["Date"] = collsArray[0];
                rowDir["Guid"] = collsArray[1];
                rowDir["Time"] = collsArray[2];
                rowDir["Name"] = collsArray[3];

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
                DataRow rowFile = dataTable.NewRow();

                rowFile["Date"] = DateTime.Now;
                rowFile["Guid"] = guid;
                rowFile["Name"] = file.Name;
                rowFile["Hash"] = File.ReadAllText(file.FullName).GetHashCode();

                dataTable.Rows.Add(rowFile);
            }

            return dataTable;
        }
    }
}
