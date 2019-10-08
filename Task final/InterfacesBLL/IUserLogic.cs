using Entities;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IUserLogic
    {
        bool Add(int roleId, string name, string password);

        bool Add(ref User user, int roleId, string password);

        bool Remove(int id);

        IEnumerable<User> GetAll();

        bool ChangeName(User user, string newName);

        User GetByName(string name);

        bool PasswordIsOk(string password, string passwordHash);

        bool ChangePassword(User user, string password);
    }
}