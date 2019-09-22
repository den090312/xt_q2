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

        IEnumerable<Award> GetAwardsByUser(User user);

        void PrintInfo();
    }
}