using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        bool JoinedAwardToUser(Guid userGuid, Guid awardGuid);

        bool UserAwardsRemoved(Guid userGuid);

        bool AwardUsersRemoved(Guid awardGuid);

        IEnumerable<Award> GetAwardsByUser(User user);

        void PrintInfo();
    }
}