using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IUserDao
    {
        bool Add(User user);

        bool RemoveByGuid(Guid userGuid);

        IEnumerable<User> GetAll();

        User GetByGuid(Guid userGuid);

        void PrintInfo();
    }
}