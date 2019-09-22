using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IAwardDao
    {
        bool AddAward(Award award);

        bool RemoveAward(Guid awardGuid);

        IEnumerable<Award> GetAll();

        Award GetAwardByGuid(Guid awardGuid);

        void PrintInfo();
    }
}