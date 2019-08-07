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

        private FileSystemWatcher watcher;

        public static long Counter { get; private set; } = 0;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

        //public void CatchEvents()
        //{
        //    var watcher = GetWatcher();

        //    watcher.Changed += OnEvents;
        //    watcher.Created += OnEvents;
        //    watcher.Deleted += OnEvents;
        //    watcher.Renamed += OnEvents;

        //    watcher.EnableRaisingEvents = true;

        //    ConsoleStart();
        //    Console.WriteLine($"Counter: {Counter}");
        //    while (Console.Read() != '3') ;
        //}

        public void RunBackup()
        {
            watcher = GetWatcher();

            watcher.Changed += OnBackup;
            watcher.Created += OnBackup;
            watcher.Deleted += OnBackup;
            watcher.Renamed += OnBackup;

            watcher.EnableRaisingEvents = true;

            ConsoleStart();
            Console.WriteLine($"Counter: {Counter}");
            while (Console.Read() != '3') ;
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

        private void OnBackup(object source, FileSystemEventArgs onEventObject)
        {
            watcher.EnableRaisingEvents = false;

            Storage.CreateBackup();

            Counter++;
            ConsoleStart();
            Console.WriteLine($"File: {onEventObject.FullPath} {onEventObject.ChangeType}");
            Console.WriteLine($"Counter: {Counter}");

            watcher.EnableRaisingEvents = true;
        }

        private static void ConsoleStart()
        {
            Console.Clear();
            Console.WriteLine("Watcher mode is on. Press '3' to exit");
            Console.WriteLine("-------------------------------------");
        }
    }
}
