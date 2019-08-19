﻿using _61_62_USERS_AND_AWARDS.Entities;
using System.Collections.Generic;

namespace _61_62_USERS_AND_AWARDS.Interfaces
{
    public interface IAwardable
    {
        void CreateStorage();

        void PrintStorageInfo();

        void AddAward(Award award);

        void RemoveAward(string awardName);

        bool AwardExists(string awardName);

        void PrintAwards();

        void PinUserToAward(string userID, string awardID);

        string[] GetArrayID(string awardName);

        List<KeyValuePair<string, string>> GetAwards();

        void EraseUser(string userID);
    }
}
