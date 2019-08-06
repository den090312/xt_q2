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
                Thread.Sleep(10);
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

        public static void CreateFile(string guid, FileInfo fileInfo)
        {
            NullCheck(guid);
            NullCheck(fileInfo);

            var filePath = fileInfo.FullName;

            Thread.Sleep(10);
            var fileContents = File.ReadAllText(fileInfo.FullName);

            Thread.Sleep(10);
            Directory.CreateDirectory($"{Backup}\\{guid}");

            filePath = filePath.Replace(Root + "\\", string.Empty);
            var path = Path.Combine(Backup, guid, filePath);

            Thread.Sleep(10);

            new Thread(() =>
            {
                var streamWriter = new StreamWriter(path, false);

                streamWriter.Write(fileContents);
                streamWriter.Close();

            }).Start();
        }

        public static void CreateDir(string guid, DirectoryInfo dirInfo)
        {
            NullCheck(guid);

            var dirPath = dirInfo.FullName;

            dirPath = dirPath.Replace(Root + "\\", string.Empty);

            var path = Path.Combine(Backup, guid, dirPath);

            Thread.Sleep(10);
            Directory.CreateDirectory(path);
        }

        public static void CreateBackup(string guid)
        {
            var storageRootInfo = new DirectoryInfo(Root);
            var dataTable = LogData.CreateTable();

            Thread.Sleep(10);
            var files = storageRootInfo.GetFiles("*.*", SearchOption.AllDirectories);

            Thread.Sleep(10);
            var directories = storageRootInfo.GetDirectories("*.*", SearchOption.AllDirectories);

            Thread.Sleep(10);
            dataTable = LogData.AddRow(dataTable, guid);

            new Thread(() => LogData.Write(dataTable)).Start();

            foreach (var dirInfo in directories)
            {
                CreateDir(guid, dirInfo);
            }
            
            foreach (var fileInfo in files)
            {
                if (fileInfo.Extension == Extension)
                {
                    CreateFile(guid, fileInfo);
                }
            }
        }

        public static void RestoreToDate(DateTime restoreDate)
        {
            var logTable = LogData.GetTable();
            var restoreGuid = LogData.GetRestoreGuid(logTable, restoreDate);
            var restorePath = $"{Backup}\\{restoreGuid}";
            var restoreFolder = new DirectoryInfo(restorePath);

            Thread.Sleep(10);
            var restoreDirectories = restoreFolder.GetDirectories("*.*", SearchOption.AllDirectories);

            Thread.Sleep(10);
            var files = restoreFolder.GetFiles("*.*", SearchOption.AllDirectories);

            DeleteFiles();
            DeleteSubDirectories(Root);

            RestoreDirs(restoreDirectories, restorePath);
            RestoreFiles(files, restorePath);
        }

        private static void RestoreFiles(FileInfo[] files, string restorePath)
        {
            foreach (var file in files)
            {
                var filePath = file.FullName.Replace(restorePath + "\\", "");
                var path = Path.Combine(Root, filePath);

                Thread.Sleep(10);
                File.Copy(file.FullName, path);
            }
        }

        private static void RestoreDirs(DirectoryInfo[] restoreDirectories, string restorePath)
        {
            foreach (var restoreDir in restoreDirectories)
            {
                var subDirPath = restoreDir.FullName.Replace(restorePath + "\\", "");
                var path = Path.Combine(Root, subDirPath);

                Thread.Sleep(10);
                Directory.CreateDirectory(path);
            }
        }

        private static void DeleteFiles()
        {
            var storageRoot = new DirectoryInfo(Root);

            Thread.Sleep(10);
            var files = storageRoot.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                file.Delete();
            }
        }

        private static void DeleteSubDirectories(string path)
        {
            var storageRoot = new DirectoryInfo(path);

            Thread.Sleep(10);
            var directories = storageRoot.GetDirectories("*.*", SearchOption.AllDirectories);

            foreach (var dir in directories)
            {
                if (Directory.Exists(dir.FullName))
                {
                    DeleteSubDirectories(dir.FullName);
                }
            }

            if (Directory.Exists(path) & path != Root)
            {
                Thread.Sleep(10);
                Directory.Delete(path, true);
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

        public static void NullCheck(FileInfo file)
        {
            if (file is null)
            {
                throw new ArgumentException($"{nameof(file)} is null!");
            }
        }
    }
}
