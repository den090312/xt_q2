using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IUserDao
    {
        bool UserAdded(User user);

        bool UserRemoved(Guid userGuid);

        IEnumerable<User> GetAll();
    }
}