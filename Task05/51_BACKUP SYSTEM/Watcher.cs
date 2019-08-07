using System;
using System.IO;
using System.Security.Permissions;

namespace _51_BACKUP_SYSTEM
{
    public class Watcher
    {
        private FileSystemWatcher watcher;

        public static long Counter { get; private set; } = 0;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

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
                Filter = "*.*"
            };

            return watcher;
        }

        private void OnBackup(object source, FileSystemEventArgs onEventObject)
        {
            watcher.EnableRaisingEvents = false;

            Storage.CreateBackup();

            watcher.EnableRaisingEvents = true;

            Counter++;
            ConsoleStart();
            Console.WriteLine($"File: {onEventObject.FullPath} {onEventObject.ChangeType}");
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
