using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IAwardLogic
    {
        Award CreateAward(string title);

        bool AwardAdded(Award award);

        bool AwardRemoved(Guid awardGuid);

        IEnumerable<Award> GetAll();
    }
}