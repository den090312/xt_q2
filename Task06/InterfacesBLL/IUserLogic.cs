using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IUserLogic
    {
        User Create(string name, DateTime dateBirth);

        bool Add(User user);

        bool RemoveByGuid(Guid userGuid);

        IEnumerable<User> GetAll();

        User GetByGuid(Guid userGuid);

        string GetInfo();
    }
}