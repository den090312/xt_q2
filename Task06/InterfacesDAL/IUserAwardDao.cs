using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IUserAwardDao
    {
        bool JoinAwardToUser(User user, Award award);

        IEnumerable<Award> GetAwardsByUser(User user, IEnumerable<Award> awards);

        bool RemoveUserAwards(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards);

        bool RemoveAwardUsers(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards);

        void PrintInfo();
    }
}