﻿using Entities;
using InterfacesBLL;
using InterfacesDAL;
using log4net;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class FeedbackLogic : IFeedbackLogic, ILoggerLogic
    {
        private readonly IFeedbackDao feedbackDao;

        private readonly ILoggerDao loggerDao;

        public ILog Log => loggerDao.Log;

        public FeedbackLogic(IFeedbackDao iFeedbackDao, ILoggerDao iLoggerDao)
        {
            NullCheck(iFeedbackDao);
            NullCheck(iLoggerDao);

            feedbackDao = iFeedbackDao;
            loggerDao = iLoggerDao;
        }

        public void StartLogger() => loggerDao.StartLogger();
        public string GetLastError() => loggerDao.GetLastError();

        public bool Add(string name, string text)
        {
            NullCheck(name);
            EmptyStringCheck(name);
            NullCheck(text);
            EmptyStringCheck(text);

            return feedbackDao.Add(name, text);
        }

        public IEnumerable<Feedback> GetAll() => feedbackDao.GetAll();

        private void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new ArgumentException($"{nameof(inputString)} is empty!");
            }
        }

        private void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
