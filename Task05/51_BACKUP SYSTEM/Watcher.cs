using System;
using System.IO;
using System.Security.Permissions;
using System.Threading;

namespace _51_BACKUP_SYSTEM
{
    public class Watcher
    {
        private int lastRead = DateTime.MinValue.Millisecond;

        public static long Counter { get; set; } = 1;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

        public void Run()
        {
            var watcher = new FileSystemWatcher
            {
                Path = Storage.Root,
                NotifyFilter = NotifyFilters.LastAccess
                             | NotifyFilters.LastWrite
                             | NotifyFilters.DirectoryName
                             | NotifyFilters.FileName,

                IncludeSubdirectories = true,
                Filter = "*.*"
            };

            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnChanged;

            watcher.EnableRaisingEvents = true;

            ConsoleStart();
            Console.WriteLine($"Counter: 0");
            while (Console.Read() != '3');
        }

        private void OnChanged(object source, FileSystemEventArgs onChangedFile)
        {
            Storage.NullCheck(onChangedFile);

            if (onChangedFile.ChangeType == WatcherChangeTypes.Changed)
            {
                Thread.Sleep(1);
                var lastWriteTime = File.GetLastWriteTime(onChangedFile.FullPath).Millisecond;

                if (lastWriteTime != lastRead)
                {
                    var guid = Guid.NewGuid().ToString();
                    Storage.CreateBackup(guid);

                    ConsoleStart();
                    lastRead = lastWriteTime;
                }
            }
            else
            {
                var guid = Guid.NewGuid().ToString();
                Storage.CreateBackup(guid);
                ConsoleStart();           
            }

            Console.WriteLine($"File: {onChangedFile.FullPath} {onChangedFile.ChangeType}");
            Console.WriteLine($"Counter: {Counter}");
            Counter++;
        }

        private static void ConsoleStart()
        {
            Console.Clear();
            Console.WriteLine("Watcher mode is on. Press '3' to exit");
            Console.WriteLine("-------------------------------------");
        }
    }
}
