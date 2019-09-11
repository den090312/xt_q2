﻿using Entities;

namespace InterfacesDAL
{
    public interface IUserDao
    {
        void AddUser(User user);

        void RemoveUsers(string userName);

        void PrintUsers();

        void EraseUser(string userID);
    }
}