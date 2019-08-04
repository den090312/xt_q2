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
            Console.WriteLine("Watcher mode is on. Press '3' to exit");
            while (Console.Read() != '3') ;          
        }

        private void OnChanged(object source, FileSystemEventArgs onChangedFile)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(onChangedFile.FullPath);

            if (lastWriteTime != lastRead)
            {
                Storage.CreateBackup(guid);
                lastRead = lastWriteTime;
            }

            Console.WriteLine($"File: {onChangedFile.FullPath} {onChangedFile.ChangeType}");
        }

        private void OnRenamed(object source, RenamedEventArgs renamedFile)
        {
            var newGuid = Guid.NewGuid().ToString();

            Storage.CreateBackup(newGuid);

            Console.WriteLine($"File: {renamedFile.FullPath} {renamedFile.ChangeType}");
        }
    }
}
