using System;
using System.Data;
using System.IO;
using System.Threading;

namespace _51_BACKUP_SYSTEM
{
    public abstract class Storage
    {
        public static string Disk { get; } = "D";

        public static string Tom { get; } = $@"{Disk}:\";

        public static string Main { get; } = $"{Tom}Task05";

        public static string Root { get; } = $@"{Main}\Storage";

        public static string Log { get; } = $@"{Main}\log.txt";

        public static string Backup { get; } = $@"{Main}\backup";

        public static string Extension { get; } = ".txt";

        public static void Create()
        {
            if (!File.Exists(Main))
            {
                Directory.CreateDirectory(Main);
            }

            if (!File.Exists(Root))
            {
                Directory.CreateDirectory(Root);
            }

            if (!File.Exists(Log))
            {
                var streamWriter = new StreamWriter(Log, true);
                streamWriter.Write("");
                streamWriter.Close();
            }

            if (!File.Exists(Backup))
            {
                Directory.CreateDirectory(Backup);
            }
        }

        public static void WriteInfo()
        {
            Console.WriteLine("---------Task folders--------");
            Console.WriteLine();
            Console.WriteLine($"Main {Main}");
            Console.WriteLine($"Root {Root}");
            Console.WriteLine($"Backup {Backup}");
            Console.WriteLine($"Extension filter {Extension}");
            Console.WriteLine("-----------------------------");
            Console.WriteLine();
        }

        public static void CreateBackup(string guid)
        {
            var storageRoot = new DirectoryInfo(Root);
            var dataTable = LogData.CreateTable();

            var thread = new Thread(() => 
            {
                Thread.Sleep(100);

                var files = storageRoot.GetFiles("*.*", SearchOption.AllDirectories);
                var directories = storageRoot.GetDirectories("*.*", SearchOption.AllDirectories);

                dataTable = LogData.GetDirectories(dataTable, directories, guid);
                dataTable = LogData.GetFiles(dataTable, files, guid);

                FileWriter.Write(dataTable);

                foreach (var dir in directories)
                {
                    FileWriter.Write(guid, dir.FullName);
                }

                foreach (var file in files)
                {
                    if (file.Extension == Extension)
                    {
                        if (File.Exists(file.FullName))
                        {
                            FileWriter.Write(guid, file.FullName, File.ReadAllText(file.FullName));
                        }
                    }
                }
            });

            thread.Start();
        }

        public static void RestoreToDate(DateTime restoreDate)
        {
            var logTable = LogData.GetTable();
            var restoreGuid = LogData.GetRestoreGuid(logTable, restoreDate);
            var restoreFolder = new DirectoryInfo($"{Backup}\\{restoreGuid}");

            CleanStorageRoot();

            restoreFolder.MoveTo(Root);
            Console.WriteLine("Backup is done");
        }

        private static void CleanStorageRoot()
        {
            var storageRoot = new DirectoryInfo(Root);
            var files = storageRoot.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                file.Delete();
            }

            var rootSubDirectories = storageRoot.GetDirectories("*.*", SearchOption.AllDirectories);

            foreach (var subDir in rootSubDirectories)
            {
                subDir.Delete();
            }
        }

        public static void NullCheck(string thoseString)
        {
            if (thoseString is null)
            {
                throw new ArgumentException($"{nameof(thoseString)} is null!");
            }
        }

        public static void NullCheck(FileSystemEventArgs onChangedFile)
        {
            if (onChangedFile is null)
            {
                throw new ArgumentException($"{nameof(onChangedFile)} is null!");
            }
        }

        public static void NullCheck(RenamedEventArgs renamedFile)
        {
            if (renamedFile is null)
            {
                throw new ArgumentException($"{nameof(renamedFile)} is null!");
            }
        }

        public static void NullCheck(DataTable dataTable)
        {
            if (dataTable is null)
            {
                throw new ArgumentException($"{nameof(dataTable)} is null!");
            }
        }

        public static void NullCheck(DirectoryInfo[] directories)
        {
            if (directories is null)
            {
                throw new ArgumentException($"{nameof(directories)} is null!");
            }
        }

        public static void NullCheck(FileInfo[] files)
        {
            if (files is null)
            {
                throw new ArgumentException($"{nameof(files)} is null!");
            }
        }
    }
}
