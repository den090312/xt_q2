using Entities;
using System;

namespace InterfacesBLL
{
    public interface IUserLogic
    {
        User CreateUser(string name, DateTime dateBirth);

        bool UserAdded(User user);

        bool UserRemoved(Guid userGuid);
    }
}