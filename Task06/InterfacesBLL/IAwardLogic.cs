using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IAwardLogic
    {
        Award CreateAward(string title);

        bool AddAward(Award award);

        bool RemoveAward(Guid awardGuid);

        IEnumerable<Award> GetAll();

        Award GetAwardByGuid(Guid awardGuid);

        void PrintInfo();
    }
}