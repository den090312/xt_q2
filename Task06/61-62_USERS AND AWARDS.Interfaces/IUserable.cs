using _61_62_USERS_AND_AWARDS.Entities;
using System.Collections.Generic;

namespace _61_62_USERS_AND_AWARDS.Interfaces
{
    public interface IUserable
    {
        void CreateStorage();

        void PrintStorageInfo();

        void AddUser(User user);

        void RemoveUser(string userName);

        bool UserExists(string userName);

        void PrintUsers(List<KeyValuePair<string, string>> awardList);

        void PinAwardToUser(string awardID, string userID);

        string GetID(string userName);

        bool RecordExists(string awardID, string userID);

        void EraseAward(string awardID);
    }
}
