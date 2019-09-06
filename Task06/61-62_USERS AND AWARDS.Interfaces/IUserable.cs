using Task06.Entities;
using System.Collections.Generic;
using System;

namespace Task06.Interfaces
{
    public interface IUserable
    {
        void CreateStorage();

        void PrintStorageInfo();

        User CreateUser(string name, DateTime dateBirth);

        void AddUser(User user);

        void RemoveUsers(string userName);

        bool UsersExists(string userName);

        void PrintUsers(List<KeyValuePair<string, string>> awardList);

        void JoinAwardToUser(string awardID, string userID);

        string[] GetUserIDArray(string userName);

        bool RecordExists(string awardID, string userID);

        void EraseAward(string awardID);
    }
}
