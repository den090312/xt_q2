using System;
using System.IO;
using System.Security.Permissions;

namespace _51_BACKUP_SYSTEM
{
    public class Watcher
    {
        private DateTime lastRead = DateTime.MinValue;

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
            watcher.Renamed += OnRenamed;

            watcher.EnableRaisingEvents = true;

            Console.Clear();
            Console.WriteLine("Режим наблюдений включен. Нажмите '3' для выхода");
            while (Console.Read() != '3') ;          
        }

        private void OnChanged(object source, FileSystemEventArgs onChangedFile)
        {
            CreateBackup(onChangedFile);
            Console.WriteLine($"File: {onChangedFile.FullPath} {onChangedFile.ChangeType}");
        }

        private void OnRenamed(object source, RenamedEventArgs renamedFile)
        {
            CreateBackup(renamedFile);
            Console.WriteLine($"File: {renamedFile.FullPath} {renamedFile.ChangeType}");
        }

        private void CreateBackup(RenamedEventArgs renamedFile)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(renamedFile.FullPath);

            if (lastWriteTime != lastRead)
            {
                Backup.Create(new DirectoryInfo(Storage.Root), Guid.NewGuid().ToString());
                lastRead = lastWriteTime;
            }
        }

        private void CreateBackup(FileSystemEventArgs onChangedFile)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(onChangedFile.FullPath);

            if (lastWriteTime != lastRead)
            {
                Backup.Create(new DirectoryInfo(Storage.Root), Guid.NewGuid().ToString());
                lastRead = lastWriteTime;
            }
        }
    }
}
