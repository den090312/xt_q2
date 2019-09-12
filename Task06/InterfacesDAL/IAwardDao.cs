using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IAwardDao
    {
        bool AwardAdded(Award award);

        bool AwardRemoved(Guid awardGuid);

        IEnumerable<Award> GetAll();

        Award GetAwardByGuid(Guid awardGuid);
    }
}