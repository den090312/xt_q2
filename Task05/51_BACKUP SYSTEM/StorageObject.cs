namespace _51_BACKUP_SYSTEM
{
    public class StorageObject
    {
        public bool IsDirectory { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Contest { get; set; } = string.Empty;

        public StorageObject(string fullName, string contest, bool isDirectory)
        {
            FullName = fullName;
            Contest = contest;
            IsDirectory = isDirectory;
        }
    }
}
