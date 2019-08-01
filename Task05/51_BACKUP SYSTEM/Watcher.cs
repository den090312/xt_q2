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
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = Storage.Root;
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.DirectoryName
                                     | NotifyFilters.FileName;

                watcher.Filter = "*.*";

                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;

                watcher.EnableRaisingEvents = true;

                Console.WriteLine("\t3: выход");
                while (Console.Read() != '3') ;
            }
        }

        private void OnChanged(object source, FileSystemEventArgs file)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(file.FullPath); 

            if (lastWriteTime != lastRead)
            {
                if (file.FullPath != Storage.LogDir)
                {
                    Console.WriteLine($"File: {file.FullPath} {file.ChangeType}");
                    Log.StreamWriter(new DirectoryInfo(Storage.Root), Guid.NewGuid().ToString());
                    lastRead = lastWriteTime;
                }
            }
        }

        private static void OnRenamed(object source, RenamedEventArgs file)
        {
            Console.WriteLine($"File: {file.OldFullPath} renamed to {file.FullPath}");
        }
    }
}
