using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        bool JoinAwardToUser(Guid userGuid, Guid awardGuid);

        bool RemoveUserAwards(Guid userGuid);

        bool RemoveAwardUsers(Guid awardGuid);

        IEnumerable<Award> GetAwardsByUserGuid(Guid userGuid);

        IEnumerable<UserAward> GetAll();

        bool AwardInUser(Award award);

        string GetInfo();
    }
}