using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        bool JoinedAwardToUser(Guid userGuid, Guid awardGuid);

        bool UserRemoved(Guid userGuid);

        bool AwardRemoved(Guid awardGuid);

        IEnumerable<UserAward> GetAll();

        IEnumerable<Award> GetAwardsByUser(User user);
    }
}