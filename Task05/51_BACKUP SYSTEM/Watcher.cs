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

        public static long Counter { get; private set; } = 0;

        public static Queue<StorageObject> StorageObjects { get; private set; }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

        public void CatchEvents()
        {
            var watcher = GetWatcher();

            watcher.Changed += OnEvents;
            watcher.Created += OnEvents;
            watcher.Deleted += OnEvents;
            watcher.Renamed += OnEvents;

            watcher.EnableRaisingEvents = true;

            ConsoleStart();
            Console.WriteLine($"Counter: {Counter}");
            while (Console.Read() != '3') ;
        }

        public void RunBackup()
        {
            var watcher = GetWatcher();

            watcher.Changed += OnBackup;
            watcher.Created += OnBackup;
            watcher.Deleted += OnBackup;
            watcher.Renamed += OnBackup;

            watcher.EnableRaisingEvents = true;
        }

        private FileSystemWatcher GetWatcher()
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

            return watcher;
        }

        private void OnEvents(object source, FileSystemEventArgs onEventObject) => UpdateQueue(onEventObject);

        private void UpdateQueue(FileSystemEventArgs onEventObject)
        {
            Thread.Sleep(1);
            var lastWriteTime = File.GetLastWriteTime(onEventObject.FullPath).Millisecond;

            if (lastWriteTime != lastRead)
            {
                StorageObjects = GetQueue();
                lastRead = lastWriteTime;
            }
        }

        public static Queue<StorageObject> GetQueue()
        {
            var storageQueue = new Queue<StorageObject>();

            Thread.Sleep(10);
            var storageRootInfo = new DirectoryInfo(Storage.Root);

            Thread.Sleep(10);
            var directories = storageRootInfo.GetDirectories("*.*", SearchOption.AllDirectories);

            foreach (var dir in directories)
            {
                var isDirectory = true;
                var storageObject = new StorageObject(dir.FullName, string.Empty, isDirectory);

                if (storageQueue.Peek().GetHashCode() != storageObject.GetHashCode())
                {
                    storageQueue.Enqueue(storageObject);
                }
            }

            Thread.Sleep(10);
            var files = storageRootInfo.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fullName = file.FullName;
                var isDirectory = false;

                Thread.Sleep(10);
                var contest = File.ReadAllText(fullName);
                var storageObject = new StorageObject(fullName, contest, isDirectory);

                if (storageQueue.Peek().GetHashCode() != storageObject.GetHashCode())
                {
                    storageQueue.Enqueue(storageObject);
                }
            }

            return storageQueue;
        }

        public static Queue<StorageObject> GetStorageObjects() => StorageObjects;

        private static void OnBackup(object source, FileSystemEventArgs onChangedFile)
        {
            Thread.Sleep(1000);
            Storage.CreateBackup();

            Counter++;
            ConsoleStart();
            Console.WriteLine($"File: {onChangedFile.FullPath} {onChangedFile.ChangeType}");
            Console.WriteLine($"Counter: {Counter}");
        }

        private static void ConsoleStart()
        {
            Console.Clear();
            Console.WriteLine("Watcher mode is on. Press '3' to exit");
            Console.WriteLine("-------------------------------------");
        }
    }
}
