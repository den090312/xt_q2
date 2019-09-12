using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IUserAwardDao
    {
        bool JoinedAwardToUser(Guid userGuid, Guid awardGuid);

        IEnumerable<UserAward> GetAll();

        IEnumerable<Award> GetAwardsByUser(User user);

        bool UserRemoved(Guid userGuid);

        bool AwardRemoved(Guid awardGuid);
    }
}