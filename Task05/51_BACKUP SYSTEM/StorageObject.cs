using System.Collections.Generic;

namespace _51_BACKUP_SYSTEM
{
    public class StorageObject
    {
        public bool IsDirectory { get; } = false;

        public string FullName { get; } = string.Empty;

        public string Contest { get; } = string.Empty;

        public StorageObject(string fullName, string contest, bool isDirectory)
        {
            FullName = fullName;
            Contest = contest;
            IsDirectory = isDirectory;
        }
    }
}
