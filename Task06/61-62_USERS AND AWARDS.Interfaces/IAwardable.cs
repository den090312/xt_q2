using System.Collections.Generic;
using Task06.Entities;

namespace Task06.Interfaces
{
    public interface IAwardable
    {
        void CreateStorage();

        void PrintStorageInfo();

        Award CreateAward(string title);

        void AddAward(Award award);

        void RemoveAwards(string awardName);

        bool AwardsExists(string awardName);

        void PrintAwards();

        void JoinUserToAward(string userID, string awardID);

        string[] GetAwardIDArray(string awardName);

        List<KeyValuePair<string, string>> GetAwardList();

        void EraseUser(string userID);
    }
}
