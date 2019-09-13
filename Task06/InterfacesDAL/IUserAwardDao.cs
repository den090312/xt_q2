﻿using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IUserAwardDao
    {
        bool JoinedAwardToUser(User user, Award award);

        IEnumerable<Award> GetAwardsByUser(User user, IEnumerable<Award> awards);

        bool UserRemoved(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards);

        bool AwardRemoved(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards);

        void PrintInfo();
    }
}