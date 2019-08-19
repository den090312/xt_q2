using _61_62_USERS_AND_AWARDS.Entities;
using System.Collections.Generic;

namespace _61_62_USERS_AND_AWARDS.Interfaces
{
    public interface IAwardable
    {
        void CreateStorage();

        void PrintStorageInfo();

        void AddAward(Award award);

        void RemoveAwards(string awardName);

        bool AwardsExists(string awardName);

        void PrintAwards();

        void PinUserToAward(string userID, string awardID);

        string[] GetArrayID(string awardName);

        List<KeyValuePair<string, string>> GetAwardList();

        void EraseUser(string userID);
    }
}
