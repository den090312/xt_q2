using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IUserLogic
    {
        User CreateUser(string name, DateTime dateBirth);

        bool UserAdded(User user);

        bool UserRemoved(Guid userGuid);

        IEnumerable<User> GetAll();
    }
}