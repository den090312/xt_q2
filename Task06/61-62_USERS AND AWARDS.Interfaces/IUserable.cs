using Task06.Entities;
using System.Collections.Generic;

namespace Task06.Interfaces
{
    public interface IUserable
    {
        void CreateStorage();

        void PrintStorageInfo();

        void AddUser(User user);

        void RemoveUsers(string userName);

        bool UsersExists(string userName);

        void PrintUsers(List<KeyValuePair<string, string>> awardList);

        void PinAwardToUser(string awardID, string userID);

        string[] GetArrayID(string userName);

        bool RecordExists(string awardID, string userID);

        void EraseAward(string awardID);
    }
}
