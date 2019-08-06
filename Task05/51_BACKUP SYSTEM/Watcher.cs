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
            while (Console.Read() != '3') ;
        }

        private void OnChanged(object source, FileSystemEventArgs onChangedFile)
        {
            Storage.NullCheck(onChangedFile);

            Thread.Sleep(1);
            var lastWriteTime = File.GetLastWriteTime(onChangedFile.FullPath).Millisecond;

            if (lastWriteTime != lastRead)
            {
                Program.Backup();

                //if (onChangedFile.ChangeType == WatcherChangeTypes.Created)
                //{
                //    FilesList.Add(onChangedFile);
                //}

                //if (onChangedFile.ChangeType == WatcherChangeTypes.Deleted)
                //{
                //    if (FilesList.Exists(x => x == onChangedFile))
                //    {
                //        FilesList.Remove(onChangedFile);
                //    }
                //}

                //if (onChangedFile.ChangeType == WatcherChangeTypes.Changed || onChangedFile.ChangeType == WatcherChangeTypes.Renamed)
                //{
                //    if (FilesList.Exists(x => x == onChangedFile))
                //    {
                //        FilesList.Remove(onChangedFile);
                //    }

                //    FilesList.Add(onChangedFile);
                //}

                lastRead = lastWriteTime;
            }

            //Console.WriteLine($"File: {onChangedFile.FullPath} {onChangedFile.ChangeType}");
            //Console.WriteLine("Press '3' to start backup");
        }
    }
}
