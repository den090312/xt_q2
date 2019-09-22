using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IAwardLogic
    {
        Award Create(string title);

        bool Add(Award award);

        bool RemoveByGuid(Guid awardGuid);

        IEnumerable<Award> GetAll();

        Award GetByGuid(Guid awardGuid);

        string GetInfo();
    }
}