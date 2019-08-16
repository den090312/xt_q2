﻿using _61_62_USERS_AND_AWARDS.Entities;

namespace _61_62_USERS_AND_AWARDS.Interfaces
{
    public interface IStorable
    {
        string Users { get; }

        string Awards { get; }

        void CreateStorage();

        void PrintStoragePaths();

        void AddUser(User user);

        void RemoveElement(string name, string path, string fileName);

        void PrintFileContent(string path);

        void AddAward(Award award);
    }
}
