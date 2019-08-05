using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;

namespace _51_BACKUP_SYSTEM
{
    public class Watcher
    {
        private DateTime lastRead = DateTime.MinValue;

        public static List<FileSystemEventArgs> FilesList { get; } = new List<FileSystemEventArgs>();

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

            Console.Clear();
            Console.WriteLine("Watcher mode is on. Press '3' to exit");
            while (Console.Read() != '3');          
        }

        private void OnChanged(object source, FileSystemEventArgs onChangedFile)
        {
            Storage.NullCheck(onChangedFile);

            DateTime lastWriteTime = File.GetLastWriteTime(onChangedFile.FullPath);

            if (lastWriteTime != lastRead)
            {
                if (onChangedFile.ChangeType == WatcherChangeTypes.Created)
                {
                    FilesList.Add(onChangedFile);
                }

                if (onChangedFile.ChangeType == WatcherChangeTypes.Deleted)
                {
                    if (FilesList.Exists(x => x == onChangedFile))
                    {
                        FilesList.Remove(onChangedFile);
                    }
                }

                if (onChangedFile.ChangeType == WatcherChangeTypes.Changed)
                {

                }

                if (onChangedFile.ChangeType == WatcherChangeTypes.Renamed)
                {

                }

                lastRead = lastWriteTime;
            }

            Console.WriteLine($"File: {onChangedFile.FullPath} {onChangedFile.ChangeType}");
            Console.WriteLine("Press '3' to start backup");
        }
    }
}
