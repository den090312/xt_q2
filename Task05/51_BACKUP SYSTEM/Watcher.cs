using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Threading;

namespace _51_BACKUP_SYSTEM
{
    public class Watcher
    {
        public static Queue<StorageObject> StorageObjects { get; set; } = new Queue<StorageObject>();

        public static long Counter { get; set; } = 0;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

        public void RunEvents()
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

            watcher.Changed += OnChangedEvents;
            watcher.Created += OnChangedEvents;
            watcher.Deleted += OnChangedEvents;
            watcher.Renamed += OnChangedEvents;

            watcher.EnableRaisingEvents = true;

            ConsoleStart();
            Console.WriteLine($"Counter: {Counter}");
            while (Console.Read() != '3');
        }

        public void RunBackup()
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

            watcher.Changed += OnChangedBackup;
            watcher.Created += OnChangedBackup;
            watcher.Deleted += OnChangedBackup;
            watcher.Renamed += OnChangedBackup;

            watcher.EnableRaisingEvents = true;
        }

        private static void OnChangedEvents(object source, FileSystemEventArgs onChangedFile) => UpdateQueue();

        private static void OnChangedBackup(object source, FileSystemEventArgs onChangedFile)
        {
            //Thread.Sleep(1000);
            Storage.CreateBackup();

            Counter++;
            ConsoleStart();
            Console.WriteLine($"File: {onChangedFile.FullPath} {onChangedFile.ChangeType}");
            Console.WriteLine($"Counter: {Counter}");
        }

        private static void UpdateQueue()
        {
            var storageRootInfo = new DirectoryInfo(Storage.Root);

            Thread.Sleep(10);
            var directories = storageRootInfo.GetDirectories("*.*", SearchOption.AllDirectories);

            foreach (var dir in directories)
            {
                var storageObject = new StorageObject(dir.FullName, string.Empty);

                StorageObjects.Enqueue(storageObject);
            }

            Thread.Sleep(10);
            var files = storageRootInfo.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fullName = file.FullName;
                var storageObject = new StorageObject(fullName, File.ReadAllText(fullName));

                StorageObjects.Enqueue(storageObject);
            }
        }

        public static void ConsoleStart()
        {
            Console.Clear();
            Console.WriteLine("Watcher mode is on. Press '3' to exit");
            Console.WriteLine("-------------------------------------");
        }
    }
}
