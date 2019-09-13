using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IUserAwardDao
    {
        bool JoinedAwardToUser(User user, Award award);

        IEnumerable<UserAward> GetAll(IEnumerable<User> users, IEnumerable<Award> awards);

        IEnumerable<Award> GetAwardsByUser(User user, IEnumerable<Award> awards);

        bool UserRemoved(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards);

        bool AwardRemoved(Guid awardGuid);

        void PrintInfo();
    }
}