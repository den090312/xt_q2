﻿using log4net;

namespace InterfacesBLL
{
    public interface ILoggerLogic
    {
        ILog Log { get; }

        void StartLogger();

        string GetLastError();
    }
}