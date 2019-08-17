using _61_62_USERS_AND_AWARDS.Entities;
using System;

namespace _61_62_USERS_AND_AWARDS.Interfaces
{
    public interface IUserable
    {
        void AddUser(User user);

        void RemoveUser(string userName);

        bool UserExists(string userName);

        void PrintUsers();
    }
}
