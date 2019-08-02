using System;
using System.IO;

namespace _51_BACKUP_SYSTEM
{
    public abstract class Storage
    {
        public static string Root { get; } = @"D:\Task05\Storage";

        public static string Log { get; } = @"D:\Task05\log.txt";

        public static string Backup { get; } = @"D:\Task05\backup";

        public static void CreateBackup(string guid)
        {
            var storageRoot = new DirectoryInfo(Root);
            var dataTable = LogData.CreateTable();
            var files = storageRoot.GetFiles("*.*", SearchOption.AllDirectories);
            var directories = storageRoot.GetDirectories("*.*", SearchOption.AllDirectories);

            dataTable = LogData.GetDirectories(dataTable, directories, storageRoot, guid);
            dataTable = LogData.GetFiles(dataTable, files, guid);

            FileWriter.Write(dataTable);

            foreach (var dir in directories)
            {
                FileWriter.Write(guid, dir.FullName);
            }

            foreach (var file in files)
            {
                FileWriter.Write(guid, file.FullName, File.ReadAllText(file.FullName));
            }
        }

        public static void RestoreToDate(DateTime restoreDate)
        {
            //заполнить таблицу
            var logTable = LogData.GetTable();

            //получить guid
            var restoreGuid = LogData.GetRestoreGuid(logTable, restoreDate);

            //получить папку в бэкапе
            //стереть подпапки Storage
            //накатить бэкап в Storage
        }
    }
}
