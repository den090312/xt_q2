using Entities;
using System;

namespace InterfacesBLL
{
    public interface IUserLogic
    {
        User CreateUser(string name, DateTime dateBirth);

        void AddUser(User user);

        void RemoveUsers(string userName);

        bool UsersExists(string userName);

        string[] GetUserIDArray(string userName);

        void PrintUsers();

        void EraseUser(string userID);
    }
}
