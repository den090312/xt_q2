using System;
using System.IO;
using System.Security.Permissions;

namespace _51_BACKUP_SYSTEM
{
    public class Watcher
    {
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

        public void Run()
        {
            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = Storage.Root;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                watcher.Filter = "*.txt";

                // Add event handlers.
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
        }

        private DateTime lastRead = DateTime.MinValue;
        
        private void OnChanged(object source, FileSystemEventArgs file)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(file.FullPath); 

            if (lastWriteTime != lastRead)
            {
                Console.WriteLine($"File: {file.FullPath} {file.ChangeType}"); 
                lastRead = lastWriteTime;
            }
        }

        private static void OnRenamed(object source, RenamedEventArgs file)
        {
            Console.WriteLine($"File: {file.OldFullPath} renamed to {file.FullPath}");
        }
    }
}
