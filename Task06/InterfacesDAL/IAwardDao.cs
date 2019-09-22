using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IAwardDao
    {
        bool Add(Award award);

        bool RemoveByGuid(Guid awardGuid);

        IEnumerable<Award> GetAll();

        Award GetByGuid(Guid awardGuid);

        string GetInfo();
    }
}