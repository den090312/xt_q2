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

        public static string LogFile { get; } = $@"{Main}\log.txt";

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

            if (!File.Exists(LogFile))
            {
                Thread.Sleep(10);
                var streamWriter = new StreamWriter(LogFile, true);

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
            Console.WriteLine($"Main {Main}");
            Console.WriteLine($"Root {Root}");
            Console.WriteLine($"Log {LogFile}");
            Console.WriteLine($"Backup {Backup}");
            Console.WriteLine($"Extension filter {Extension}");
            Console.WriteLine("-----------------------------");
        }

        public static void CreateFile(string guid, StorageObject eventObject)
        {
            NullCheck(guid);

            var filePath = eventObject.FullName;
            NullCheck(filePath);

            Thread.Sleep(10);
            var fileContents = File.ReadAllText(filePath);

            Thread.Sleep(10);
            Directory.CreateDirectory($"{Backup}\\{guid}");

            filePath = filePath.Replace(Root + "\\", string.Empty);
            var path = Path.Combine(Backup, guid, filePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(path, false);
            streamWriter.Write(fileContents);
            streamWriter.Close();
        }

        public static void CreateDir(string guid, string dirPath)
        {
            NullCheck(guid);
            NullCheck(dirPath);

            dirPath = dirPath.Replace(Root + "\\", string.Empty);

            var path = Path.Combine(Backup, guid, dirPath);

            Directory.CreateDirectory(path);
        }

        public static void CreateBackup()
        {
            var guid = Guid.NewGuid().ToString();

            var eventQueue = Watcher.EventQueue;

            while (eventQueue.Count != 0)
            {
                var eventObject = eventQueue.Dequeue();

                if (eventObject.Contest == string.Empty)
                {
                    CreateDir(guid, eventObject.FullName);
                    Console.Write(".");
                }
                else
                {
                    CreateFile(guid, eventObject);
                    Console.Write(".");
                }
            }

            var guidpath = $"{Backup}\\{guid}";

            if (Directory.Exists(guidpath))
            {
                Log.AddRecord(guid);
            }
        }

        public static void RestoreToDate(DateTime restoreDate)
        {
            var logTable = Log.GetTable();
            var guid = Log.GetRestoreGuid(logTable, restoreDate);
            var path = $"{Backup}\\{guid}";
            var pathInfo = new DirectoryInfo(path);

            Thread.Sleep(10);
            var directories = pathInfo.GetDirectories("*.*", SearchOption.AllDirectories);

            Thread.Sleep(10);
            var files = pathInfo.GetFiles("*.*", SearchOption.AllDirectories);

            DeleteFiles();
            DeleteDirectories(Root);

            RestoreDirectories(directories, path);
            RestoreFiles(files, path);
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

        private static void RestoreDirectories(DirectoryInfo[] directories, string restorePath)
        {
            foreach (var dir in directories)
            {
                var subDirPath = dir.FullName.Replace(restorePath + "\\", "");
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

        private static void DeleteDirectories(string path)
        {
            var storageRoot = new DirectoryInfo(path);

            Thread.Sleep(10);
            var directories = storageRoot.GetDirectories("*.*", SearchOption.AllDirectories);

            foreach (var dir in directories)
            {
                if (Directory.Exists(dir.FullName))
                {
                    DeleteDirectories(dir.FullName);
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

        public static void NullCheck(DirectoryInfo directoryInfo)
        {
            if (directoryInfo is null)
            {
                throw new ArgumentException($"{nameof(directoryInfo)} is null!");
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
