using System;
using System.IO;

namespace Entities
{
    public class Logger
    {
        public static string Name { get; }

        public static FileInfo ConfigFile { get; }

        static Logger()
        {
            Name = "LOGGER";
            ConfigFile = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }
    }
}
