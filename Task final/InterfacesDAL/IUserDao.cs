using Entities;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IUserDao
    {
        bool Add(ref User user);

        bool Remove(int userId);

        IEnumerable<User> GetAll();

        bool UpdateName(User user);

        User GetByName(string name);
    }
}
