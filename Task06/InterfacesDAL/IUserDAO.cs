using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IUserDao
    {
        bool AddUser(User user);

        bool RemoveUser(Guid userGuid);

        IEnumerable<User> GetAll();

        User GetUserByGuid(Guid userGuid);

        void PrintInfo();
    }
}