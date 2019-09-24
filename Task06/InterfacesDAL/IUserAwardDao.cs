using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IUserAwardDao
    {
        bool JoinAwardToUser(User user, Award award);

        IEnumerable<Award> GetAwardsByUserGuid(Guid userGuid, IEnumerable<Award> awards);

        bool RemoveUserAwards(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards);

        bool RemoveAwardUsers(Guid awardGuid, IEnumerable<User> users, IEnumerable<Award> awards);

        List<KeyValuePair<Guid, Guid>> GetGuidPairs();

        string GetInfo();
    }
}