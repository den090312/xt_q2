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

        public static long Counter { get; private set; } = 0;

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
            while (Console.Read() != '3');
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

        private static void OnEvents(object source, FileSystemEventArgs onChangedFile) => UpdateQueue();

        private static void UpdateQueue()
        {
            var storageRootInfo = new DirectoryInfo(Storage.Root);

            Thread.Sleep(10);
            var directories = storageRootInfo.GetDirectories("*.*", SearchOption.AllDirectories);

            foreach (var dir in directories)
            {
                var isFolder = true;
                var storageObject = new StorageObject(dir.FullName, string.Empty, isFolder);

                StorageObjects.Enqueue(storageObject);
            }

            Thread.Sleep(10);
            var files = storageRootInfo.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fullName = file.FullName;
                var isFolder = false;

                Thread.Sleep(10);
                var contest = File.ReadAllText(fullName);
                var storageObject = new StorageObject(fullName, contest, isFolder);

                StorageObjects.Enqueue(storageObject);
            }
        }

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
