﻿using System.IO;

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
            var dataTable = DataTableCreator.Create();
            var files = storageRoot.GetFiles("*.*", SearchOption.AllDirectories);
            var directories = storageRoot.GetDirectories("*.*", SearchOption.AllDirectories);

            dataTable = DataTableCreator.GetDirectories(dataTable, directories, storageRoot, guid);
            dataTable = DataTableCreator.GetFiles(dataTable, files, guid);

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

        public static void RestoreFromBackup()
        {

        }
    }
}
