﻿namespace _51_BACKUP_SYSTEM
{
    public abstract class Storage
    {
        public static string Root { get; } = @"D:\Task05\Storage";

        public static string LogDir { get; } = @"D:\Task05\log.txt";

        public static string Log { get; } = "log.txt";

        public static string BackUp { get; } = @"D:\Task05\backup";
    }
}
