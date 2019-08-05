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

        public static void Write(string guid, FileInfo file)
        {
            NullCheck(guid);
            NullCheck(file);

            var filePath = file.FullName;
            var fileContents = File.ReadAllText(file.FullName);

            Directory.CreateDirectory($"{Backup}\\{guid}");

            filePath = filePath.Replace(Root + "\\", string.Empty);

            var path = Path.Combine(Backup, guid, filePath);
            var streamWriter = new StreamWriter(path, false);

            streamWriter.Write(fileContents);
            streamWriter.Close();
        }

        public static void Write(string guid, DirectoryInfo dir)
        {
            NullCheck(guid);

            var dirPath = dir.FullName;

            dirPath = dirPath.Replace(Root + "\\", string.Empty);

            var path = Path.Combine(Backup, guid, dirPath);

            Directory.CreateDirectory(path);
        }

        public static void Write(DataTable dataTable)
        {
            NullCheck(dataTable);

            var streamWriter = new StreamWriter(Log, true);

            foreach (DataRow rowTable in dataTable.Rows)
            {
                var itemArray = rowTable.ItemArray;

                int i;

                for (i = 0; i < itemArray.Length - 1; i++)
                {
                    streamWriter.Write($"{itemArray[i].ToString()}{LogData.Separator}");
                }

                streamWriter.Write(itemArray[i].ToString());
                streamWriter.WriteLine();
            }

            streamWriter.Close();
        }

        public static void CreateBackup(string guid)
        {
            var storageRoot = new DirectoryInfo(Root);
            var dataTable = LogData.CreateTable();

            Thread.Sleep(100);
            var files = storageRoot.GetFiles("*.*", SearchOption.AllDirectories);
            var directories = storageRoot.GetDirectories("*.*", SearchOption.AllDirectories);

            dataTable = LogData.GetDirectories(dataTable, directories, guid);
            dataTable = LogData.GetFiles(dataTable, files, guid);

            var thread1 = new Thread(() =>
            {
                Write(dataTable);
            });

            thread1.Start();
            
            foreach (var dir in directories)
            {
                var thread2 = new Thread(() =>
                {
                    Write(guid, dir);
                });

                thread2.Start();
            }

            foreach (var file in files)
            {
                if (file.Extension == Extension)
                {
                    var thread3 = new Thread(() =>
                    {
                        Write(guid, file);
                    });

                    thread3.Start();
                }
            }
        }

        public static void RestoreToDate(DateTime restoreDate)
        {
            var logTable = LogData.GetTable();
            var restoreGuid = LogData.GetRestoreGuid(logTable, restoreDate);
            var restorePath = $"{Backup}\\{restoreGuid}";

            var restoreFolder = new DirectoryInfo(restorePath);
            var restoreDirectories = restoreFolder.GetDirectories("*.*", SearchOption.AllDirectories);
            var files = restoreFolder.GetFiles("*.*", SearchOption.AllDirectories);

            DeleteFiles();
            DeleteSubDirectories(Root);

            foreach (var restoreDir in restoreDirectories)
            {
                var subDirPath = restoreDir.FullName.Replace(restorePath + "\\", "");
                var path = Path.Combine(Root, subDirPath);

                Directory.CreateDirectory(path);
            }

            foreach (var file in files)
            {
                var filePath = file.FullName.Replace(restorePath + "\\", "");
                var path = Path.Combine(Root, filePath);

                File.Copy(file.FullName, path);
            }

            Console.WriteLine("Backup is done");
        }

        private static void DeleteFiles()
        {
            var storageRoot = new DirectoryInfo(Root);
            var files = storageRoot.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                file.Delete();
            }
        }

        public static void DeleteSubDirectories(string path)
        {
            var storageRoot = new DirectoryInfo(path);

            Thread.Sleep(100);
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
                Thread.Sleep(100);
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
