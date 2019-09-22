using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IUserLogic
    {
        User CreateUser(string name, DateTime dateBirth);

        bool AddUser(User user);

        bool RemoveUser(Guid userGuid);

        IEnumerable<User> GetAll();

        User GetUserByGuid(Guid userGuid);

        void PrintInfo();
    }
}