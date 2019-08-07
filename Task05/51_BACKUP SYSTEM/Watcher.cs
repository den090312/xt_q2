using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Threading;

namespace _51_BACKUP_SYSTEM
{
    public class Watcher
    {
        private int lastRead = DateTime.MinValue.Millisecond;

        public static Queue<StorageObject> EventQueue { get; set; } = new Queue<StorageObject>();

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
                Filter = "*.*",
                InternalBufferSize = 64000
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
                Filter = "*.*",
                InternalBufferSize = 64000
            };

            watcher.Changed += OnChangedBackup;
            watcher.Created += OnChangedBackup;
            watcher.Deleted += OnChangedBackup;
            watcher.Renamed += OnChangedBackup;

            watcher.EnableRaisingEvents = true;
        }

        private void OnChangedEvents(object source, FileSystemEventArgs onChangedFile)
        {
            Storage.NullCheck(onChangedFile);

            if (onChangedFile.ChangeType == WatcherChangeTypes.Changed)
            {
                var lastWriteTime = File.GetLastWriteTime(onChangedFile.FullPath).Millisecond;

                if (lastWriteTime != lastRead)
                {
                    FillEventQueue(onChangedFile);
                }
            }
            else
            {
                FillEventQueue(onChangedFile);            
            }
        }

        private void OnChangedBackup(object source, FileSystemEventArgs onChangedFile)
        {
            Thread.Sleep(1000);
            Storage.CreateBackup();

            Counter++;
            ConsoleStart();
            Console.WriteLine($"File: {onChangedFile.FullPath} {onChangedFile.ChangeType}");
            Console.WriteLine($"Counter: {Counter}");
        }

        private void FillEventQueue(FileSystemEventArgs onChangedFile)
        {
            var storageRootInfo = new DirectoryInfo(Storage.Root);

            Thread.Sleep(10);
            var directories = storageRootInfo.GetDirectories("*.*", SearchOption.AllDirectories);

            foreach (var dir in directories)
            {
                var storageObject = new StorageObject(dir.FullName, string.Empty);

                EventQueue.Enqueue(storageObject);
            }

            Thread.Sleep(10);
            var files = storageRootInfo.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fullName = file.FullName;
                var storageObject = new StorageObject(fullName, File.ReadAllText(fullName));

                EventQueue.Enqueue(storageObject);
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
