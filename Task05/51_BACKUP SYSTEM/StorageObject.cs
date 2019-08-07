namespace _51_BACKUP_SYSTEM
{
    public class StorageObject
    {
        public string FullName { get; set; } = string.Empty;

        public string Contest { get; set; } = string.Empty;

        public StorageObject(string fullName, string contest)
        {
            FullName = fullName;
            Contest = contest;
        }
    }
}
