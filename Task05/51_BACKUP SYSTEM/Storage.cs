namespace _51_BACKUP_SYSTEM
{
    public abstract class Storage
    {
        public static string Root { get; } = @"D:\Storage";

        public static string LogDir { get; } = @"D:\Storage\log.txt";
    }
}
