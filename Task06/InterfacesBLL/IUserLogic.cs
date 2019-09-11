using Entities;
using System;

namespace InterfacesBLL
{
    public interface IUserLogic
    {
        User CreateUser(string name, DateTime dateBirth);

        void AddUser(User user);

        void RemoveUsers(string userName);

        void PrintUsers();

        string[] GetUserIdArray(string userName);
    }
}