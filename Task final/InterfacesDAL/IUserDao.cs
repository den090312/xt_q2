using Entities;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IUserDao
    {
        bool Add(User user);

        bool Remove(int UserId);

        IEnumerable<User> GetAll();

        bool UpdateName(User user);
    }
}
