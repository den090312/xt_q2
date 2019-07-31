namespace _51_BACKUP_SYSTEM
{
    public abstract class Storage
    {
        public static string Root { get; } = @"D:\Storage";

        public static string LogDir { get; } = @"D:\log.txt";

        //public static string LogName { get; } = "log.txt";

        //public virtual string FullName { get; protected set; }
    }
}
