using Entities;
using System;

namespace InterfacesBLL
{
    public interface IAwardLogic
    {
        Award CreateAward(string title);

        bool AwardAdded(Award award);

        bool AwardRemoved(Guid awardGuid);
    }
}