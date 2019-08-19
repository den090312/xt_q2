using Task06.Entities;
using System.Collections.Generic;

namespace Task06.Interfaces
{
    public interface IAwardable
    {
        void CreateStorage();

        void PrintStorageInfo();

        void AddAward(Award award);

        void RemoveAwards(string awardName);

        bool AwardsExists(string awardName);

        void PrintAwards();

        void JoinUserToAward(string userID, string awardID);

        string[] GetAwardArrayID(string awardName);

        List<KeyValuePair<string, string>> GetAwardList();

        void EraseUser(string userID);
    }
}
