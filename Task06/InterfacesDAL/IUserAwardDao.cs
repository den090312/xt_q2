using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IUserAwardDao
    {
        bool JoinedAwardToUser(User user, Award award);

        IEnumerable<UserAward> GetAll();

        IEnumerable<Award> GetAwardsByUser(User user);

        bool UserRemoved(Guid userGuid);

        bool AwardRemoved(Guid awardGuid);

        void PrintInfo();
    }
}