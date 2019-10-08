using System;
using System.IO;

namespace Entities
{
    public class Logger
    {
        public static string Name { get; }

        public static FileInfo Config { get; }

        static Logger()
        {
            Name = "LOGGER";
            Config = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }
    }
}
