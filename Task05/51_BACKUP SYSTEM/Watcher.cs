using System;
using System.IO;
using System.Security.Permissions;

namespace _51_BACKUP_SYSTEM
{
    public class Watcher
    {
        private DateTime lastRead = DateTime.MinValue;
        private string guid = string.Empty;

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

            guid = Guid.NewGuid().ToString();

            Console.Clear();
            Console.WriteLine("Режим наблюдений включен. Нажмите '3' для выхода");
            while (Console.Read() != '3') ;          
        }

        private void OnChanged(object source, FileSystemEventArgs onChangedFile)
        {
            StartBackup(onChangedFile);
            Console.WriteLine($"File: {onChangedFile.FullPath} {onChangedFile.ChangeType}");
        }

        private void OnRenamed(object source, RenamedEventArgs renamedFile)
        {
            StartBackup(renamedFile);
            Console.WriteLine($"File: {renamedFile.FullPath} {renamedFile.ChangeType}");
        }

        private void StartBackup(RenamedEventArgs renamedFile)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(renamedFile.FullPath);

            if (lastWriteTime != lastRead)
            {
                Storage.CreateBackup(guid);
                lastRead = lastWriteTime;
            }
        }

        private void StartBackup(FileSystemEventArgs onChangedFile)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(onChangedFile.FullPath);

            if (lastWriteTime != lastRead)
            {
                Storage.CreateBackup(guid);
                lastRead = lastWriteTime;
            }
        }
    }
}
